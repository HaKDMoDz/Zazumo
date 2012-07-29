using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phat.Animations
{
    public class ScrollAnimation : Animation
    {
        private readonly TimeSpan _duration;
        private readonly Vector2 _destination;
        private Single _distance;
        private Vector2 _direction; 

        public ScrollAnimation(Vector2 destination, TimeSpan duration)
        {
            if (duration.TotalMilliseconds == 0)
                throw new Exception("animation duration can not be 0ms.");

            this._destination = destination;
            this._duration = duration;
        }

        public override void Initialize()
        {
            var position = new Vector2(Actor.Location.X, Actor.Location.Y);

            _distance = (position - _destination).Length();
            _direction = (_destination - position);
            _direction.Normalize();
        }

        protected override Boolean RunImplementaion(Int64 ticks)
        {
            var ellapsedMilliseconds = ((Single)ticks / 10000f);
            var changePercentage = ellapsedMilliseconds / (Single)_duration.TotalMilliseconds;

            Actor.Location = new Vector3(Actor.Location.X + (_direction.X * _distance * changePercentage), Actor.Location.Y + (_direction.Y * _distance * changePercentage), 0f);

            if ((_direction.X < 0f && this.Actor.Location.X <= _destination.X) || (_direction.X > 0f && this.Actor.Location.X >= _destination.X) || _direction.X == 0f)
            {
                if ((_direction.Y < 0f && this.Actor.Location.Y <= _destination.Y) || (_direction.Y > 0f && this.Actor.Location.Y >= _destination.Y) || _direction.Y == 0f)
                {
                    this.Actor.Location = new Vector3(_destination.X, _destination.Y, 0f);
                    return true;
                }
            }

            return false;
        }

        public override String AnimatedProperty
        {
            get { return "Location"; }
        }
    }
}
