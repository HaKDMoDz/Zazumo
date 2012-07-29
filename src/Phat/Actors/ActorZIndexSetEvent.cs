using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Actors
{
    public class ActorZIndexSetEvent : ActorEvent
    {
        public Int32 ZIndex { get; private set; }
        
        public ActorZIndexSetEvent(Guid actorId, Int32 zIndex)
            : base(actorId)
        {
            this.ZIndex = zIndex;
        }

    }
}
