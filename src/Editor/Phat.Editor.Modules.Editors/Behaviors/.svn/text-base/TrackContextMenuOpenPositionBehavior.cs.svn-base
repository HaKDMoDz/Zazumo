using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Phat.Editor.Modules.Editors.Behaviors
{
    public class TrackContextMenuOpenPositionBehavior : Behavior<FrameworkElement>
    {
        public static DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(Int32), typeof(TrackContextMenuOpenPositionBehavior), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(Int32), typeof(TrackContextMenuOpenPositionBehavior), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

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

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.ContextMenuOpening += new ContextMenuEventHandler(AssociatedObject_ContextMenuOpening);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.ContextMenuOpening += new ContextMenuEventHandler(AssociatedObject_ContextMenuOpening);
        }

        void AssociatedObject_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var position = Mouse.GetPosition(this.AssociatedObject);
            var scrollviewer = FindScrollViewer(this.AssociatedObject);

            this.X = (Int32)position.X + (Int32)scrollviewer.HorizontalOffset;
            this.Y = (Int32)position.Y + (Int32)scrollviewer.VerticalOffset ;            
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
