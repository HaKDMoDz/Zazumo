using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using Phat.ActorResources;
using System.ComponentModel.Composition;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors.WorldObjectEditorTypes
{
    [ExportArchetypeBasedWorldEditorType(ViewNames.WorldEditorProperties_ArchetypeBasedConcreteWorldObject)]
    public class ArchetypeBasedRetractableSpikeButtonWorldObjectTypeDefinition : ArchetypeBasedWorldEditorTypeDefinition<ArchetypeBasedRetractableSpikeButtonWorldObject, RetractableSpikeButtonWorldObjectArchetypeData>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly SpriteLoader _spriteLoader;

        [ImportingConstructor]
        public ArchetypeBasedRetractableSpikeButtonWorldObjectTypeDefinition(IPackageRepository packageRepository, SpriteLoader spriteLoader)
        {
            this._packageRepository = packageRepository;
            this._spriteLoader = spriteLoader;
        }

        public override WorldObjectViewModel CreateWorldObjectViewModel()
        {
            return new ArchetypeBasedRetractableSpikeButtonWorldObjectViewModel(_packageRepository, _spriteLoader);
        }
    }
}
