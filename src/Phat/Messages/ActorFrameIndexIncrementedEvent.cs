using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Messages
{
    public class ActorFrameIndexIncrementedEvent : ActorEvent
    {
        public ActorFrameIndexIncrementedEvent(Guid actorId)
            : base(actorId)
        {

        }
    }
}
