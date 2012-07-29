using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel;
using Phat.ActorResources;
using System.Configuration;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel.Composition;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared)]
    public class SpriteLoader : NotificationObject
    {
        private readonly IPackageRepository _repository;
        private readonly Dictionary<String, BitmapSource> _textures;
        private readonly Dictionary<String, ImageSource> _spriteImages;
        private readonly Dictionary<String, SpriteViewModel> _sprites;

        [ImportingConstructor]
        public SpriteLoader(IPackageRepository repository)
        {
            this._repository = repository;
            this._textures = new Dictionary<String, BitmapSource>();
            this._spriteImages = new Dictionary<String, ImageSource>();
            this._sprites = new Dictionary<string, SpriteViewModel>();
        }

        public SpriteViewModel LoadSprite(String spriteKey)
        {
            if (_sprites.ContainsKey(spriteKey))
                return _sprites[spriteKey];

            var spriteAsset = _repository.Assets.Where(x => x.Key == spriteKey).FirstOrDefault();

            if (spriteAsset == null)
                return null;

            var spriteData = _repository.GetAssetData<SpriteResource>(spriteAsset.Id);

            var textureAsset = _repository.Assets.Where(x => x.Key == spriteData.TextureKey).FirstOrDefault();

            if (textureAsset == null)
                return null;

            var textureResource = _repository.GetAssetData<Texture2DResource>(textureAsset.Id);

            if (String.IsNullOrWhiteSpace(textureResource.Path))
                return null;

            if (!_textures.ContainsKey(textureResource.Key))
                _textures[textureResource.Key] = new BitmapImage(new Uri(Path.Combine(ConfigurationManager.AppSettings["ContentRoot"], textureResource.Path)));

            var textureImage = _textures[textureResource.Key];
            
            if (textureImage == null)
                return null;

            if (!_spriteImages.ContainsKey(spriteData.Key))
                _spriteImages[spriteData.Key] = new CroppedBitmap(textureImage, new Int32Rect(spriteData.UCoordinate, spriteData.VCoordinate, spriteData.Width, spriteData.Height));            

            var sprite = new SpriteViewModel();
            sprite.SpriteKey = spriteKey;
            sprite.IsFlippedHorizontally = spriteData.HorizontalFlip;
            sprite.IsFlippedVertically = spriteData.VerticalFlip;
            sprite.SpriteImage = _spriteImages[spriteData.Key];

            _sprites[spriteKey] = sprite;
            return sprite;
        }
    }
}
