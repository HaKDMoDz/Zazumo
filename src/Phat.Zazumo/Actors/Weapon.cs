using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phat.Zazumo.Actors
{
    public abstract class Weapon
    {
        public TimeSpan FireDelay { get; protected set; }

        public abstract Vector2[] FindTargets(ZazumoActor zazumoActor, IEnumerable<EnemyActor> enemies);
    }
}
