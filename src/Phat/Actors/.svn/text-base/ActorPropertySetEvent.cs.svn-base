using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Actors
{
    public class ActorPropertySetEvent  : ActorEvent
    {
        public String Property { get; private set; }
        public Object Value { get; set; }

        public ActorPropertySetEvent(Guid actorId, String property, Object value)
            : base(actorId)
        {
            this.Property = property;
            this.Value = value;
        } 
    }
}
