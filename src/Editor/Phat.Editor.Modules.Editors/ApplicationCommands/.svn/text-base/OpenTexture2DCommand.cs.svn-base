using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using Phat.Editor.Modules.Editors.Properties;
using Phat.Editor.Infrastructure;
using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Commands;
using Phat.Editor.Interfaces.DatabaseModel;

namespace Phat.Editor.Modules.Editors.ApplicationCommands
{
    /// <summary>
    /// Represents the open Texture2D menu item.
    /// </summary>
    [ExportContextMenuItem(EditorMenuItems.Texture2D_Open, MenuPosition.AppendTo, MenuItems.Root, EditorAssetTypes.Texture2D, IsPrimary=true)]
    public class Texture2DOpenMenuItem : MenuItem
    {
        [ImportingConstructor]
        public Texture2DOpenMenuItem(OpenTexture2DCommand command) : base(Resources.Texture2DMenu_Open, command.Command) { }
    }

    /// <summary>
    /// Represents the open Texture2D command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class OpenTexture2DCommand
    {
        private readonly IWorkspaceService _workSpaceService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public OpenTexture2DCommand(IEventAggregator eventAggregator, IWorkspaceService workSpaceService)
        {
            this._workSpaceService = workSpaceService;
            this.Command = new DelegateCommand<Object>(CreateNewTexture2DExecute, CanCreateNewTexture2D);
        }

        public void CreateNewTexture2DExecute(Object asset)
        {
            _workSpaceService.OpenEditor(ViewNames.Texture2DEditor, (Asset)asset);
        }

        public Boolean CanCreateNewTexture2D(Object package)
        {
            return true;
        }
    }
}
