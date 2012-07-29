using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.MefExtensions;
using System.Windows;
using Phat.Editor.Views;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using Microsoft.Practices.Prism.Regions;
using Phat.Editor.Infrastructure;
using Microsoft.Practices.Prism.Modularity;
using Phat.Editor.Modules.Core;
using Phat.Editor.Interfaces;
using System.Windows.Input;
using Phat.Editor.Interfaces.DatabaseModel;
using System.Data.SqlServerCe;
using System.Configuration;
using System.Data.Entity.Database;
using System.Data.Entity.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Phat.Editor.Modules.Editors;
using Phat.Editor.Xna;

namespace Phat.Editor
{
    /// <summary>
    /// Provides the bootstrapping sequence.
    /// </summary>
    public class Bootstrapper : MefBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.Container.ComposeExportedValue<ViewFactory>(new ViewFactory(this.Container));
            this.Container.ComposeExportedValue<CompositionContainer>(this.Container);
            this.Container.ComposeExportedValue<IPackageRepository>(new PackageRepository(this.Container.GetExportedValue<IEventAggregator>()));

        }

        /// <summary>
        /// Configures the Microsoft.Practices.Prism.MefExtensions.MefBootstrapper.AggregateCatalog
        /// used by MEF.
        /// </summary>
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(BehaviorRegistry).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CoreModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(EditorsModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(XnaModule).Assembly));
        }

        /// <summary>
        /// Creates the shell or main window of the applic                                                                  ation.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        protected override DependencyObject CreateShell()
        {
            var shell = Container.GetExportedValue<Shell>();
            Application.Current.MainWindow = shell;
            shell.Show();
            return shell;
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mappings = base.ConfigureRegionAdapterMappings();

            mappings.RegisterMapping(typeof(Window), new WindowRegionAdapter(this.Container.GetExport<IRegionBehaviorFactory>().Value));

            return mappings;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            foreach (var globalCommand in this.Container.GetExports<IGlobalCommand>())
            {
                ((FrameworkElement)this.Shell).InputBindings.Add(new InputBinding(globalCommand.Value.Command, globalCommand.Value.InputGesture));
            }
        }
    }
}
