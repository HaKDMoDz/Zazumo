using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Messages
{
    public class LevelSelectedEvent
    {
        public Int32 Level { get; private set; }

        public LevelSelectedEvent(Int32 level)
        {
            this.Level = level;
        }
    }
}
