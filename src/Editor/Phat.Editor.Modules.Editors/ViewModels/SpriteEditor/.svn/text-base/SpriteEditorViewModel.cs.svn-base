using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Phat.Editor.Interfaces.DatabaseModel;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;
using System.Configuration;
using System.Windows.Media;
using System.Windows.Data;
using Phat.Editor.Modules.Editors.ValueConverters;

namespace Phat.Editor.Modules.Editors.ViewModels
{

    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class SpriteEditorViewModel : EditorViewModel<SpriteResource>
    {
        private readonly IPackageRepository _repository;

        private readonly ObservableCollection<Asset> _textures;
        public IEnumerable<Asset> Textures
        {
            get { return _textures; }
        }

        private Asset _texture;
        public Asset Texture
        {
            get { return _texture; }
            set
            {
                this._texture = value;
                RaisePropertyChanged(() => this.Texture);
                LoadTextureImage();
                this.MarkUnsaved();
            }
        }

        private BitmapImage _textureImage;
        public BitmapImage TextureImage
        {
            get { return _textureImage; }
            set
            {
                _textureImage = value;
                RaisePropertyChanged(() => this.TextureImage);
            }
        }

        private CroppedBitmap _spriteImage;
        public CroppedBitmap SpriteImage
        {
            get { return _spriteImage; }
            set
            {
                _spriteImage = value;
                RaisePropertyChanged(() => this.SpriteImage);
            }
        }

        private Int32 _height;
        public Int32 Height
        {
            get { return _height; }
            set
            {
                _height = value;
                this.RaisePropertyChanged(() => this.Height);
                this.LoadSpriteImage();
                this.MarkUnsaved();
            }
        }

        private Int32 _width;
        public Int32 Width
        {
            get { return _width; }
            set
            {
                _width = value;
                this.RaisePropertyChanged(() => this.Width);
                this.LoadSpriteImage();
                this.MarkUnsaved();
            }
        }

        private Int32 _uCoordinate;
        public Int32 UCoordinate
        {
            get { return _uCoordinate; }
            set
            {
                _uCoordinate = value;
                this.RaisePropertyChanged(() => this.UCoordinate);
                this.LoadSpriteImage();
                this.MarkUnsaved();
            }
        }

        private Int32 _vCoordinate;
        public Int32 VCoordinate
        {
            get { return _vCoordinate; }
            set
            {
                _vCoordinate = value;
                this.RaisePropertyChanged(() => this.VCoordinate);
                this.LoadSpriteImage();
                this.MarkUnsaved();
            }
        }

        private Boolean _isSizeLocked;
        public Boolean IsSizeLocked
        {
            get { return _isSizeLocked; }
            set
            {
                _isSizeLocked = value;
                this.RaisePropertyChanged(() => this.IsSizeLocked);
            }
        }

        private Boolean _isFlippedHorizontally;
        public Boolean IsFlippedHorizontally
        {
            get { return _isFlippedHorizontally; }
            set
            {
                _isFlippedHorizontally = value;
                this.RaisePropertyChanged(() => this.IsFlippedHorizontally);
                this.MarkUnsaved();
            }
        }

        private Boolean _isFlippedVertically;
        public Boolean IsFlippedVertically
        {
            get { return _isFlippedVertically; }
            set
            {
                _isFlippedVertically = value;
                this.RaisePropertyChanged(() => this.IsFlippedVertically);
                this.MarkUnsaved();
            }
        }

        [ImportingConstructor]
        public SpriteEditorViewModel(IPackageRepository repository)
        {
            this._repository = repository;
            this._textures = new ObservableCollection<Asset>();
            this._isSizeLocked = true;

            foreach (var asset in _repository.Assets.Where(x => x.Type == EditorAssetTypes.Texture2D))
            {
                _textures.Add(asset);
            }

            this.Texture = _textures.FirstOrDefault();
        }
                
        protected override SpriteResource MoveViewToModel()
        {
            var model = new SpriteResource();
            model.Height = this.Height;
            model.HorizontalFlip = this.IsFlippedHorizontally;
            model.Key = this.Asset.Key;
            model.TextureKey = this.Texture.Key;
            model.UCoordinate = this.UCoordinate;
            model.VCoordinate = this.VCoordinate;
            model.VerticalFlip = this.IsFlippedVertically;
            model.Width = this.Width;
            return model;
        }

        protected override void MoveModelToView(SpriteResource model)
        {
            this.Height = model.Height;
            this.Width = model.Width;
            this.IsFlippedHorizontally = model.HorizontalFlip;
            this.IsFlippedVertically = model.VerticalFlip;
            
            if (!String.IsNullOrWhiteSpace(model.TextureKey))
            {
                var textureAsset = _repository.Assets.Where (x => x.Key == model.TextureKey).FirstOrDefault();

                if (textureAsset != null)
                {
                    this.Texture = textureAsset;
                }
            }

            this.UCoordinate = model.UCoordinate;
            this.VCoordinate = model.VCoordinate;
        }

        private void LoadTextureImage()
        {
            if (this.Texture == null)
            {
                TextureImage = null;
                return;
            }

            var textureResource = _repository.GetAssetData<Texture2DResource>(Texture.Id);

            if (String.IsNullOrWhiteSpace(textureResource.Path))
            {
                TextureImage = null;
                return;
            }

            TextureImage = new BitmapImage(new Uri(Path.Combine(ConfigurationManager.AppSettings["ContentRoot"], textureResource.Path)));

            LoadSpriteImage();
        }

        private void LoadSpriteImage()
        {
            if (this.TextureImage == null)
                return;

            if (this.Width == 0)
                return;

            if (this.Height == 0)
                return;

            if (this.UCoordinate < 0)
                this.UCoordinate = 0;

            if (this.VCoordinate < 0)
                this.VCoordinate = 0;

            if (!IsSizeLocked)
            {
                if (UCoordinate >= this.TextureImage.PixelWidth)
                    UCoordinate = (Int32)this.TextureImage.PixelWidth;

                if (VCoordinate >= this.TextureImage.PixelHeight)
                    VCoordinate = (Int32)this.TextureImage.PixelHeight;

                if ((UCoordinate + Width) > this.TextureImage.PixelWidth)
                {
                    Width = (Int32)this.TextureImage.PixelWidth - UCoordinate;
                }

                if ((VCoordinate + Height) > this.TextureImage.PixelHeight)
                {
                    Height = (Int32)this.TextureImage.PixelHeight - VCoordinate;
                }
            }
            else
            {
                if ((UCoordinate + Width) > this.TextureImage.PixelWidth)
                    UCoordinate = (Int32)this.TextureImage.PixelWidth - Width;

                if ((VCoordinate + Height) > this.TextureImage.PixelHeight)
                    VCoordinate = (Int32)this.TextureImage.PixelHeight - Height;
            }


            _spriteImage = new CroppedBitmap(this.TextureImage, new System.Windows.Int32Rect(this.UCoordinate, this.VCoordinate, this.Width, this.Height));
            RaisePropertyChanged(() => this.SpriteImage);
        }

        private Boolean IsValidNumber(String value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return false;

            if (!Regex.IsMatch(value, @"^[\d]+$"))
            {
                return false;
            }

            var number = Int32.Parse(value);

            if (number < 1)
                return false;

            if (number > 256)
                return false;

            return true;
        }
    }
}
