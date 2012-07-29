using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces.DatabaseModel;

namespace Phat.Editor.Interfaces.Events
{
    /// <summary>
    /// Represents a package database being opened.
    /// </summary>
    public class PackageDatabaseOpenedEvent
    {
        /// <summary>
        /// Gets the package database model.
        /// </summary>
        public PackageDatabase PackageDatabase { get; private set; }

        /// <summary>
        /// Gets the name of the package database.
        /// </summary>
        public String DatabaseName { get; private set; }

        public PackageDatabaseOpenedEvent(String name, PackageDatabase database)
        {
            this.DatabaseName = name;
            this.PackageDatabase = database;
        }
    }
}
