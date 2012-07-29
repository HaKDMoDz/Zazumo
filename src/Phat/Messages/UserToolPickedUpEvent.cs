using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Messages
{
    public class UserToolPickedUpEvent
    {
        public ToolType ToolType { get; private set; }
        public Int32 X { get; private set; }
        public Int32 Y { get; private set; }
        public Int32 Index { get; private set; }
                
        public UserToolPickedUpEvent(ToolType toolType, Int32 x, Int32 y, Int32 index)
        {
            this.ToolType = toolType;
            this.X = x;
            this.Y = y;
            this.Index = index;
        }
    }
}
