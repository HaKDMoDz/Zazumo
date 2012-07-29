using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using System.ComponentModel.Composition;
using Phat.Editor.Modules.Core.Properties;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using System.Data.Entity.Database;
using Phat.Editor.Interfaces.DatabaseModel;
using System.Data.SqlServerCe;
using System.IO;
using Microsoft.Practices.Prism.Events;
using Phat.Editor.Interfaces.Events;

namespace Phat.Editor.Modules.Core.ApplicationCommands
{
    /// <summary>
    /// Represents the file open menu item.
    /// </summary>
    [ExportMenuItem(MenuItems.File_Open, MenuPosition.InsertBefore, MenuItems.File_Save)]
    public class FileOpenPackageDatabaseMenuItem : MenuItem
    {
        [ImportingConstructor]
        public FileOpenPackageDatabaseMenuItem(OpenPackageDatabaseCommand openCommand) : base(Strings.MainMenu_File_Open, openCommand.Command) { }
    }

    [ExportGlobalCommand]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GlobalOpenPackageDatabaseCommand : GlobalCommand
    {
        [ImportingConstructor]
        public GlobalOpenPackageDatabaseCommand(OpenPackageDatabaseCommand openCommand) : base("Ctrl+O", openCommand.Command, new KeyGesture(Key.O, ModifierKeys.Control)) { }
    }

   /// <summary>
    /// Represents the file open command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class OpenPackageDatabaseCommand
    {
        private Boolean _isPackageDatabaseOpen;
        private readonly IEventAggregator _eventAggregator;

        public ICommand Command { get; private set; }

        [ImportingConstructor]
        public OpenPackageDatabaseCommand(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            this.Command = new DelegateCommand(OpenDatabaseExecute);
            this._isPackageDatabaseOpen = false;
        }

        public void OpenDatabaseExecute()
        {
            if (_isPackageDatabaseOpen)
            {
                var result = MessageBox.Show("Save before opening?", "Phat Editor", MessageBoxButton.YesNoCancel);

                if (result == MessageBoxResult.No)
                {
                    OpenDatabase();
                }
                else if (result == MessageBoxResult.Yes)
                {
                    OpenDatabase();
                }
                else
                {
                }
            }
            else
            {
                OpenDatabase();
            }

        }

        private void OpenDatabase()
        {
            DbDatabase.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            //DbDatabase.SetInitializer<PackageDatabase>(new DropCreateCeDatabaseAlways<PackageDatabase>());
            //var dataDirectory = AppDomain.CurrentDomain.BaseDirectory ?? Environment.CurrentDirectory;
            //var conn = new SqlCeConnection(Path.Combine(dataDirectory, "PackageDatabase.sdf"));

            this._isPackageDatabaseOpen = true;

            var db = new PackageDatabase();

            this._eventAggregator.GetEvent<CompositePresentationEvent<PackageDatabaseOpenedEvent>>()
                .Publish(new PackageDatabaseOpenedEvent("Database", db));
        }
    }
}
