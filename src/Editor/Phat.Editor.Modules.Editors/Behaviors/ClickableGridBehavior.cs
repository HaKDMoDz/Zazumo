using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace Phat.Editor.Modules.Editors.Behaviors
{
    public class ClickableGridBehavior : Behavior<Grid>
    {
        public static DependencyProperty ActionCommandProperty =
            DependencyProperty.Register("ActionCommand", typeof(ICommand), typeof(ClickableGridBehavior));

        public ICommand ActionCommand
        {
            get { return (ICommand)this.GetValue(ActionCommandProperty); }
            set { this.SetValue(ActionCommandProperty, value); }
        }
        
        private Boolean _isMouseDown;
        private TerrainPosition _previousPosition;

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.MouseDown += new System.Windows.Input.MouseButtonEventHandler(AssociatedObject_MouseDown);
            this.AssociatedObject.MouseUp += new System.Windows.Input.MouseButtonEventHandler(AssociatedObject_MouseUp);
            this.AssociatedObject.MouseMove += new System.Windows.Input.MouseEventHandler(AssociatedObject_MouseMove);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.MouseDown -= new System.Windows.Input.MouseButtonEventHandler(AssociatedObject_MouseDown);
            this.AssociatedObject.MouseMove -= new System.Windows.Input.MouseEventHandler(AssociatedObject_MouseMove);
            this.AssociatedObject.MouseUp -= new System.Windows.Input.MouseButtonEventHandler(AssociatedObject_MouseUp);
        }

        void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            e.Handled = true;

            this._isMouseDown = false;
            this.AssociatedObject.ReleaseMouseCapture();
        }

        void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            this.AssociatedObject.Focus();

            e.Handled = true;

            this._isMouseDown = true;
            var result = this.AssociatedObject.CaptureMouse();
            Execute(e.GetPosition(this.AssociatedObject));
        }

        void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            e.Handled = true;

            if (!_isMouseDown)
                return;

            Execute(e.GetPosition(this.AssociatedObject));
        }

        private void Execute(Point position)
        {
            var rowHeight = this.AssociatedObject.ActualHeight / this.AssociatedObject.RowDefinitions.Count;
            var row = (Int32)Math.Floor(position.Y / rowHeight);

            var columnWidth = this.AssociatedObject.ActualWidth / this.AssociatedObject.ColumnDefinitions.Count;
            var column = (Int32)Math.Floor(position.X / columnWidth);

            if (_previousPosition == null)
            {
                _previousPosition = new TerrainPosition() { Row = row, Column = column };
                ActionCommand.Execute(new TerrainPosition() { Row = row, Column = column });
            }
            else
            {
                if (_previousPosition.Column == column && _previousPosition.Row == row)
                    return;
                else
                {
                    _previousPosition = new TerrainPosition() { Row = row, Column = column };
                    ActionCommand.Execute(new TerrainPosition() { Row = row, Column = column });
                }
            }
        }
    }
}
