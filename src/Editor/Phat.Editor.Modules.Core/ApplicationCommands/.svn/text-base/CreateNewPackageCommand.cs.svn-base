using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Phat.Editor.Interfaces.Events;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Phat.Editor.Interfaces;
using Phat.Editor.Modules.Core.Properties;
using Phat.Editor.Infrastructure;

namespace Phat.Editor.Modules.Core.ApplicationCommands
{
    /// <summary>
    /// Represents the create new package menu item.
    /// </summary>
    [ExportMenuItem(MenuItems.File_New, MenuPosition.InsertBefore, MenuItems.File_Open)]
    public class FileCreateNewPackageMenuItem : MenuItem
    {
        [ImportingConstructor]
        public FileCreateNewPackageMenuItem(CreateNewPackageCommand command) : base(Strings.MainMenu_File_CreateNewPackage, command.Command) { }
    }

    /// <summary>
    /// Represents the create new package menu item.
    /// </summary>
    [ExportPackageDatabaseConextMenuItemAttribute(MenuItems.File_New, MenuPosition.AppendTo, MenuItems.Root)]
    public class PackageDatabaseCreateNewPackageMenuItem : MenuItem
    {
        [ImportingConstructor]
        public PackageDatabaseCreateNewPackageMenuItem(CreateNewPackageCommand command) : base(Strings.MainMenu_File_CreateNewPackage, command.Command) { }
    }

    /// <summary>
    /// Represents the create new Package command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CreateNewPackageCommand
    {
        private readonly IRegionViewUIService _viewUIService;

        private Boolean _isPackageDatabaseOpen;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public CreateNewPackageCommand(IEventAggregator eventAggregator, IRegionViewUIService viewUIService)
        {
            this._isPackageDatabaseOpen = false;
            this._viewUIService = viewUIService;

            this.Command = new DelegateCommand(CreateNewPackageExecute, CanCreateNewPackage);

            eventAggregator.GetEvent<CompositePresentationEvent<PackageDatabaseOpenedEvent>>()
                .Subscribe(x => 
                    {
                        this._isPackageDatabaseOpen = true;
                        ((DelegateCommand)Command).RaiseCanExecuteChanged();
                    });
        }

        public void CreateNewPackageExecute()
        {
            _viewUIService.ShowView(ViewNames.CreateNewPackage, RegionName.Popup);
        }

        public Boolean CanCreateNewPackage()
        {
            return _isPackageDatabaseOpen;
        }
    }
}
