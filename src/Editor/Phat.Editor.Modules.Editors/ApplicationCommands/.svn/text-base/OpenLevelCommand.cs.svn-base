using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using Phat.Editor.Modules.Editors.Properties;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Phat.Editor.Interfaces.DatabaseModel;

namespace Phat.Editor.Modules.Editors.ApplicationCommands
{
    [ExportContextMenuItem(EditorMenuItems.Level_Open, MenuPosition.AppendTo, MenuItems.Root, EditorAssetTypes.Level, IsPrimary = true)]
    public class LevelOpenMenuItem : MenuItem
    {
        [ImportingConstructor]
        public LevelOpenMenuItem(OpenLevelCommand command) : base(Resources.LevelMenu_Open, command.Command) { }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class OpenLevelCommand
    {
        private readonly IWorkspaceService _workSpaceService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public OpenLevelCommand(IWorkspaceService workSpaceService)
        {
            this._workSpaceService = workSpaceService;
            this.Command = new DelegateCommand<Object>(x => _workSpaceService.OpenEditor(ViewNames.LevelEditor, (Asset)x));
        }
    }
}
