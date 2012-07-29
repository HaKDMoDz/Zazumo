using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.ActorResources;
using System.Collections.ObjectModel;
using Phat.Editor.Interfaces.DatabaseModel;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [ExportArchetypeDataEditor(typeof(ConcreteWorldObjectArchetypeData))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class ConcreteWorldObjectArchetypeDataViewModel : ArchetypeDataViewModel
    {
        private String _collisionHullKey;
        public String CollisionHullKey
        {
            get { return _collisionHullKey; }
            set
            {
                if (value == _collisionHullKey)
                    return;

                this._collisionHullKey = value;
                RaisePropertyChanged(() => CollisionHullKey);
                MarkUnsaved();
            }
        }

        private readonly ObservableCollection<Asset> _sprites;
        public IEnumerable<Asset> Sprites
        {
            get { return _sprites; }
        }

        private Asset _sprite;
        public Asset Sprite
        {
            get { return _sprite; }
            set
            {
                if (value == _sprite)
                    return;

                this._sprite = value;
                RaisePropertyChanged(() => Sprite);
                SpriteKey = Sprite.Key;
            }
        }

        private String _spriteKey;
        public String SpriteKey
        {
            get { return _spriteKey; }
            set
            {
                if (value == _spriteKey)
                    return;

                if (String.IsNullOrWhiteSpace(value))
                    return;

                this._spriteKey = value;
                RaisePropertyChanged(() => SpriteKey);
                this.Sprite = Sprites.Where(x => x.Key == _spriteKey).FirstOrDefault();
                MarkUnsaved();
            }
        }     

        private Single _width;
        public Single Width
        {
            get { return _width; }
            set
            {
                if (value == _width)
                    return;

                this._width = value;
                RaisePropertyChanged(() => Width);
                MarkUnsaved();
            }
        }

        private Single _height;
        public Single Height
        {
            get { return _height; }
            set
            {
                if (value == _height)
                    return;

                this._height = value;
                RaisePropertyChanged(() => Height);
                MarkUnsaved();
            }
        }

        private readonly ObservableCollection<String> _behaviors;
        public IEnumerable<String> Behaviors
        {
            get { return _behaviors; }
        }


        public String _behavior;
        public String Behavior
        {
            get { return _behavior; }
            set
            {
                if (value == _behavior)
                    return;

                this._behavior = value;
                RaisePropertyChanged(() => Behavior);
                MarkUnsaved();
            }
        }
        
        [ImportingConstructor]
        public ConcreteWorldObjectArchetypeDataViewModel(IPackageRepository repository, BehaviorRegistry behaviorRegistry)
        {
            this._sprites = new ObservableCollection<Asset>();
            this._behaviors = new ObservableCollection<String>();

            foreach (var asset in repository.Assets.Where(x => x.Type == EditorAssetTypes.Sprite))
                _sprites.Add(asset);

            foreach (var behavior in behaviorRegistry.GetRegisteredBehaviors())
                _behaviors.Add(behavior);
        }

        public override ArchetypeData MoveViewToModel()
        {
            var model = new ConcreteWorldObjectArchetypeData();
            model.Behavior = this.Behavior;
            model.CollisionHullKey = this.CollisionHullKey;
            model.Height = this.Height;
            model.Width = this.Width;
            model.SpriteKey = this.SpriteKey;
            
            return model;
        }

        public override void MoveModelToView(ArchetypeData model)
        {
            var m = model as ConcreteWorldObjectArchetypeData;

            this.Behavior = m.Behavior;
            this.CollisionHullKey = m.CollisionHullKey;
            this.Height = m.Height;
            this.Width = m.Width;
            this.SpriteKey = m.SpriteKey;
        }
    }
}
