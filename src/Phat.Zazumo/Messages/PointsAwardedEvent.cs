using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Zazumo.Messages
{
    public class PointsAwardedEvent
    {
        public Int32 Score { get; private set; }
        public Single X { get; private set; }
        public Single Y { get; private set; }

        public PointsAwardedEvent(Int32 score, Single x, Single y)
        {
            this.Score = score;
            this.X = x;
            this.Y = y;
        }
    }
}
