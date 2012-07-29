using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.Regions;
using Phat.Editor.Infrastructure;
using Phat.Editor.Modules.Core.Views;

namespace Phat.Editor.Modules.Core
{
    [ModuleExport(typeof(CoreModule))]
    [Module(ModuleName = "Core", OnDemand=false)]
    public class CoreModule : IModule
    {
        [Import]
        public IRegionManager RegionManager { get; set; }       

        public void Initialize()
        {
            RegionManager.RegisterViewWithRegion(RegionName.Tools.ToString(), typeof(PackageDatabaseTreeView));
            RegionManager.RegisterViewWithRegion(RegionName.Menu.ToString(), typeof(MainMenuView));            
        }
    }
}
