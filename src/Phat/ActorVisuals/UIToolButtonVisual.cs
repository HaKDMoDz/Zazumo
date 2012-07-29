using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Microsoft.Xna.Framework.Graphics;
using Phat.ActorModel;
using Microsoft.Xna.Framework;

namespace Phat.ActorVisuals
{
    public class UIToolButtonVisual : UIVisual
    {
        public UIToolButtonVisual(UIToolButtonActor actor, SpriteBatch spriteBatch, IResourceDictionary resourceDictionary)
            : base(actor, spriteBatch, resourceDictionary)
        {
        }

        public override void Draw(TimeSpan ellapsedTime, ViewPort viewPort)
        {
            base.Draw(ellapsedTime, viewPort);

            var font = (SpriteFont)ResourceDictionary.GetResource("TitleFont");

            _spriteBatch.DrawString(font, "X" + ((UIToolButtonActor)_actor).AvailableCount.ToString(), new Vector2(_actor.Location.X + 60f + XOffset, _actor.Location.Y + 65f + YOffset), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
        }
    }
}
