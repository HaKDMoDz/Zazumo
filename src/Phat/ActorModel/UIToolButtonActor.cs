using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorModel
{
    public class UIToolButtonActor : UIButtonActor
    {
        public Int32 AvailableCount { get; private set; }

        public void SetAvailableCount(Int32 count)
        {
            this.AvailableCount = count;
        }
    }
}
