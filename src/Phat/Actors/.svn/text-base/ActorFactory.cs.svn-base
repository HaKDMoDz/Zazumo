using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.MessageBus;
using Microsoft.Xna.Framework;
using System.Reflection;
using Phat;
using Phat.ActorResources;

namespace Phat.Actors
{
    /// <summary>
    /// Builds and initializes all actors.
    /// </summary>
    public class ActorFactory : IActorFactory
    {
        private readonly IActorRepository _repository;
        private readonly IBus _bus;
        private readonly IResourceDictionary _resourceDictionary;
        private readonly IProcessRunner _processRunner;
        private readonly Dictionary<String, Type> _typeNameMap;
        
        /// <summary>
        /// Creates a new instance of the <see cref="ActorFactory"/> class.
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="repository"></param>
        /// <param name="resourceDictionary"></param>
        /// <param name="processRunner"></param>
        public ActorFactory(IBus bus, IActorRepository repository, IResourceDictionary resourceDictionary, IProcessRunner processRunner)
        {
            this._bus = bus;
            this._repository = repository;
            this._resourceDictionary = resourceDictionary;
            this._processRunner = processRunner;
            this._typeNameMap = new Dictionary<String, Type>();
        }
        
        /// <summary>
        /// Creates an actor of type TActor with no default data.
        /// </summary>
        /// <typeparam name="TActor">The Type of actor to create</typeparam>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        public TActor Create<TActor>() where TActor : Actor
        {
            var actor = InstantiateActor<TActor>();
            _bus.Publish(new ActorCreatedEvent(actor), true);
            return actor;
        }

        /// <summary>
        /// Creates an actor of type TActor with default data defined in a loaded archetype resource.
        /// </summary>
        /// <typeparam name="TActor">The Type of actor to create.</typeparam>
        /// <param name="archetypeKey">The archetype key to load from resources.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        public TActor Create<TActor>(String archetypeKey) where TActor : Actor
        {
            return this.Create<TActor>(archetypeKey, Vector2.Zero);
        }

        /// <summary>
        /// Creates an actor of type TActor with default data defined in a loaded archetype resource.
        /// </summary>
        /// <typeparam name="TActor">The Type of actor to create.</typeparam>
        /// <param name="archetypeKey">The archetype key to load from resources.</param>
        /// <param name="location">The initial location of the actor.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        public TActor Create<TActor>(String archetypeKey, Vector2 location) where TActor : Actor
        {
            var archetypeResource = (ArchetypeResource)_resourceDictionary.GetResource(archetypeKey);

            var actor = InstantiateActor<TActor>();

            actor.Initialize(archetypeResource.Data);
            actor.SetLocation(location.X, location.Y, 0f);

            _bus.Publish(new ActorCreatedEvent(actor, archetypeResource.Data), true);
            return actor;
        }

        /// <summary>
        /// Creates an actor of type TActor passing in a default data object.
        /// </summary>
        /// <typeparam name="TActor">The Type of actor to create.</typeparam>
        /// <param name="defaultData">The data to initialize the object to.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        public TActor Create<TActor>(Object defaultData) where TActor : Actor
        {
           return Create<TActor>(defaultData, Vector2.Zero);
        }

        /// <summary>
        /// Creates an actor of type TActor passing in a default data object.
        /// </summary>
        /// <typeparam name="TActor">The Type of actor to create.</typeparam>
        /// <param name="defaultData">The data to initialize the object to.</param>
        /// <param name="location">The initial location of the actor.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        public TActor Create<TActor>(Object defaultData, Vector2 location)
            where TActor : Actor
        {
            var actor = InstantiateActor<TActor>();
            actor.Initialize(defaultData);
            actor.SetLocation(location.X, location.Y, 0f);

            _bus.Publish(new ActorCreatedEvent(actor, defaultData), true);
            return actor;
        }

