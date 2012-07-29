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
    /// Represents the create new Texture2D menu item.
    /// </summary>
    [ExportPackageConextMenuItemAttribute(EditorMenuItems.Package_New_Texture2D, MenuPosition.InsertAfter, EditorMenuItems.Package_New_TerrainTileDefinition)]
    public class PackageCreateNewTexture2DMenuItem : MenuItem
    {
        [ImportingConstructor]
        public PackageCreateNewTexture2DMenuItem(CreateNewTexture2DCommand command) : base(Resources.PackageMenu_New_Texture, command.Command) { }
    }

    /// <summary>
    /// Represents the create new Texture2D command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CreateNewTexture2DCommand
    {
        private readonly IRegionViewUIService _viewUIService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public CreateNewTexture2DCommand(IEventAggregator eventAggregator, IRegionViewUIService viewUIService)
        {
            this._viewUIService = viewUIService;

            this.Command = new DelegateCommand<Object>(CreateNewTexture2DExecute, CanCreateNewTexture2D);
        }

        public void CreateNewTexture2DExecute(Object package)
        {
            _viewUIService.ShowView(ViewNames.CreateNewTexture2D, RegionName.Popup, x =>
            {
                ((CreateNewTexture2DViewModel)x.DataContext).Package = (Package)package;
            });
        }

        public Boolean CanCreateNewTexture2D(Object package)
        {
            return true;
        }
    }
}
