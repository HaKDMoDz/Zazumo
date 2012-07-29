using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Physics
{
    public sealed class Collision
    {
        public Actor Actor1 { get; private set; }
        public Actor Actor2 { get; private set; }

        public Collision(Actor actor1, Actor actor2)
        {
            this.Actor1 = actor1;
            this.Actor2 = actor2;
        }

        public override Boolean Equals(Object obj)
        {
            var other = obj as Collision;

            if (other == null)
                return false;

            if (this.Actor1 != other.Actor1)
                return false;

            if (this.Actor2 != other.Actor2)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return this.Actor1.ActorId.GetHashCode() ^ this.Actor2.ActorId.GetHashCode(); 
        }
    }
}
