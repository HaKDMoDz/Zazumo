using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Animations;
using Phat.Actors;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace Phat
{
    public abstract class GameMode : IGameSetters
    {
        private readonly List<BaseController> _controllers;
        private readonly List<IProcess> _processes;
        private readonly Dictionary<Type, List<BaseController>> _controllerSubscriptions;
        private readonly Dictionary<Type, Delegate> _subscriptions;
        private readonly Dictionary<Guid, ActorController> _actorControllers;

        private IProcessRunner _processRunner;
        private GameStack _gameStack;

        protected Object SaveData { get; private set; }
        protected ViewPort ViewPort { get; private set; }
        protected IActorFactory ActorFactory { get; private set; }
        protected IActorRepository ActorRepository { get; private set; }
        protected IResourceDictionary ResourceDictionary { get; private set; }

        private IBus _bus;
        private Boolean _isSuspended;

        public GameMode()
        {
            _controllers = new List<BaseController>();
            _controllerSubscriptions = new Dictionary<Type, List<BaseController>>();
            _subscriptions = new Dictionary<Type, Delegate>();
            _processes = new List<IProcess>();
            _actorControllers = new Dictionary<Guid, ActorController>();
        }

        protected AudioManager AudioManager
        {
            get { return AudioManager.Current; }
        }

        protected abstract void OnInitialize(Object initializationData);

        public void Initialize(Object initializtionData)
        {
            OnInitialize(initializtionData);

            _bus.Subscribe<ActorDestroyedEvent>(x =>
                {
                    if (_actorControllers.ContainsKey(x.Actor.ActorId))
                    {
                        var controller = _actorControllers[x.Actor.ActorId];
                        controller.Suspend();
                        _controllers.Remove(controller);
                        _actorControllers.Remove(x.Actor.ActorId);
                    }
                }, this);
        }
        
        protected void RunStoryboard(Storyboard storyboard)
        {
            storyboard.Begin(_processRunner, ActorRepository);
        }

        public virtual void Update(TimeSpan ellapsedTime)
        {
            foreach (var controller in _controllers)
            {
                controller.Update(ellapsedTime);
            }
        }

        protected TController AddController<TController>()
            where TController : BaseController, new()
        {
            var controllerInstance = InstantiateController<TController>();
            
            _controllers.Add(controllerInstance);

            return controllerInstance;
        }

        protected TController AddActorController<TController>(Actor actor)
            where TController : ActorController, new()
        {
            var controllerInstance = AddController<TController>();
            controllerInstance.Actor = actor;
            _actorControllers.Add(actor.ActorId, controllerInstance);

            return controllerInstance;
        }

        protected DelayHelper In(Int32 delayValue)
        {
            var helper = new DelayHelper(delayValue, this._processRunner);
            _processes.Add(helper.Process);
            return helper;
        }

        protected TController SetController<TController>()
            where TController : BaseController, new()
        {
            foreach (var controller in _controllers)
            {
                controller.Suspend();
            }

            _controllers.Clear();

            foreach (var pair in _controllerSubscriptions)
            {
                pair.Value.Clear();
            }
            
            var controllerInstance = InstantiateController<TController>();
            _controllers.Add(controllerInstance);
            OnActiveControllerChanged(controllerInstance);

            return controllerInstance;
        }

        protected void Publish(Object message)
        {
            this._bus.Publish(message);
        }

        protected void Save()
        {
#if WINDOWS
                using (var userStore = IsolatedStorageFile.GetUserStoreForDomain())
#else
            using (var userStore = IsolatedStorageFile.GetUserStoreForApplication())
#endif
            {
                {
                    using (var file = userStore.OpenFile("SaveData.Xml", System.IO.FileMode.Truncate))
                    {
                        XmlSerializer serializer = new XmlSerializer(SaveData.GetType());
                        serializer.Serialize(file, SaveData);
                    }
                }
            }
        }

        protected void Subscribe<TMessage>(Action<TMessage> action)
        {
            _subscriptions.Add(typeof(TMessage), action);
            _bus.Subscribe(typeof(TMessage), (Action<Object>)(x => GameModeEventHandler(x)), this, Priority.High);
        }

        private TController InstantiateController<TController>() where TController : BaseController, new()
        {
            var controllerInstance = Activator.CreateInstance<TController>();
            ((IGameSetters)controllerInstance).SetRepository(ActorRepository);
            ((IGameSetters)controllerInstance).SetActorFactory(ActorFactory);
            ((IGameSetters)controllerInstance).SetResourceDictionary(ResourceDictionary);
            ((IGameSetters)controllerInstance).SetProcessRunner(_processRunner);
            ((IGameSetters)controllerInstance).SetGameMode(this);
            ((IGameSetters)controllerInstance).SetBus(_bus);
            ((IGameSetters)controllerInstance).SetViewPort(ViewPort);
            ((IGameSetters)controllerInstance).SetSaveData(SaveData);

            foreach (var handler in typeof(TController).GetInterfaces())
            {
                if (!handler.IsGenericType)
                    continue;

                if (handler.GetGenericTypeDefinition() == typeof(IHandle<>))
                {
                    var eventType = handler.GetGenericArguments().Single();

                    if (!_controllerSubscriptions.ContainsKey(eventType))
                    {
                        _controllerSubscriptions.Add(eventType, new List<BaseController>());
                        _bus.Subscribe(eventType, (Action<Object>)(x => EventHandler(x)), this, Priority.High);
                    }

                    _controllerSubscriptions[eventType].Add(controllerInstance);
                }
            }

            controllerInstance.Initialize();
            
            return controllerInstance;
        }

        protected TController GetController<TController>()
            where TController : BaseController
        {
            foreach (var controller in _controllers)
            {
                if (controller.GetType() == typeof(TController))
                    return (TController)controller;
            }

            throw new Exception("No controller was found for this game mode");
        }

        protected virtual void OnActiveControllerChanged(BaseController controller)
        {

        }

        private void EventHandler(Object @event)
        {
            if (_isSuspended)
                return;

            foreach (var controller in _controllerSubscriptions[@event.GetType()])
            {
                var handlerType = typeof(IHandle<>).MakeGenericType(@event.GetType());
                handlerType.GetMethod("Handle").Invoke(controller, new Object[] { @event });
            }
        }

        private void GameModeEventHandler(Object @event)
        {
            if (_isSuspended)
                return;

            if (_subscriptions.ContainsKey(@event.GetType()))
            {
                _subscriptions[@event.GetType()].DynamicInvoke(@event);
            }
        }

        public void Pop()
        {
            Pop(null);
        }

        public void Pop(Object popState)
        {
            foreach (var actor in ActorRepository.GetActors())
            {
                actor.Destroy();
            }

            _bus.Unsubscribe(this);
            _gameStack.Pop(popState);
        }

        public TGameMode Push<TGameMode>()
            where TGameMode : GameMode
        {
            return Push<TGameMode>(null);
        }

        public TGameMode Push<TGameMode>(Object pushState)
            where TGameMode : GameMode
        {
            foreach (var actor in ActorRepository.GetActors())
            {
                actor.Destroy();
            }

            return _gameStack.Push<TGameMode>(pushState);
        }

        void IGameSetters.SetGameModeStack(GameStack gameStack)
        {
            this._gameStack = gameStack;
        }

        public void Suspend()
        {
            _isSuspended = true;

            foreach (var controller in _controllers)
            {
                controller.Suspend();
            }

            OnSuspend();
        }

        public void Resume(Object popState)
        {
            _isSuspended = false;

            foreach (var controller in _controllers)
            {
                controller.Resume();
            }

            OnResume(popState);
        }

        protected virtual void OnSuspend()
        {
            foreach (var process in _processes)
            {
                _processRunner.KillProcess(process);
            }

            foreach (var actor in ActorRepository.GetActors())
            {
                actor.Destroy();
            }

            _processes.Clear();
        }

        protected virtual void OnResume(Object popState)
        {

        }

        void IGameSetters.SetActorFactory(IActorFactory actorFactory)
        {
            this.ActorFactory = actorFactory;
        }

        void IGameSetters.SetProcessRunner(IProcessRunner processRunner)
        {
            this._processRunner = processRunner;
        }

        void IGameSetters.SetBus(IBus bus)
        {
            this._bus = bus;
        }

        void IGameSetters.SetSaveData(Object saveData)
        {
            this.SaveData = saveData;
        }

        void IGameSetters.SetRepository(IActorRepository actorRepository)
        {
            this.ActorRepository = actorRepository;
        }

        void IGameSetters.SetResourceDictionary(IResourceDictionary resourceDictionary)
        {
            this.ResourceDictionary = resourceDictionary;
        }

        void IGameSetters.SetViewPort(ViewPort viewPort)
        {
            this.ViewPort = viewPort;
        }

        void IGameSetters.SetGameMode(GameMode gameMode)
        {
            throw new NotImplementedException();
        }
    }
}
