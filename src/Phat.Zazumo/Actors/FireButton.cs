using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;
using Phat.Zazumo.Messages;

namespace Phat.Zazumo.Actors
{
    public class FireButton : UIButtonActor
    {
        public override void OnButtonPressed(Phat.Messages.UIButtonPressedEvent @event)
        {
            base.OnButtonPressed(@event);

            Publish(new FireButtonPressedEvent());
        }

        public override void OnButtonReleased(Phat.Messages.UIButtonReleasedEvent @event)
        {
            base.OnButtonReleased(@event);

            Publish(new FireButtonReleasedEvent());
        }
    }
}
