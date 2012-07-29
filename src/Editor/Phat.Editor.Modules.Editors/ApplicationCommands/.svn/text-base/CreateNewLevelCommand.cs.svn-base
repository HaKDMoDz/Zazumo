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
    [ExportPackageConextMenuItemAttribute(EditorMenuItems.Package_New_Level, MenuPosition.InsertAfter, EditorMenuItems.Package_New_FrameSet )]
    public class PackageCreateNewLevelMenuItem : MenuItem
    {
        [ImportingConstructor]
        public PackageCreateNewLevelMenuItem(CreateNewLevelCommand command) : base(Resources.PackageMenu_New_Level, command.Command) { }
    }

    /// <summary>
    /// Represents the create new archetype command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CreateNewLevelCommand
    {
        private readonly IRegionViewUIService _viewUIService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public CreateNewLevelCommand(IRegionViewUIService viewUIService)
        {
            this._viewUIService = viewUIService;

            this.Command = new DelegateCommand<Object>(CreateNewAssetExecute, CanCreateNewAsset);
        }

        public void CreateNewAssetExecute(Object package)
        {
            _viewUIService.ShowView(ViewNames.CreateNewLevel, RegionName.Popup, x =>
            {
                ((CreateNewLevelViewModel)x.DataContext).Package = (Package)package;
            });
        }

        public Boolean CanCreateNewAsset(Object package)
        {
            return true;
        }
    }
}
