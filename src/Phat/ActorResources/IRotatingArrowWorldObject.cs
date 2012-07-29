using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    public interface IRotatingArrowWorldObject : IArrowWorldObject
    {
        String UpSprite { get; }
        String DownSprite { get; }
        String LeftSprite { get; }
        String RightSprite { get; }
    }
}
