using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Processors;

namespace Phat
{
    public class DelayHelper
    {
        private readonly Int32 _delayValue;
        private readonly IProcessRunner _processRunner;

        public DelayedContext Milliseconds
        {
            get
            {
                return new DelayedContext(TimeSpan.FromMilliseconds(_delayValue), this._processRunner, (ActionProcess)Process);
            }
        }

        public DelayedContext Seconds
        {
            get
            {
                return new DelayedContext(TimeSpan.FromSeconds(_delayValue), this._processRunner, (ActionProcess)Process);
            }
        }

        public DelayedContext Minutes
        {
            get
            {
                return new DelayedContext(TimeSpan.FromMinutes(_delayValue), this._processRunner, (ActionProcess)Process);
            }
        }

        public ActionProcess Process { get; set; }

        public DelayHelper(Int32 delayValue, IProcessRunner processRunner)
        {
            this.Process = new ActionProcess();
            this._processRunner = processRunner;
            this._delayValue = delayValue;
        }

        public class DelayedContext
        {
            private readonly TimeSpan _delay;
            private readonly IProcessRunner _processRunner;
            private readonly ActionProcess _actionProcess;

            private Single _ellapsedMilliseconds;

            public DelayedContext(TimeSpan delay, IProcessRunner processRunner, ActionProcess actionProcess)
            {
                this._delay = delay;
                this._actionProcess = actionProcess;
                this._processRunner = processRunner;
                this._ellapsedMilliseconds = 0;
            }

            public void Run(Action action)
            {
                _actionProcess.Action = t =>
                    {
                        _ellapsedMilliseconds += ((Single)t / 10000f);
                        if (_ellapsedMilliseconds < _delay.TotalMilliseconds)
                        {
                            return ProcessState.Running;
                        }
                        else
                        {
                            action.Invoke();
                            return ProcessState.Completed;
                        }
                        
                    };
                _processRunner.ScheduleProcess(_actionProcess);
                                
            }
        }
    }
}
