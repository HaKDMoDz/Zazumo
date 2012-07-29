using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using System.Collections.ObjectModel;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    public class TerrainWorldObjectViewModel : WorldObjectViewModel
    {
        private readonly SpriteLoader _spriteLoader;
        private readonly IPackageRepository _packageRepository;
        private readonly ObservableCollection<Tile> _tiles;

        public Single TotalWidth { get { return _columns * _tileWidth * Phat.Settings.MetersToPixels; } }
        public Single TotalHeight { get { return _rows * _tileHeight * Phat.Settings.MetersToPixels; } }

        public IEnumerable<Tile> Tiles
        {
            get { return _tiles; }
        }

        private Int32 _columns;
        public Int32 Columns
        {
            get { return _columns; }
            set
            {
                if (value == _columns)
                    return;

                this._columns = value;
                RaisePropertyChanged(() => Columns);
                RaisePropertyChanged(() => TotalWidth);
                NotifyWorldObjectValueChanged("Columns", value);
            }
        }

        private Int32 _rows;
        public Int32 Rows
        {
            get { return _rows; }
            set
            {
                if (value == _rows)
                    return;

                this._rows = value;
                RaisePropertyChanged(() => Rows);
                RaisePropertyChanged(() => TotalHeight);
                NotifyWorldObjectValueChanged("Rows", value);
            }
        }

        private Int32 _tileWidth;
        public Int32 TileWidth
        {
            get { return _tileWidth; }
            set
            {
                if (value == _tileWidth)
                    return;

                this._tileWidth = value;
                RaisePropertyChanged(() => TileWidth);
                RaisePropertyChanged(() => TotalWidth);
                NotifyWorldObjectValueChanged("TileWidth", value);
            }
        }

        private Int32 _tileHeight;
        public Int32 TileHeight
        {
            get { return _tileHeight; }
            set
            {
                if (value == _tileHeight)
                    return;

                this._tileHeight = value;
                RaisePropertyChanged(() => TileHeight);
                RaisePropertyChanged(() => TotalHeight);
                NotifyWorldObjectValueChanged("TileHeight", value);
            }
        }

        public override IWorldObject MoveViewToModel()
        {
            var model = new TerrainWorldObject();
            model.Id = this.Id;
            model.Rows = this.Rows;
            model.Behavior = "Terrain";
            model.Columns = this.Columns;
            model.TileWidth = this.TileWidth;
            model.TileHeight = this.TileHeight;
            model.X = this.X;
            model.Y = this.Y;

            var tileKeys = new List<String>();

            for (Int32 row = 0; row < this.Rows; row++)
            {
                for (Int32 column = 0; column < this.Columns; column++)
                {
                    var tile = _tiles.Where(x => x.Row == row && x.Column == column).FirstOrDefault();
                    if (tile == null)
                        tileKeys.Add(String.Empty);
                    else
                        tileKeys.Add(tile.Key);
                }
            }

            model.TileDefinitionKeys = tileKeys.ToArray();

            return model;
        }

        public override void SetModel(IWorldObject model)
        {
            base.SetModel(model);

            var m = model as TerrainWorldObject;
            this.Columns = m.Columns;
            this.Rows = m.Rows;
            this.TileHeight = m.TileHeight;
            this.TileWidth = m.TileWidth;

            Int32 counter = 0;

            if (m.TileDefinitionKeys == null)
                return;


            var tileData = new List<TerrainTileDefinitionResource>();

            foreach (var asset in this._packageRepository.Assets.Where(x => x.Type == EditorAssetTypes.TerrainTileDefinition))
            {
                tileData.Add(this._packageRepository.GetAssetData<TerrainTileDefinitionResource>(asset.Id));
            }            

            foreach (var tile in m.TileDefinitionKeys)
            {
                var row = counter / Columns;
                var column = counter % Columns;

                if (!String.IsNullOrWhiteSpace(tile))
                {
                    var data = tileData.Where(x => x.Key == tile).Single();
                    SetTile(new TerrainPosition() { Row = row, Column= column}, data);
                }

                counter++;
            }
        }

        public TerrainWorldObjectViewModel(IPackageRepository packageRepository,  SpriteLoader spriteLoader)
        {
            this._packageRepository = packageRepository;
            this._spriteLoader = spriteLoader;
            this._tiles = new ObservableCollection<Tile>();
        }

        public void SetTile(TerrainPosition position, TerrainTileDefinitionResource tileResource)
        {
            var tilesAtPosition = _tiles.Where(x => x.Column == position.Column && x.Row == position.Row);

            if (tilesAtPosition.Count() > 0)
            {
                foreach (var tile in tilesAtPosition.ToArray())
                    _tiles.Remove(tile);
            }

            var newTile = new Tile() { Key = tileResource.Key,  Row = position.Row, Column = position.Column, Sprite = _spriteLoader.LoadSprite(tileResource.SpriteKey) };
            _tiles.Add(newTile);
        }

        public String GetTileKey(TerrainPosition position)
        {
            var tileAtPosition = _tiles.Where(x => x.Column == position.Column && x.Row == position.Row).FirstOrDefault();

            if (tileAtPosition == null)
                return String.Empty;
            else
                return tileAtPosition.Key;
        }

        public class Tile
        {
            public Int32 Row { get; set; }
            public Int32 Column { get; set; }
            public String Key { get; set; }
            public SpriteViewModel Sprite { get; set; }
        }        
    }
}
