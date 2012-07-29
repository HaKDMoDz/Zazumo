using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Messages
{
    public class KeyPickedUpEvent : ActorEvent
    {
        public KeyPickedUpEvent(Guid actorId)
            : base(actorId)
        {

        }
    }
}
