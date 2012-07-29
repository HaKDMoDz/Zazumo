using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Phat.Editor.Modules.Core.Properties;
using Microsoft.Practices.Prism.Commands;
using System.Windows;

namespace Phat.Editor.Modules.Core.ApplicationCommands
{
    /// <summary>
    /// Represents the file exit menu item.
    /// </summary>
    [ExportMenuItem(MenuItems.File_Save, MenuPosition.InsertBefore, MenuItems.File_Exit)]
    public class FileSaveMenuItem : MenuItem
    {
        [ImportingConstructor]
        public FileSaveMenuItem(SaveCommand saveCommand) : base(Strings.MainMenu_File_Save, saveCommand.Command) { }
    }

    [ExportGlobalCommand]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GlobalSaveCommand : GlobalCommand
    {
        [ImportingConstructor]
        public GlobalSaveCommand(SaveCommand saveCommand) : base("Ctrl+S", saveCommand.Command, new KeyGesture(Key.S, ModifierKeys.Control)) { }
    }

    /// <summary>
    /// Represents the application save command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SaveCommand
    {
        private readonly IWorkspaceService _workspaceService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public SaveCommand(IWorkspaceService workspaceService)
        {
            this._workspaceService = workspaceService;

            Command = new DelegateCommand(() => this._workspaceService.SaveCurrentEditor());
        }        
    }
}
