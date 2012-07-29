using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Phat.Processors
{
    public class ProcessRunner : IProcessRunner
    {
        private readonly IList<IProcess> _processes;
        private readonly IList<IProcess> _completedProcess;
        private readonly Object _lockObject;

        public ProcessRunner()
        {
            _lockObject = new Object();
            //ThreadPool.SetMaxThreads(10, 5);
            _processes = new List<IProcess>();
            _completedProcess = new List<IProcess>();
        }

        public void ScheduleProcess(IProcess process)
        {
            _processes.Add(process);
        }

        public void RunScheduledProcesses(TimeSpan ellapsedTime, Int32 timeoutInMilliseconds)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            Int32 processIndex = 0;

            lock (_lockObject)
            {
                while (processIndex < _processes.Count) // && sw.ElapsedMilliseconds < timeoutInMilliseconds)
                {
                    //ThreadPool.QueueUserWorkItem(Runner, _processes[processIndex]);
                    Runner(ellapsedTime, _processes[processIndex]);
                    processIndex++;
                }

                for (Int32 index = 0; index < _completedProcess.Count; index++)
                {
                    _processes.Remove(_completedProcess[index]);
                }

                _completedProcess.Clear();
            }

            if (processIndex < _processes.Count)
                Debug.WriteLine("processes did not finish in time.");
        }

        public void KillProcess(IProcess process)
        {
            if (_processes.Contains(process))
                RemoveProcess(process);
        }

        private void Runner(TimeSpan ellapsedTime, Object obj)
        {
            var process = obj as IProcess;

            if (process == null)
                throw new Exception();

            var runtime = DateTime.Now.Ticks;
            if (process.Run(ellapsedTime.Ticks) == ProcessState.Completed)
                RemoveProcess(process);

        }

        private void RemoveProcess(IProcess process)
        {
            _completedProcess.Add(process);
        }
    }
}
