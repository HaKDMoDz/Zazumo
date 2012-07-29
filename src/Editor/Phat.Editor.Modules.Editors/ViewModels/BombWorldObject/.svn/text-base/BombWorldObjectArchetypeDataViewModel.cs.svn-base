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
    [ExportArchetypeDataEditor(typeof(BombWorldObjectArchetypeData))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class BombWorldObjectArchetypeDataViewModel : ConcreteWorldObjectArchetypeDataViewModel
    {
        private Int32 _timer;
        public Int32 Timer
        {
            get { return _timer; }
            set
            {
                if (value == _timer)
                    return;

                this._timer = value;
                RaisePropertyChanged(() => Timer);
                MarkUnsaved();
            }
        }

        private readonly ObservableCollection<Asset> _animations;
        public IEnumerable<Asset> Animations
        {
            get { return _animations; }
        }

        private Asset _activeAnimation;
        public Asset ActiveAnimation
        {
            get { return _activeAnimation; }
            set
            {
                if (value == _activeAnimation)
                    return;

                this._activeAnimation = value;
                RaisePropertyChanged(() => ActiveAnimation);
            }
        }

        [ImportingConstructor]
        public BombWorldObjectArchetypeDataViewModel(IPackageRepository repository, BehaviorRegistry behaviorRegistry)
            : base(repository, behaviorRegistry)
        {
            this._animations = new ObservableCollection<Asset>();

            foreach (var asset in repository.Assets.Where(x => x.Type == EditorAssetTypes.FrameSet))
                _animations.Add(asset);
        }

        public override void MoveModelToView(ArchetypeData model)
        {
            base.MoveModelToView(model);

            var m = model as BombWorldObjectArchetypeData;

            this.Timer = m.Timer;
            this.ActiveAnimation = Animations.Where(x => x.Key == m.ActiveAnimation).FirstOrDefault(); ;
        }

        public override ArchetypeData MoveViewToModel()
        {
            var model = new BombWorldObjectArchetypeData();
            model.Behavior = this.Behavior;
            model.CollisionHullKey = this.CollisionHullKey;
            model.Height = this.Height;
            model.Width = this.Width;
            model.SpriteKey = this.SpriteKey;
            model.Timer = this.Timer;
            model.ActiveAnimation = this.ActiveAnimation.Key;

            return model;
        }
    }
}
