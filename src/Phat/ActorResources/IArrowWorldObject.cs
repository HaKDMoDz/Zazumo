using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    public enum ArrowDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public interface IArrowWorldObject : IConcreteWorldObject
    {
        ArrowDirection ArrowDirection { get; }
    }
}
