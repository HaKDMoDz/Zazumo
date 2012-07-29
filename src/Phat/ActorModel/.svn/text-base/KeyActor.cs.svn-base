using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Messages;

namespace Phat.ActorModel
{
    public class KeyActor : PlaceableActor<KeyActor>, ITouchable
    {
        public void Touch(Actor other)
        {
            OnTouch(other);
        }

        protected virtual void OnTouch(Actor other)
        {
            Publish(new KeyPickedUpEvent(this.ActorId));
            this.Destroy();
        }
    }
}
