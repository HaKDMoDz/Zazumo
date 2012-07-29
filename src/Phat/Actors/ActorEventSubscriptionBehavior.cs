using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Phat.MessageBus;
using Phat.Messages;

namespace Phat.Actors
{
    public class ActorEventSubscriptionBehavior
    {
        private readonly IActorRepository _repository;
        private readonly IBus _bus;
        
        public ActorEventSubscriptionBehavior(IEnumerable<Type> eventTypes, IBus bus, IActorRepository repository)
        {
            _repository = repository;

            foreach (var type in eventTypes)
            {
                bus.Subscribe(type, (Action<Object>)(x => EventHandler(x)), this, Priority.High);
            }

            bus.Subscribe(typeof(ActorCollidedEvent), (Action<Object>)(x => EventHandler(x)), this, Priority.High);
            
            bus.Subscribe<ActorDestroyedEvent>(x => ActorDestroyed(x), this, Priority.High);

            _bus = bus;

            bus.Subscribe<ActorCreatedEvent>(x => ActorCreated(x), this);
        }

        private void ActorDestroyed(ActorDestroyedEvent @event)
        {
            _repository.RemoveActor(@event.Actor);
        }

        public void ActorCreated(ActorCreatedEvent @event)
        {
            ((IEventPublisher)@event.Actor).SetBus(_bus);
        }

        public void EventHandler(Object @event)
        {
            var actorEvent = @event as ActorEvent;
            
            if (actorEvent == null)
                throw new Exception();

            var actor = _repository.GetActorById(actorEvent.ActorId);

            if (actor == null)
                return;

            var eventProvider = actor as IActorEventSubscriptionProvider;

            if (eventProvider == null)
                throw new Exception();

            eventProvider.HandleEvent(@event);
        }
    }
}
