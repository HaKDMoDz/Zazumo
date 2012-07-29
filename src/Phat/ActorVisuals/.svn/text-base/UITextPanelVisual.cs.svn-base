using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;
using Microsoft.Xna.Framework.Graphics;
using Phat;
using Microsoft.Xna.Framework;
using Phat.Visual;
using Phat.ActorResources;
using Phat.Actors;

namespace Phat.ActorVisuals
{
    public class UITextPanelVisual : UIVisual
    {
        private new readonly UITextPanelActor _actor;
        private List<Animation> _currentAnimations;

        Single x = 400;
        Single y = 220;
        Single width = 100;
        Single height = 100;
        
        public UITextPanelVisual(UITextPanelActor actor, SpriteBatch spriteBatch, IResourceDictionary resourceDictionary)
            : base(actor, spriteBatch, resourceDictionary)
        {
            this._actor = actor;
            this._currentAnimations = new List<Animation>();
        }

        public override void HandleEvent(Object @event)
        {
            base.HandleEvent(@event);

            if (@event.GetType() == typeof(AnimationStartedEvent))
            {
                this._currentAnimations.Add(((AnimationStartedEvent)@event).Animation);
            }       
        }

        public override void Draw(TimeSpan ellapsedTime, ViewPort viewPort)
        {
            foreach (var animation in this._currentAnimations.ToArray())
            {
                if (animation.IsComplete == true)
                {
                    this._currentAnimations.Remove(animation);
                }
                else
                {
                    if (animation.AnimatedProperty == "XOffset")
                        XOffset = (Single)animation.AnimatedValue;
                    else if (animation.AnimatedProperty == "YOffset")
                        YOffset = (Single)animation.AnimatedValue;
                    else if (animation.AnimatedProperty == "Width")
                        width = (Single)animation.AnimatedValue;
                    else if (animation.AnimatedProperty == "Height")
                        height = (Single)animation.AnimatedValue;
                }
            }

            var sprite = (SpriteResource)ResourceDictionary.GetResource("SolidSprite");
            var spriteFont = (SpriteFont)ResourceDictionary.GetResource("TextFont");

            _spriteBatch.Draw((Texture2D)ResourceDictionary.GetResource(sprite.TextureKey),
                    new Rectangle((Int32)x + (Int32)XOffset, (Int32)y + (Int32)YOffset, (Int32)width, (Int32)height),
                    new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height) /* texture source */,
                    new Color(0.19f, 0.19f, 0.19f, 0.9f));

            _spriteBatch.Draw((Texture2D)ResourceDictionary.GetResource(sprite.TextureKey),
                                new Rectangle((Int32)x + (Int32)XOffset, (Int32)y + (Int32)YOffset, 2, (Int32)height),
                                new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height) /* texture source */,
                                Color.Black);

            _spriteBatch.Draw((Texture2D)ResourceDictionary.GetResource(sprite.TextureKey),
                                new Rectangle((Int32)x + (Int32)XOffset, (Int32)y + (Int32)YOffset, (Int32)width, 2),
                                new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height) /* texture source */,
                                Color.Black);

            _spriteBatch.Draw((Texture2D)ResourceDictionary.GetResource(sprite.TextureKey),
                                new Rectangle((Int32)x + (Int32)XOffset, (Int32)y + (Int32)height + (Int32)YOffset, (Int32)width, 2),
                                new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height) /* texture source */,
                                Color.Black);

            _spriteBatch.Draw((Texture2D)ResourceDictionary.GetResource(sprite.TextureKey),
                                new Rectangle((Int32)x + (Int32)width + (Int32)XOffset, (Int32)y + (Int32)YOffset, 2, (Int32)height),
                                new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height) /* texture source */,
                                Color.Black);

            SpriteFont font = null; // (SpriteFont)ResourceDictionary.GetResource(_actor.FontFamily);

            if (width > 500)
                _spriteBatch.DrawString(font, ((UITextPanelActor)_actor).Text, new Vector2(x + 10 + XOffset, y + 10 + YOffset), Color.White);
        }

        public override VisualHitTestResult GetHitResult(Vector2 location, ViewPort viewPort)
        {

            if (location.X >= this.x
               && location.X <= (this.x + this.width)
               && location.Y >= this.y
               && location.Y <= (this.y + this.height)
               )
            {
                return new VisualHitTestResult(this._actor, new Vector2(location.X - x, location.Y - y), true);
            }
            else
            {
                return new VisualHitTestResult(this._actor, new Vector2(0f, 0f), false);
            }
        }
    }
}
