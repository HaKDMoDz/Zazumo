using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;
using Phat.Editor.Interfaces.DatabaseModel;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [ExportArchetypeDataEditor(typeof(RetractableSpikeWorldObjectArchetypeData))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class RetractableSpikeWorldObjectArchetypeDataViewModel : ConcreteWorldObjectArchetypeDataViewModel
    {
        private SpikePosition _spikePosition;
        public SpikePosition SpikePosition
        {
            get { return _spikePosition; }
            set
            {
                if (value == _spikePosition)
                    return;

                this._spikePosition = value;
                RaisePropertyChanged(() => SpikePosition);
                MarkUnsaved();
            }
        }

        private Asset _spikeUpSprite;
        public Asset SpikeUpSprite
        {
            get { return _spikeUpSprite; }
            set
            {
                if (value == _spikeUpSprite)
                    return;

                this._spikeUpSprite = value;
                RaisePropertyChanged(() => SpikeUpSprite);
            }
        }

        private Asset _spikeDownSprite;
        public Asset SpikeDownSprite
        {
            get { return _spikeDownSprite; }
            set
            {
                if (value == _spikeDownSprite)
                    return;

                this._spikeDownSprite = value;
                RaisePropertyChanged(() => SpikeDownSprite);
            }
        }

        [ImportingConstructor]
        public RetractableSpikeWorldObjectArchetypeDataViewModel(IPackageRepository repository, BehaviorRegistry behaviorRegistry)
            : base(repository, behaviorRegistry)
        {
            this.SpikeUpSprite = this.Sprites.FirstOrDefault();
            this.SpikeDownSprite = this.Sprites.FirstOrDefault();
        }

        public override void MoveModelToView(ArchetypeData model)
        {
            base.MoveModelToView(model);

            var m = model as RetractableSpikeWorldObjectArchetypeData;
            
            this.SpikePosition = m.SpikePosition;
            this.SpikeUpSprite = Sprites.Where(x => x.Key == m.SpikeUpSprite).FirstOrDefault();
            this.SpikeDownSprite = Sprites.Where(x => x.Key == m.SpikeDownSprite).FirstOrDefault();
        }

        public override ArchetypeData MoveViewToModel()
        {
            var model = new RetractableSpikeWorldObjectArchetypeData();
            model.Behavior = this.Behavior;
            model.CollisionHullKey = this.CollisionHullKey;
            model.Height = this.Height;
            model.Width = this.Width;
            model.SpriteKey = this.SpriteKey;
            model.SpikePosition = this.SpikePosition;
            model.SpikeUpSprite = this.SpikeUpSprite.Key;
            model.SpikeDownSprite = this.SpikeDownSprite.Key;

            return model;
        }
    }
}
