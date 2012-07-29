using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Microsoft.Xna.Framework.Graphics;
using Phat.ActorModel;
using Phat.ActorResources;
using Microsoft.Xna.Framework;

namespace Phat.ActorVisuals
{
    public class UICarouselVisual : UIVisual
    {
        private const Int32 ItemVisibilityDistance = 4;
        private const Single FadeRate = 1.5f;
        private const Single ShrinkRate = 50f;
        private const Single ScrollRate = 0.2f;

        private new readonly UICarousel _actor;
        private new readonly SpriteBatch _spriteBatch;

        public override void HandleEvent(Object @event)
        {
            base.HandleEvent(@event);
        }

        public UICarouselVisual(UICarousel actor, SpriteBatch spriteBatch, IResourceDictionary resourceDictionary)
            : base(actor, spriteBatch, resourceDictionary)
        {
            this._actor = actor;
            this._spriteBatch = spriteBatch;
        }

        public override void Draw(TimeSpan ellapsedTime, ViewPort viewPort)
        {
            if (_actor.SpriteKeys.Count == 0)
                return;

            RunAnimations();

            _actor.CurrentPosition += (_actor.Velocity * (Single)ellapsedTime.TotalSeconds * ScrollRate);

            if (_actor.Velocity < 0)
            {
                _actor.Velocity += _actor.Damping * (Single)ellapsedTime.TotalSeconds;
                if (_actor.Velocity > 0)
                    _actor.Velocity = 0;
            }

            if (_actor.Velocity > 0)
            {
                _actor.Velocity -= _actor.Damping * (Single)ellapsedTime.TotalSeconds;
                if (_actor.Velocity < 0)
                    _actor.Velocity = 0;
            }

            Int32 firstIndex = (Int32)Math.Max(0, Math.Floor(_actor.CurrentPosition)- ItemVisibilityDistance);
            for (; firstIndex < Math.Floor(_actor.CurrentPosition); firstIndex++)
            {
                DrawItem(_actor.SpriteKeys[firstIndex], firstIndex - (Int32)Math.Floor(_actor.CurrentPosition));
            }

            Int32 lastIndex = (Int32)Math.Min(_actor.SpriteKeys.Count - 1, Math.Floor(_actor.CurrentPosition) + ItemVisibilityDistance);
            for (Int32 index = _actor.SelectedIndex; index <= lastIndex; index++)
            {
                DrawItem(_actor.SpriteKeys[index], index - (Int32)Math.Floor(_actor.CurrentPosition));
            }

            DrawItem(_actor.SpriteKeys[(Int32)Math.Floor(_actor.CurrentPosition)], 0);
        }

        public override Phat.Visual.VisualHitTestResult GetHitResult(Vector2 location, ViewPort viewPort)
        {
            return new Phat.Visual.VisualHitTestResult(this._actor, Vector2.Zero, false);
        }

        public override void Initialize(Object initializationData)
        {

        }

        private void DrawItem(String spriteKey, Int32 relativeIndex)
        {

            var sprite = (SpriteResource)ResourceDictionary.GetResource(spriteKey);
            var texture = (Texture2D)ResourceDictionary.GetResource(sprite.TextureKey);

            Single angle = (Single)(Math.PI / 2) - (relativeIndex * _actor.Spacing);

            Single fraction = (Single)(_actor.CurrentPosition - Math.Floor(_actor.CurrentPosition));
            angle += fraction * _actor.Spacing;
            Int32 x = (Int32)(_actor.HorizontalRadius * (Single)Math.Cos(angle) + _actor.Location.X + _actor.HorizontalRadius);
            Int32 y = (Int32)(_actor.VerticalRadius * (Single)Math.Sin(angle) + _actor.Location.Y + _actor.VerticalRadius);
            Single opacity = 1f;
            Int32 width = (Int32)_actor.ItemWidth;
            Int32 height = (Int32)_actor.ItemHeight;

            var distance = (Single)Math.Abs(angle - Math.PI / 2);

            if (distance != 0)
            {
                var distanceRatio =(Single)( distance / (Math.PI));
                opacity = 1f -  (distanceRatio * FadeRate);
                width = (Int32)(_actor.ItemWidth - (ShrinkRate * distanceRatio));
                height = (Int32)(_actor.ItemHeight - (ShrinkRate * distanceRatio));
            }

            Color c = new Color(1f, 1f, 1f, opacity);


            if ((relativeIndex == 0 && fraction < 0.5f) || (relativeIndex == 1 && fraction >= 0.5f))
            {
                var selectionSprite = (SpriteResource)ResourceDictionary.GetResource("SolidSprite");

                _spriteBatch.Draw((Texture2D)ResourceDictionary.GetResource(selectionSprite.TextureKey),
                        new Rectangle(x - 2 + (Int32)XOffset, y - 2 + (Int32)YOffset, width + 4 + (Int32)WidthOffset, height+ 4 + (Int32)HeightOffset),
                        new Rectangle(selectionSprite.UCoordinate, selectionSprite.VCoordinate, selectionSprite.Width, selectionSprite.Height) /* texture source */,
                        new Color(0f, 0f, 1f, 1f));
            }


            _spriteBatch.Draw(texture,
                                    new Rectangle(x + (Int32)XOffset,
                                                  y + (Int32)YOffset,
                                                  width,
                                                  height),
                                    new Rectangle(sprite.UCoordinate, sprite.VCoordinate, sprite.Width, sprite.Height),
                                    c
                                    );
        }
    }
}
