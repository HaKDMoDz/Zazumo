using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using Phat.Editor.Modules.Editors.Properties;
using Phat.Editor.Infrastructure;
using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Commands;
using Phat.Editor.Interfaces.DatabaseModel;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors.ApplicationCommands
{
    /// <summary>
    /// Represents the create new sprite menu item.
    /// </summary>
    [ExportPackageConextMenuItemAttribute(EditorMenuItems.Package_New_Sprite, MenuPosition.InsertAfter, EditorMenuItems.Package_New_FrameSet)]
    public class PackageCreateNewSpriteMenuItem : MenuItem
    {
        [ImportingConstructor]
        public PackageCreateNewSpriteMenuItem(CreateNewSpriteCommand command) : base(Resources.PackageMenu_New_Sprite, command.Command) { }
    }

    /// <summary>
    /// Represents the create new sprite command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CreateNewSpriteCommand
    {
        private readonly IRegionViewUIService _viewUIService;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public CreateNewSpriteCommand(IEventAggregator eventAggregator, IRegionViewUIService viewUIService)
        {
            this._viewUIService = viewUIService;

            this.Command = new DelegateCommand<Object>(CreateNewSpriteExecute, CanCreateNewSprite);
        }

        public void CreateNewSpriteExecute(Object package)
        {
            _viewUIService.ShowView(ViewNames.CreateNewSprite, RegionName.Popup, x =>
            {
                ((CreateNewSpriteViewModel)x.DataContext).Package = (Package)package;
            });
        }

        public Boolean CanCreateNewSprite(Object package)
        {
            return true;
        }
    }
}
