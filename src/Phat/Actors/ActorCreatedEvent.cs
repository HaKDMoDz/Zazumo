using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat
{
    public class ActorCreatedEvent
    {
        public Actor Actor { get; private set; }
        public Object InitializationData { get; private set; }
        
        public ActorCreatedEvent(Actor actor)
        {
            this.Actor = actor;
        }

        public ActorCreatedEvent(Actor actor, Object initializationData)
            : this(actor)
        {
            this.InitializationData = initializationData;
        }
    }
}
