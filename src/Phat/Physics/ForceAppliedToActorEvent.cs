using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phat
{
    public class ForceAppliedToActorEvent
    {
        public Guid ActorId { get; private set; }
        public Vector2 Force { get; private set; }
        
        public ForceAppliedToActorEvent(Guid actorId, Vector2 force)
        {
            this.ActorId = actorId;
            this.Force = force;
        }
    }
}
