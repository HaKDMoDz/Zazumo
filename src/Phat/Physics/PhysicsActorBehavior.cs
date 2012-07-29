using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Actors;
using System.Reflection;

namespace Phat.Physics
{
    public class PhysicsActorBehavior
    {
        private readonly Dictionary<Guid, IActorPhysics> _actorMap;
        private readonly PhysicsSubsystem _physicsSubsystem;
        private readonly IResourceDictionary _resources;
        private readonly IActorRepository _repository;

        public PhysicsActorBehavior(PhysicsSubsystem physicsSubsystem, IActorRepository actorRepository, IBus bus, IResourceDictionary resources, IEnumerable<Type> actorEventTypes)
        {
            _physicsSubsystem = physicsSubsystem;
            _resources = resources;
            _repository = actorRepository;
            _actorMap = new Dictionary<Guid, IActorPhysics>();

            bus.Subscribe<ActorCreatedEvent>(x => ActorCreated(x), this, Priority.Low);
            bus.Subscribe<ActorDestroyedEvent>(x => ActorDestroyed(x), this, Priority.High);
            bus.Subscribe<ActorPositionSetEvent>(x => SetPosition(x), this, Priority.High);
            bus.Subscribe<ActorVelocitySetEvent>(x => SetVelocity(x), this, Priority.High);
            bus.Subscribe<ForceAppliedToActorEvent>(x => ForceAppliedToActor(x), this, Priority.High);

            foreach (var type in actorEventTypes)
            {
                bus.Subscribe(type, (Action<Object>)(x => EventHandler(x)), this, Priority.High);
            }
        }

        private void ActorDestroyed(ActorDestroyedEvent @event)
        {
            _physicsSubsystem.RemoveActor(@event.Actor);
            _actorMap.Remove(@event.Actor.ActorId);
        }

        private void ActorCreated(ActorCreatedEvent @event)
        {
            var physics = _physicsSubsystem.AddActor(@event.Actor, @event.InitializationData);
            _actorMap[@event.Actor.ActorId] = physics;
        }

        private void ForceAppliedToActor(ForceAppliedToActorEvent @event)
        {
            var actor = _repository.GetActorById(@event.ActorId);
            _physicsSubsystem.ApplyForce(actor, @event.Force);
        }

        private void SetPosition(ActorPositionSetEvent @event)
        {
            var actor = _repository.GetActorById(@event.ActorId);           
            _physicsSubsystem.SetPosition(actor, @event.Position);
        }

        private void SetVelocity(ActorVelocitySetEvent @event)
        {
            var actor = _repository.GetActorById(@event.ActorId);
            _physicsSubsystem.SetVelocity(actor, @event.Velocity);
        }

        private void EventHandler(Object @event)
        {
            var actorEvent = @event as ActorEvent;

            if (actorEvent == null)
                throw new Exception();

            if (!_actorMap.ContainsKey(actorEvent.ActorId))
                return;

            var physics = _actorMap[actorEvent.ActorId];

            physics.HandleEvent(@event);
        }
    }
}
