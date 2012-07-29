using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Phat.MessageBus;
using Phat.Actors;
using Phat.Processors;
using Phat.Visual;
using Phat;
using Phat.Physics;
using Phat.Scripting;
using Microsoft.Xna.Framework.Content;

namespace Phat
{
    public class Configurator
    {
        private readonly IDictionary<Type, Object> _resolvedDependencies;
        private readonly IDictionary<Type, Type> _concreteTypes;
        private readonly IList<Type> _unresolvedDependencies;
        private readonly IList<IProcess> _processes;
        private readonly IDictionary<Type, Object[]> _constructorParameters;

        private Object _saveData;

        private Assembly _actorAssembly;
        private Assembly _scriptAssembly;

        private Int32 viewPortWidth = 0;
        private Int32 viewPortHeight = 0;

        private Type _gameType;
                
        public static Configurator Configure()
        {
            Configurator c = new Configurator();
            return c;
        }

        public Configurator()
        {
            _resolvedDependencies = new Dictionary<Type, Object>();
            _concreteTypes = new Dictionary<Type, Type>();
            _processes = new List<IProcess>();
            _unresolvedDependencies = new List<Type>();
            _constructorParameters = new Dictionary<Type, Object[]>();
        }

        public Configurator UseDefaultConfiguration()
        {
            _concreteTypes.Add(typeof(ActorEventSubscriptionBehavior), typeof(ActorEventSubscriptionBehavior));
            _concreteTypes.Add(typeof(IActorFactory), typeof(ActorFactory));
            _concreteTypes.Add(typeof(IActorRepository), typeof(ActorRepository));
            _concreteTypes.Add(typeof(IBus), typeof(Bus));
            _concreteTypes.Add(typeof(IProcessRunner), typeof(ProcessRunner));
            _concreteTypes.Add(typeof(IResourceDictionary), typeof(ResourceDictionary));
            _concreteTypes.Add(typeof(VisualActorBehaviorSubsystem), typeof(VisualActorBehaviorSubsystem));
            _concreteTypes.Add(typeof(IActorVisualFactory), typeof(ActorVisualFactory));
            _concreteTypes.Add(typeof(ViewPort), typeof(ViewPort));
            _concreteTypes.Add(typeof(PhysicsSubsystem), typeof(PhysicsSubsystem));
            _concreteTypes.Add(typeof(PhysicsActorBehavior), typeof(PhysicsActorBehavior));
            _concreteTypes.Add(typeof(IActorPhysicsFactory), typeof(ActorPhysicsFactory));
            _concreteTypes.Add(typeof(GameStack), typeof(GameStack));
            _concreteTypes.Add(typeof(ScriptManager), typeof(ScriptManager));

            var physicsFactory = new ActorPhysicsFactory();
            _resolvedDependencies.Add(typeof(IBus), new Bus());
            _resolvedDependencies.Add(typeof(IProcessRunner), new ProcessRunner());
            _resolvedDependencies.Add(typeof(IActorVisualFactory), new ActorVisualFactory());
            _resolvedDependencies.Add(typeof(ViewPort), new ViewPort());
            _resolvedDependencies.Add(typeof(IActorPhysicsFactory), physicsFactory);
            _resolvedDependencies.Add(typeof(GameStack), new GameStack(this));
            
            _unresolvedDependencies.Add(typeof(IActorRepository));
            _unresolvedDependencies.Add(typeof(IActorFactory));
            _unresolvedDependencies.Add(typeof(IResourceDictionary));
            _unresolvedDependencies.Add(typeof(ActorEventSubscriptionBehavior));
            _unresolvedDependencies.Add(typeof(VisualActorBehaviorSubsystem));
            _unresolvedDependencies.Add(typeof(PhysicsActorBehavior));
            _unresolvedDependencies.Add(typeof(PhysicsSubsystem));
            _unresolvedDependencies.Add(typeof(ScriptManager));

            _processes.Add(new BusProcessor((IQueueingBus)_resolvedDependencies[typeof(IBus)], 1000));
            //_processes.Add((IProcess)_resolvedDependencies[typeof(PhysicsSubsystem)]);
            
            return this;
        }

