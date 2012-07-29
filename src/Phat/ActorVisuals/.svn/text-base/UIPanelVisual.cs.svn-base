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
    public class UIPanelVisual: UIVisual
    {
        private UIResource _resource;
        private readonly List<Animation> _currentAnimations;

        public UIPanelVisual(UIPanelActor actor, SpriteBatch spriteBatch, IResourceDictionary resourceDictionary)
            : base(actor, spriteBatch, resourceDictionary)
        {
            this._currentAnimations = new List<Animation>();
        }

        public override void HandleEvent(object @event)
        {
            if (@event.GetType() == typeof(AnimationStartedEvent))
            {
                this._currentAnimations.Add(((AnimationStartedEvent)@event).Animation);
            }
        }

        public override void Draw(TimeSpan ellapsedTime, ViewPort viewPort)
        {
            Single XOffset = 0f;
            Single YOffset = 0f;
            Single WidthOffset = 0f;
            Single HeightOffset = 0f;
            Single opacity = _actor.Opacity;

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
                    else if (animation.AnimatedProperty == "WidthOffset")
                        WidthOffset = (Single)animation.AnimatedValue;
                    else if (animation.AnimatedProperty == "HeightOffset")
                        HeightOffset = (Single)animation.AnimatedValue;
                    else if (animation.AnimatedProperty == "Opacity")
                        opacity = (Single)animation.AnimatedValue;
                }
            }

            var sprite = (SpriteResource)ResourceDictionary.GetResource("SolidSprite");
            
            _spriteBatch.Draw((Texture2D)ResourceDictionary.GetResource(sprite.TextureKey),
                    new Rectangle((Int32)_actor.Location.X + (Int32)XOffset, (Int32)_actor.Location.Y + (Int32)YOffset, (Int32)this._resource.Width + (Int32)WidthOffset, (Int32)this._resource.Height + (Int32)HeightOffset),
                    new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height) /* texture source */,
                    new Color(0.25f, 0.5f, 0.7f, 0.9f));

            _spriteBatch.Draw((Texture2D)ResourceDictionary.GetResource(sprite.TextureKey),
                                new Rectangle((Int32)_actor.Location.X + (Int32)XOffset, (Int32)_actor.Location.Y + (Int32)YOffset, 2, (Int32)this._resource.Height),
                                new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height) /* texture source */,
                                Color.White);

            _spriteBatch.Draw((Texture2D)ResourceDictionary.GetResource(sprite.TextureKey),
                                new Rectangle((Int32)_actor.Location.X + (Int32)XOffset, (Int32)_actor.Location.Y + (Int32)YOffset, (Int32)this._resource.Width, 2),
                                new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height) /* texture source */,
                                Color.White);

            _spriteBatch.Draw((Texture2D)ResourceDictionary.GetResource(sprite.TextureKey),
                                new Rectangle((Int32)_actor.Location.X + (Int32)XOffset, (Int32)_actor.Location.Y + (Int32)this._resource.Height + (Int32)YOffset + (Int32)HeightOffset, (Int32)this._resource.Width, 2),
                                new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height) /* texture source */,
                                Color.White);

            _spriteBatch.Draw((Texture2D)ResourceDictionary.GetResource(sprite.TextureKey),
                                new Rectangle((Int32)_actor.Location.X + (Int32)this._resource.Width + (Int32)WidthOffset + (Int32)XOffset, (Int32)_actor.Location.Y + (Int32)YOffset, 2, (Int32)this._resource.Height),
                                new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height) /* texture source */,
                                Color.White);
        }

        public override VisualHitTestResult GetHitResult(Vector2 location, ViewPort viewPort)
        {

            if (location.X >= (Int32)_actor.Location.X
               && location.X <= ((Int32)_actor.Location.X + this._resource.Width)
               && location.Y >= (Int32)_actor.Location.Y
               && location.Y <= ((Int32)_actor.Location.Y + this._resource.Height)
               )
            {
                return new VisualHitTestResult(this._actor, new Vector2(location.X - (Int32)_actor.Location.X, location.Y - (Int32)_actor.Location.Y), true);
            }
            else
            {
                return new VisualHitTestResult(this._actor, new Vector2(0f, 0f), false);
            }
        }

        public override void Initialize(object initializationData)
        {
            base.Initialize(initializationData);

            this._resource = (UIResource)initializationData;
        }
    }
}
