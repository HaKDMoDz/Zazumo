using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;

namespace Phat.Zazumo.Resources
{
    public enum ZazumoShape
    {
        None,
        Star,
        Pear,
        Swirl
    }

    public class PowerUpData : CharacterArchetypeData
    {
        public ZazumoShape Shape { get; set; }
    }
}