        public Configurator SetGameAssembly(Assembly assembly)
        {
            _actorAssembly = assembly;
            _scriptAssembly = assembly;

            _concreteTypes.Add(typeof(IEnumerable<Type>), typeof(IEnumerable<Type>));

            var events = new List<Type>();
            foreach (var type in assembly.GetExportedTypes())
            {
                if (typeof(ActorEvent).IsAssignableFrom(type))
                {
                    events.Add(type);
                }
            }

            foreach (var type in Assembly.GetExecutingAssembly().GetExportedTypes())
            {
                if (typeof(ActorEvent).IsAssignableFrom(type))
                {
                    events.Add(type);
                }
            }

            _resolvedDependencies.Add(typeof(IEnumerable<Type>), events);

            return this;
        }

        public Configurator SetActorAssembly<TActorInAssembly>()
            where TActorInAssembly : Actor
        {
            _actorAssembly = typeof(TActorInAssembly).Assembly;

            return this;
        }


        public Configurator SetScriptAssembly<TScriptInAssembly>()
            where TScriptInAssembly : Script
        {
            _scriptAssembly = typeof(TScriptInAssembly).Assembly;

            return this;
        }

        public Configurator SetActorEventAssembly<TEventInAssembly>()
            where TEventInAssembly : ActorEvent
        {
            _concreteTypes.Add(typeof(IEnumerable<Type>), typeof(IEnumerable<Type>));

            var events = new List<Type>();
            foreach (var type in typeof(TEventInAssembly).Assembly.GetExportedTypes())
            {
                if (typeof(ActorEvent).IsAssignableFrom(type))
                {
                    events.Add(type);
                }
            }

            _resolvedDependencies.Add(typeof(IEnumerable<Type>), events);
            
            return this;
        }

        public Configurator SetSaveData(Object saveData)
        {
            this._saveData = saveData;
            return this;
        }



        public Configurator SetContentManager(ContentManager contentManager)
        {
            _resolvedDependencies.Add(typeof(ContentManager), contentManager);
            return this;
        }

        public Configurator SetVisualSubsystem<TVisualSubsystem>(params Object[] parameters)
            where TVisualSubsystem : class, IVisualSubsystem
        {
            _concreteTypes.Add(typeof(IVisualSubsystem), typeof(TVisualSubsystem));
            _unresolvedDependencies.Add(typeof(IVisualSubsystem));
            _constructorParameters.Add(typeof(TVisualSubsystem), parameters);
            return this;
        }

        public Configurator SetInitialGameHandler<TGame>()
            where TGame : GameMode
        {
            _gameType = typeof(TGame);
            return this;
        }

        public Configurator Initialize()
        {
            ResolveInitialDependencies();

            var processRunner = Resolve<IProcessRunner>();
            
            foreach (var process in _processes)
            {
                processRunner.ScheduleProcess(process);
            }

            if (_gameType == null)
                throw new Exception("Configuration initialization failed.  No initial game type was specified.");

            this.Resolve<ViewPort>().ResolutionX = viewPortWidth;
            this.Resolve<ViewPort>().ResolutionY = viewPortHeight;
            this.Resolve<PhysicsSubsystem>().Initialize();

            if (_actorAssembly == null)
                throw new Exception("SetActorAssembly must be called prior to initialization.");
            ((ActorFactory)this.Resolve<IActorFactory>()).RegisterActorTypes(_actorAssembly);
            ((ActorFactory)this.Resolve<IActorFactory>()).RegisterActorTypes(this.GetType().Assembly);


            /*var scriptManager = this.Resolve<ScriptManager>();
            foreach (var type in _scriptAssembly.GetExportedTypes())
            {
                var nameAttribute = (ScriptNameAttribute)type.GetCustomAttributes(typeof(ScriptNameAttribute), false).FirstOrDefault();

                if (nameAttribute == null)
                    continue;

                if (typeof(Script).IsAssignableFrom(type))
                {
                    scriptManager.RegisterScript(nameAttribute.ScriptName, type);
                }
            } */           

            return this;
        }

        public GameMode Run()
        {
            var game = (GameMode)this.Resolve(_gameType);
            RunGame(game, null);
            return game;
        }

        public TGameMode Run<TGameMode>(Object pushState)
            where TGameMode : GameMode
        {
            var game = (GameMode)this.Resolve(typeof(TGameMode));
            RunGame(game, pushState);
            return (TGameMode)game;
        }

