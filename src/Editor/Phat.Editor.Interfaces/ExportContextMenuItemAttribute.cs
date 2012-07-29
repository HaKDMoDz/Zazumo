using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Phat.Editor.Interfaces
{
    /// <summary>
    /// MEF Metadata attribute to ensure context menu items are registered appropriately
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportContextMenuItemAttribute : ExportMenuItemAttribute
    {
        /// <summary>
        /// Gets the type of the asset the menu item will be attached to.
        /// </summary>
        public String AssetType { get; private set; }

        /// <summary>
        /// Gets or sets whether this command will be fired when the tree node is double clicked.
        /// </summary>
        public Boolean IsPrimary { get; set; }

        public ExportContextMenuItemAttribute(String key, MenuPosition relativePosition, String relativeMenuItemKey, String assetType)
            : base(key, relativePosition, relativeMenuItemKey)
        {
            this.AssetType = assetType;
            this.IsMainMenuItem = false;
        }
    }

     /// <summary>
    /// MEF metadata interface, to allow us to work with compile-safe data types.
    /// </summary>
    public interface IContextMenuItemMetaData : IMenuItemMetaData
    {
        String AssetType { get; }
        Boolean IsPrimary { get; }
    }

    /// <summary>
    /// MEF Metadata attribute to ensure package database context menu items are registered appropriately
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportPackageDatabaseConextMenuItemAttribute : ExportContextMenuItemAttribute
    {
        public ExportPackageDatabaseConextMenuItemAttribute(String key, MenuPosition relativePosition, String relativeMenuItemKey)
            : base(key, relativePosition, relativeMenuItemKey, AssetTypes.PacakgeDatabase)
        {
        }
    }

    /// <summary>
    /// MEF Metadata attribute to ensure package database context menu items are registered appropriately
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportPackageConextMenuItemAttribute : ExportContextMenuItemAttribute
    {
        public ExportPackageConextMenuItemAttribute(String key, MenuPosition relativePosition, String relativeMenuItemKey)
            : base(key, relativePosition, relativeMenuItemKey, AssetTypes.Package)
        {
        }
    }
}
