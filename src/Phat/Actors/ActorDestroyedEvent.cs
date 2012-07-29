using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Actors
{
    public class ActorDestroyedEvent
    {
        public Actor Actor { get; private set; }

        public ActorDestroyedEvent(Actor actor)
        {
            this.Actor = actor;
        }

    }
}
