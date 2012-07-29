using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Actors;
using Phat;

namespace Phat.Scripting
{
    public class ScriptExecutionContext : IGameSetters, IScriptActivityExecutor, IProcess
    {
        public IActorRepository ActorRepository { get; private set; }
        private Queue<ScriptActivity> _queuedActivities;
        private ScriptActivity _currentActivity;
        
        public ScriptExecutionContext()
        {
            _queuedActivities = new Queue<ScriptActivity>();
        }

        #region IGameSetters Members

        void IGameSetters.SetGameMode(GameMode gameMode)
        {
            throw new NotImplementedException();
        }

        void IGameSetters.SetActorFactory(IActorFactory actorFactory)
        {
            throw new NotImplementedException();
        }

        void IGameSetters.SetBus(IBus bus)
        {
            throw new NotImplementedException();
        }

        void IGameSetters.SetProcessRunner(IProcessRunner processRunner)
        {
            throw new NotImplementedException();
        }

        void IGameSetters.SetRepository(IActorRepository actorRepository)
        {
            this.ActorRepository = actorRepository;
        }

        void IGameSetters.SetResourceDictionary(IResourceDictionary resourceDictionary)
        {
            throw new NotImplementedException();
        }

        void IGameSetters.SetViewPort(ViewPort viewPort)
        {
            throw new NotImplementedException();
        }

        void IGameSetters.SetSaveData(object saveData)
        {
            throw new NotImplementedException();
        }

        void IGameSetters.SetGameModeStack(GameStack gameStack)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IScriptActivityExecutor Members

        void IScriptActivityExecutor.ExecuteActivities(ScriptActivityContainer activities)
        {
            foreach (var activity in activities)
            {
                _queuedActivities.Enqueue(activity);
            }

            if (_queuedActivities.Count == 0)
                return;

            _currentActivity = _queuedActivities.Dequeue(); 
        }

        #endregion

        #region IProcess Members

        ProcessState IProcess.Run(long ticksSinceLastRun)
        {
            
            while (true)
            {
                if (_currentActivity.Execute(this) == ScriptActivityRunningState.Completed)
                {
                    if (_queuedActivities.Count == 0)
                        return ProcessState.Completed;
                    else
                        _currentActivity = _queuedActivities.Dequeue();
                }
                else
                {
                    return ProcessState.Running;
                }
            }            
        }

        #endregion
    }
}
