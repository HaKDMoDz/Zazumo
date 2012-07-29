using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Microsoft.Xna.Framework.Input.Touch;
using Phat.Messages;

namespace Phat.ActorModel
{
    public class UIButtonActor : UIActor
    {
        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            this.Handle<UIButtonPressedEvent>()
                .ApplyDefaultHandler(OnButtonPressed);

            this.Handle<UIButtonReleasedEvent>()
                .ApplyDefaultHandler(OnButtonReleased);

                    
        }

        public virtual void OnButtonPressed(UIButtonPressedEvent @event)
        {

        }

        public virtual void OnButtonReleased(UIButtonReleasedEvent @event)
        {

        }
    }
}
