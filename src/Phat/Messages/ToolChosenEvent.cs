using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Messages
{
    public class ToolChosenEvent
    {
        public ToolType ToolType { get; private set; }
        
        public ToolChosenEvent(ToolType toolType)
        {
            this.ToolType = toolType;
        }
    }
}
