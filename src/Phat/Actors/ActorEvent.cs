using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat
{
    public class ActorEvent
    {
        public Guid ActorId { get; private set; }
        
        public ActorEvent(Guid actorId)
        {
            this.ActorId = actorId;
        }
    }
}
