using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.MessageBus
{
    public class BusProcessor : IProcess
    {
        private readonly IQueueingBus _bus;
        private readonly Int32 _timeout;

        public BusProcessor(IQueueingBus bus, Int32 timeout)
        {
            _bus = bus;
            _timeout = timeout;
        }

        public ProcessState Run(Int64 ticksSinceLastRun)
        {
            _bus.RunQueuedHandlers(_timeout);
            return ProcessState.Running;
        }
    }
}
