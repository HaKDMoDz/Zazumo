using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Messages
{
    public class ActorCollidedEvent : ActorEvent
    {
        public Actor OtherActor { get; private set; }
        public Object CollisionData { get; set; }
        public Boolean Cancel { get; set; }
        
        public ActorCollidedEvent(Guid actorId, Actor otherActor, Object collisionData)
            : base(actorId)
        {
            this.OtherActor = otherActor;
            this.CollisionData = collisionData;
        }
    }
}
