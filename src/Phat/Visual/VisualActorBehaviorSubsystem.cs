using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Phat.Actors;

namespace Phat.Visual
{
    public class VisualActorBehaviorSubsystem
    {
        private readonly IVisualSubsystem _visualProcessor;
        private readonly Dictionary<Guid, IActorVisual> _visualMap;
        IResourceDictionary _resources;

        public VisualActorBehaviorSubsystem(IEnumerable<Type> actorEventTypes, IVisualSubsystem visualSubsystem, IBus bus, IResourceDictionary resources)
        {
            _visualProcessor = visualSubsystem;
            _visualMap = new Dictionary<Guid, IActorVisual>();
            _resources = resources;

            bus.Subscribe<ActorCreatedEvent>(x => ActorCreated(x), this, Priority.Low);
            bus.Subscribe<ActorDestroyedEvent>(x => ActorDestroyed(x), this, Priority.Low);

            bus.Subscribe(typeof(ActorSpriteSetEvent), (Action<Object>)(x => EventHandler(x)), this, Priority.High);
            bus.Subscribe(typeof(AnimationStartedEvent), (Action<Object>)(x => EventHandler(x)), this, Priority.High);
            bus.Subscribe(typeof(ActorFrameSetSetEvent), (Action<Object>)(x => EventHandler(x)), this, Priority.High);
            bus.Subscribe(typeof(ActorZIndexSetEvent), (Action<Object>)(x => ZIndexEventHandler((ActorZIndexSetEvent)x)), this, Priority.High);
            
            foreach (var type in actorEventTypes)
            {
                    bus.Subscribe(type, (Action<Object>)(x => EventHandler(x)), this, Priority.High);
            }
        }

        private void ZIndexEventHandler(ActorZIndexSetEvent @event)
        {
            this._visualProcessor.SetZIndex(_visualMap[@event.ActorId], @event.ZIndex);
        }

        private void ActorDestroyed(ActorDestroyedEvent @event)
        {
            _visualMap.Remove(@event.Actor.ActorId);
            _visualProcessor.Remove(@event.Actor);
        }

        private void ActorCreated(ActorCreatedEvent @event)
        {
            IActorVisual visual = null;

            visual = _visualProcessor.AddActor(@event.Actor, @event.InitializationData);
            _visualMap[@event.Actor.ActorId] = visual;
        }

        private void EventHandler(Object @event)
        {
            var actorEvent = @event as ActorEvent;

            if (actorEvent == null)
                throw new Exception();

            if (!_visualMap.ContainsKey(actorEvent.ActorId))
                return;

            var visual = _visualMap[actorEvent.ActorId];

            visual.HandleEvent(@event);
        }
    }
}
