using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class ArchetypeBasedRetractableSpikeWorldObject : ArchetypeBasedConcreteWorldObject, IRetractableSpikeWorldObject
    {
        private RetractableSpikeWorldObjectArchetypeData Data
        {
            get { return (RetractableSpikeWorldObjectArchetypeData)base.ArchetypeData; }
        }

        public SpikePosition SpikePosition
        {
            get { return Data.SpikePosition; }
        }

        public String SpikeUpSprite
        {
            get { return Data.SpikeUpSprite; }
        }

        public String SpikeDownSprite
        {
            get { return Data.SpikeDownSprite; }            
        }
    }
}
