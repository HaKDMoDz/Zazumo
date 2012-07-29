using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Messages
{
    public class ActorAnimationSetUpdatedEvent : ActorEvent
    {
        public String AnimationSetKey { get; set; }
        
        public ActorAnimationSetUpdatedEvent(Guid actorId, String animationSetKey)
            : base(actorId)
        {
            this.AnimationSetKey = animationSetKey;
        }
    }
}
