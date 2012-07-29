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
    [ExportContextMenuItem(EditorMenuItems.FrameSet_Open, MenuPosition.AppendTo, MenuItems.Root, EditorAssetTypes.FrameSet, IsPrimary = true)]
    public class FrameSetOpenMenuItem : MenuItem
    {
        [ImportingConstructor]
        public FrameSetOpenMenuItem(OpenFrameSetCommand command) : base(Resources.FrameSetMenu_Open, command.Command) { }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class OpenFrameSetCommand
    {
        private readonly IWorkspaceService _workSpaceService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public OpenFrameSetCommand(IWorkspaceService workSpaceService)
        {
            this._workSpaceService = workSpaceService;
            this.Command = new DelegateCommand<Object>(x => _workSpaceService.OpenEditor(ViewNames.FrameSetEditor, (Asset)x));
        }
    }
}
