using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.ActorResources;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [ExportArchetypeDataEditor(typeof(MirrorWorldObjectArchetypeData))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class MirrorWorldObjectArchetypeDataViewModel : ConcreteWorldObjectArchetypeDataViewModel
    {
        private MirrorDirection _mirrorDirection;
        public MirrorDirection MirrorDirection
        {
            get { return _mirrorDirection; }
            set
            {
                if (value == _mirrorDirection)
                    return;

                this._mirrorDirection = value;
                RaisePropertyChanged(() => MirrorDirection);
                MarkUnsaved();
            }
        }

        [ImportingConstructor]
        public MirrorWorldObjectArchetypeDataViewModel(IPackageRepository repository, BehaviorRegistry behaviorRegistry)
            : base(repository, behaviorRegistry)
        {
        }

        public override void MoveModelToView(ArchetypeData model)
        {
            base.MoveModelToView(model);

            var m = model as MirrorWorldObjectArchetypeData;

            this.MirrorDirection = m.MirrorDirection;
        }

        public override ArchetypeData MoveViewToModel()
        {
            var model = new MirrorWorldObjectArchetypeData();
            model.Behavior = this.Behavior;
            model.CollisionHullKey = this.CollisionHullKey;
            model.Height = this.Height;
            model.Width = this.Width;
            model.SpriteKey = this.SpriteKey;
            model.MirrorDirection = this.MirrorDirection;

            return model;
        }

    }
}
