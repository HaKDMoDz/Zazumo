using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;
using Microsoft.Xna.Framework.Graphics;
using Phat;
using Microsoft.Xna.Framework;
using Phat.Visual;
using Phat.Actors;

namespace Phat.ActorVisuals
{
    public class UITextBlockVisual : IActorVisual
    {
        private readonly UITextBlockActor _actor;
        private readonly SpriteBatch _spriteBatch;
        private List<Animation> _currentAnimations;
        private readonly IResourceDictionary _resourceDictionary;

        public UITextBlockVisual(UITextBlockActor actor, SpriteBatch spriteBatch, IResourceDictionary resourceDictionary)
        {
            _actor = actor;
            _spriteBatch = spriteBatch;
            _currentAnimations = new List<Animation>();
            this._resourceDictionary = resourceDictionary;
        }

        public void Draw(TimeSpan ellapsedTime, ViewPort viewPort)
        {            
            Single XOffset = 0f;
            Single YOffset = 0f;
            Single Scale = 1f;
            Single Opacity = 1f;

            var font = (SpriteFont)this._resourceDictionary.GetResource(_actor.FontKey);

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
                    else if (animation.AnimatedProperty == "Opacity")
                        Opacity = (Single)animation.AnimatedValue;

                    else if (animation.AnimatedProperty == "Scale")
                        Scale = (Single)animation.AnimatedValue;
                }
            }

            Vector2 stringLocation;

            switch (_actor.TextAlignment)
            {
                case UITextBlockActor.Alignment.Left:
                    stringLocation = new Vector2(_actor.Location.X + XOffset, _actor.Location.Y + YOffset);
                    break;
                case UITextBlockActor.Alignment.Center:
                    stringLocation = new Vector2((viewPort.ResolutionX /2f) - ((font.MeasureString(_actor.Text).X) * _actor.Scale / 2f)+ _actor.Location.X + XOffset, _actor.Location.Y +  YOffset);

                    break;
                case UITextBlockActor.Alignment.Right:
                    stringLocation = new Vector2(viewPort.ResolutionX + XOffset - _actor.Location.X - font.MeasureString(_actor.Text).X, _actor.Location.Y);
                    break;
                default:
                    throw new Exception("An unknown text alignment was assigned");
            }

            Color c;
            if (Opacity != 1f)
                c = new Color(1f, 1f, 1f, Opacity);
            else
                c = _actor.Color;
                        
            _spriteBatch.DrawString(font, _actor.Text, stringLocation, c, 0f, Vector2.Zero, Scale * _actor.Scale, SpriteEffects.None, 0);
        }

        public void HandleEvent(object @event)
        {
            if (@event.GetType() == typeof(AnimationStartedEvent))
            {
                this._currentAnimations.Add(((AnimationStartedEvent)@event).Animation);
            }
        }

        public VisualHitTestResult GetHitResult(Vector2 location, ViewPort viewPort)
        {
            Vector2 stringLocation;

            var font = (SpriteFont)this._resourceDictionary.GetResource(_actor.FontKey);

            switch (_actor.TextAlignment)
            {
                case UITextBlockActor.Alignment.Left:
                    stringLocation = new Vector2(_actor.Location.X, _actor.Location.Y);
                    break;
                case UITextBlockActor.Alignment.Center:
                    stringLocation = new Vector2((viewPort.ResolutionX / 2f) - (font.MeasureString(_actor.Text).X / 2f) + _actor.Location.X, _actor.Location.Y);
                    break;
                case UITextBlockActor.Alignment.Right:
                    stringLocation = new Vector2(viewPort.ResolutionX - _actor.Location.X - font.MeasureString(_actor.Text).X, _actor.Location.Y);
                    break;
                default:
                    throw new Exception("An unknown text alignment was assigned");
            }

            if (location.X >= stringLocation.X
               && location.X <= (stringLocation.X + font.MeasureString(_actor.Text).X)
               && location.Y >= stringLocation.Y
               && location.Y <= (stringLocation.Y + font.MeasureString(_actor.Text).Y)
               )
            {
                return new VisualHitTestResult(this._actor, new Vector2(location.X - stringLocation.X, location.Y - stringLocation.Y), true);
            }
            else
            {
                return new VisualHitTestResult(this._actor, new Vector2(0f, 0f), false);
            }
        }


        public void Initialize(Object initializationData)
        {
        }
    }
}
