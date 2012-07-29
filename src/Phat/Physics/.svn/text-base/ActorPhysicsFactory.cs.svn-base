using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Physics
{
    public class ActorPhysicsFactory : IActorPhysicsFactory
    {
        private readonly Dictionary<Type, Delegate> _factoryMethods;

        public ActorPhysicsFactory()
        {
            _factoryMethods = new Dictionary<Type, Delegate>();
        }

        public IActorPhysics BuildPhysicsFixture(Actor actor)
        {
            var type = actor.GetType();

            while (type != typeof(Object))
            {
                if (_factoryMethods.ContainsKey(type))
                    return (IActorPhysics)_factoryMethods[type].DynamicInvoke(actor);

                type = type.BaseType;
            }

            throw new Exception(String.Format("No physics fixture was defined for actor type {0}.", actor.GetType()));
        }

        public void  RegisterPhysicsFactory<TActor>(Func<TActor,IActorPhysics> factoryMethod) where TActor : Actor
        {
            _factoryMethods[typeof(TActor)] = factoryMethod;
        }
    }
}
