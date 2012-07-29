using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    public interface IRetractableSpikeButtonWorldObject : IConcreteWorldObject
    {
        String AlternateSprite { get; }
    }
}
