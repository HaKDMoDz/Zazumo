using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Microsoft.Xna.Framework;

namespace Phat.Messages
{
    public class BombExplodedEvent : ActorEvent
    {
        public Vector2 Epicenter { get; private set; }

        public BombExplodedEvent(Guid actorId, Vector2 epicenter) 
            : base(actorId)
        {
            this.Epicenter = epicenter;
        }
    }
}
