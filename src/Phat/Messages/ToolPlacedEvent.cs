using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Messages
{
    public class ToolPlacedEvent
    {
        public ToolType ToolType { get; private set; }
        public Actor ToolActor { get; private set; }
        public Int32 X { get; private set; }
        public Int32 Y { get; private set; }
        public Int32 Index { get; private set; }
        
        public ToolPlacedEvent(ToolType toolType, Actor toolActor, Int32 x, Int32 y, Int32 index)
        {
            this.ToolType = toolType;
            this.ToolActor = toolActor;
            this.X = x;
            this.Y = y;
            this.Index = index;
        }
    }
}
