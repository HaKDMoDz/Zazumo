using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorVisuals;
using Microsoft.Xna.Framework.Graphics;
using Phat.ActorResources;
using Microsoft.Xna.Framework;
using Phat.Zazumo.Actors;

namespace Phat.Zazumo.Visuals
{
    public class AmmoMeterVisual : SpriteVisual
    {
        public AmmoMeterVisual(Actor actor, SpriteBatch spriteBatch, IResourceDictionary resourceDictionary)
            : base(actor, spriteBatch, resourceDictionary)
        {
            
        }

        public override void Draw(TimeSpan ellapsedTime, ViewPort viewPort)
        {
            var resource = (UIResource)Resource;

            var screenX = (Int32)(resource.X * viewPort.ResolutionX) + (Int32)Properties.XOffset;
            var screenY = (Int32)(resource.Y * viewPort.ResolutionY) + (Int32)Properties.YOffset;
            var screenHeight = (Int32)(Resource.Height * viewPort.ResolutionY) + (Int32)Properties.HeightOffset;
            var screenWidth = (Int32)(Resource.Width * viewPort.ResolutionX) + (Int32)Properties.WidthOffset;

            SpriteResource solid = (SpriteResource)ResourceDictionary.GetResource("Solid");
            var texture = (Texture2D)ResourceDictionary.GetResource(solid.TextureKey);

            this._spriteBatch.Draw(texture, new Rectangle(screenX - 10, screenY + 10, screenWidth - 10, screenHeight),
                                new Rectangle(solid.UCoordinate, solid.VCoordinate, solid.Width, solid.Height),
                                new Color(0f, 0f, 0f, 0.5f));

            var pixelReduction = (Int32)((1f - ((AmmoMeter)Actor).AmmoLevel) * resource.Height * viewPort.ResolutionY);

            SpriteResource ammoMeter = (SpriteResource)ResourceDictionary.GetResource(Resource.SpriteKey);

            this._spriteBatch.Draw(texture, new Rectangle(screenX, screenY + pixelReduction, screenWidth, screenHeight - pixelReduction),
                    new Rectangle(ammoMeter.UCoordinate, ammoMeter.VCoordinate, ammoMeter.Width, ammoMeter.Height),
                    new Color(1f, 1f, 1f, 0.5f));

        }
    }
}
