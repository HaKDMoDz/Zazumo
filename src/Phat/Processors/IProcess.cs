using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat
{
    public enum ProcessState
    {
        Running,
        Completed
    }

    public interface IProcess
    {
        ProcessState Run(Int64 ticksSinceLastRun);
    }
}
