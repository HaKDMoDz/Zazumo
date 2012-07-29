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
    public class ArchetypeBasedRetractableSpikeWorldObjectTypeDefinition : ArchetypeBasedWorldEditorTypeDefinition<ArchetypeBasedRetractableSpikeWorldObject, RetractableSpikeWorldObjectArchetypeData>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly SpriteLoader _spriteLoader;

        [ImportingConstructor]
        public ArchetypeBasedRetractableSpikeWorldObjectTypeDefinition(IPackageRepository packageRepository, SpriteLoader spriteLoader)
        {
            this._packageRepository = packageRepository;
            this._spriteLoader = spriteLoader;
        }

        public override WorldObjectViewModel CreateWorldObjectViewModel()
        {
            return new ArchetypeBasedRetractableSpikeWorldObjectViewModel(_packageRepository, _spriteLoader);
        }
    }
}
