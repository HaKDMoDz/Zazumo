using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class ArchetypeBasedArrowWorldObject : ArchetypeBasedConcreteWorldObject, IArrowWorldObject
    {
        private ArrowWorldObjectArchetypeData Data
        {
            get { return (ArrowWorldObjectArchetypeData)base.ArchetypeData; }
        }

        public ArrowDirection ArrowDirection
        {
            get { return Data.ArrowDirection; }
        }
    }
}
