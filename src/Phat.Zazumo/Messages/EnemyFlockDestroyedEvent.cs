using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Zazumo.Messages
{
    public class EnemyFlockDestroyedEvent
    {
        public Single X { get; private set; }
        public Single Y { get; private set; }

        public EnemyFlockDestroyedEvent(Single x, Single y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
