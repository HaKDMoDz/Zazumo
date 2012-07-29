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
    [ExportContextMenuItem(EditorMenuItems.Archetype_Open, MenuPosition.AppendTo, MenuItems.Root, EditorAssetTypes.Archetype, IsPrimary = true)]
    public class ArchetypeOpenMenuItem : MenuItem
    {
        [ImportingConstructor]
        public ArchetypeOpenMenuItem(OpenArchetypeCommand command) : base(Resources.ArchetypeMenu_Open, command.Command) { }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class OpenArchetypeCommand
    {
        private readonly IWorkspaceService _workSpaceService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public OpenArchetypeCommand(IWorkspaceService workSpaceService)
        {
            this._workSpaceService = workSpaceService;
            this.Command = new DelegateCommand<Object>(x => _workSpaceService.OpenEditor(ViewNames.ArchetypeEditor, (Asset)x));
        }
    }
}
