using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using Phat.Editor.Modules.Editors.Properties;
using Phat.Editor.Infrastructure;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Phat.Editor.Modules.Editors.ViewModels;
using Phat.Editor.Interfaces.DatabaseModel;

namespace Phat.Editor.Modules.Editors.ApplicationCommands
{
    /// <summary>
    /// Represents the create new archetype menu item.
    /// </summary>
    [ExportPackageConextMenuItemAttribute(EditorMenuItems.Package_New_Archetype, MenuPosition.AppendTo, EditorMenuItems.Package_New)]
    public class PackageCreateNewArchetypeMenuItem : MenuItem
    {
        [ImportingConstructor]
        public PackageCreateNewArchetypeMenuItem(CreateNewArchetypeCommand command) : base(Resources.PackageMenu_New_Archetype, command.Command) { }
    }

    /// <summary>
    /// Represents the create new archetype command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CreateNewArchetypeCommand
    {
        private readonly IRegionViewUIService _viewUIService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public CreateNewArchetypeCommand(IRegionViewUIService viewUIService)
        {
            this._viewUIService = viewUIService;

            this.Command = new DelegateCommand<Object>(CreateNewAssetExecute, CanCreateNewAsset);
        }

        public void CreateNewAssetExecute(Object package)
        {
            _viewUIService.ShowView(ViewNames.CreateNewArchetype, RegionName.Popup, x =>
            {
                ((CreateNewArchetypeViewModel)x.DataContext).Package = (Package)package;
            });
        }

        public Boolean CanCreateNewAsset(Object package)
        {
            return true;
        }
    }
}
