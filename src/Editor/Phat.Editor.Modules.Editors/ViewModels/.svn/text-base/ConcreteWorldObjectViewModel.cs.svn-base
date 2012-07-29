using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.ProfessionalBurglar.Resources;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Configuration;
using System.Windows;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    public class ConcreteWorldObjectViewModel : WorldObjectViewModel
    {
        private readonly SpriteLoader _spriteLoader;

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

        public String _spriteKey;
        public String SpriteKey
        {
            get { return _spriteKey; }
            set
            {
                if (value == _spriteKey)
                    return;

                this._spriteKey = value;
                RaisePropertyChanged(() => SpriteKey);
                this.Sprite = _spriteLoader.LoadSprite(_spriteKey);
            }
        }

        private SpriteViewModel _sprite;
        public SpriteViewModel Sprite
        {
            get { return _sprite; }
            set
            {
                this._sprite= value;
                RaisePropertyChanged(() => Sprite);
            }
        }
    
        public override void SetModel(IWorldObject model)
        {
            base.SetModel(model);

            var m = model as ConcreteWorldObject;
            this.Height = m.Height;
            this.Width = m.Width;
            this.SpriteKey = m.SpriteKey;
        }

        public override IWorldObject MoveViewToModel()
        {
            var model = new ConcreteWorldObject();
            model.Id = this.Id;
            model.Name = this.Name;
            model.Behavior = this.Behavior;
            model.X = this.X;
            model.Y = this.Y;
            model.Width = this.Width;
            model.Height = this.Height;
            model.SpriteKey = this.SpriteKey;

            return model;
        }

        public ConcreteWorldObjectViewModel(SpriteLoader spriteLoader)
        {
            this._spriteLoader = spriteLoader;
        }
    }
}
