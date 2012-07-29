using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Messages
{
    public class PanelFlickEvent
    {
        public Single DeltaX { get; private set; }
        public Single DeltaY { get; private set; }

        public PanelFlickEvent(Single deltaX, Single deltaY)
        {
            this.DeltaX = deltaX;
            this.DeltaY = deltaY;
        }
    }
}
