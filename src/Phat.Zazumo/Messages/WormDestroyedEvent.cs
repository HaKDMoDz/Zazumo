using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Actors;

namespace Phat.Zazumo.Messages
{
    public class WormDestroyedEvent
    {
        public WormActor Worm { get; private set; }

        public WormDestroyedEvent(WormActor worm)
        {
            this.Worm = worm;
        }
    }
}
