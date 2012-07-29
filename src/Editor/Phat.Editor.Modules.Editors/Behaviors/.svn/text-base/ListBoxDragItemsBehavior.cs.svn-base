using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors.Behaviors
{
    public class ListBoxDragItemsBehavior : Behavior<ListBox>
    {
        public static DependencyProperty IsGridSnappingEnabledProperty =
            DependencyProperty.Register("IsGridSnappingEnabled", typeof(Boolean), typeof(ListBoxDragItemsBehavior));

        public static DependencyProperty GridSnapXProperty =
            DependencyProperty.Register("GridSnapX", typeof(Int32), typeof(ListBoxDragItemsBehavior));

        public static DependencyProperty GridSnapYProperty =
            DependencyProperty.Register("GridSnapY", typeof(Int32), typeof(ListBoxDragItemsBehavior));

        public static DependencyProperty CopyCommandProperty =
            DependencyProperty.Register("CopyCommand", typeof(ICommand), typeof(ListBoxDragItemsBehavior));

        public static DependencyProperty PasteCommandProperty =
            DependencyProperty.Register("PasteCommand", typeof(ICommand), typeof(ListBoxDragItemsBehavior));

        public Boolean IsGridSnappingEnabled
        {
            get { return (Boolean)this.GetValue(IsGridSnappingEnabledProperty); }
            set { this.SetValue(IsGridSnappingEnabledProperty, value); }
        }

        public Int32 GridSnapX
        {
            get { return (Int32 )this.GetValue(GridSnapXProperty); }
            set { this.SetValue(GridSnapXProperty, value); }
        }

        public Int32 GridSnapY
        {
            get { return (Int32)this.GetValue(GridSnapYProperty); }
            set { this.SetValue(GridSnapYProperty, value); }
        }

        public ICommand CopyCommand
        {
            get { return (ICommand)this.GetValue(CopyCommandProperty); }
            set { this.SetValue(CopyCommandProperty, value); }
        }

        public ICommand PasteCommand
        {
            get { return (ICommand)this.GetValue(PasteCommandProperty); }
            set { this.SetValue(PasteCommandProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(AssociatedObject_PreviewMouseDown);
            this.AssociatedObject.PreviewMouseUp += new MouseButtonEventHandler(AssociatedObject_PreviewMouseUp);
            this.AssociatedObject.PreviewMouseMove += new MouseEventHandler(AssociatedObject_MouseMove);
            this._isStatic = true;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.PreviewMouseDown -= new System.Windows.Input.MouseButtonEventHandler(AssociatedObject_PreviewMouseDown);
            this.AssociatedObject.PreviewMouseUp -= new MouseButtonEventHandler(AssociatedObject_PreviewMouseUp);
            this.AssociatedObject.PreviewMouseMove -= new MouseEventHandler(AssociatedObject_MouseMove);
        }

        private Boolean _isDragging;
        private Boolean _isStatic;
        private Point _startingPosition;
        private Point _scrollPosition;
        private Point _grabLocation = new Point(0.0, 0.0);

        void AssociatedObject_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            this._isDragging = false;
            this._isStatic = true;
            this.AssociatedObject.ReleaseMouseCapture();
        }

        void AssociatedObject_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            var hitItem = this.AssociatedObject.InputHitTest(e.GetPosition(this.AssociatedObject)) as FrameworkElement;

            if (hitItem == null)
            {
                return;
            }

            if (hitItem.DataContext == null)
            {
                return;
            }

            while (hitItem.GetType() != typeof(ListBoxItem))
            {
                hitItem = (FrameworkElement)VisualTreeHelper.GetParent(hitItem);
                if (hitItem == null)
                {
                    return;
                }
            }

            var dataContext = hitItem.DataContext;

            if (!this.AssociatedObject.Items.Contains(dataContext))
            {
                return;
            }

            this.AssociatedObject.SelectedItem = dataContext;
            Keyboard.Focus(this.AssociatedObject);

            this._isDragging = true;
            this._grabLocation = e.GetPosition(hitItem);
            this._isStatic = true;
            this._startingPosition = e.GetPosition(this.AssociatedObject);

            var scrollViewer = FindScrollViewer(this.AssociatedObject);
            if (scrollViewer != null)
            {
                _scrollPosition = new Point(scrollViewer.HorizontalOffset, scrollViewer.VerticalOffset);
            }
            this.AssociatedObject.CaptureMouse();

            e.Handled = true;
        }

        void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging)
                return;

            var viewModel = this.AssociatedObject.SelectedItem as WorldObjectViewModel;

            if (viewModel == null)
                return;

            e.Handled = true;

            var position = e.GetPosition(this.AssociatedObject);

            if (_isStatic)
            {             
                var mPosition = e.GetPosition(this.AssociatedObject);
                var distance = Math.Sqrt((mPosition.X - _startingPosition.X) * (mPosition.X - _startingPosition.X) + (mPosition.Y - _startingPosition.Y) * (mPosition.Y - _startingPosition.Y));
                if (distance < 5.0)
                    return;

                _isStatic = false;

                // Ctrl + Drag to drag a copy.
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    if (CopyCommand != null)
                    {
                        if (CopyCommand.CanExecute(null))
                        {
                            CopyCommand.Execute(null);

                            if (this.PasteCommand != null)
                            {
                                if (this.PasteCommand.CanExecute(null))
                                {
                                    this.PasteCommand.Execute(null);
                                    viewModel = this.AssociatedObject.SelectedItem as WorldObjectViewModel;
                                }
                            }
                        }
                    }
                }
            }

            var newLocationX = (position.X - _grabLocation.X + _scrollPosition.X) / Settings.MetersToPixels;
            var newLocationY = (position.Y - _grabLocation.Y + _scrollPosition.Y) / Settings.MetersToPixels;

            if (IsGridSnappingEnabled)
            {
                if (GridSnapX > 0)
                    newLocationX = Math.Floor(newLocationX / GridSnapX) * GridSnapX;

                if (GridSnapY > 0)
                    newLocationY = Math.Floor(newLocationY / GridSnapY) * GridSnapY;                
            }

            viewModel.X = (Single)(newLocationX);
            viewModel.Y = (Single)(newLocationY);
        }

        static ScrollViewer FindScrollViewer(DependencyObject parent)
        {
            var childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childCount; i++)
            {
                var elt = VisualTreeHelper.GetChild(parent, i);
                if (elt is ScrollViewer) return (ScrollViewer)elt;
                var result = FindScrollViewer(elt);
                if (result != null) return result;
            }
            return null;
        }
    }
}
