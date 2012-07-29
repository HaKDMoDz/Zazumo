using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using Phat.Editor.Interfaces.DatabaseModel;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [ExportArchetypeDataEditor(typeof(UIResource))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class UIResourceArchetypeDataViewModel : ArchetypeDataViewModel
    {
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

        private Single _x;
        public Single X
        {
            get { return _x; }
            set
            {
                if (value == _x)
                    return;

                this._x = value;
                RaisePropertyChanged(() => X);
                MarkUnsaved();
            }
        }

        private Single _y;
        public Single Y
        {
            get { return _y; }
            set
            {
                if (value == _y)
                    return;

                this._y = value;
                RaisePropertyChanged(() => Y);
                MarkUnsaved();
            }
        }

        [ImportingConstructor]
        public UIResourceArchetypeDataViewModel(IPackageRepository repository, BehaviorRegistry behaviorRegistry)
        {
            this._sprites = new ObservableCollection<Asset>();

            foreach (var asset in repository.Assets.Where(x => x.Type == EditorAssetTypes.Sprite))
                _sprites.Add(asset);
        }

        public override ArchetypeData MoveViewToModel()
        {
            var model = new UIResource();

            model.Height = this.Height;
            model.Width = this.Width;
            model.SpriteKey = this.SpriteKey;
            model.X = this.X;
            model.Y = this.Y;

            return model;
        }

        public override void MoveModelToView(ArchetypeData model)
        {
            var m = model as UIResource;

            this.Height = m.Height;
            this.Width = m.Width;
            this.SpriteKey = m.SpriteKey;
            this.X = m.X;
            this.Y = m.Y;
        }
    }
}
