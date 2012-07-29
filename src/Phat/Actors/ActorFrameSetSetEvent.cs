using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Actors
{
    public class ActorFrameSetSetEvent : ActorEvent
    {
        public String FrameSetKey { get; private set; }

        public ActorFrameSetSetEvent(Guid actorId, String frameSetKey)
            : base(actorId)
        {
            this.FrameSetKey = frameSetKey;
        }
    }
}
