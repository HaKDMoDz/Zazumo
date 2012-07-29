using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Phat.Editor.Infrastructure
{
    /// <summary>
    /// UI service that maps a view name to a view and displays it in the desired region.
    /// </summary>
    public interface IRegionViewUIService
    {
        /// <summary>
        /// Shows the view identified by <see cref="viewName"/> in the region defined be <see cref="regionName"/>.
        /// </summary>
        /// <param name="viewName">The name of the view.</param>
        /// <param name="regionName">The name of the region.</param>
        void ShowView(String viewName, RegionName regionName);

        /// <summary>
        /// Shows the view identified by <see cref="viewName"/> in the region defined be <see cref="regionName"/>.
        /// </summary>
        /// <param name="viewName">The name of the view.</param>
        /// <param name="regionName">The name of the region.</param>
        /// <param name="initialization">Initizliation code to run on the view once it has been resolved.</param>
        void ShowView(String viewName, RegionName regionName, Action<UserControl> initialization);
    }
}
