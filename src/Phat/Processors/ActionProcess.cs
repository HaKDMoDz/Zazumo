using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Processors
{
    public class ActionProcess : IProcess
    {
        public Func<Int64, ProcessState> Action { get; set; }

        public ActionProcess()
        {

        }

        public ActionProcess(Func<Int64, ProcessState> action)
        {
            this.Action = action;
        }

        public ProcessState Run(Int64 ticksSinceLastRun)
        {
            return this.Action.Invoke(ticksSinceLastRun);
        }
    }
}
