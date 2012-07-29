using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class ArchetypeBasedBombWorldObject : ArchetypeBasedConcreteWorldObject, IBombWorldObject
    {
        private BombWorldObjectArchetypeData Data
        {
            get { return (BombWorldObjectArchetypeData)base.ArchetypeData; }
        }

        public String ActiveAnimation
        {
            get { return Data.ActiveAnimation; }
        }

        public Int32 Timer
        {
            get { return Data.Timer; }
        }
    }
}
