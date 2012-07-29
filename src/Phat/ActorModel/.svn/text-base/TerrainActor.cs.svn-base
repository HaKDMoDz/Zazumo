using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Microsoft.Xna.Framework;
using Phat.ActorResources;

namespace Phat.ActorModel
{
    public class TerrainActor : Actor<TerrainActor>, ITouchable
    {
        private String[] _tiles;
        private Single _tileWidth;
        private Single _tileHeight;
        private Int32 _rows;
        private Int32 _columns;

        protected override void  OnInitializing(object initializationData)
        {
            base.OnInitializing(initializationData);

            var terrainResource = initializationData as TerrainWorldObject;
            
            SetLocation(terrainResource.X, terrainResource.Y, 0);

            this._tiles = terrainResource.TileDefinitionKeys;
            this._tileWidth = terrainResource.TileWidth;
            this._tileHeight = terrainResource.TileHeight;
            this._rows = terrainResource.Rows;
            this._columns = terrainResource.Columns;
        }
        
        public void Touch(Actor other)
        {
            this.OnTouch(other);
        }

        private void OnTouch(Actor other)
        {
            var mortal = other as IMortal;

            if (mortal == null)
                return;

            mortal.Kill();
        }

        public Boolean AllowPlacementAt(Vector2 position)
        {
            var x = (Int32)Math.Floor(position.X * Settings.MetersToPixels / _tileWidth);
            var y = (Int32)Math.Floor(position.Y * Settings.MetersToPixels / _tileHeight);

            var tileKey =  _tiles[(y * _columns) + x];

            if (String.IsNullOrEmpty(((TerrainTileDefinitionResource)Resources.GetResource(tileKey)).CollisionHullKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
