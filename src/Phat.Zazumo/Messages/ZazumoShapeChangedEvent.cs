using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Resources;

namespace Phat.Zazumo.Messages
{
    public class ZazumoShapeChangedEvent : ActorEvent
    {
        public ZazumoShape Shape { get; private set; }
        public Single AmmoLevel { get; private set; }

        public ZazumoShapeChangedEvent(Guid actorId, ZazumoShape shape, Single ammoLevel)
            : base(actorId)
        {
            this.Shape = shape;
            this.AmmoLevel = ammoLevel;
        }        
    }
}