        /// <summary>
        /// Creates an actor.
        /// </summary>
        /// <param name="actorTypeName">The type of actor to instantiate.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        public Actor Create(String actorTypeName)
        {
            if (String.IsNullOrEmpty(actorTypeName))
                throw new Exception("The overload of the create method with no actor type parameter requires an actor type name.");

            var actor = InstantiateActor(_typeNameMap[actorTypeName]);
            _bus.Publish(new ActorCreatedEvent(actor), true);

            return actor;
        }

        /// <summary>
        /// Creates an actor.
        /// </summary>
        /// <param name="actorTypeName">The type of actor to instantiate.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        public Actor Create(String actorTypeName, String archetypeKey)
        {
            return this.Create(actorTypeName, archetypeKey, Vector2.Zero);
        }

        /// <summary>
        /// Creates an actor.
        /// </summary>
        /// <param name="actorTypeName">The type of actor to instantiate.</param>
        /// <param name="archetypeKey">The archetype key to load from resources.</param>
        /// <param name="location">The initial location of the actor.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        public Actor Create(String actorTypeName, String archetypeKey, Vector2 location)
        {
            if (String.IsNullOrEmpty(actorTypeName))
                throw new Exception("The overload of the create method with no actor type parameter requires an actor type name.");

            var actor = InstantiateActor(_typeNameMap[actorTypeName]);

            var archetypeResource = (ArchetypeResource)_resourceDictionary.GetResource(archetypeKey);
            actor.Initialize(archetypeResource.Data);
            actor.SetLocation(location.X, location.Y, 0f);

            _bus.Publish(new ActorCreatedEvent(actor, archetypeResource.Data), true);
            return actor;
        }

        /// <summary>
        /// Creates an actor.
        /// </summary>
        /// <param name="actorTypeName">The type of actor to instantiate.</param>
        /// <param name="defaultData">The data to initialize the object to.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        public Actor Create(String actorTypeName, Object defaultData)
        {
            return Create(actorTypeName, defaultData, Vector2.Zero);
        }

        /// <summary>
        /// Creates an actor.
        /// </summary>
        /// <param name="actorTypeName">The type of actor to instantiate.</param>
        /// <param name="archetypeKey">The archetype key to load from resources.</param>
        /// <param name="defaultData">The data to initialize the object to.</param>
        /// <param name="location">The initial location of the actor.</param>
        /// <returns>Returns an initialized, ready to use actor.</returns>
        public Actor Create(String actorTypeName, Object defaultData, Vector2 location)
        {
            if (String.IsNullOrEmpty(actorTypeName))
                throw new Exception("The overload of the create method with no actor type parameter requires an actor type name.");

            var actor = InstantiateActor(_typeNameMap[actorTypeName]);

            actor.Initialize(defaultData);
            actor.SetLocation(location.X, location.Y, 0f);

            _bus.Publish(new ActorCreatedEvent(actor, defaultData), true);
            return actor;
        }


        private TActor InstantiateActor<TActor>()
            where TActor : Actor
        {
            return (TActor)InstantiateActor(typeof(TActor));
        }

        private Actor InstantiateActor(Type actorType)
        {
            var actor = (Actor)Activator.CreateInstance(actorType);
            _repository.AddActor(actor);
            ((IEventPublisher)actor).SetBus(_bus);
            ((ILatentFunctionProvider)actor).SetProcessRunner(_processRunner);
            ((IActorFactoryProvider)actor).SetActorFactory(this);
            actor.Resources = this._resourceDictionary;
            return actor;
        }

        internal void RegisterActorTypes(Assembly actorAssembly)
        {
            foreach (var type in actorAssembly.GetExportedTypes())
            {
                if (typeof(Actor).IsAssignableFrom(type))
                {
                    _typeNameMap.Add(type.Name, type);

                    if (type.Name == "BreakableWallActor")
                        _typeNameMap.Add("ExplodableWall", type);

                    if (type.Name == "DoorActor")
                        _typeNameMap.Add("LockedDoor", type);

                    if (type.Name.EndsWith("Actor"))
                        _typeNameMap.Add(type.Name.Substring(0, type.Name.Length - "Actor".Length), type);
                }
            }
        }
    }
}
