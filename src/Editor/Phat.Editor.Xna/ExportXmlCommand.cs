using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using Phat.Editor.Xna.Properties;
using Microsoft.Practices.Prism.Events;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using Phat.Editor.Interfaces.Events;
using System.IO;
using System.Configuration;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Phat.ActorResources;

namespace Phat.Editor.Xna
{
    /// <summary>
    /// Represents the file open menu item.
    /// </summary>
    [ExportMenuItem(XnaMenuItems.Tools_ExportXml, MenuPosition.AppendTo, MenuItems.Tools)]
    public class ToolsExportXmlDatabaseMenuItem : MenuItem
    {
        [ImportingConstructor]
        public ToolsExportXmlDatabaseMenuItem(ExportXmlCommand command) : base(Resources.MainMenu_Tools_ExportXml, command.Command) { }
    }

    /// <summary>
    /// Represents the file open command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ExportXmlCommand
    {
        private Boolean _isPackageDatabaseOpen;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPackageRepository _packageRepository;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public ExportXmlCommand(IPackageRepository packageRepository, IEventAggregator eventAggregator)
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
                var packageFileName = Path.Combine(ConfigurationManager.AppSettings["ContentRoot"], "Packages", package.Name) + ".xml";

                List<ResourceModel> packageResources = new List<ResourceModel>();

                foreach (var asset in _packageRepository.Assets.Where(x => x.PackageId == package.Id).OrderBy(x => x.Key))
                {
                    var obj = _packageRepository.GetAssetData(asset.Id);
                    packageResources.Add((ResourceModel)obj);
                }

                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                xmlSettings.Indent = true;

                using (var stream = File.Open(packageFileName, FileMode.Create))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlSettings))
                    {
                        IntermediateSerializer.Serialize<ResourceModel[]>(xmlWriter, packageResources.ToArray(), null);
                    }
                }
            }
        }
    }
}
