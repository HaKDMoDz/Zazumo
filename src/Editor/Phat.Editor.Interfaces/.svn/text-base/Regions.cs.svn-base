using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace Phat.Editor
{
    /// <summary>
    /// Declares the names of the regions used in the shell.
    /// </summary>
    public enum RegionName
    {
        /// <summary>
        /// The menu region
        /// </summary>
        Menu,

        /// <summary>
        /// The Tools region.
        /// </summary>
        Tools,

        /// <summary>
        /// The content region.
        /// </summary>
        Content,

        /// <summary>
        /// The popup window region.
        /// </summary>
        Popup
    }

    /// <summary>
    /// When inherited, provides compile time checked xaml markup for region names.
    /// </summary>
    public class BaseRegionExtension : MarkupExtension
    {
        private RegionName _regionName;

        public BaseRegionExtension(RegionName regionName)
        {
            this._regionName = regionName;
        }

        public override Object ProvideValue(IServiceProvider serviceProvider)
        {
            return _regionName.ToString();
        }

        /// <summary>
        /// Gets the region enumeration.
        /// </summary>
        public RegionName Name
        {
            get { return _regionName; }
        }
    }

    /// <summary>
    /// Represents the menu region.
    /// </summary>
    public class MenuRegion : BaseRegionExtension
    {
        public MenuRegion()
            : base (RegionName.Menu)
        {

        }
    }

    /// <summary>
    /// Represents the tools region.
    /// </summary>
    public class ToolsRegion : BaseRegionExtension
    {
        public ToolsRegion()
            : base(RegionName.Tools)
        {

        }
    }

    /// <summary>
    /// Represents the content region.
    /// </summary>
    public class ContentRegion : BaseRegionExtension
    {
        public ContentRegion()
            : base(RegionName.Content)
        {

        }
    }

    /// <summary>
    /// Represents the popup region.
    /// </summary>
    public class PopupRegion : BaseRegionExtension
    {
        public PopupRegion()
            : base(RegionName.Popup)
        {

        }
    }
}
