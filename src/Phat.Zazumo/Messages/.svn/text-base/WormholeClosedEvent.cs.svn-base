using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Actors;
using Phat.Zazumo.Resources;

namespace Phat.Zazumo.Messages
{
    public class WormholeClosedEvent
    {
        public WormholeActor Wormhole { get; private set; }
        public LevelSpawnData MiniBossData { get; set; }

        public WormholeClosedEvent(WormholeActor wormhole, LevelSpawnData miniBossData)
        {
            this.Wormhole = wormhole;
            this.MiniBossData = miniBossData;
        }
    }
}
