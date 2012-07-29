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
    [ExportContextMenuItem(EditorMenuItems.Sprite_Open, MenuPosition.AppendTo, MenuItems.Root, EditorAssetTypes.Sprite, IsPrimary = true)]
    public class SpriteOpenMenuItem : MenuItem
    {
        [ImportingConstructor]
        public SpriteOpenMenuItem(OpenSpriteCommand command) : base(Resources.SpriteMenu_Open, command.Command) { }
    }

    /// <summary>
    /// Represents the open Texture2D command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class OpenSpriteCommand
    {
        private readonly IWorkspaceService _workSpaceService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public OpenSpriteCommand(IWorkspaceService workSpaceService)
        {
            this._workSpaceService = workSpaceService;
            this.Command = new DelegateCommand<Object>(x => _workSpaceService.OpenEditor(ViewNames.SpriteEditor, (Asset)x));
        }
    }
}
