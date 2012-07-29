﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using Phat.Editor.Interfaces.DatabaseModel;
using System.Collections.ObjectModel;
using Phat.ProfessionalBurglar.Resources;
using Phat.Editor.Interfaces.Events;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    public class CreateNewSpriteViewModel : NotificationObject
    {
        public event EventHandler Completed;

        private readonly IPackageRepository _packageRepository;
        private readonly IEventAggregator _eventAggregator;

        private DelegateCommand _okCommand;
        public ICommand OkCommand { get { return _okCommand; } }

        private DelegateCommand _cancelCommand;
        public ICommand CancelCommand { get { return _cancelCommand; } }

        private String _packageName;
        public String PackageName
        {
            get { return _packageName; }
            set
            {
                _packageName = value;
                this.RaisePropertyChanged(() => this.PackageName);

                _okCommand.RaiseCanExecuteChanged();
            }
        }

        private Package _package;
        public Package Package
        {
            get { return _package; }
            set
            {
                this._package = value;
                this.PackageName = this._package.Name;
                CreateDefaultName();
            }
        }

        public ObservableCollection<Package> Packages { get; private set; }

        private String _assetName;
        public String AssetName
        {
            get { return _assetName; }
            set
            {
                _assetName = value;
                this.RaisePropertyChanged(() => this.AssetName);
                _okCommand.RaiseCanExecuteChanged();
            }
        }
        
        [ImportingConstructor]
        public CreateNewSpriteViewModel(IPackageRepository packageRepository, IEventAggregator eventAggregator)
        {
            Packages = new ObservableCollection<Package>();

            _okCommand = new DelegateCommand(OkExecute, OkCanExecute);
            _cancelCommand = new DelegateCommand(() =>
            {
                if (Completed != null)
                    this.Completed(this, new EventArgs());
            });

            this._eventAggregator = eventAggregator;
            this._packageRepository = packageRepository;
            foreach (var p in _packageRepository.Packages)
            {
                this.Packages.Add(p);
            }
            this.Package = this.Packages.First();
        }

        private void CreateDefaultName()
        {
            Int32 uniqueNameNumber = 1;

            while (true)
            {
                String name = String.Format("{0}.{1}.{2}{3}", PackageName.Replace(' ', '_'), "Sprites", "Sprite", uniqueNameNumber);

                if (IsNameUnique(name))
                {
                    this.AssetName = name;
                    break;
                }

                uniqueNameNumber++;
            }
        }

        private Boolean IsNameUnique(String name)
        {
            return _packageRepository.Assets.Where(x => x.Key == name).Count() == 0;
        }

        public void OkExecute()
        {
            Asset a = new Asset();
            a.PackageId = this.Package.Id;
            a.Type = EditorAssetTypes.Sprite;
            a.Key = this.AssetName;
            a.Id = Guid.NewGuid();
                        
            this._packageRepository.Assets.Add(a);
            _packageRepository.SaveAssetData(a.Id, new SpriteResource() { Key = this.AssetName, TextureKey = String.Empty, Width=25, Height=24 });

            this._packageRepository.Save();

            if (Completed != null)
                this.Completed(this, new EventArgs());

            this._eventAggregator.GetEvent<CompositePresentationEvent<AssetCreatedEvent>>().Publish(new AssetCreatedEvent(a));
        }

        public Boolean OkCanExecute()
        {
            if (String.IsNullOrWhiteSpace(this.AssetName))
                return false;

            if (!IsNameUnique(this.AssetName))
                return false;


            return true;
        }
    }
}