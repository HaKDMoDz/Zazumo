using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [ExportArchetypeDataEditor(typeof(ArrowWorldObjectArchetypeData))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class ArrowWorldObjectArchetypeDataViewModel : ConcreteWorldObjectArchetypeDataViewModel
    {
        private ArrowDirection _arrowDirection;
        public ArrowDirection ArrowDirection
        {
            get { return _arrowDirection; }
            set
            {
                if (value == _arrowDirection)
                    return;

                this._arrowDirection = value;
                RaisePropertyChanged(() => ArrowDirection);
                MarkUnsaved();
            }
        }

        [ImportingConstructor]
        public ArrowWorldObjectArchetypeDataViewModel(IPackageRepository repository, BehaviorRegistry behaviorRegistry)
            : base(repository, behaviorRegistry)
        {
        }

        public override void MoveModelToView(ArchetypeData model)
        {
            base.MoveModelToView(model);
                        
            var m = model as ArrowWorldObjectArchetypeData;

            this.ArrowDirection = m.ArrowDirection;
        }

        public override ArchetypeData MoveViewToModel()
        {
            var model = new ArrowWorldObjectArchetypeData();
            model.Behavior = this.Behavior;
            model.CollisionHullKey = this.CollisionHullKey;
            model.Height = this.Height;
            model.Width = this.Width;
            model.SpriteKey = this.SpriteKey;
            model.ArrowDirection = this.ArrowDirection;

            return model;
        }
    }
}
