using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat
{
    public abstract class ActorState<TActor> : IActorState<TActor>
        where TActor : Actor<TActor>
    {
        private TActor _actor;

        public abstract void EnterState();

        void IActorState.SetActor(Actor actor)
        {
            _actor = (TActor)actor;
        }

        public TActor Actor { get { return _actor; } }        
    }
}
