using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Messages
{
    public class RetractableSpikeButtonToggledEvent : ActorEvent
    {
        public RetractableSpikeButtonToggledEvent(Guid actorId)
            : base(actorId)
        {

        }
    }
}
