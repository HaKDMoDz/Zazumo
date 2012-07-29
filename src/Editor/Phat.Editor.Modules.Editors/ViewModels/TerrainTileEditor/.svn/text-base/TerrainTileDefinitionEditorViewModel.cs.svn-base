using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.ActorResources;
using Phat.Editor.Interfaces;
using System.Windows.Media.Imaging;
using System.IO;
using System.Configuration;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Phat.Editor.Interfaces.DatabaseModel;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class TerrainTileDefinitionEditorViewModel : EditorViewModel<TerrainTileDefinitionResource>
    {
        private readonly IPackageRepository _repository;
        private readonly SpriteLoader _spriteLoader;

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

        private String _collisionData;
        public String CollisionData
        {
            get { return _collisionData; }
            set
            {
                if (value == _collisionData)
                    return;

                this._collisionData = value;
                RaisePropertyChanged(() => CollisionData);
                MarkUnsaved();   
            }
        }

        private Int32 _layer;
        public Int32 Layer
        {
            get { return _layer; }
            set
            {
                if (value == _layer)
                    return;

                this._layer = value;
                RaisePropertyChanged(() => Layer);
                MarkUnsaved();
            }
        }
        
        private  String _spriteKey;
        public String SpriteKey
        {
            get { return _spriteKey; }
            set
            {
                if (value == _spriteKey)
                    return;

                this._spriteKey = value;
                MarkUnsaved();
                RaisePropertyChanged(() => SpriteKey);
                this.Sprite = _spriteLoader.LoadSprite(this._spriteKey);
            }
        }

        private SpriteViewModel _sprite;
        public SpriteViewModel Sprite
        {
            get { return _sprite; }
            set
            {
                _sprite = value;
                RaisePropertyChanged(() => Sprite);
            }
        }
        
        private readonly ObservableCollection<Asset> _sprites;
        public IEnumerable<Asset> Sprites
        {
            get { return _sprites; }
        }

        private Asset _selectedSprite;
        public Asset SelectedSprite
        {
            get { return _selectedSprite; }
            set
            {
                if (value == _selectedSprite)
                    return;

                this._selectedSprite = value;
                RaisePropertyChanged(() => SelectedSprite);
                SpriteKey = SelectedSprite.Key;
            }
        }

        [ImportingConstructor]
        public TerrainTileDefinitionEditorViewModel(IPackageRepository repository, SpriteLoader spriteLoader)
        {
            this._repository = repository;
            this._spriteLoader = spriteLoader;

            this._sprites = new ObservableCollection<Asset>();

            foreach (var asset in repository.Assets.Where(x => x.Type == EditorAssetTypes.Sprite).OrderBy(x => x.Key))
            {
                _sprites.Add(asset);
            }

            this.SelectedSprite = _sprites.FirstOrDefault();
        }

        protected override TerrainTileDefinitionResource MoveViewToModel()
        {
            var model = new TerrainTileDefinitionResource();
            model.Key = this.Asset.Key;
            model.CollisionData = this.CollisionData;
            model.CollisionHullKey = this.CollisionHullKey;
            model.SpriteKey = this.SpriteKey;
            model.Layer = this.Layer;
            return model;
        }

        protected override void MoveModelToView(TerrainTileDefinitionResource model)
        {
            this.CollisionData = model.CollisionData;
            this.CollisionHullKey = model.CollisionHullKey;
            this.Layer = model.Layer;

            if (!String.IsNullOrWhiteSpace(model.SpriteKey))
            {
                var spriteAsset = _repository.Assets.Where(x => x.Key == model.SpriteKey).FirstOrDefault();

                if (spriteAsset != null)
                {
                    this.SelectedSprite = spriteAsset;
                }
            }            
        }
    }
}
