using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class ArchetypeBasedRetractableSpikeButtonWorldObject : ArchetypeBasedConcreteWorldObject, IRetractableSpikeButtonWorldObject
    {
        private RetractableSpikeButtonWorldObjectArchetypeData Data
        {
            get { return (RetractableSpikeButtonWorldObjectArchetypeData)base.ArchetypeData; }
        }

        public String AlternateSprite
        {
            get { return Data.AlernateSprite; }
        }
    }
}
