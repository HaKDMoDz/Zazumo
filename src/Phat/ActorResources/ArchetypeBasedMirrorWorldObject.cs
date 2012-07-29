using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class ArchetypeBasedMirrorWorldObject : ArchetypeBasedConcreteWorldObject, IMirrorWorldObject
    {
        private MirrorWorldObjectArchetypeData Data
        {
            get { return (MirrorWorldObjectArchetypeData)base.ArchetypeData; }
        }

        public MirrorDirection MirrorDirection
        {
            get { return Data.MirrorDirection; }
        }
    }
}
