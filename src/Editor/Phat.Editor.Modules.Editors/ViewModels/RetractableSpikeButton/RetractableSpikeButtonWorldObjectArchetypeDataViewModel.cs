using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;
using System.Collections.ObjectModel;
using Phat.Editor.Interfaces.DatabaseModel;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [ExportArchetypeDataEditor(typeof(RetractableSpikeButtonWorldObjectArchetypeData))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class RetractableSpikeButtonWorldObjectArchetypeDataViewModel : ConcreteWorldObjectArchetypeDataViewModel
    {
        private Asset _alternateSprite;
        public Asset AlternateSprite
        {
            get { return _alternateSprite; }
            set
            {
                if (value == _alternateSprite)
                    return;

                this._alternateSprite = value;
                RaisePropertyChanged(() => AlternateSprite);
            }
        }

        [ImportingConstructor]
        public RetractableSpikeButtonWorldObjectArchetypeDataViewModel(IPackageRepository repository, BehaviorRegistry behaviorRegistry)
            : base(repository, behaviorRegistry)
        {
            
        }

        public override void MoveModelToView(ArchetypeData model)
        {
            base.MoveModelToView(model);

            var m = model as RetractableSpikeButtonWorldObjectArchetypeData;

            this.AlternateSprite = Sprites.Where(x => x.Key == m.AlernateSprite).FirstOrDefault();            
        }

        public override ArchetypeData MoveViewToModel()
        {
            var model = new RetractableSpikeButtonWorldObjectArchetypeData();
            model.Behavior = this.Behavior;
            model.CollisionHullKey = this.CollisionHullKey;
            model.Height = this.Height;
            model.Width = this.Width;
            model.SpriteKey = this.SpriteKey;
            model.AlernateSprite = this.AlternateSprite.Key;

            return model;
        }
    }
}
