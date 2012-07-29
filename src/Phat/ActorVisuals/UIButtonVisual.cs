using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Phat.ActorResources;
using Phat.Messages;
using Phat.ActorModel;

namespace Phat.ActorVisuals
{
    public class UIButtonVisual : SpriteVisual
    {
        private readonly IBus _bus;
        private Boolean _isPressed;


        public UIButtonVisual(Actor actor, SpriteBatch spriteBatch, IResourceDictionary resourceDictionary, IBus bus)
            : base(actor, spriteBatch, resourceDictionary)
        {
            this._bus = bus;
            this._isPressed = false;
        }

        public override void Draw(TimeSpan ellapsedTime, ViewPort viewPort)
        {
            var resource = (UIResource)Resource;

            var screenX = (Int32)(resource.X * viewPort.ResolutionX) + (Int32)Properties.XOffset;
            var screenY = (Int32)(resource.Y * viewPort.ResolutionY) + (Int32)Properties.YOffset;
            var screenHeight = (Int32)(Resource.Height * viewPort.ResolutionY) + (Int32)Properties.HeightOffset;
            var screenWidth = (Int32)(Resource.Width * viewPort.ResolutionX) + (Int32)Properties.WidthOffset;

            this.Draw(ellapsedTime, new Rectangle(screenX, screenY, screenWidth, screenHeight));

            Boolean hasTouch = false;

#if WINDOWS_PHONE
            foreach (var touch in Microsoft.Xna.Framework.Input.Touch.TouchPanel.GetState())
            {
                if (
                    (touch.Position.X >= screenX && touch.Position.X <= screenX + screenWidth) &&
                    (touch.Position.Y >= screenY && touch.Position.Y <= screenY + screenHeight))
                {
                    hasTouch = true;
                }
            }
#endif
            if (_isPressed == hasTouch)
                return;

            if (hasTouch == false)
            {
                _isPressed = false;
                _bus.Publish(new UIButtonReleasedEvent((UIButtonActor)Actor));
            }
            else
            {
                _isPressed = true;
                _bus.Publish(new UIButtonPressedEvent((UIButtonActor)Actor));                
            }            
        }
    }
}
