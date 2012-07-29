using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Zazumo.Messages
{
    public class ZazumoSizeChangedEvent : ActorEvent
    {
        public Int32 Size { get; private set; }

        public ZazumoSizeChangedEvent(Guid actorId, Int32 size)
            : base(actorId)
        {
            this.Size = size;
        }
    }
}
