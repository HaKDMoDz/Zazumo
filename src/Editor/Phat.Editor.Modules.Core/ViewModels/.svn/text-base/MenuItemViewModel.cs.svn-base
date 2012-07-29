using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Phat.Editor.Moduels.Core.ViewModels;

namespace Phat.Editor.Modules.Core.ViewModels
{
    /// <summary>
    /// Represents an item in the main menu.
    /// </summary>
    public class MenuItemViewModel
    {
        private readonly ObservableCollection<MenuItemViewModel> _children;

        /// <summary>
        /// Gets the child menu items.
        /// </summary>
        public IEnumerable<MenuItemViewModel> Children { get { return _children; } }

        /// <summary>
        /// Gets the text of this menu item.
        /// </summary>
        public String HeaderText { get; private set; }

        /// <summary>
        /// Gets the command of this menu item.
        /// </summary>
        public ICommand Command { get; private set; }

        /// <summary>
        /// Gets or sets the Gesture text.
        /// </summary>
        public String GestureText { get; set; }

        /// <summary>
        /// Gets or sets the Model.
        /// </summary>
        public Object Model { get; set; }

        public MenuItemViewModel(Object model, String headerText, ICommand command, String gestureText, IEnumerable<MenuItemModel> children)
        {
            this._children = new ObservableCollection<MenuItemViewModel>();
            this.HeaderText = headerText;
            this.Command = command;
            this.GestureText = gestureText;
            this.Model = model;

            foreach (var child in children)
            {
                MenuItemViewModel viewModel = null;

                if (child is MenuSeparatorModel)
                    viewModel = new MenuSeparatorViewModel();
                else
                    viewModel = new MenuItemViewModel(model, child.HeaderText, child.Command, child.GestureText, child.Children);

                this._children.Add(viewModel);
            }
        }
    }

    public sealed class MenuSeparatorViewModel : MenuItemViewModel
    {
        public MenuSeparatorViewModel()
            : base(null, String.Empty, null, String.Empty, new List<MenuItemModel>())
        {

        }
    } 
}
