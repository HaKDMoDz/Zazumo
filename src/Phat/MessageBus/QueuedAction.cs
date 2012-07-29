using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.MessageBus
{
    public class QueuedAction
    {
        public Delegate Action { get; set; }
        public Object Message { get; set; }

        public QueuedAction(Delegate action, Object message)
        {
            this.Action = action;
            this.Message = message;
        }
    }
}
