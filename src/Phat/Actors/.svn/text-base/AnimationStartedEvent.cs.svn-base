using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Actors
{
    public class AnimationStartedEvent : ActorEvent
    {
        public Animation Animation { get; private set; }

        public AnimationStartedEvent(Guid actorId, Animation animation)
            : base (actorId)
        {
            this.Animation = animation;
        }
    }
}
