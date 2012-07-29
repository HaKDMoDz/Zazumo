using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Common;

namespace Phat.Editor.Interfaces.DatabaseModel
{
    public class PackageDatabase : DbContext
    {

        public PackageDatabase()
        {

        }

        public PackageDatabase(String nameOrConnectionString)
            : base( nameOrConnectionString)
        {

        }

        public PackageDatabase(DbConnection existingConnection, Boolean contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        public DbSet<Package> Packages { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetData> AssetData { get; set; }
    }
}
