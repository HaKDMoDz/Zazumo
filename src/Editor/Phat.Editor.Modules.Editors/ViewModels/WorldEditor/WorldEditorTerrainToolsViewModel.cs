using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;
using System.Collections.ObjectModel;
using Phat.Editor.Interfaces.DatabaseModel;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel;
using Phat.ActorResources;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    public enum TerrainEditorTools
    {
        Paint,
        Fill,
        EyeDropper
    }

    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class WorldEditorTerrainToolsViewModel : NotificationObject
    {
        private readonly IPackageRepository _repository;
        private readonly SpriteLoader _spriteLoader;
        
        private readonly ObservableCollection<TileViewModel> _tiles;
        public IEnumerable<TileViewModel> Tiles
        {
            get { return _tiles; }
        }

        private Boolean _isGridVisible;
        public Boolean IsGridVisible
        {
            get { return _isGridVisible; }
            set
            {
                if (value == _isGridVisible)
                    return;

                this._isGridVisible = value;
                RaisePropertyChanged(() => IsGridVisible);
            }
        }

        private TileViewModel _selectedTile;
        public TileViewModel SelectedTile
        {
            get { return _selectedTile; }
            set
            {
                if (value == _selectedTile)
                    return;

                this._selectedTile = value;
                RaisePropertyChanged(() => SelectedTile);
            }
        }

        public String SelectedTileKey
        {
            get {
                if (SelectedTile == null)
                    return String.Empty;

                return SelectedTile.Key;
             }

            set
            {
                if (value == null)
                    return;

                this.SelectedTile = _tiles.Where(x => x.Key == value).FirstOrDefault();
            }
        }

        private TerrainEditorTools _selectedTool;
        public TerrainEditorTools SelectedTool 
        {
            get { return _selectedTool; }
            set
            {
                this._selectedTool = value;
                
                this.IsEyeDropperSelected = false;
                this.IsFillSelected = false;
                this.IsPaintSelected = false;

                switch (_selectedTool)
                {
                    case TerrainEditorTools.Paint:
                        this.IsPaintSelected = true;
                        break;
                    case TerrainEditorTools.Fill:
                        this.IsFillSelected = true;
                        break;
                    case TerrainEditorTools.EyeDropper:
                        this.IsEyeDropperSelected = true;
                        break;                 
                }

                RaisePropertyChanged(() => IsPaintSelected);
                RaisePropertyChanged(() => IsFillSelected);
                RaisePropertyChanged(() => IsEyeDropperSelected);
            }
        }

        public Boolean IsPaintSelected { get; private set; }
        public Boolean IsFillSelected { get; private set; }
        public Boolean IsEyeDropperSelected { get; private set; }
        
        public ICommand SetToolCommand { get; private set; }

        [ImportingConstructor]
        public WorldEditorTerrainToolsViewModel(IPackageRepository repository, SpriteLoader spriteLoader)
        {
            this._repository = repository;
            this._spriteLoader = spriteLoader;
            this._isGridVisible = false;

            this._tiles = new ObservableCollection<TileViewModel>();

            foreach (var asset in repository.Assets.Where(x => x.Type == EditorAssetTypes.TerrainTileDefinition).OrderBy(x => x.Key))
            {
                var tileData = _repository.GetAssetData<TerrainTileDefinitionResource>(asset.Id);
                _tiles.Add(new TileViewModel() { Key = tileData.Key, Sprite = _spriteLoader.LoadSprite(tileData.SpriteKey) });
            }

            this.SelectedTile = _tiles.FirstOrDefault();
            this.SelectedTool = TerrainEditorTools.Paint;

            SetToolCommand = new DelegateCommand<String>(x =>
                {
                    if (x == "Fill")
                        SelectedTool = TerrainEditorTools.Fill;
                    else if (x == "Paint")
                        SelectedTool = TerrainEditorTools.Paint;
                    else if (x == "EyeDropper")
                        SelectedTool = TerrainEditorTools.EyeDropper;
                });
        }

        [Bindable(true)]
        public class TileViewModel
        {
            public String Key { get; set; }
            public SpriteViewModel Sprite { get; set; }
        }
    }

    
}
