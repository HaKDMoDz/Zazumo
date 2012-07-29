using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;
using Microsoft.Win32;
using System.Configuration;
using System.IO;

namespace Phat.Editor.Actions
{
    public class OpenFileDialogAction : TriggerAction<DependencyObject>
    {
        public static DependencyProperty FilePathProperty = DependencyProperty.Register("FilePath", typeof(String), typeof(OpenFileDialogAction), new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static DependencyProperty FilterProperty = DependencyProperty.Register("Filter", typeof(String), typeof(OpenFileDialogAction));
        
        public String FilePath
        {
            get { return (String)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        public String Filter
        {
            get { return (String)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the Action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = Filter;
            ofd.InitialDirectory = ConfigurationManager.AppSettings["ContentRoot"];
            ofd.FileOk += new System.ComponentModel.CancelEventHandler(ofd_FileOk);

            var result = ofd.ShowDialog();

            if (result.Value)
                FilePath = ofd.FileName.Remove(0,ConfigurationManager.AppSettings["ContentRoot"].Length + 1);
        }

        void ofd_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var ofd = (OpenFileDialog)sender;

            if (!ofd.FileName.StartsWith(ofd.InitialDirectory, StringComparison.InvariantCultureIgnoreCase))
                e.Cancel = true;
        }
    }
}
