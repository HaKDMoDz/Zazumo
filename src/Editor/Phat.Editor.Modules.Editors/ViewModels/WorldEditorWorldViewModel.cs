using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Phat.ProfessionalBurglar.Resources;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class WorldEditorWorldViewModel : NotificationObject, IWorldEditor
    {
        private readonly Dictionary<Type, WorldObjectDefinition> _worldObjectModelToObjectTypeDefinitionMapping;
        private readonly IPackageRepository _packageRepository;

        public IWorldEditorContext Context { get; set; }

        private readonly ObservableCollection<WorldObjectTypeViewModel> _objectTypes;
        public IEnumerable<WorldObjectTypeViewModel> ObjectTypes
        {
            get { return _objectTypes; }
        }

        private readonly ObservableCollection<NewWorldObjectArchetypeViewModel> _archetypes;
        public IEnumerable<NewWorldObjectArchetypeViewModel> Archetypes
        {
            get { return _archetypes; }
        }

        private readonly ObservableCollection<WorldObjectViewModel> _worldObjects;
        public IEnumerable<WorldObjectViewModel> WorldObjects
        {
            get { return _worldObjects; }
        }

        private object _tools;
        public Object Tools
        {
            get { return _tools; }
            set
            {
                _tools = value;
                RaisePropertyChanged(() => Tools);
                RaisePropertyChanged(() => WorldEditorTools);
            }
        }
        protected WorldEditorWorldToolsViewModel WorldEditorTools
        {
            get { return (WorldEditorWorldToolsViewModel)Tools; }
        }

        #region GridSnappingProperties
        public Boolean IsGridSnappingEnabled
        {
            get { return WorldEditorTools.IsGridSnappingEnabled; }
            set { WorldEditorTools.IsGridSnappingEnabled = value; }
        }

        public Int32 GridSnapX
        {
            get { return WorldEditorTools.GridSnapX; }
            set { WorldEditorTools.GridSnapX = value; }
        }

        public Int32 GridSnapY
        {
            get { return WorldEditorTools.GridSnapY; }
            set { WorldEditorTools.GridSnapY = value; }
        }
        #endregion

        public Int32 CreationPointX { get; set; }
        public Int32 CreationPointY { get; set; }

        private WorldObjectViewModel _selectedWorldObject;
        public WorldObjectViewModel SelectedWorldObject
        {
            get { return _selectedWorldObject; }
            set
            {
                _selectedWorldObject = value;

                if (_selectedWorldObject == null)
                {
                    Context.ClearProperties();
                    this.SelectedObjectName = "No Object Selected";
                    _editSelectedObjectCommand.RaiseCanExecuteChanged();
                    _deleteSelectedObjectCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => SelectedWorldObject);
                    return;
                }

                IWorldObject worldObject = _selectedWorldObject.MoveViewToModel();

                var typeDefinition = _worldObjectModelToObjectTypeDefinitionMapping[worldObject.GetType()];

                Context.ShowProperties(
                    typeDefinition.PropertiesView,
                    worldObject);

                if (!String.IsNullOrWhiteSpace(_selectedWorldObject.Name))
                    SelectedObjectName = _selectedWorldObject.Name;
                else
                {
                    if (worldObject is ArchetypeBasedWorldObject)
                        SelectedObjectName = ((ArchetypeBasedWorldObject)worldObject).ArchetypeKey.Split('.').Last();
                    else
                        SelectedObjectName = worldObject.GetType().Name;
                }

                RaisePropertyChanged(() => SelectedWorldObject);

                _editSelectedObjectCommand.RaiseCanExecuteChanged();
                _deleteSelectedObjectCommand.RaiseCanExecuteChanged();
            }
        }

        private String _selectedObjectName;
        public String SelectedObjectName
        {
            get { return _selectedObjectName; }
            set
            {
                this._selectedObjectName = value;
                RaisePropertyChanged(() => SelectedObjectName);
            }
        }

        private DelegateCommand _editSelectedObjectCommand;
        public ICommand EditSelectedObjectCommand
        {
            get { return _editSelectedObjectCommand; }
        }

        private DelegateCommand _deleteSelectedObjectCommand;
        public ICommand DeleteSelectedObjectCommand
        {
            get { return _deleteSelectedObjectCommand; }
        }

        [ImportingConstructor]
        public WorldEditorWorldViewModel(CompositionContainer container, IPackageRepository packageRepository)
        {
            this._objectTypes = new ObservableCollection<WorldObjectTypeViewModel>();
            this._packageRepository = packageRepository;
            this._worldObjects = new ObservableCollection<WorldObjectViewModel>();
            this._worldObjectModelToObjectTypeDefinitionMapping = new Dictionary<Type, WorldObjectDefinition>();

            this.SelectedObjectName = "No Object Selected";

            this._editSelectedObjectCommand = new DelegateCommand(EditSelectedObjectExecute, EditSelectedObjectCanExecute);
            this._deleteSelectedObjectCommand = new DelegateCommand(DeleteSelectedObjectExecute, DeleteSelectedObjectCanExecute);

            var worldObjectTypeExports = container.GetExports<IWorldEditorTypeDefinition, IWorldEditorTypeMetaData>();

            foreach (var export in worldObjectTypeExports)
            {
                var definition = new WorldObjectTypeViewModel(this, export.Metadata, export.Value);
                _objectTypes.Add(definition);
                _worldObjectModelToObjectTypeDefinitionMapping[export.Value.WorldObjectType] = definition;
            }

            var archetypeBasedWorldObjectTypeExports = container.GetExports<IArchetypeBasedWorldEditorTypeDefinition, IArchetypeBasedWorldEditorTypeMetaData>();
            foreach (var export in archetypeBasedWorldObjectTypeExports)
            {
                var definition = new ExistingWorldObjectArchetypeViewModel(this, export.Metadata, export.Value);
                _worldObjectModelToObjectTypeDefinitionMapping[export.Value.WorldObjectType] = definition;
            }

            this._archetypes = new ObservableCollection<NewWorldObjectArchetypeViewModel>();
            foreach (var asset in _packageRepository.Assets.Where(x => x.Type == EditorAssetTypes.Archetype))
            {
                var data = _packageRepository.GetAssetData<ArchetypeResource>(asset.Id);

                if (!(data.Data is WorldObjectArchetypeData))
                    continue;

                IArchetypeBasedWorldEditorTypeDefinition typeDefinition = null;
                IArchetypeBasedWorldEditorTypeMetaData metaData = null;

                foreach (var export in archetypeBasedWorldObjectTypeExports)
                {
                    if (export.Value.ArchetypeDataType == data.Data.GetType())
                    {
                        typeDefinition = export.Value;
                        metaData = export.Metadata;
                        break;
                    }
                }

                var definition = new NewWorldObjectArchetypeViewModel(
                    this, asset.Key.Split('.').Last(),
                    (WorldObjectArchetypeData)data.Data, 
                    asset.Key,
                    metaData,
                    typeDefinition);

                this._archetypes.Add(definition);
            }
        }

        public void EditSelectedObjectExecute()
        {
            var model = _selectedWorldObject.MoveViewToModel();
            var typeDefinition = _worldObjectModelToObjectTypeDefinitionMapping[model.GetType()];

            Context.OpenChildEditor(typeDefinition.CustomEditorView, _selectedWorldObject);
        }

        public Boolean EditSelectedObjectCanExecute()
        {
            if (_selectedWorldObject == null)
                return false;

            var model = _selectedWorldObject.MoveViewToModel();
            var typeDefinition = _worldObjectModelToObjectTypeDefinitionMapping[model.GetType()];

            if (!String.IsNullOrWhiteSpace(typeDefinition.CustomEditorView))
                return true;
            else
                return false;
        }

        public void DeleteSelectedObjectExecute()
        {
            this.Context.RemoveWorldObjectViewModel(_selectedWorldObject);
            _worldObjects.Remove(_selectedWorldObject);
            this.SelectedWorldObject = null;
        }

        public Boolean DeleteSelectedObjectCanExecute()
        {
            return this._selectedWorldObject != null;
        }

        public void CreateWorldObject(WorldObjectDefinition worldObjectType)
        {
            this.SelectedWorldObject = null;
            var newWorldObject = worldObjectType.GetDefaultedObject();
            var viewModel = worldObjectType.CreateWorldObjectViewModel();
            viewModel.Context = Context;

            newWorldObject.Id = Guid.NewGuid();
            newWorldObject.X = CreationPointX;
            newWorldObject.Y = CreationPointY;

            viewModel.SetRepository(_packageRepository);
            viewModel.SetModel(newWorldObject);
            viewModel.X = CreationPointX;
            viewModel.Y = CreationPointY;

            _worldObjects.Add(viewModel);
            Context.AddWorldObjectViewModel(viewModel);
            this.SelectedWorldObject = viewModel;
        }

        public void Initialize()
        {
            foreach (var worldObject in Context.World.Objects)
            {
                var viewModel = _worldObjectModelToObjectTypeDefinitionMapping[worldObject.GetType()]
                                    .CreateWorldObjectViewModel();

                viewModel.Context = Context;
                Context.AddWorldObjectViewModel(viewModel);

                viewModel.SetRepository(_packageRepository);
                viewModel.SetModel(worldObject);

                _worldObjects.Add(viewModel);
            }
        }
    }

    public class MenuItemViewModel
    {
        public String Text { get; set; }
        public ICommand Command { get; set; }
    }

    public abstract class WorldObjectDefinition
    {
        public abstract IWorldObject GetDefaultedObject();
        public abstract WorldObjectViewModel CreateWorldObjectViewModel();
        public abstract String PropertiesView { get; }
        public abstract String CustomEditorView { get; }
    }

    public class WorldObjectTypeViewModel : WorldObjectDefinition
    {
        public String Name { get; private set; }
        public IWorldEditorTypeDefinition TypeDefinition { get; private set; }
        public IWorldEditorTypeMetaData MetaData { get; private set; }

        public ICommand Command { get; private set; }

        public WorldObjectTypeViewModel(WorldEditorWorldViewModel parent, IWorldEditorTypeMetaData metaData, IWorldEditorTypeDefinition typeDefinition)
        {
            this.Name = typeDefinition.Name;
            this.TypeDefinition = typeDefinition;
            this.MetaData = metaData;

            this.Command = new DelegateCommand(() =>
            {
                parent.CreateWorldObject(this);
            });
        }
        public override String PropertiesView { get { return this.MetaData.PropertiesView; } }
        public override String CustomEditorView { get { return this.MetaData.CustomEditorView; } }

        public override IWorldObject GetDefaultedObject()
        {
            return TypeDefinition.GetDefaultedObject();
        }

        public override WorldObjectViewModel CreateWorldObjectViewModel()
        {
            return TypeDefinition.CreateWorldObjectViewModel();
        }
    }

    public class ExistingWorldObjectArchetypeViewModel : WorldObjectDefinition
    {
        private readonly IArchetypeBasedWorldEditorTypeMetaData _metaData;
        private readonly IArchetypeBasedWorldEditorTypeDefinition _typeDefinition;

        public ExistingWorldObjectArchetypeViewModel(WorldEditorWorldViewModel parent,
                                             IArchetypeBasedWorldEditorTypeMetaData metaData,
                                             IArchetypeBasedWorldEditorTypeDefinition typeDefinition)
        {
            this._metaData = metaData;
            this._typeDefinition = typeDefinition;
        }

        public override String PropertiesView { get { return this._metaData.PropertiesView; } }
        public override String CustomEditorView { get { return this._metaData.CustomEditorView; } }

        public override IWorldObject GetDefaultedObject()
        {
            throw new NotImplementedException();
        }

        public override WorldObjectViewModel CreateWorldObjectViewModel()
        {
            return this._typeDefinition.CreateWorldObjectViewModel();
        }
    }

    public class NewWorldObjectArchetypeViewModel : WorldObjectDefinition
    {
        private readonly IArchetypeBasedWorldEditorTypeMetaData _metaData;
        private readonly IArchetypeBasedWorldEditorTypeDefinition _typeDefinition;
        private readonly WorldObjectArchetypeData _data;
        private readonly String _archetypeKey;

        public String Name { get; private set; }
        public ICommand Command { get; private set; }

        public NewWorldObjectArchetypeViewModel(WorldEditorWorldViewModel parent,
                                             String name,
                                             WorldObjectArchetypeData data,
                                             String archetypeKey,
                                             IArchetypeBasedWorldEditorTypeMetaData metaData,
                                             IArchetypeBasedWorldEditorTypeDefinition typeDefinition)
        {
            this.Name = name;
            this._data = data;
            this._metaData = metaData;
            this._typeDefinition = typeDefinition;
            this._archetypeKey = archetypeKey;

            this.Command = new DelegateCommand(() =>
            {
                parent.CreateWorldObject(this);
            });
        }

        public override String PropertiesView { get { return this._metaData.PropertiesView; } }
        public override String CustomEditorView { get { return this._metaData.CustomEditorView; } }

        public override IWorldObject GetDefaultedObject()
        {
            return this._typeDefinition.GetDefaultedObject(_archetypeKey, _data);
        }

        public override WorldObjectViewModel CreateWorldObjectViewModel()
        {
            return this._typeDefinition.CreateWorldObjectViewModel();
        }
    }
}
