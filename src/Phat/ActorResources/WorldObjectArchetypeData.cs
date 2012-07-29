using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public abstract class WorldObjectArchetypeData : ArchetypeData
    {
        public String Behavior { get; set; }
    }
}
