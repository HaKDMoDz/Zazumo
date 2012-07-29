using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Phat.Editor.Interfaces.Events;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Phat.Editor.Modules.Core.ApplicationCommands
{
    /// <summary>
    /// Represents the file open menu item.
    /// </summary>
    [ExportMenuItem("Tools.ExportBinaryData", MenuPosition.AppendTo, MenuItems.Tools)]
    public class ToolsExportBinaryDataDatabaseMenuItem : MenuItem
    {
        [ImportingConstructor]
        public ToolsExportBinaryDataDatabaseMenuItem(ExportBinaryDataCommand command) : base("Export Binary", command.Command) { }
    }

    /// <summary>
    /// Represents the file open command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ExportBinaryDataCommand
    {
        private Boolean _isPackageDatabaseOpen;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPackageRepository _packageRepository;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public ExportBinaryDataCommand(IPackageRepository packageRepository, IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            this._packageRepository = packageRepository;
            this.Command = new DelegateCommand(ExportExecute, () => _isPackageDatabaseOpen);
            this._isPackageDatabaseOpen = false;

            eventAggregator.GetEvent<CompositePresentationEvent<PackageDatabaseOpenedEvent>>()
              .Subscribe(x =>
              {
                  this._isPackageDatabaseOpen = true;
                  ((DelegateCommand)Command).RaiseCanExecuteChanged();
              });
        }

        public void ExportExecute()
        {
            foreach (var package in _packageRepository.Packages)
            {
                var packageFileName = Path.Combine(ConfigurationManager.AppSettings["ContentRoot"], "Packages", package.Name) + ".dat";

                var packageResources = new List<Phat.ActorResources.ResourceModel>();

                foreach (var asset in _packageRepository.Assets.Where(x => x.PackageId == package.Id).OrderBy(x => x.Key))
                {
                    var obj = _packageRepository.GetAssetData(asset.Id);
                    packageResources.Add((Phat.ActorResources.ResourceModel)obj);
                }

                using (var stream = File.Open(packageFileName, FileMode.Create))
                {
                    var serializer = new BinaryFormatter();
                    serializer.Serialize(stream, packageResources.ToArray());
                }
            }
        }
    }
}
