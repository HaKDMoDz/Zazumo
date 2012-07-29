using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace Phat.Editor.Modules.Editors.Behaviors
{
    public class AreaSelectionBehavior : Behavior<Grid>
    {
        public static DependencyProperty BrushProperty = 
            DependencyProperty.Register("Brush", typeof(Brush), typeof(AreaSelectionBehavior));

        public static DependencyProperty ThicknessProperty = 
            DependencyProperty.Register("Thickness", typeof(Thickness), typeof(AreaSelectionBehavior));

        public static DependencyProperty XProperty = 
            DependencyProperty.Register("X", typeof(Int32), typeof(AreaSelectionBehavior), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SelectionChanged));

        public static DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(Int32), typeof(AreaSelectionBehavior), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SelectionChanged));

        public static DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(Int32), typeof(AreaSelectionBehavior), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SelectionChanged));

        public static DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(Int32), typeof(AreaSelectionBehavior), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SelectionChanged));

        public static DependencyProperty IsSizeLockedProperty = 
            DependencyProperty.Register("IsSizeLocked", typeof(Boolean), typeof(AreaSelectionBehavior), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None));

        public static void SelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AreaSelectionBehavior)d).BindSelectionOutlineValues();
        }

        public Brush Brush
        {
            get { return (Brush)this.GetValue(BrushProperty); }
            set { this.SetValue(BrushProperty, value); }
        }

        public Thickness Thickness
        {
            get { return (Thickness)this.GetValue(ThicknessProperty); }
            set { this.SetValue(ThicknessProperty, value); }
        }

        public Int32 X
        {
            get { return (Int32)this.GetValue(XProperty); }
            set { this.SetValue(XProperty, value); }
        }

        public Int32 Y
        {
            get { return (Int32)this.GetValue(YProperty); }
            set { this.SetValue(YProperty, value); }
        }

        public Int32 Height
        {
            get { return (Int32)this.GetValue(HeightProperty); }
            set { this.SetValue(HeightProperty, value); }
        }

        public Int32 Width
        {
            get { return (Int32)this.GetValue(WidthProperty); }
            set { this.SetValue(WidthProperty, value); }
        }


        public Boolean IsSizeLocked
        {
            get { return (Boolean)this.GetValue(IsSizeLockedProperty); }
            set { this.SetValue(IsSizeLockedProperty, value); }
        }

        private Border _selectionOutline;

        protected override void OnAttached()
        {
            base.OnAttached();
            
            _selectionOutline = new Border();
            _selectionOutline.BorderThickness = Thickness;
            _selectionOutline.BorderBrush = Brush;
            _selectionOutline.VerticalAlignment = VerticalAlignment.Top;
            _selectionOutline.HorizontalAlignment = HorizontalAlignment.Left;

            this.AssociatedObject.Focusable = true;
            this.AssociatedObject.Children.Add(_selectionOutline);
            this.AssociatedObject.SizeChanged += new SizeChangedEventHandler(AssociatedObject_SizeChanged);
            this.AssociatedObject.MouseDown += new System.Windows.Input.MouseButtonEventHandler(AssociatedObject_MouseDown);
            this.AssociatedObject.MouseUp += new System.Windows.Input.MouseButtonEventHandler(AssociatedObject_MouseUp);
            this.AssociatedObject.MouseMove += new MouseEventHandler(AssociatedObject_MouseMove);
            this.AssociatedObject.KeyDown += new KeyEventHandler(AssociatedObject_KeyDown);
        }
        
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.SizeChanged -= new SizeChangedEventHandler(AssociatedObject_SizeChanged);
            this.AssociatedObject.MouseDown -= new System.Windows.Input.MouseButtonEventHandler(AssociatedObject_MouseDown);
            this.AssociatedObject.MouseUp -= new System.Windows.Input.MouseButtonEventHandler(AssociatedObject_MouseUp);
            this.AssociatedObject.MouseMove -= new MouseEventHandler(AssociatedObject_MouseMove);
            this.AssociatedObject.KeyDown -= new KeyEventHandler(AssociatedObject_KeyDown);
            this.AssociatedObject.Children.Remove(_selectionOutline);        
            _selectionOutline = null;
        }

        private Point _position1;
        private Boolean _isDragging;

        void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.AssociatedObject.Focus();

            if (e.LeftButton != System.Windows.Input.MouseButtonState.Pressed)
                return;

            var position = e.GetPosition(this.AssociatedObject);
            this.X = (Int32)position.X;
            this.Y = (Int32)position.Y;

            if (!this.IsSizeLocked)
            {
                _position1 = position;
            }

            this._isDragging = true;
            this.AssociatedObject.CaptureMouse();

            
            BindSelectionOutlineValues();
        }

        void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.AssociatedObject.ReleaseMouseCapture();
            this._isDragging = false;
        }
        
        void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging)
                return;

            var position = e.GetPosition(this.AssociatedObject);

            if (IsSizeLocked)
            {
                this.X = (Int32)position.X;
                this.Y = (Int32)position.Y;
            }
            else
            {
                Int32 top = (Int32)Math.Min(position.Y, _position1.Y);
                Int32 bottom = (Int32)Math.Max(position.Y, _position1.Y);
                Int32 left = (Int32)Math.Min(position.X, _position1.X);
                Int32 right = (Int32)Math.Max(position.X, _position1.X);

                this.X = left;
                this.Y = top;
                this.Width = right - left;
                this.Height = bottom - top;
            }
        }

        void AssociatedObject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                this.X -= 1;
                e.Handled = true;
            }

            if (e.Key == Key.Right)
            {
                this.X += 1;
                e.Handled = true;
            }

            if (e.Key == Key.Up)
            {
                this.Y -= 1;
                e.Handled = true;
            }

            if (e.Key == Key.Down)
            {
                this.Y += 1;
                e.Handled = true;
            }
        }

        void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BindSelectionOutlineValues();
        }

        private void BindSelectionOutlineValues()
        {
            if (this.AssociatedObject.ActualHeight == 0 || this.AssociatedObject.ActualWidth == 0)
                return;

            _selectionOutline.SetValue(Panel.ZIndexProperty, 99);

            if (this.Height == 0 || this.Width == 0)
                _selectionOutline.Visibility = Visibility.Collapsed;
            else
                _selectionOutline.Visibility = Visibility.Visible;

            if (X < 0)
                X = 0;

            if (Y < 0)
                Y = 0;


            if (!IsSizeLocked)
            {
                if (X >= this.AssociatedObject.ActualWidth)
                    X = (Int32)this.AssociatedObject.ActualWidth;

                if (Y >= this.AssociatedObject.ActualHeight)
                    Y = (Int32)this.AssociatedObject.ActualHeight;

                if ((X + Width) > this.AssociatedObject.ActualWidth)
                {
                    Width = (Int32)this.AssociatedObject.ActualWidth - X;
                }

                if ((Y + Height) > this.AssociatedObject.ActualHeight)
                {
                    Height = (Int32)this.AssociatedObject.ActualHeight - Y;
                }
            }
            else
            {
                if ((X + Width) > this.AssociatedObject.ActualWidth)
                    X = (Int32)this.AssociatedObject.ActualWidth - Width;

                if ((Y + Height) > this.AssociatedObject.ActualHeight)
                    Y = (Int32)this.AssociatedObject.ActualHeight - Height;
            }

            _selectionOutline.Margin = new Thickness(X, Y, 0, 0);         

            _selectionOutline.Width = Width;
            _selectionOutline.Height = Height;
        }
    }
}
