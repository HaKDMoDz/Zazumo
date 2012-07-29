using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Actors
{
    public interface IActorEventHandler
    {
        Type EventType { get; }
        IEnumerable<Type> HandledStates { get; }

        Delegate GetHandler();
        Delegate GetHandler(Type state);
    }

    public class ActorEventHandler<TActor, TActorEvent> : IActorEventHandler
        where TActor : Actor<TActor>
        where TActorEvent : ActorEvent 
    {
        private readonly Type _eventType;
        private readonly List<Type> _handledStates;
        private readonly Dictionary<Type, Delegate> _stateHandlers;

        private Delegate _defaultHandler;

        public ActorEventHandler()
        {
            _eventType = typeof(TActorEvent);
            _handledStates = new List<Type>();
            _stateHandlers = new Dictionary<Type, Delegate>();
        }

        public ActorEventHandler<TActor, TActorEvent> ApplyStateHandler<TState>(Action<TState, TActorEvent> action)
            where TState : IActorState<TActor>
        {
            _handledStates.Add(typeof(TState));
            _stateHandlers[typeof(TState)] = action;
            return this;
        }

        public ActorEventHandler<TActor, TActorEvent> ApplyDefaultHandler(Action<TActorEvent> action)
        {
            _defaultHandler = action;
            return this;
        }

        Type IActorEventHandler.EventType
        {
            get { return _eventType; }
        }

        IEnumerable<Type> IActorEventHandler.HandledStates
        {
            get { return _handledStates; }
        }

        Delegate IActorEventHandler.GetHandler()
        {
            return _defaultHandler;
        }

        Delegate IActorEventHandler.GetHandler(Type state)
        {
            return _stateHandlers[state];
        }
    }
}
