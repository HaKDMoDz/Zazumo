using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;

namespace Phat.Editor.Modules.Editors.Behaviors
{
    public class DynamicGridDefinitionBehavior : Behavior<Grid>
    {
        public static DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(Int32), typeof(DynamicGridDefinitionBehavior), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None, DefinitionUpdated));

        public static DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(Int32), typeof(DynamicGridDefinitionBehavior), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None, DefinitionUpdated));

        public static void DefinitionUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DynamicGridDefinitionBehavior)d).UpdateDefinition();
        }

        public Int32 Rows
        {
            get { return (Int32)this.GetValue(RowsProperty); }
            set { this.SetValue(RowsProperty, value); }
        }

        public Int32 Columns
        {
            get { return (Int32)this.GetValue(ColumnsProperty); }
            set { this.SetValue(ColumnsProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            UpdateDefinition();
        }

        private void UpdateDefinition()
        {
            if (this.AssociatedObject == null)
                return;

            this.AssociatedObject.RowDefinitions.Clear();

            for (Int32 i = 0; i < Rows; i++)
            {
                this.AssociatedObject.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            }

            this.AssociatedObject.ColumnDefinitions.Clear();

            for (Int32 i = 0; i < Columns; i++)
            {
                this.AssociatedObject.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }            
        }
    }
}
