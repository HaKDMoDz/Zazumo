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
    [ExportContextMenuItem(EditorMenuItems.World_Open, MenuPosition.AppendTo, MenuItems.Root, EditorAssetTypes.World, IsPrimary = true)]
    public class WorldOpenMenuItem : MenuItem
    {
        [ImportingConstructor]
        public WorldOpenMenuItem(OpenWorldCommand command) : base(Resources.WorldMenu_Open, command.Command) { }
    }

    /// <summary>
    /// Represents the open world command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class OpenWorldCommand
    {
        private readonly IWorkspaceService _workSpaceService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public OpenWorldCommand(IWorkspaceService workSpaceService)
        {
            this._workSpaceService = workSpaceService;
            this.Command = new DelegateCommand<Object>(x => _workSpaceService.OpenEditor(ViewNames.WorldEditor, (Asset)x));
        }
    }
}
