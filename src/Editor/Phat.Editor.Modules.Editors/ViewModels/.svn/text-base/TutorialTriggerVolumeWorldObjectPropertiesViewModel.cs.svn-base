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
    public class TutorialTriggerVolumeWorldObjectPropertiesViewModel : WorldObjectPropertiesViewModel
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

        private String  _text;
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

        [ImportingConstructor]
        public TutorialTriggerVolumeWorldObjectPropertiesViewModel(IPackageRepository repository)
        {
        }

        public override void MoveModelToView(IWorldObject model)
        {
            base.MoveModelToView(model);

            var m = model as TutorialTriggerVolumeWorldObject;

            this.Height = m.Height;
            this.Width = m.Width;
            this.Text = m.Text;
        }
    }
}
