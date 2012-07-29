using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Messages
{
    public class MouseButtonDownEvent
    {
        public enum MouseButton
        {
            Left, 
            Right
        }

        public Double MouseX { get; private set; }
        public Double MouseY { get; private set; }
        public MouseButton Button { get; private set; }

        public MouseButtonDownEvent(Double mouseX, Double mouseY, MouseButton button)
        {
            this.MouseX = mouseX;
            this.MouseY = mouseY;
            this.Button = button;
        }
    }
}
