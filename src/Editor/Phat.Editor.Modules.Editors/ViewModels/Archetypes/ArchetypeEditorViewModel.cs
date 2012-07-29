using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;
using Phat.ActorResources;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Hosting;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class ArchetypeEditorViewModel : EditorViewModel<ArchetypeResource>
    {
        private readonly CompositionContainer _container;
        private readonly ObservableCollection<String> _archetypeDataTypes;
        private readonly IEnumerable<Lazy<ArchetypeDataViewModel, IArchetypeDataEditorMetaData>> _exportedEditors;

        private ArchetypeDataViewModel _archetypeData;
        public ArchetypeDataViewModel ArchetypeData 
        {
            get { return _archetypeData; }
            set
            {
                _archetypeData = value;
                RaisePropertyChanged(() => ArchetypeData);
            }
        }

        public ArchetypeEditorViewModel()
        {
            this._archetypeDataTypes = new ObservableCollection<String>();
        }

        public IEnumerable<String> ArchetypeDataTypes
        {
            get { return _archetypeDataTypes; }
        }

        private String _selectedArchetypeDataType;

        public String SelectedArchetypeDataType
        {
            get { return _selectedArchetypeDataType; }
            set
            {
                _selectedArchetypeDataType = value;

                var editor = _exportedEditors.Where(x => x.Metadata.ArchetypeDataType.Name == _selectedArchetypeDataType).Single();
                
                if (ArchetypeData != null)
                {
                    if (ArchetypeData.GetType() == editor.Value.GetType())
                        return;
                }

                ArchetypeData = editor.Value;
                MarkUnsaved();
                editor.Value.Parent = this;
                RaisePropertyChanged(() => SelectedArchetypeDataType);
            }
        }
                
        [ImportingConstructor]
        public ArchetypeEditorViewModel(CompositionContainer container)
        {
            this._container = container;
            this._archetypeDataTypes = new ObservableCollection<String>();

            _exportedEditors = this._container.GetExports<ArchetypeDataViewModel, IArchetypeDataEditorMetaData>();

            foreach (var export in _exportedEditors)
            {
                _archetypeDataTypes.Add(export.Metadata.ArchetypeDataType.Name);
            }
        }

        protected override ArchetypeResource MoveViewToModel()
        {
            var model = new ArchetypeResource();
            model.Key = this.Asset.Key;

            if (ArchetypeData != null)
                model.Data = ArchetypeData.MoveViewToModel();

            return model;
        }

        protected override void MoveModelToView(ArchetypeResource model)
        {
            if (model.Data == null)
                return;

            var editor = _exportedEditors.Where(x => x.Metadata.ArchetypeDataType == model.Data.GetType()).Single();
            this.SelectedArchetypeDataType = editor.Metadata.ArchetypeDataType.Name;

            this.ArchetypeData.MoveModelToView(model.Data);
        }
    }
}
