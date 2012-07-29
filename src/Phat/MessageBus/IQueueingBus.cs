using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.MessageBus
{
    public interface IQueueingBus
    {
        void RunQueuedHandlers(Int32 timeoutInMilliseconds);
    }
}
