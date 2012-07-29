using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces.DatabaseModel;
using Phat.Editor.Interfaces;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using Microsoft.Practices.Prism.ViewModel;
using System.Text.RegularExpressions;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    public class CreateNewTerrainViewModel : NotificationObject
    {
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

        private String _height;
        public String Height
        {
            get { return _height; }
            set
            {
                _height = value;
                this.RaisePropertyChanged(() => this.Height);
                _okCommand.RaiseCanExecuteChanged();
            }
        }
        
        private String _width;
        public String Width
        {
            get { return _width; }
            set
            {
                _width = value;
                this.RaisePropertyChanged(() => this.Width);
                _okCommand.RaiseCanExecuteChanged();
            }
        }

        private String _tileHeight;
        public String TileHeight
        {
            get { return _tileHeight; }
            set
            {
                _tileHeight = value;
                this.RaisePropertyChanged(() => this.TileHeight);
                _okCommand.RaiseCanExecuteChanged();
            }
        }

        private String _tileWidth;
        public String TileWidth
        {
            get { return _tileWidth; }
            set
            {
                _tileWidth = value;
                this.RaisePropertyChanged(() => this.TileWidth);
                _okCommand.RaiseCanExecuteChanged();
            }
        }

        public event EventHandler Completed;

        [ImportingConstructor]
        public CreateNewTerrainViewModel(IPackageRepository packageRepository, IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            this._packageRepository = packageRepository;
            
            _okCommand = new DelegateCommand(OkExecute, OkCanExecute);
            _cancelCommand = new DelegateCommand(() =>
                {
                    if (Completed != null)
                        this.Completed(this, new EventArgs());
                });

            this.TileHeight = "1";
            this.TileWidth = "1";
            this.Height = "25";
            this.Width = "25";
        }

        private void CreateDefaultName()
        {
            Int32 uniqueNameNumber = 1;
                        
            while (true)
            {
                String name = String.Format("{0}.{1}.{2}{3}", PackageName.Replace(' ', '_'), "Terrains", "Terrain", uniqueNameNumber);

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
            /*Package p = new Package();
            p.Name = this.PackageName;
            p.Id = Guid.NewGuid();
            this._packageRepository.Packages.Add(p);
            this._packageRepository.Save();*/

            if (Completed != null)
                this.Completed(this, new EventArgs());

            // this._eventAggregator.GetEvent<CompositePresentationEvent<PackageCreatedEvent>>().Publish(new PackageCreatedEvent(p));
        }

        public Boolean OkCanExecute()
        {
            if (String.IsNullOrWhiteSpace(this.PackageName))
                return false;

            if (!IsNameUnique(this.PackageName))
                return false;

            if (!IsValidNumber(TileWidth))
                return false;

            if (!IsValidNumber(TileHeight))
                return false;

            if (!IsValidNumber(Width))
                return false;

            if (!IsValidNumber(Height))
                return false;
            
            return true;
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
