using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Phat.ActorResources;
using System.Collections.ObjectModel;
using Phat.Editor.Interfaces.DatabaseModel;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class ConcreteWorldObjectPropertiesViewModel : WorldObjectPropertiesViewModel
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
                NotifyWorldObjectValueChanged("CollisionHullKey", value);
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
                NotifyWorldObjectValueChanged("SpriteKey", value);
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
                NotifyWorldObjectValueChanged("Width", value);
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
                NotifyWorldObjectValueChanged("Height", value);
            }
        }

        [ImportingConstructor]
        public ConcreteWorldObjectPropertiesViewModel(IPackageRepository repository)
        {
            this._sprites = new ObservableCollection<Asset>();

            foreach (var asset in repository.Assets.Where(x => x.Type == EditorAssetTypes.Sprite))
            {
                _sprites.Add(asset);
            }
        }

        public override void MoveModelToView(IWorldObject model)
        {
            base.MoveModelToView(model);

            var m = model as ConcreteWorldObject;

            this.Height = m.Height;
            this.Width = m.Width;

            this.CollisionHullKey = m.CollisionHullKey;
            this.SpriteKey = m.SpriteKey;
        }
    }
}
