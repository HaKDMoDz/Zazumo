using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;
using Microsoft.Practices.Prism.Events;
using Phat.Editor.Interfaces.DatabaseModel;
using System.Data.Entity;
using Phat.Editor.Interfaces.Events;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Phat.Editor.Infrastructure
{
    [Export(typeof(IPackageRepository))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PackageRepository : IPackageRepository
    {
        private PackageDatabase _database;

        public DbSet<Package> Packages { get { return _database.Packages; } }
        public DbSet<Asset> Assets { get { return _database.Assets; } }
        
        [ImportingConstructor]
        public PackageRepository(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<CompositePresentationEvent<PackageDatabaseOpenedEvent>>()
                .Subscribe(x =>
                    {
                        this._database = x.PackageDatabase;
                    }, ThreadOption.PublisherThread, true);
        }

        public void Save()
        {
            this._database.SaveChanges();
        }
        
        public void SaveAssetData(Guid assetId, Object data)
        {
            using (var stream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, data);

                stream.GetBuffer();

                foreach (var asset in _database.AssetData.Where(x => x.AssetId == assetId))
                {
                    _database.AssetData.Remove(asset);
                }

                var assetData = new AssetData();
                assetData.Data = stream.GetBuffer();
                assetData.AssetId = assetId;
                assetData.Id = Guid.NewGuid();

                _database.AssetData.Add(assetData);
            }
        }

        public T GetAssetData<T>(Guid assetId)
        {
            return (T)GetAssetData(assetId);
        }

        public object GetAssetData(Guid assetId)
        {
            BinaryFormatter bf = new BinaryFormatter();

            var assetData = _database.AssetData.Where(x => x.AssetId == assetId).Single();

            using (var stream = new MemoryStream(assetData.Data))
            {
                var data = bf.Deserialize(stream);
                return data;
            }
        }

        public void DeleteAssetData(Guid assetId)
        {
            var assetData = _database.AssetData.Where(x => x.AssetId == assetId).Single();
            _database.AssetData.Remove(assetData);
        }
    }
}
