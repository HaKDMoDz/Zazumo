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
    [ExportContextMenuItem(EditorMenuItems.TerrainTileDefinition_Open, MenuPosition.AppendTo, MenuItems.Root, EditorAssetTypes.TerrainTileDefinition, IsPrimary = true)]
    public class TerrainTileDefinitionOpenMenuItem : MenuItem
    {
        [ImportingConstructor]
        public TerrainTileDefinitionOpenMenuItem(OpenTerrainTileDefinitionCommand command) : base(Resources.TerrainTileDefinitionMenu_Open, command.Command) { }
    }

    /// <summary>
    /// Represents the open terrain tile definition command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class OpenTerrainTileDefinitionCommand
    {
        private readonly IWorkspaceService _workSpaceService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public OpenTerrainTileDefinitionCommand(IWorkspaceService workSpaceService)
        {
            this._workSpaceService = workSpaceService;
            this.Command = new DelegateCommand<Object>(x => _workSpaceService.OpenEditor(ViewNames.TerrainTileDefinitionEditor, (Asset)x));
        }
    }
}
