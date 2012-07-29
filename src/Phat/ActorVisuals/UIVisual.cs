using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Microsoft.Xna.Framework.Graphics;
using Phat.Visual;
using Microsoft.Xna.Framework;
using Phat.Actors;
using Phat.ActorResources;

namespace Phat.ActorVisuals
{
    public class UIVisual: VisualBase
    {
        protected readonly Actor _actor;
        protected readonly SpriteBatch _spriteBatch;
        private readonly List<Animation> _currentAnimations;

        protected Single XOffset { get; set; }
        protected Single YOffset { get; set; }
        protected Single WidthOffset { get; set; }
        protected Single HeightOffset { get; set; }
        protected Single opacity { get; set; }

        private UIResource _resource;
        
        public UIVisual(Actor actor, SpriteBatch spriteBatch, IResourceDictionary resourceDictionary)
            : base(resourceDictionary)
        {
            this._actor = actor;
            this._spriteBatch = spriteBatch;
            this._currentAnimations = new List<Animation>();
        }

        public override void HandleEvent(Object @event)
        {
            if (@event.GetType() == typeof(AnimationStartedEvent))
            {
                this._currentAnimations.Add(((AnimationStartedEvent)@event).Animation);
            }       
        }

        public override VisualHitTestResult GetHitResult(Vector2 location, ViewPort viewPort)
        {
            var screenLocation = new Vector2(_actor.Location.X * viewPort.ResolutionX, _actor.Location.Y * viewPort.ResolutionY);

            if (location.X >= screenLocation.X
                && location.X <= (screenLocation.X + _resource.Width * viewPort.ResolutionX)
                && location.Y >= screenLocation.Y
                && location.Y <= (screenLocation.Y + _resource.Height * viewPort.ResolutionY)
                )
            {
                return new VisualHitTestResult(this._actor, new Vector2(location.X - screenLocation.X, location.Y - screenLocation.Y), true);
            }
            else
            {
                return new VisualHitTestResult(this._actor, new Vector2(0f, 0f), false);
            }
        }

        public override void Draw(TimeSpan ellapsedTime, ViewPort viewPort)
        {
            if (_actor.IsDestoryed)
                return;

            if (this.ResourceDictionary == null)
                return;

            var sprite = (SpriteResource)ResourceDictionary.GetResource(_resource.SpriteKey);
            var texture = (Texture2D)ResourceDictionary.GetResource(sprite.TextureKey);
            var location = ((ILocatable)_actor).Location;

            RunAnimations();

            Color c;

            if (opacity != 1f)
                c = new Color(opacity, opacity, opacity, 1f);
            else
                c = Color.White;

            _spriteBatch.Draw(texture,
                                    new Rectangle((Int32)(location.X * viewPort.ResolutionX) + (Int32)XOffset,
                                                  (Int32)(location.Y * viewPort.ResolutionY) + (Int32)YOffset,
                                                  (Int32)(_resource.Width * viewPort.ResolutionX) + (Int32)WidthOffset,
                                                  (Int32)(_resource.Height * viewPort.ResolutionY) + (Int32)HeightOffset),
                                    new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height) /* texture source */,
                                    c
                                    );
        }

        protected void RunAnimations()
        {
            XOffset = 0f;
            YOffset = 0f;
            WidthOffset = 0f;
            HeightOffset = 0f;
            opacity = _actor.Opacity;

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
        }


        public override void Initialize(Object initializationData)
        {
            _resource = (UIResource)initializationData;
        }
    }
}
