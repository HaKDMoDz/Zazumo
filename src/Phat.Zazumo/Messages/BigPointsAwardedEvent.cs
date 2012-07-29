using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Zazumo.Messages
{
    public class BigPointsAwardedEvent
    {
        public Int32 Score { get; private set; }
        public Single X { get; private set; }
        public Single Y { get; private set; }
        public Boolean IncreasesMultiplier { get; private set; }

        public BigPointsAwardedEvent(Int32 score, Single x, Single y, Boolean increasesMultiplier)
        {
            this.Score = score;
            this.X = x;
            this.Y = y;
            this.IncreasesMultiplier = increasesMultiplier;
        }
    }
}
