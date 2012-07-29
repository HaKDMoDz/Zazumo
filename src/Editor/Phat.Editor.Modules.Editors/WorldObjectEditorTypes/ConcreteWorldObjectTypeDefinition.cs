using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.ActorResources;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors.WorldObjectEditorTypes
{
    [ExportWorldEditorType(ViewNames.WorldEditorProperties_ConcreteWorldObject)]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared)]
    public class ConcreteWorldObjectTypeDefinition : WorldEditorTypeDefinition<ConcreteWorldObject>
    {
        private readonly SpriteLoader _spriteLoader;

        [ImportingConstructor]
        public ConcreteWorldObjectTypeDefinition(SpriteLoader spriteLoader)
            : base("Concrete Object")
        {
            this._spriteLoader = spriteLoader;
        }

        protected override void SetDefaultValues(ConcreteWorldObject obj)
        {
            obj.Width = 1;
            obj.Height = 1;
            obj.Behavior = String.Empty;
            obj.CollisionHullKey = String.Empty;
            obj.SpriteKey = String.Empty;
        }

        public override WorldObjectViewModel CreateWorldObjectViewModel()
        {
            return new ConcreteWorldObjectViewModel(_spriteLoader);
        }
    }
}
