using System;

namespace Phat
{
    public interface IProcessRunner
    {
        void RunScheduledProcesses(TimeSpan ellapsedTime, Int32 timeoutInMilliseconds);
        void ScheduleProcess(IProcess process);
        void KillProcess(IProcess process);
    }
}
