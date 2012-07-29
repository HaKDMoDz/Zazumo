using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.Editor.Infrastructure;
using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Commands;
using Phat.Editor.Interfaces;
using Phat.Editor.Modules.Editors.Properties;
using Phat.Editor.Modules.Editors.ViewModels;
using Phat.Editor.Interfaces.DatabaseModel;

namespace Phat.Editor.Modules.Editors.ApplicationCommands
{    
    /// <summary>
    /// Represents the create new terrain menu item.
    /// </summary>
    [ExportPackageConextMenuItemAttribute(EditorMenuItems.Package_New_Terrain, MenuPosition.AppendTo, EditorMenuItems.Package_New)]
    public class PackageCreateNewTerrainMenuItem : MenuItem
    {
        [ImportingConstructor]
        public PackageCreateNewTerrainMenuItem(CreateNewTerrainCommand command) : base(Resources.PackageMenu_New_Terrain, command.Command) { }
    }

    /// <summary>
    /// Represents the create new Package command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CreateNewTerrainCommand
    {
        private readonly IRegionViewUIService _viewUIService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public CreateNewTerrainCommand(IEventAggregator eventAggregator, IRegionViewUIService viewUIService)
        {
            this._viewUIService = viewUIService;

            this.Command = new DelegateCommand<Object>(CreateNewTerrainExecute, CanCreateNewTerrain);
        }

        public void CreateNewTerrainExecute(Object package)
        {
            _viewUIService.ShowView(ViewNames.CreateNewTerrain, RegionName.Popup, x =>
                {
                    ((CreateNewTerrainViewModel)x.DataContext).Package = (Package)package;
                });
        }

        public Boolean CanCreateNewTerrain(Object package)
        {
            return true;
        }
    }
}
