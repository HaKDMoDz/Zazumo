using System;
namespace Phat
{
    public interface IActorVisualFactory
    {
        IActorVisual BuildVisual(Phat.Actor actor);
        void RegisterVisualFactory<TActor>(Func<TActor, IActorVisual> factoryMethod) 
            where TActor : Actor;
    }
}
