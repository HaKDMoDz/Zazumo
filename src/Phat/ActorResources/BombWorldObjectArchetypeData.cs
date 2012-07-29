using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class BombWorldObjectArchetypeData : ConcreteWorldObjectArchetypeData
    {
        public String ActiveAnimation { get; set; }
        public Int32 Timer { get; set; }
    }
}
