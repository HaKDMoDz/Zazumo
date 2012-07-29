using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    public interface IConcreteWorldObject : IWorldObject
    {
        String SpriteKey { get; }
        Single Height { get; }
        Single Width { get; }

        String CollisionHullKey { get; }
    }
}
