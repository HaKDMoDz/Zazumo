using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using Phat.Editor.Modules.Core.Properties;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using System.Configuration;
using System.Windows;

namespace Phat.Editor.Modules.Core.ApplicationCommands
{
    /// <summary>
    /// Represents the file exit menu item.
    /// </summary>
    [ExportMenuItem(MenuItems.File_Save, MenuPosition.AppendTo, MenuItems.Tools)]
    public class ToolsSetContentPathMenuItem : MenuItem
    {
        [ImportingConstructor]
        public ToolsSetContentPathMenuItem(SetContentPathCommand command) : base(Strings.MainMenu_Tools_SetContentPath, command.Command) { }
    }

    /// <summary>
    /// Represents the application save command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SetContentPathCommand
    {
        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public SetContentPathCommand()
        {
            Command = new DelegateCommand(SetContentPath);
        }

        public void SetContentPath()
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            var result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath);

                config.AppSettings.Settings["ContentRoot"].Value = fbd.SelectedPath;
                config.Save(ConfigurationSaveMode.Modified);                
            }
        }
    }
}
