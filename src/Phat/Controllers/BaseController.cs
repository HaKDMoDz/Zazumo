using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Animations;

namespace Phat
{
    public abstract class BaseController : IGameSetters
    {
        private Phat.GameMode _gameMode;

        private IBus _bus;
        private IProcessRunner _processRunner;

        public IActorFactory ActorFactory { get; private set; }
        public IActorRepository ActorRepository { get; private set; }
        public IResourceDictionary ResourceDictionary { get; private set; }
        protected Object SaveData { get; private set; }      

        protected ViewPort ViewPort { get; private set; }

        protected abstract void OnInitialize();

        public void Initialize()
        {
            this.OnInitialize();
        }

        public void Suspend()
        {
            OnSuspended();
        }

        public void Resume()
        {
            OnResumed();
        }

        protected DelayHelper In(Int32 delayValue)
        {
            return new DelayHelper(delayValue, this._processRunner);
        }

        public void RunStoryboard(Storyboard storyboard)
        {
            storyboard.Begin(_processRunner, ActorRepository);
        }

        protected AudioManager AudioManager
        {
            get { return AudioManager.Current; }
        }

        protected void Publish(Object message)
        {
            this._bus.Publish(message);
        }

        protected virtual void OnSuspended()
        {
        }

        protected virtual void OnResumed()
        {
        }
        
        void IGameSetters.SetActorFactory(IActorFactory actorFactory)
        {
            this.ActorFactory = actorFactory;
        }

        void IGameSetters.SetRepository(IActorRepository actorRepository)
        {
            this.ActorRepository = actorRepository;
        }

        void IGameSetters.SetResourceDictionary(IResourceDictionary resourceDictionary)
        {
            this.ResourceDictionary = resourceDictionary;
        }

        void IGameSetters.SetBus(IBus bus)
        {
            _bus = bus;
        }
               
        void IGameSetters.SetGameModeStack(GameStack gameStack)
        {
            throw new NotImplementedException();
        }

        void IGameSetters.SetGameMode(GameMode gameMode)
        {
            this._gameMode = gameMode;
        }

        void IGameSetters.SetProcessRunner(IProcessRunner processRunner)
        {
            _processRunner = processRunner;
        }

        void IGameSetters.SetViewPort(ViewPort viewPort)
        {
            this.ViewPort = viewPort;
        }

        void IGameSetters.SetSaveData(Object saveData)
        {
            this.SaveData = saveData;
        }

        public virtual void Update(TimeSpan ellapsedTime)
        {
            
        }
    }
}
