using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    public enum MirrorDirection
    {
        Up,
        Down
    }

    public interface IMirrorWorldObject : IConcreteWorldObject
    {
        MirrorDirection MirrorDirection { get; }
    }
}
