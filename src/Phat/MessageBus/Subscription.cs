using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.MessageBus
{
    public class Subscription
    {
        public Delegate Action { get; private set; }
        public Priority Priority { get; private set; }
        public Type MessageType { get; private set; }
        public Object Subscriber { get; private set; }

        public Subscription(Priority priority, Delegate action, Type messageType, Object subscriber)
        {
            this.Action = action;
            this.Priority = priority;
            this.MessageType = messageType;
            this.Subscriber = subscriber;
        }
    }
}
