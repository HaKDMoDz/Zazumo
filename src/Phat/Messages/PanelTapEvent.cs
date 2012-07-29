using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Messages
{
    public class PanelTapEvent
    {
        public Single X { get; private set; }
        public Single Y { get; private set; }

        public PanelTapEvent(Single x, Single y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
