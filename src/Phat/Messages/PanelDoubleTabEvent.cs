using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Messages
{
    public class PanelDoubleTapEvent
    {
        public Single X { get; private set; }
        public Single Y { get; private set; }

        public PanelDoubleTapEvent(Single x, Single y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
