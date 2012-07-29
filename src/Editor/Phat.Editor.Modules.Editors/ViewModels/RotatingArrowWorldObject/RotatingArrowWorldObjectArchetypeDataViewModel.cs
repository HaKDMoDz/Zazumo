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
    [ExportArchetypeDataEditor(typeof(RotatingArrowWorldObjectArchetypeData))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class RotatingArrowWorldObjectArchetypeDataViewModel : ArrowWorldObjectArchetypeDataViewModel
    {
        private Asset _upSprite;
        public Asset UpSprite
        {
            get { return _upSprite; }
            set
            {
                if (value == _upSprite)
                    return;

                this._upSprite = value;
                RaisePropertyChanged(() => UpSprite);
            }
        }

        private Asset _downSprite;
        public Asset DownSprite
        {
            get { return _downSprite; }
            set
            {
                if (value == _downSprite)
                    return;

                this._downSprite = value;
                RaisePropertyChanged(() => DownSprite);
            }
        }

        private Asset _leftSprite;
        public Asset LeftSprite
        {
            get { return _leftSprite; }
            set
            {
                if (value == _leftSprite)
                    return;

                this._leftSprite = value;
                RaisePropertyChanged(() => LeftSprite);
            }
        }

        private Asset _rightSprite;
        public Asset RightSprite
        {
            get { return _rightSprite; }
            set
            {
                if (value == _rightSprite)
                    return;

                this._rightSprite = value;
                RaisePropertyChanged(() => RightSprite);
            }
        }
        [ImportingConstructor]
        public RotatingArrowWorldObjectArchetypeDataViewModel(IPackageRepository repository, BehaviorRegistry behaviorRegistry)
            : base(repository, behaviorRegistry)
        {

        }

        public override void MoveModelToView(ArchetypeData model)
        {
            base.MoveModelToView(model);

            var m = model as RotatingArrowWorldObjectArchetypeData;

            this.UpSprite = Sprites.Where(x => x.Key == m.UpSprite).FirstOrDefault();
            this.DownSprite = Sprites.Where(x => x.Key == m.DownSprite).FirstOrDefault();
            this.LeftSprite = Sprites.Where(x => x.Key == m.LeftSprite).FirstOrDefault();
            this.RightSprite = Sprites.Where(x => x.Key == m.RightSprite).FirstOrDefault();
        }

        public override ArchetypeData MoveViewToModel()
        {
            var model = new RotatingArrowWorldObjectArchetypeData();
            model.Behavior = this.Behavior;
            model.CollisionHullKey = this.CollisionHullKey;
            model.Height = this.Height;
            model.Width = this.Width;
            model.SpriteKey = this.SpriteKey;
            model.ArrowDirection = this.ArrowDirection;
            model.UpSprite = this.UpSprite.Key;
            model.DownSprite = this.DownSprite.Key;
            model.LeftSprite = this.LeftSprite.Key;
            model.RightSprite = this.RightSprite.Key;

            return model;
        }
    }
}
