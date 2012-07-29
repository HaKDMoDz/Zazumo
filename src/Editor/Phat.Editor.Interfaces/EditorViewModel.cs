using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces.DatabaseModel;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Windows;

namespace Phat.Editor.Interfaces
{
    public abstract class EditorViewModel<T> : NotificationObject, IEditor
    {
        private String _title;
        public String Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => this.Title);
            }
        }

        private Asset _asset;
        public Asset Asset
        {
            get { return _asset; }
            set
            {
                _asset = value;
                SetAsset(_asset);
            }
        }

        private Boolean _hasUnsavedChanges;
        public Boolean HasUnsavedChanges
        {
            get { return _hasUnsavedChanges; }
            set
            {
                _hasUnsavedChanges = value;
                RaisePropertyChanged(() => this.HasUnsavedChanges);
            }
        }

        public void MarkUnsaved()
        {
            HasUnsavedChanges = true;

            if (Asset == null)
                return;

            this.Title = Asset.Key.Split('.').Last() + "*";
        }

        public ICommand CloseCommand { get; set; }

        [Import]
        private IWorkspaceService _workspaceService = null;

        [Import]
        private IPackageRepository _packageRepository = null;

        public EditorViewModel()
        {
            this.CloseCommand = new DelegateCommand(Close);
        }

        public void Close()
        {
            if (HasUnsavedChanges)
            {
                var result = MessageBox.Show("Save changes before closing?", Title, MessageBoxButton.YesNoCancel);

                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        return;
                    case MessageBoxResult.No:
                        this._workspaceService.CloseEditor(this.Asset);
                        break;
                    case MessageBoxResult.Yes:
                        Save();
                        this._workspaceService.CloseEditor(this.Asset);
                        break;
                    default:
                        break;
                }

            }
            else
            {
                this._workspaceService.CloseEditor(this.Asset);
            }
        }

        private void SetAsset(Asset asset)
        {
            Title = asset.Key.Split('.').Last();

            T model = _packageRepository.GetAssetData<T>(asset.Id);
            MoveModelToView(model);
            HasUnsavedChanges = false;
            this.Title = Asset.Key.Split('.').Last();
        }

        public void Save()
        {
            this.Title = Asset.Key.Split('.').Last();
            HasUnsavedChanges = false;
            var model = MoveViewToModel();
            _packageRepository.SaveAssetData(Asset.Id, model);
            _packageRepository.Save();
        }

        protected abstract T MoveViewToModel();
        protected abstract void MoveModelToView(T model);
    }
}
