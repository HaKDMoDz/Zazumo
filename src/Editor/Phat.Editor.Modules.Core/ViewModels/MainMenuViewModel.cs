using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.Editor.Infrastructure;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition.Hosting;
using System.Collections.ObjectModel;
using Phat.Editor.Interfaces;
using Phat.Editor.Modules.Core.Properties;
using System.Windows.Controls;
using System.Windows;
using Phat.Editor.Modules.Core.ViewModels;
using Phat.Editor.Modules.Core;

namespace Phat.Editor.Moduels.Core.ViewModels
{
    /// <summary>
    /// Represents the view model for the main menu.
    /// </summary>
    [Export]
    public class MainMenuViewModel
    {
        private readonly ObservableCollection<MenuItemViewModel> _topLevelMenuItems;

        public IEnumerable<MenuItemViewModel> TopLevelMenuItems
        {
            get { return _topLevelMenuItems; }
        }

        private readonly MainMenuItemStyleSelector _styleSelector;

        public StyleSelector StyleSelector { get { return _styleSelector; } }

        [ImportingConstructor]
        public MainMenuViewModel(CompositionContainer container)
        {
            _topLevelMenuItems = new ObservableCollection<MenuItemViewModel>();
            _styleSelector = new MainMenuItemStyleSelector();
            
            var menuItemImports =  container.GetExports<IMenuItem, IMenuItemMetaData>();
            var globalCommandImports = container.GetExports<IGlobalCommand>();

            var menuBuilder = new MenuBuilder(globalCommandImports);

            foreach (var import in menuItemImports.Where(x => x.Metadata.IsMainMenuItem))
            {
                menuBuilder.AddItem(import.Value, import.Metadata);
            }

            foreach (var rootItem in menuBuilder.Build().Children)
            {
                MenuItemViewModel viewModel;

                if (rootItem is MenuSeparatorModel)
                {
                    viewModel  = new MenuSeparatorViewModel();
                }
                else
                {
                    viewModel = new MenuItemViewModel(null, rootItem.HeaderText, rootItem.Command, rootItem.GestureText, rootItem.Children);
                }

                _topLevelMenuItems.Add(viewModel);
            }
        }

        public class MainMenuItemStyleSelector : StyleSelector
        {
            public override Style SelectStyle(Object item, DependencyObject container)
            {
                if (item is MenuSeparatorViewModel)
                {
                    return ((FrameworkElement)container).FindResource("MainMenuSeparatorStyle") as Style;                    
                }
                else
                {
                    return ((FrameworkElement)container).FindResource("MainMenuItemStyle") as Style;
                }
            }
        }
    }

    /// <summary>
    /// Represents the file menu.
    /// </summary>
    [ExportMenuItem(MenuItems.File, MenuPosition.PrependTo, MenuItems.Root)]
    public class FileMenu : Phat.Editor.Interfaces.MenuItem
    {
        public FileMenu() : base(Strings.MainMenu_File) { }
    }
    
    /// <summary>
    /// Represents the edit menu.
    /// </summary>
    [ExportMenuItem(MenuItems.Edit, MenuPosition.InsertAfter, MenuItems.File)]
    public class EditMenu : Phat.Editor.Interfaces.MenuItem
    {
        public EditMenu() : base(Strings.MainMenu_Edit) { }
    }

    /// <summary>
    /// Represents the tools menu.
    /// </summary>
    [ExportMenuItem(MenuItems.Tools, MenuPosition.InsertAfter, MenuItems.Edit)]
    public class ToolsMenu : Phat.Editor.Interfaces.MenuItem
    {
        public ToolsMenu() : base(Strings.MainMenu_Tools) { }
    }

    /// <summary>
    /// Represents the help menu.
    /// </summary>
    [ExportMenuItem(MenuItems.Help, MenuPosition.InsertAfter, MenuItems.Tools)]
    public class HelpMenu : Phat.Editor.Interfaces.MenuItem
    {
        public HelpMenu() : base(Strings.MainMenu_Help) { }
    }
}
