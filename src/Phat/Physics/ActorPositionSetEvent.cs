using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phat
{
    public class ActorPositionSetEvent
    {
        public Guid ActorId { get; private set; }
        public Vector2 Position { get; private set; }

        public ActorPositionSetEvent(Guid actorId, Vector2 position)
        {
            this.ActorId = actorId;
            this.Position = position;
        }
    }
}
