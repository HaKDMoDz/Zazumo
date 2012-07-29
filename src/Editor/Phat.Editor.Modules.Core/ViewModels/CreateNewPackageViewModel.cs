using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using Phat.Editor.Interfaces.DatabaseModel;
using Microsoft.Practices.Prism.Events;
using Phat.Editor.Interfaces.Events;

namespace Phat.Editor.Modules.Core.ViewModels
{
    [Export]
    public class CreateNewPackageViewModel : NotificationObject
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

        public event EventHandler Completed;

        [ImportingConstructor]
        public CreateNewPackageViewModel(IPackageRepository packageRepository, IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            this._packageRepository = packageRepository;

            _okCommand = new DelegateCommand(OkExecute, OkCanExecute);
            _cancelCommand = new DelegateCommand(() =>
                {
                    if (Completed != null)
                        this.Completed(this, new EventArgs());
                });

            Int32 uniqueNameNumber = 1;

            while (true)
            {
                String packageName = "Package" + uniqueNameNumber;

                if (IsPackageNameUnique(packageName))
                {
                    this.PackageName = packageName;
                    break;
                }

                uniqueNameNumber++;
            }
        }

        private Boolean IsPackageNameUnique(String name)
        {
            return _packageRepository.Packages.Where(x => x.Name == name).Count() == 0;            
        }

        public void OkExecute()
        {
            Package p = new Package();
            p.Name = this.PackageName;
            p.Id = Guid.NewGuid();
            this._packageRepository.Packages.Add(p);
            this._packageRepository.Save();

            if (Completed != null)
                this.Completed(this, new EventArgs());

            this._eventAggregator.GetEvent<CompositePresentationEvent<PackageCreatedEvent>>().Publish(new PackageCreatedEvent(p));
        }

        public Boolean OkCanExecute()
        {
            if (String.IsNullOrWhiteSpace(this.PackageName))
                return false;

            if (!IsPackageNameUnique(this.PackageName))
                return false;

            return true;
        }
    }
}
