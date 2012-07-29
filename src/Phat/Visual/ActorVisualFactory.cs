using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Visual
{
    public class ActorVisualFactory : IActorVisualFactory
    {
        private readonly Dictionary<Type, Delegate> _factoryMethods;

        public ActorVisualFactory()
        {
            _factoryMethods = new Dictionary<Type, Delegate>();
        }

        public void RegisterVisualFactory<TActor>(Func<TActor, IActorVisual> factoryMethod)
            where TActor : Actor
        {
            _factoryMethods[typeof(TActor)] = factoryMethod;
        }

        public IActorVisual BuildVisual(Actor actor)
        {
            var type = actor.GetType();

            while (type != typeof(Object))
            {
                if (_factoryMethods.ContainsKey(type))
                    return (IActorVisual)_factoryMethods[type].DynamicInvoke(actor);

                type = type.BaseType;
            }

            throw new Exception(String.Format("No rendering visual was defined for actor type {0}.", actor.GetType()));
        }
    }
}
