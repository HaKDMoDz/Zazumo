using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Messages
{
    public class MoneyBagCollectedEvent : ActorEvent
    {
        public MoneyBagCollectedEvent(Guid actorId) : base(actorId)
        {

        }
    }
}
