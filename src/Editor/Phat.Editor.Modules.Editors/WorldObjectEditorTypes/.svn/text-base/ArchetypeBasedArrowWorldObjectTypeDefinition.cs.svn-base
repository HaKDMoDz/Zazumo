using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors.WorldObjectEditorTypes
{
    [ExportArchetypeBasedWorldEditorType(ViewNames.WorldEditorProperties_ArchetypeBasedConcreteWorldObject)]
    public class ArchetypeBasedArrowWorldObjectTypeDefinition : ArchetypeBasedWorldEditorTypeDefinition<ArchetypeBasedArrowWorldObject, ArrowWorldObjectArchetypeData>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly SpriteLoader _spriteLoader;

        [ImportingConstructor]
        public ArchetypeBasedArrowWorldObjectTypeDefinition(IPackageRepository packageRepository, SpriteLoader spriteLoader)
        {
            this._packageRepository = packageRepository;
            this._spriteLoader = spriteLoader;
        }

        public override WorldObjectViewModel CreateWorldObjectViewModel()
        {
            return new ArchetypeBasedArrowWorldObjectViewModel(_packageRepository, _spriteLoader);
        }
    }
}
