using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phat.Zazumo.Actors
{
    public class DefaultWeapon : Weapon
    {
        public DefaultWeapon()
        {
            FireDelay = TimeSpan.FromMilliseconds(600);
        }

        public override Vector2[] FindTargets(ZazumoActor zazumoActor, IEnumerable<EnemyActor> enemies)
        {
            var shortestDistance = Single.PositiveInfinity;
            Vector3 target = Vector3.Zero;

            // Find nearest enemy.
            foreach (var e in enemies)
            {
                if (e.Location.X < -1.0f || e.Location.X > 10.0f || e.Location.Y < -1.0f || e.Location.Y > 6f)
                    continue;

                var distance = (e.Location - zazumoActor.Location).LengthSquared();
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    target = e.Location;
                }

                return new Vector2[] { new Vector2(target.X, target.Y) };
            }

            return new Vector2[] { };

        }
    }
}
