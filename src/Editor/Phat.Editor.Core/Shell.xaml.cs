using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using Phat.Editor.Modules.Core.ApplicationCommands;

namespace Phat.Editor.Views
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    [Export]
    public partial class Shell : Window
    {
        public Shell()
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(Shell_Closing);
            InitializeComponent();
        }

        [Import]
        public ExitCommand ExitCommand { get; private set; }

        void Shell_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //e.Cancel = true;

            //ExitCommand.Command.Execute(null);
        }
    }
}
