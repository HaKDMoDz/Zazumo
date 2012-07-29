using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.ComponentModel;
using Phat.Editor.Interfaces;
using Phat.ActorResources;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class WorldEditorTerrainViewModel : NotificationObject, IWorldChildEditor
    {
        private readonly IPackageRepository _packageRepository;

        public IWorldEditorContext Context { get; set; }

        private Object _tools;
        public Object Tools
        {
            get { return _tools; }
            set 
            {
                _tools = value;
                RaisePropertyChanged(() => Tools);
                RaisePropertyChanged(() => TerrainTools);

                TerrainTools.PropertyChanged += new PropertyChangedEventHandler(TerrainTools_PropertyChanged);
            }
        }

        void TerrainTools_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsGridVisible = TerrainTools.IsGridVisible;
            RaisePropertyChanged(() => IsGridVisible);
        }

        public Boolean IsGridVisible { get; private set; }

        private WorldEditorTerrainToolsViewModel TerrainTools
        {
            get { return (WorldEditorTerrainToolsViewModel)Tools; }
        }

        public WorldObjectViewModel ChildViewModel { get; set; }
        public TerrainWorldObjectViewModel Terrain
        {
            get { return (TerrainWorldObjectViewModel)ChildViewModel; }
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ToolExecutionCommand { get; private set; }
        public ICommand SetToolCommand { get; private set; }

        [ImportingConstructor]
        public WorldEditorTerrainViewModel(IPackageRepository packageRepository)
        {
            this._packageRepository = packageRepository;

            CloseCommand = new DelegateCommand(() => this.Context.CloseChildEditor());
            ToolExecutionCommand = new DelegateCommand<TerrainPosition>(UseToolExecute);
            
            SetToolCommand = new DelegateCommand<String>(x =>
                {
                    if (x == "Fill")
                        TerrainTools.SelectedTool = TerrainEditorTools.Fill;
                    else if (x == "Paint")
                        TerrainTools.SelectedTool = TerrainEditorTools.Paint;
                    else if (x == "EyeDropper")
                        TerrainTools.SelectedTool = TerrainEditorTools.EyeDropper;
                });
        }

        public void Initialize()
        {
            Context.SetEditorTools(ViewNames.WorldEditorTools_Terrain);
        }

        public void UseToolExecute(TerrainPosition position)
        {
            if (String.IsNullOrWhiteSpace(TerrainTools.SelectedTileKey))
                return;

            var tileAsset = this._packageRepository.Assets.Where(x => x.Key == TerrainTools.SelectedTileKey).Single();
            var tileData = this._packageRepository.GetAssetData<TerrainTileDefinitionResource>(tileAsset.Id);

            if (TerrainTools.SelectedTool == TerrainEditorTools.Paint)
                Terrain.SetTile(position, tileData);
            else if (TerrainTools.SelectedTool == TerrainEditorTools.Fill)
                Paint(position, tileData, new List<Int32>());
            else if (TerrainTools.SelectedTool == TerrainEditorTools.EyeDropper)
            {
                TerrainTools.SelectedTileKey = Terrain.GetTileKey(position);
                TerrainTools.SelectedTool = TerrainEditorTools.Paint;
            }
        }

        private void Paint(TerrainPosition position, TerrainTileDefinitionResource tileData, List<Int32>visitedPositions)
        {
            var initialTileKey = Terrain.GetTileKey(position);

            Terrain.SetTile(position, tileData);
            visitedPositions.Add(position.Row * Terrain.Columns + position.Column);
            
            // North
            if (position.Row > 0)
            {
                if (!visitedPositions.Contains((position.Row - 1) * Terrain.Columns + position.Column))
                {
                    var newPosition = new TerrainPosition() { Row = position.Row - 1, Column = position.Column };
                    if (Terrain.GetTileKey(newPosition) == initialTileKey)
                    {
                        Paint(newPosition, tileData, visitedPositions);
                    }
                }
            }

            // South
            if (position.Row < (Terrain.Rows - 1))
            {
                if (!visitedPositions.Contains((position.Row + 1) * Terrain.Columns + position.Column))
                {
                    var newPosition = new TerrainPosition() { Row = position.Row + 1, Column = position.Column };
                    if (Terrain.GetTileKey(newPosition) == initialTileKey)
                    {
                        Paint(newPosition, tileData, visitedPositions);
                    }
                }
            }

            // West
            if (position.Column < (Terrain.Columns - 1))
            {
                if (!visitedPositions.Contains(position.Row * Terrain.Columns + position.Column + 1))
                {
                    var newPosition = new TerrainPosition() { Row = position.Row, Column = position.Column + 1 };
                    if (Terrain.GetTileKey(newPosition) == initialTileKey)
                    {
                        Paint(newPosition, tileData, visitedPositions);
                    }
                }
            }

            // East
            if (position.Column > 0)
            {
                if (!visitedPositions.Contains(position.Row * Terrain.Columns + position.Column - 1))
                {
                    var newPosition = new TerrainPosition() { Row = position.Row, Column = position.Column - 1 };
                    if (Terrain.GetTileKey(newPosition) == initialTileKey)
                    {
                        Paint(newPosition, tileData, visitedPositions);
                    }
                }
            }
        }
    }
}
