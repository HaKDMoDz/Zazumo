using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using Phat.Editor.Modules.Editors.ViewModels;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors.WorldObjectEditorTypes
{
    [ExportArchetypeBasedWorldEditorType(ViewNames.WorldEditorProperties_ArchetypeBasedConcreteWorldObject)]
    public class ArchetypeBasedConcreteWorldObjectTypeDefinition : ArchetypeBasedWorldEditorTypeDefinition<ArchetypeBasedConcreteWorldObject, ConcreteWorldObjectArchetypeData>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly SpriteLoader _spriteLoader;

        [ImportingConstructor]
        public ArchetypeBasedConcreteWorldObjectTypeDefinition(IPackageRepository packageRepository, SpriteLoader spriteLoader)
        {
            this._packageRepository = packageRepository;
            this._spriteLoader = spriteLoader;
        }

        public override WorldObjectViewModel CreateWorldObjectViewModel()
        {
            return new ArchetypeBasedConcreteWorldObjectViewModel(_packageRepository, _spriteLoader);
        }
    }
}
