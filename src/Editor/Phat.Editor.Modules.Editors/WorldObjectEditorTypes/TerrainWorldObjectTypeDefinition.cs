using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.ActorResources;
using Phat.Editor.Modules.Editors.ViewModels;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors.WorldObjectEditorTypes
{
    [ExportWorldEditorType(ViewNames.WorldEditorProperties_TerrainWorldObject, CustomEditorView=ViewNames.WorldEditor_Terrain)]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared)]
    public class TerrainWorldObjectTypeDefinition : WorldEditorTypeDefinition<TerrainWorldObject>
    {
        private readonly SpriteLoader _spriteLoader;
        private readonly IPackageRepository _packageRepository;

        [ImportingConstructor]
        public TerrainWorldObjectTypeDefinition(IPackageRepository packageRepository, SpriteLoader spriteLoader)
            : base("Terrain")
        {
            this._spriteLoader = spriteLoader;
            this._packageRepository = packageRepository;
        }

        protected override void SetDefaultValues(TerrainWorldObject obj)
        {
            obj.Columns = 25;
            obj.Rows = 25;
            obj.TileDefinitionKeys = new String[0];
            obj.TileHeight = 1;
            obj.TileWidth = 1;
            obj.Behavior = "Terrain";
        }

        public override WorldObjectViewModel CreateWorldObjectViewModel()
        {
            return new TerrainWorldObjectViewModel(_packageRepository, _spriteLoader);
        }
    }

}
