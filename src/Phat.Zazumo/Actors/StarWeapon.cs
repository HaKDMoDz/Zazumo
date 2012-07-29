using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phat.Zazumo.Actors
{
    public class StarWeapon : Weapon
    {
        private Vector2 _lastTarget;
        private Vector2 _lastPosition;
        
        public StarWeapon ()
	    {
            this.FireDelay = TimeSpan.FromMilliseconds(50);
            this._lastTarget = Vector2.Zero;
            this._lastPosition = Vector2.Zero;

    	}

        public override Vector2[] FindTargets(ZazumoActor zazumoActor, IEnumerable<EnemyActor> enemies)
        {
            var moveDiretion = new Vector2(zazumoActor.Location.X, zazumoActor.Location.Y) - _lastPosition;
            moveDiretion.Normalize();

            _lastPosition = new Vector2(zazumoActor.Location.X, zazumoActor.Location.Y);

            var requestedTarget = new Vector2(moveDiretion.X * -10f, moveDiretion.Y * -6f);

            if (Single.IsNaN(requestedTarget.X) || Single.IsNaN(requestedTarget.Y))
                return new Vector2[]{};

            if (_lastTarget == Vector2.Zero)
            {
                _lastTarget = requestedTarget;
                return new Vector2[] { requestedTarget };
            }
            else
            {
                _lastTarget.Normalize();
                var lastAngle = (Single)(Math.Acos(_lastTarget.X) * (_lastTarget.Y > 0 ? -1.0 : 1.0));

                requestedTarget.Normalize();
                var requestedAngle = (Single)(Math.Acos(requestedTarget.X) * (requestedTarget.Y > 0 ? -1.0 : 1.0));

                Single angle = (0.7f * lastAngle) + (0.3f * requestedAngle);

                var direction = new Vector2((Single)Math.Cos(angle), (Single)Math.Sin(angle) * -1.0f);
                direction.Normalize();
                var newtarget = new Vector2(direction.X * 10f, direction.Y * 6f);
                _lastTarget = newtarget;

                return new Vector2[] { newtarget };
            }
        }
    }
}
