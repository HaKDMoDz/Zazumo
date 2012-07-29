using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat
{
    public sealed class Archetype
    {
        public String ActorType { get; private set; }
        public ActorResource Resource { get; set; }

        public Archetype(String actorType)
        {
            this.ActorType = actorType;
        }

        public Archetype(String actorType, ActorResource resource)
            : this(actorType)
        {
            this.Resource = resource;
        }
    }
}