        private void RunGame(GameMode game, Object pushState)
        {
            ((IGameSetters)game).SetActorFactory(Resolve<IActorFactory>());
            ((IGameSetters)game).SetResourceDictionary(Resolve<IResourceDictionary>());
            ((IGameSetters)game).SetRepository(Resolve<IActorRepository>());
            ((IGameSetters)game).SetBus(Resolve<IBus>());
            ((IGameSetters)game).SetProcessRunner(Resolve<IProcessRunner>());
            ((IGameSetters)game).SetGameModeStack(Resolve<GameStack>());
            ((IGameSetters)game).SetViewPort(Resolve<ViewPort>());
            ((IGameSetters)game).SetSaveData(_saveData);
            
            Resolve<GameStack>().Push(game, pushState);
        }

        private void ResolveInitialDependencies()
        {
            while (_unresolvedDependencies.Count > 0)
            {
                var resolvedTypes = new List<Type>();

                foreach (var unresolvedType in _unresolvedDependencies)
                {
                    var concreteType = _concreteTypes[unresolvedType];
                    var ctors = concreteType.GetConstructors();
                    var parameters = new ParameterInfo[0];
                    ConstructorInfo greediestConstructor = null;

                    foreach (var constructor in ctors)
                    {
                        var newParams = constructor.GetParameters();

                        if (newParams.Length >= parameters.Length)
                        {
                            parameters = newParams;
                            greediestConstructor = constructor;
                        }
                    }

                    Boolean canResolve = true;

                    foreach (var p in parameters)
                    {
                        if (!CanResolve(concreteType, p.ParameterType))
                            canResolve = false;
                    }

                    if (canResolve)
                    {
                        Object obj = ResolveType(concreteType, greediestConstructor);
                        _resolvedDependencies.Add(unresolvedType, obj);
                        resolvedTypes.Add(unresolvedType);

                        if (obj is IProcess)
                            _processes.Add((IProcess)obj);
                    }
                }

                if (resolvedTypes.Count == 0 && _unresolvedDependencies.Count > 0)
                    throw new Exception("No dependencies could be resolved.");

                foreach (var type in resolvedTypes)
                {
                    _unresolvedDependencies.Remove(type);
                }

                resolvedTypes.Clear();
            }
        }

        private Object ResolveType(Type objectType, ConstructorInfo constructor)
        {
            List<Object> resolvedParameters = new List<Object>();

            var parameters = constructor.GetParameters();

            if (parameters == null)
                return constructor.Invoke(new Object[0]);

            foreach (var parameter in parameters)
            {
                resolvedParameters.Add(ResolveParameter(objectType, parameter.ParameterType));
            }

            return constructor.Invoke(resolvedParameters.ToArray());

        }

        private Object ResolveParameter(Type objectType, Type parameterType)
        {
            if (_constructorParameters.ContainsKey(objectType))
            {
                var result = _constructorParameters[objectType].Where(x => parameterType.IsAssignableFrom(x.GetType())).FirstOrDefault();

                if (result != null)
                    return result;
            }

            return _resolvedDependencies[parameterType];
        }

        private Boolean CanResolve(Type objectType, Type parameterType)
        {
            if (_resolvedDependencies.ContainsKey(parameterType))
                return true;

            if (_constructorParameters.ContainsKey(objectType))
            {
                if (_constructorParameters[objectType].Where(x => parameterType.IsAssignableFrom(x.GetType())).Count() > 0)
                    return true;
            }

            return false;
        }

        public T Resolve<T>()
        {
            if (_resolvedDependencies.ContainsKey(typeof(T)))
                return (T)_resolvedDependencies[typeof(T)];

            var concreteType = _concreteTypes[typeof(T)];
            return (T)_resolvedDependencies[concreteType];
        }

        public Object Resolve(Type type)
        {
            Type concreteType = type;
            
            if (_concreteTypes.ContainsKey(type))
                concreteType = _concreteTypes[type];

            var ctors = concreteType.GetConstructors();
            var parameters = new ParameterInfo[0];
            ConstructorInfo greediestConstructor = null;

            foreach (var constructor in ctors)
            {
                var newParams = constructor.GetParameters();

                if (newParams.Length >= parameters.Length)
                {
                    parameters = newParams;
                    greediestConstructor = constructor;
                }
            }

            Boolean canResolve = true;

            foreach (var p in parameters)
            {
                if (!CanResolve(concreteType, p.ParameterType))
                    canResolve = false;
            }

            if (canResolve)
            {
                return ResolveType(concreteType, greediestConstructor);
            }
            else
            {
                throw new Exception();
            }
        }

        public Configurator SetViewportSize(Int32 width, Int32 height)
        {
            viewPortHeight = height;
            viewPortWidth = width;

            return this;
        }
    }
}
