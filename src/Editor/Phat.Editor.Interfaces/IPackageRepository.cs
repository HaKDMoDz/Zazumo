using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Phat.Editor.Interfaces.DatabaseModel;

namespace Phat.Editor.Interfaces
{
    public interface IPackageRepository
    {
        DbSet<Package> Packages { get; }
        DbSet<Asset> Assets { get; }

        void DeleteAssetData(Guid assetId);
        void SaveAssetData(Guid assetId, Object data);
        T GetAssetData<T>(Guid assetId);
        Object GetAssetData(Guid assetId);


        void Save();
    }
}
