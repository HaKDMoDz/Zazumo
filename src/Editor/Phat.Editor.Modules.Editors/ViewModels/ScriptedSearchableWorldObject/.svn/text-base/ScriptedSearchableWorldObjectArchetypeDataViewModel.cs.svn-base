using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [ExportArchetypeDataEditor(typeof(ScriptedSearchableWorldObjectArchetypeData))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class ScriptedSearchableWorldObjectArchetypeDataViewModel : ConcreteWorldObjectArchetypeDataViewModel
    {
        private String _scriptKey;
        public String ScriptKey
        {
            get { return _scriptKey; }
            set
            {
                if (value == _scriptKey)
                    return;

                this._scriptKey = value;
                RaisePropertyChanged(() => ScriptKey);
                MarkUnsaved();
            }
        }

        [ImportingConstructor]
        public ScriptedSearchableWorldObjectArchetypeDataViewModel(IPackageRepository repository, BehaviorRegistry behaviorRegistry)
            : base(repository, behaviorRegistry)
        {
        }

        public override void MoveModelToView(ArchetypeData model)
        {
            base.MoveModelToView(model);
                        
            var m = model as ScriptedSearchableWorldObjectArchetypeData;

            this.ScriptKey = m.ScriptKey;
        }

        public override ArchetypeData MoveViewToModel()
        {
            var model = new ScriptedSearchableWorldObjectArchetypeData();
            model.Behavior = this.Behavior;
            model.CollisionHullKey = this.CollisionHullKey;
            model.Height = this.Height;
            model.Width = this.Width;
            model.SpriteKey = this.SpriteKey;
            model.ScriptKey = this.ScriptKey;

            return model;
        }
    }
}
