using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces.DatabaseModel;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Media.Imaging;
using System.Configuration;
using System.IO;
using Phat.ActorResources;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class Texture2DEditorViewModel : EditorViewModel<Texture2DResource>
    {
        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                RaisePropertyChanged(() => this.ImageSource);
            }
        }

        private String _filePath;
        public String FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                LoadImage();
                RaisePropertyChanged(() => this.FilePath);
                MarkUnsaved();
                _refreshImageCommand.RaiseCanExecuteChanged();
            }
        }

        private DelegateCommand _refreshImageCommand;
        public ICommand RefreshImageCommand { get { return _refreshImageCommand; } }

        public Texture2DEditorViewModel()
        {
            this._refreshImageCommand = new DelegateCommand(() => LoadImage(), () => !String.IsNullOrWhiteSpace(FilePath));
        }

        private void LoadImage()
        {
            if (String.IsNullOrWhiteSpace(FilePath))
                return;

            ImageSource = new BitmapImage(new Uri(Path.Combine(ConfigurationManager.AppSettings["ContentRoot"], FilePath)));
        }

        protected override Texture2DResource MoveViewToModel()
        {
            Texture2DResource model = new Texture2DResource();
            model.Key = this.Asset.Key;
            model.Path = this.FilePath;
            return model;
        }

        protected override void MoveModelToView(Texture2DResource model)
        {
            this.FilePath = model.Path;
        }
    }
}
