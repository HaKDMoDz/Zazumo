using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Messages
{
    public class BombCountdownChangedEvent : ActorEvent
    {
        public Int32 Countdown { get; private set; }

        public BombCountdownChangedEvent(Guid actorId, Int32 countdown)
            : base (actorId)
        {
            this.Countdown = countdown;
        }
    }
}
