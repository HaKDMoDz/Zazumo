using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;

namespace Phat.Zazumo.Resources
{
    public class ClamData : CharacterArchetypeData
    {
        public ZazumoShape Shape { get; set; }
        public Int32 HitPoints { get; set; }
    }
}
