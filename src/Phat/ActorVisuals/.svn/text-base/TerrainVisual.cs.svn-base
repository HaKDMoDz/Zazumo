using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Phat.Actors;
using Phat.ActorModel;
using Phat.Visual;
using Phat.ActorResources;

namespace Phat.ActorVisuals
{
    public class TerrainVisual : VisualBase
    {
        private readonly TerrainActor _actor;
        private readonly SpriteBatch _terrainSpriteBatch;
        
        private TerrainWorldObject _terrainResource;
        
        public TerrainVisual(TerrainActor actor, SpriteBatch terrainSpriteBatch, IResourceDictionary resourceDictionary)
            : base(resourceDictionary)
        {
            this._actor = actor;
            this._terrainSpriteBatch = terrainSpriteBatch;
        }

        public override void HandleEvent(Object @event)
        {
            
        }

        public override VisualHitTestResult GetHitResult(Vector2 location, ViewPort viewPort)
        {
            var horizontalOffset = ((Int32)(viewPort.Left % _terrainResource.TileWidth));
            var verticalOffset = ((Int32)(viewPort.Top % _terrainResource.TileHeight));

            var relativeLocation = new Vector2((location.X / viewPort.ZoomFactor) - _actor.Location.X + viewPort.Left + horizontalOffset,
                                                (location.Y / viewPort.ZoomFactor) - _actor.Location.Y + viewPort.Top + verticalOffset);
                        
            if (relativeLocation.X >=  _actor.Location.X
                && relativeLocation.X <= (_actor.Location.X + _terrainResource.Columns  * _terrainResource.TileWidth)
                && relativeLocation.Y >= _actor.Location.Y
                && relativeLocation.Y <= (_actor.Location.Y + _terrainResource.Rows * _terrainResource.TileHeight)
                )
            {
                return new VisualHitTestResult(this._actor, new Vector2(relativeLocation.X, relativeLocation.Y), true);
            }
            else
            {
                return new VisualHitTestResult(this._actor, new Vector2(0f, 0f), false);
            }
        }

        public override void Draw(TimeSpan ellapsedTime, ViewPort viewPort)
        {
            if (this.ResourceDictionary == null)
                return;

            _terrainSpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone);
            
            var location = ((ILocatable)_actor).Location;

            var horizontalOffset = ((Int32)(viewPort.Left % _terrainResource.TileWidth) * viewPort.ZoomFactor * Phat.Settings.MetersToPixels);
            var verticalOffset = ((Int32)(viewPort.Top % _terrainResource.TileHeight) * viewPort.ZoomFactor * Phat.Settings.MetersToPixels);
            
            var horizontalTileResolution = Math.Min(_terrainResource.Columns,  Math.Ceiling(viewPort.ResolutionX / (_terrainResource.TileWidth * viewPort.ZoomFactor * Phat.Settings.MetersToPixels))) + 1;
            var verticalTileResolution = Math.Min(_terrainResource.Rows, Math.Ceiling(viewPort.ResolutionY / (_terrainResource.TileHeight * viewPort.ZoomFactor * Phat.Settings.MetersToPixels))) + 1;
            var initialRow = viewPort.Top / (_terrainResource.TileHeight);
            var initialColumn = viewPort.Left / (_terrainResource.TileWidth);

            for (Int32 row = 0; row < verticalTileResolution && (row + initialRow) < _terrainResource.Rows; row++)
            {
                var tileYScreenLocation = (Int32)(Math.Floor(row * _terrainResource.TileHeight * viewPort.ZoomFactor * Phat.Settings.MetersToPixels)) - verticalOffset;

                for (Int32 column = 0; column < (horizontalTileResolution + 1) && (column + initialColumn) < _terrainResource.Columns; column++)
                {
                    var tileXScreenLocation = (Int32)(Math.Floor(column * _terrainResource.TileWidth * viewPort.ZoomFactor * Phat.Settings.MetersToPixels)) - horizontalOffset;

                    if (String.IsNullOrEmpty(_terrainResource.TileDefinitionKeys[((row + initialRow) * _terrainResource.Columns) + column + initialColumn]))
                        continue;

                    var tile = (TerrainTileDefinitionResource)ResourceDictionary.GetResource(_terrainResource.TileDefinitionKeys[((row + initialRow) * _terrainResource.Columns) + column + initialColumn]);
                    var sprite = (SpriteResource)ResourceDictionary.GetResource(tile.SpriteKey);
                    var texture = (Texture2D)ResourceDictionary.GetResource(sprite.TextureKey);

                    var spriteEffect = SpriteEffects.None;

                    if (sprite.HorizontalFlip)
                        spriteEffect = SpriteEffects.FlipHorizontally;

                    if (sprite.VerticalFlip)
                        spriteEffect |= SpriteEffects.FlipVertically;

                    _terrainSpriteBatch.Draw(texture,
                                    new Rectangle((Int32)tileXScreenLocation, 
                                                  (Int32)tileYScreenLocation, 
                                                  (Int32)(Math.Ceiling(_terrainResource.TileWidth * viewPort.ZoomFactor * Phat.Settings.MetersToPixels) + (viewPort.ZoomFactor == 1.0 ? 0 : 1)),
                                                  (Int32)(Math.Ceiling(_terrainResource.TileHeight * viewPort.ZoomFactor * Phat.Settings.MetersToPixels)) + (viewPort.ZoomFactor == 1.0 ? 0 : 1)) /* destination */,
                                    new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height) /* texture source */,
                                    Color.White /* no tint */,
                                    0f, Vector2.Zero, spriteEffect, tile.Layer
                                    );
                }
            }

            

            _terrainSpriteBatch.End();
        }

        public override void Initialize(Object initializationData)
        {
            _terrainResource = initializationData as TerrainWorldObject;
        }
    }
}
