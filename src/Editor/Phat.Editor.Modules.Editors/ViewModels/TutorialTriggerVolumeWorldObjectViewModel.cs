using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.ActorResources;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Configuration;
using System.Windows;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    public class TutorialTriggerVolumeWorldObjectViewModel : WorldObjectViewModel
    {
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
        
        private String _text;
        public String Text
        {
            get { return _text; }
            set
            {
                if (value == _text)
                    return;

                this._text = value;
                RaisePropertyChanged(() => Text);
                NotifyWorldObjectValueChanged("Text", value);
            }
        }

        public override void SetModel(IWorldObject model)
        {
            base.SetModel(model);

            var m = model as TutorialTriggerVolumeWorldObject;
            this.Height = m.Height;
            this.Width = m.Width;
            this.Text = m.Text;
        }

        public override IWorldObject MoveViewToModel()
        {
            var model = new TutorialTriggerVolumeWorldObject();
            model.Id = this.Id;
            model.Name = this.Name;
            model.Behavior = this.Behavior;
            model.X = this.X;
            model.Y = this.Y;
            model.Width = this.Width;
            model.Height = this.Height;
            model.Text = this.Text;

            return model;
        }

        public TutorialTriggerVolumeWorldObjectViewModel()
        {
        }
    }
}
