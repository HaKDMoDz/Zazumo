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
    [ExportContextMenuItem(EditorMenuItems.Storyboard_Open, MenuPosition.AppendTo, MenuItems.Root, EditorAssetTypes.Storyboard, IsPrimary = true)]
    public class StoryboardOpenMenuItem : MenuItem
    {
        [ImportingConstructor]
        public StoryboardOpenMenuItem(OpenStoryboardCommand command) : base(Resources.StoryboardMenu_Open, command.Command) { }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class OpenStoryboardCommand
    {
        private readonly IWorkspaceService _workSpaceService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public OpenStoryboardCommand(IWorkspaceService workSpaceService)
        {
            this._workSpaceService = workSpaceService;
            this.Command = new DelegateCommand<Object>(x => _workSpaceService.OpenEditor(ViewNames.StoryboardEditor, (Asset)x));
        }
    }
}
