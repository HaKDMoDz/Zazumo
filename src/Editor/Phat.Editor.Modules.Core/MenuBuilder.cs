using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Phat.Editor.Modules.Core
{
    public class MenuBuilder
    {
        private readonly IEnumerable<Lazy<IGlobalCommand>> _globalCommandImportData;
        private readonly List<MenuItemInfo> _menuInfo;

        public MenuBuilder(IEnumerable<Lazy<IGlobalCommand>> globalCommandImportData)
        {
            this._globalCommandImportData = globalCommandImportData;
            this._menuInfo = new List<MenuItemInfo>();
        }

        public void AddItem(IMenuItem menuItem, IMenuItemMetaData importData)
        {
            StringBuilder gestureText = new StringBuilder();

            var linkedCommands = _globalCommandImportData.Where(x => x.Value.Command == menuItem.Command);

            foreach (var link in linkedCommands)
            {
                gestureText.Append(link.Value.GestureText).Append(" ");
            }

            var itemViewModel = new MenuItemModel(menuItem.HeaderText, menuItem.Command, gestureText.ToString().Trim());
            _menuInfo.Add(new MenuItemInfo(importData.Key, importData.RelativePosition, importData.RelativeMenuItemKey, itemViewModel, importData.ShouldPrependSeparator));

        }

        public MenuItemModel Build()
        {
            var root = new MenuItemModel(String.Empty, null, String.Empty);

            foreach (var info in _menuInfo)
            {
                MenuItemModel relativeItem;

                if (info.RelativeMenuItemKey == MenuItems.Root)
                    relativeItem = root;
                else
                {
                    var relativeItemInfo = _menuInfo.Where(x => x.MenuItemKey == info.RelativeMenuItemKey).FirstOrDefault();

                    if (relativeItemInfo == null)
                        throw new MenuItemNotFoundException(String.Format("The menu item with key {0} declares a relative item key {1} that was not found.", info.MenuItemKey, info.RelativeMenuItemKey));

                    relativeItem = relativeItemInfo.MenuItem;
                }

                switch (info.RelativePosition)
                {
                    case MenuPosition.InsertAfter:
                        relativeItem.InsertAfter(info.MenuItem);
                        break;
                    case MenuPosition.InsertBefore:
                        relativeItem.InsertBefore(info.MenuItem);
                        break;
                    case MenuPosition.AppendTo:
                        if (info.ShouldPrependSeparator)
                            relativeItem.AppendChild(new MenuSeparatorModel());

                        relativeItem.AppendChild(info.MenuItem);
                        break;
                    case MenuPosition.PrependTo:
                        relativeItem.PrependChild(info.MenuItem);
                        break;
                }
            }

            return root;
        }
    }

    public class MenuItemModel
    {
        private MenuItemModel _parent;

        private readonly List<MenuItemModel> _previousSiblings;
        private readonly List<MenuItemModel> _nextSiblings;
        private readonly ObservableCollection<MenuItemModel> _children;

        /// <summary>
        /// Gets the child menu items.
        /// </summary>
        public IEnumerable<MenuItemModel> Children { get { return _children; } }

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

        public MenuItemModel(String headerText, ICommand command, String gestureText)
        {
            this._children = new ObservableCollection<MenuItemModel>();
            this._previousSiblings = new List<MenuItemModel>();
            this._nextSiblings = new List<MenuItemModel>();
            this.HeaderText = headerText;
            this.Command = command;
            this.GestureText = gestureText;
        }

        /// <summary>
        /// Ensures that the menu item is valid.
        /// </summary>
        public void Validate()
        {
        }

        /// <summary>
        /// Inserts a sibling item in the position before this item.
        /// </summary>
        /// <param name="newItem">The menu item to insert</param>
        public void InsertBefore(MenuItemModel newItem)
        {
            if (_parent == null)
            {
                _previousSiblings.Add(newItem);
            }
            else
            {
                _parent.InsertChildBefore(newItem, this);
            }
        }

        /// <summary>
        /// Inserts a sibling item in the position after this item.
        /// </summary>
        /// <param name="newItem">The menu item to insert</param>
        public void InsertAfter(MenuItemModel newItem)
        {
            if (_parent == null)
            {
                _nextSiblings.Add(newItem);
            }
            else
            {
                _parent.InsertChildAfter(newItem, this);
            }
        }

        /// <summary>
        /// Adds a child item to the end of this list.
        /// </summary>
        /// <param name="newItem">The menu item to insert</param>
        public void AppendChild(MenuItemModel newItem)
        {
            this._children.Add(newItem);
            newItem.SetParent(this);
        }

        /// <summary>
        /// Adds a child item to the start of this list.
        /// </summary>
        /// <param name="newItem">The menu item to insert</param>
        public void PrependChild(MenuItemModel newItem)
        {
            this._children.Insert(0, newItem);
            newItem.SetParent(this);
        }

        private void InsertChildBefore(MenuItemModel childToInsert, MenuItemModel relativePositionMarker)
        {
            var index = this._children.IndexOf(relativePositionMarker);

            if (index > 0)
            {
                if (this._children[index - 1] is MenuSeparatorModel)
                {
                    index--;
                }
            }

            this._children.Insert(index, childToInsert);
            childToInsert.SetParent(this);
        }

        private void InsertChildAfter(MenuItemModel childToInsert, MenuItemModel relativePositionMarker)
        {
            var index = this._children.IndexOf(relativePositionMarker);
            this._children.Insert(index + 1, childToInsert);
            childToInsert.SetParent(this);
        }

        private void SetParent(MenuItemModel parent)
        {
            this._parent = parent;

            foreach (var item in _previousSiblings)
                _parent.InsertChildBefore(item, this);

            foreach (var item in _nextSiblings)
                _parent.InsertChildAfter(item, this);
        }
    }

    public sealed class MenuSeparatorModel : MenuItemModel
    {
        public MenuSeparatorModel()
            : base(String.Empty, null, String.Empty)
        {

        }
    }

    /// <summary>
    /// Represents menu item information for the purpose of determining positions.
    /// </summary>
    public class MenuItemInfo
    {
        public String MenuItemKey { get; private set; }
        public MenuPosition RelativePosition { get; set; }
        public String RelativeMenuItemKey { get; private set; }

        public Boolean ShouldPrependSeparator { get; private set; }

        public MenuItemModel MenuItem { get; private set; }

        public MenuItemInfo(String menuItemKey, MenuPosition relativePosition, String relativeMenuItemKey, MenuItemModel menuItem, Boolean shouldPrependSeparator)
        {
            this.MenuItemKey = menuItemKey;
            this.RelativePosition = relativePosition;
            this.RelativeMenuItemKey = relativeMenuItemKey;
            this.MenuItem = menuItem;
            this.ShouldPrependSeparator = shouldPrependSeparator;
        }
    }

    /// <summary>
    /// Represents an exception that is thrown when a menu item is placed relative to another item 
    /// that is not found.
    /// </summary>
    [Serializable]
    public class MenuItemNotFoundException : Exception
    {
        public MenuItemNotFoundException() { }
        public MenuItemNotFoundException(string message) : base(message) { }
        public MenuItemNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected MenuItemNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
