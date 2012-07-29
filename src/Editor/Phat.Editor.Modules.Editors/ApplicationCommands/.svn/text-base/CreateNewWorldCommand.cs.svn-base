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
    /// Represents the create new world menu item.
    /// </summary>
    [ExportPackageConextMenuItemAttribute(EditorMenuItems.Package_New_World, MenuPosition.InsertAfter, EditorMenuItems.Package_New_Texture2D)]
    public class PackageCreateNewWorldMenuItem : MenuItem
    {
        [ImportingConstructor]
        public PackageCreateNewWorldMenuItem(CreateNewWorldCommand command) : base(Resources.PackageMenu_New_World, command.Command) { }
    }

    /// <summary>
    /// Represents the create new world command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CreateNewWorldCommand
    {
        private readonly IRegionViewUIService _viewUIService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public CreateNewWorldCommand(IRegionViewUIService viewUIService)
        {
            this._viewUIService = viewUIService;

            this.Command = new DelegateCommand<Object>(CreateNewAssetExecute, CanCreateNewAsset);
        }

        public void CreateNewAssetExecute(Object package)
        {
            _viewUIService.ShowView(ViewNames.CreateNewWorld, RegionName.Popup, x =>
            {
                ((CreateNewWorldViewModel)x.DataContext).Package = (Package)package;
            });
        }

        public Boolean CanCreateNewAsset(Object package)
        {
            return true;
        }
    }
}
