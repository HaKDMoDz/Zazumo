using System;
using FarseerPhysics.Dynamics;
using Phat.Physics;

namespace Phat
{
    public interface IActorPhysicsFactory
    {
        IActorPhysics BuildPhysicsFixture(Actor actor);
        void RegisterPhysicsFactory<TActor>(Func<TActor, IActorPhysics> factoryMethod) 
            where TActor : Actor;
    }
}
