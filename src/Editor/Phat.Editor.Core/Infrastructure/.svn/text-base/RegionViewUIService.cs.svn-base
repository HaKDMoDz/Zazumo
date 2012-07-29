using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;

namespace Phat.Editor.Infrastructure
{
    [Export(typeof(IRegionViewUIService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class RegionViewUIService : IRegionViewUIService
    {
        private readonly ViewFactory _viewFactory;
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public RegionViewUIService(ViewFactory viewFactory, IRegionManager regionManager)
        {
            this._viewFactory = viewFactory;
            this._regionManager = regionManager;
        }

        public void ShowView(String viewName, RegionName regionName)
        {
            var view = this._viewFactory.GetView(viewName);

            _regionManager.AddToRegion(regionName.ToString(), view);
        }

        public void ShowView(String viewName, RegionName regionName, Action<UserControl> initialization)
        {
            var view = this._viewFactory.GetView(viewName);
            initialization.Invoke(view);
            _regionManager.AddToRegion(regionName.ToString(), view);
        }
    }
}
