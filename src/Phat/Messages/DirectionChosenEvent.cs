using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Messages
{
    public enum Direction
    {
        Up, 
        Down,
        Left,
        Right
    }

    public class DirectionChosenEvent
    {
        public Direction Direction { get; set; }

        public DirectionChosenEvent(Direction direction)
        {
            this.Direction = direction;
        }
    }
}
