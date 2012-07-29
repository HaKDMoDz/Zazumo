using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat
{
    public interface IActorState
    {
        void SetActor(Actor actor);
        void EnterState();
    }

     public interface IActorState <TActor> : IActorState
        where TActor : Actor<TActor>
    {
         
    }
}
