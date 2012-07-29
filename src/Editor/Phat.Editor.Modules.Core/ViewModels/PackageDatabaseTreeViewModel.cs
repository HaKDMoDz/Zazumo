using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces.Events;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel.Composition.Hosting;
using Phat.Editor.Interfaces;
using Microsoft.Practices.Prism.ViewModel;
using Phat.Editor.Interfaces.DatabaseModel;
using System.Windows.Input;

namespace Phat.Editor.Modules.Core.ViewModels
{
    /// <summary>
    /// The view model for the pacakage database tree.
    /// </summary>
    [Export]
    public class PackageDatabaseTreeViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ObservableCollection<PackageTreeNodeViewModel> _rootNodes;
        private readonly CompositionContainer _container;
        private readonly IPackageRepository _repository;

        private readonly ContextMenuItemStyleSelector _contextMenuStyleSelector;
        public StyleSelector ContextMenuStyleSelector { get { return _contextMenuStyleSelector; } }

        public class ContextMenuItemStyleSelector : StyleSelector
        {
            public override Style SelectStyle(Object item, DependencyObject container)
            {
                if (item is MenuSeparatorViewModel)
                {
                    return ((FrameworkElement)container).FindResource("ContextMenuSeparatorStyle") as Style;
                }
                else
                {
                    return ((FrameworkElement)container).FindResource("ContextMenuItemStyle") as Style;
                }
            }
        }

        public IEnumerable<PackageTreeNodeViewModel> RootNodes { get { return _rootNodes; } }

        [ImportingConstructor]
        public PackageDatabaseTreeViewModel(CompositionContainer container, IEventAggregator eventAggregator, IPackageRepository repository)
        {
            this._eventAggregator = eventAggregator;
            this._rootNodes = new ObservableCollection<PackageTreeNodeViewModel>();
            this._container = container;
            this._contextMenuStyleSelector = new ContextMenuItemStyleSelector();
            this._repository = repository;

            this._eventAggregator.GetEvent<CompositePresentationEvent<PackageDatabaseOpenedEvent>>()
                .Subscribe(PackageDatabaseOpened, ThreadOption.UIThread, true);

            eventAggregator.GetEvent<CompositePresentationEvent<PackageCreatedEvent>>().Subscribe(
             x =>
             {
                 _rootNodes.First().AddChild(new PackageNodeViewModel(x.NewPackage, this, container, _repository));
             }, ThreadOption.UIThread, true);

            eventAggregator.GetEvent<CompositePresentationEvent<AssetCreatedEvent>>().Subscribe(
             x =>
             {
                 var packageNode = _rootNodes.First().Children.Where(node => ((Package)node.Model).Id == x.NewAsset.PackageId).Single();
                 packageNode.AddChild(new AssetNodeViewModel(x.NewAsset, this, container));
             }, ThreadOption.UIThread, true);

            eventAggregator.GetEvent<CompositePresentationEvent<PackageDeletedEvent>>().Subscribe(
             x =>
             {
                 var packageNode = _rootNodes.First().Children.Where(node => ((Package)node.Model).Id == x.PackageId).Single();
                 _rootNodes.First().RemoveChild(packageNode);
             }, ThreadOption.UIThread, true);
        }

        public void PackageDatabaseOpened(PackageDatabaseOpenedEvent @event)
        {
            _rootNodes.Clear();
            var databaseNode = new PackageDatabaseNodeViewModel(this, _container, _repository) { Name = @event.DatabaseName };
            databaseNode.IsExpanded = true;
            _rootNodes.Add(databaseNode);
        }
    }

    /// <summary>
    /// Represents the interaction logic for a package node view.
    /// </summary>
    public class PackageTreeNodeViewModel : NotificationObject
    {
        private readonly PackageDatabaseTreeViewModel _parent;

        public StyleSelector StyleSelector { get { return _parent.ContextMenuStyleSelector; } }

        private readonly ObservableCollection<PackageTreeNodeViewModel> _children;

        /// <summary>
        /// Gets the child elements of this node.
        /// </summary>
        public IEnumerable<PackageTreeNodeViewModel> Children { get { return _children; } }

        /// <summary>
        /// Gets the node name.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Gets the model.
        /// </summary>
        public Object Model { get; set; }

        /// <summary>
        ///  Gets or sets the command to run when the node is double clicked.
        /// </summary>
        public ICommand PrimaryCommand { get; set; }

        private Boolean _isExpanded;
        public Boolean IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                this.RaisePropertyChanged(() => this.IsExpanded);
            }
        }        

        private readonly ObservableCollection<MenuItemViewModel> _contextMenuTopLevelItems;

        public IEnumerable<MenuItemViewModel> ContextMenuTopLevelItems { get { return _contextMenuTopLevelItems; } }

        public PackageTreeNodeViewModel(Object model, PackageDatabaseTreeViewModel parent, CompositionContainer container, String assetType)
        {
            this._parent = parent;
            this._contextMenuTopLevelItems = new ObservableCollection<MenuItemViewModel>();
            this._children = new ObservableCollection<PackageTreeNodeViewModel>();
            this.Model = model;

            var menuItemImports = container.GetExports<IMenuItem, IContextMenuItemMetaData>();
            var globalCommandImports = container.GetExports<IGlobalCommand>();

            var menuBuilder = new MenuBuilder(globalCommandImports);

            foreach (var import in menuItemImports.Where(x => x.Metadata.AssetType == assetType))
            {
                if (import.Metadata.IsPrimary)
                {
                    this.PrimaryCommand = import.Value.Command;
                }

                menuBuilder.AddItem(import.Value, import.Metadata);
            }

            foreach (var rootItem in menuBuilder.Build().Children)
            {
                MenuItemViewModel viewModel;

                if (rootItem is MenuSeparatorModel)
                {
                    viewModel = new MenuSeparatorViewModel();
                }
                else
                {
                    viewModel = new MenuItemViewModel(this.Model, rootItem.HeaderText, rootItem.Command, rootItem.GestureText, rootItem.Children);
                }

                _contextMenuTopLevelItems.Add(viewModel);
            }
        }

        public void AddChild(PackageTreeNodeViewModel child)
        {
            this._children.Add(child);
        }

        public void RemoveChild(PackageTreeNodeViewModel child)
        {
            this._children.Remove(child);
        }
    }

    public class PackageDatabaseNodeViewModel : PackageTreeNodeViewModel
    {
        public PackageDatabaseNodeViewModel(PackageDatabaseTreeViewModel parent, CompositionContainer container, IPackageRepository repository)
            :base (null, parent, container, AssetTypes.PacakgeDatabase)
        {
            foreach (var package in repository.Packages.OrderBy(x => x.Name))
            {
                this.AddChild(new PackageNodeViewModel(package, parent, container, repository));
            }         
        }
    }

    public class PackageNodeViewModel : PackageTreeNodeViewModel
    {
        public PackageNodeViewModel(Package package, PackageDatabaseTreeViewModel parent, CompositionContainer container, IPackageRepository repository)
            : base(package, parent, container, AssetTypes.Package)
        {
            this.Name = package.Name;
            this.Model = package;

            foreach (var asset in repository.Assets.Where(x => x.PackageId == package.Id).OrderBy(x => x.Key))
            {
                this.AddChild(new AssetNodeViewModel(asset, parent, container));
            }  
        }
    }

    public class AssetNodeViewModel : PackageTreeNodeViewModel
    {
        public AssetNodeViewModel(Asset asset, PackageDatabaseTreeViewModel parent, CompositionContainer container)
            : base(asset, parent, container, asset.Type)
        {
            this.Name = asset.Key;
            this.Model = asset;
        }
    }
}
