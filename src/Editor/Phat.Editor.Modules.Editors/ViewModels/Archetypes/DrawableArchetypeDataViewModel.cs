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
    [ExportArchetypeDataEditor(typeof(Drawable))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class DrawableArchetypeDataViewModel : ArchetypeDataViewModel
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
                
        [ImportingConstructor]
        public DrawableArchetypeDataViewModel(IPackageRepository repository, BehaviorRegistry behaviorRegistry)
        {
            this._sprites = new ObservableCollection<Asset>();

            foreach (var asset in repository.Assets.Where(x => x.Type == EditorAssetTypes.Sprite))
                _sprites.Add(asset);
        }

        public override ArchetypeData MoveViewToModel()
        {
            var model = new Drawable();
            model.Height = this.Height;
            model.Width = this.Width;
            model.SpriteKey = this.SpriteKey;
            
            return model;
        }

        public override void MoveModelToView(ArchetypeData model)
        {
            var m = model as Drawable;

            this.Height = m.Height;
            this.Width = m.Width;
            this.SpriteKey = m.SpriteKey;
        }
    }
}
