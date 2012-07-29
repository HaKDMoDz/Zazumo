using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class RetractableSpikeWorldObjectArchetypeData : ConcreteWorldObjectArchetypeData
    {
        public SpikePosition SpikePosition { get; set; }
        public String SpikeUpSprite { get; set; }
        public String SpikeDownSprite { get; set; }
    }
}
