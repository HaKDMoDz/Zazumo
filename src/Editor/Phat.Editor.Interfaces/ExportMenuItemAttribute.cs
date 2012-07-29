using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace Phat.Editor.Interfaces
{
    /// <summary>
    /// MEF Metadata attribute to ensure menu items are registered appropriately
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportMenuItemAttribute : ExportAttribute
    {
        public Boolean IsMainMenuItem { get; protected set; }

        /// <summary>
        /// Gets a unique identifier for this menu item.
        /// </summary>
        public String Key { get; private set; }

        /// <summary>
        /// Gets the unique identifier for the menu item to position this item relative to.
        /// </summary>
        public String RelativeMenuItemKey { get; private set; }

        /// <summary>
        /// Gets the relative positioning method to use.
        /// </summary>
        public MenuPosition RelativePosition { get; private set; }

        /// <summary>
        /// Gets or sets whether a separator should be placed before this menu item.
        /// </summary>
        public Boolean ShouldPrependSeparator { get; set; }

        public ExportMenuItemAttribute(String key, MenuPosition relativePosition, String relativeMenuItemKey)
            : base(typeof(IMenuItem))
        {
            this.Key = key;
            this.RelativeMenuItemKey = relativeMenuItemKey;
            this.RelativePosition = relativePosition;
            this.IsMainMenuItem = true;
        }
    }

    /// <summary>
    /// MEF metadata interface, to allow us to work with compile-safe data types.
    /// </summary>
    public interface IMenuItemMetaData
    {
        Boolean IsMainMenuItem { get; }

        /// <summary>
        /// Gets a unique identifier for this menu item.
        /// </summary>
        String Key { get; }

        /// <summary>
        /// Gets the unique identifier for the menu item to position this item relative to.
        /// </summary>
        String RelativeMenuItemKey { get; }

        /// <summary>
        /// Gets the relative positioning method to use.
        /// </summary>
        MenuPosition RelativePosition { get; }


        /// <summary>
        /// Gets whether a separator should be placed before this menu item.
        /// </summary>
        Boolean ShouldPrependSeparator { get; }
    }

    /// <summary>
    /// Specifies the how a menu item is positioned relative to another item.
    /// </summary>
    public enum MenuPosition
    {
        /// <summary>
        /// Menu item will be positioned below a sibling item.
        /// </summary>
        InsertAfter,

        /// <summary>
        /// Menu item will be positioned above a sibling item.
        /// </summary>
        InsertBefore,

        /// <summary>
        /// Menu item will be added as the last item in a list of children.
        /// </summary>
        /// <remarks>
        /// Order is undefined for multiple items using this positioning method under a single parent.
        /// </remarks>
        AppendTo,

        /// <summary>
        /// Menu item will be added as the first in a list of children.
        /// </summary>
        /// <remarks>
        /// Order is undefined for multiple items using this positioning method under a single parent.
        /// </remarks>
        PrependTo
    }

    /// <summary>
    /// Provides compile-safe string representations of well known menu items.
    /// </summary>
    public class MenuItems
    {
        public const String Root = "Root";

        public const String File = "File";
        public const String File_New = "New";
        public const String File_Open = "File.Open";
        public const String File_Save = "File.Save";        
        public const String File_Exit = "File.Exit";

        public const String Edit = "Edit";

        public const String Tools = "Tools";
        public const String Tools_SetContentPath = "Tools.SetContentPath";

        public const String Help = "Help";
    }
}
