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
using Microsoft.Win32;
using Phat.Editor.Interfaces.DatabaseModel;
using Phat.Animations;

namespace Phat.Editor.Xna
{    
    [ExportMenuItem(XnaMenuItems.Tools_ImportXml, MenuPosition.InsertAfter, XnaMenuItems.Tools_ExportXml)]
    public class ToolsImportXmlMenuItem : MenuItem
    {
        [ImportingConstructor]
        public ToolsImportXmlMenuItem(ImportXmlCommand command) : base(Resources.MainMenu_Tools_ImportXml, command.Command) { }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ImportXmlCommand
    {
        private Boolean _isPackageDatabaseOpen;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPackageRepository _packageRepository;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public ImportXmlCommand(IPackageRepository packageRepository, IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            this._packageRepository = packageRepository;
            this.Command = new DelegateCommand(Execute, () => _isPackageDatabaseOpen);
            this._isPackageDatabaseOpen = false;

            eventAggregator.GetEvent<CompositePresentationEvent<PackageDatabaseOpenedEvent>>()
              .Subscribe(x =>
              {
                  this._isPackageDatabaseOpen = true;
                  ((DelegateCommand)Command).RaiseCanExecuteChanged();
              });
        }

        public void Execute()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Xml files (*.xml)|*.xml";
            var result = ofd.ShowDialog();

            if (!result.Value)
                return;

            ResourceModel[] importedResources;

            using (var reader = XmlReader.Create(ofd.FileName))
            {
                importedResources = IntermediateSerializer.Deserialize<ResourceModel[]>(reader, null);
            }

            var packageName = importedResources.First().Key.Split('.').First();

            var existingPackage = _packageRepository.Packages.Where(x => x.Name == packageName).FirstOrDefault();

            // delete old package if it exists.
            if (existingPackage != null)
            {
                foreach (var asset in _packageRepository.Assets.Where(x => x.PackageId == existingPackage.Id).ToArray())
                {
                    _packageRepository.Assets.Remove(asset);
                    _packageRepository.DeleteAssetData(asset.Id);
                }
                                
                _packageRepository.Packages.Remove(existingPackage);
                _packageRepository.Save();

                this._eventAggregator.GetEvent<CompositePresentationEvent<PackageDeletedEvent>>().Publish(new PackageDeletedEvent(existingPackage.Id));
            }

            Package p = new Package();
            p.Name = packageName;
            p.Id = Guid.NewGuid();
            this._packageRepository.Packages.Add(p);
                       
            this._eventAggregator.GetEvent<CompositePresentationEvent<PackageCreatedEvent>>().Publish(new PackageCreatedEvent(p));
 
            List<ResourceModel> packageResources = new List<ResourceModel>();

            foreach (var resource in importedResources)
            {
                Asset a = new Asset();
                a.PackageId = p.Id;
                if (resource is LevelResource)
                    a.Type = "Editors.Level";
                else if (resource is WorldResource)
                    a.Type = "Editors.World";
                else if (resource is ArchetypeResource)
                    a.Type = "Editors.Archetype";
                else if (resource is FrameSetResource)
                    a.Type = "Editors.FrameSet";
                else if (resource is SpriteResource)
                    a.Type = "Editors.Sprite";
                else if (resource is Storyboard)
                    a.Type = "Editors.Storyboard";
                else if (resource is TerrainTileDefinitionResource)
                    a.Type = "Editors.TerrainTileDefinition";
                else if (resource is Texture2DResource)
                    a.Type =  "Editors.Texture2D";

                a.Key = resource.Key;
                a.Id = Guid.NewGuid();

                this._packageRepository.Assets.Add(a);
                _packageRepository.SaveAssetData(a.Id, resource);

                this._eventAggregator.GetEvent<CompositePresentationEvent<AssetCreatedEvent>>().Publish(new AssetCreatedEvent(a));
            }

            _packageRepository.Save();
        }
    }
}
