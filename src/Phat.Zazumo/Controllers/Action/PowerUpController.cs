using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Actors;
using Microsoft.Xna.Framework;

namespace Phat.Zazumo.Controllers.Action
{
    public class PowerUpController : PatternActorController<PowerUpActor>
    {
        protected override void OnActorInitialized()
        {
            base.OnActorInitialized();
            this.SetPatternSegment(new PowerUpJumpPatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            base.Update(ellapsedTime);
        }
    }

    public class PowerUpRestPatternSegment : PatternSegment<PowerUpActor>
    {
        private TimeSpan PauseDuration = TimeSpan.FromMilliseconds(250);

        public override void Begin()
        {
        }

        public override void End()
        {
            if (!Actor.IsHeld)
                SetPatternSegment(new PowerUpJumpPatternSegment());
            else
                SetPatternSegment(new PowerUpHeldPatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {

        }

        public override Boolean IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime >= PauseDuration || Actor.IsHeld;
        }
    }

    public class PowerUpJumpPatternSegment : PatternSegment<PowerUpActor>
    {
        private static Random r = new Random();

        public TimeSpan JumpDuration = TimeSpan.FromMilliseconds(1500);
        private Single _destinationX;
        private Single _destinationY;
        private Single _sourceX;
        private Single _sourceY;
        private Single _height;
        private Single _totalTime;

        private Actor _trail;

        public override void Begin()
        {            
            this._destinationX = (Single)r.NextDouble() * 9f + 0.25f;
            this._destinationY = (Single)r.NextDouble() * 5f + 0.25f;
            this._height = (Single)r.NextDouble() * 3f;

            this._sourceX = Actor.Location.X;
            this._sourceY = Actor.Location.Y;

            this._trail = base.ParentController.ActorFactory.Create<PowerUpTrailParticleSystemActor>(new Object());
            this._trail.AttachTo(Actor, new Vector2(0.5f, 0.5f));
        }

        public override void End()
        {
            this._trail.Destroy();
            if (!Actor.IsHeld)
                SetPatternSegment(new PowerUpRestPatternSegment());
            else
                SetPatternSegment(new PowerUpHeldPatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            _totalTime += (Single)ellapsedTime.TotalSeconds;
            Single t = _totalTime / (Single)JumpDuration.TotalSeconds;

            var x = (1f - t) * _sourceX + t * _destinationX;
            var y1 = (1f - t) * (_sourceY + ((1f - 2f * t) * (1f - 2f * t) * this._height) - _height);
            var y2 = (t) * (_destinationY + ((1f - 2f * t) * (1f - 2f * t) * this._height) - _height);
            Actor.SetLocation(x, y1 + y2, 0f);
        }

        public override Boolean IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime >= JumpDuration || Actor.IsHeld;
        }
    }

    public class PowerUpHeldPatternSegment : PatternSegment<PowerUpActor>
    {
        public override void Begin()
        {
        }

        public override void End()
        {
            SetPatternSegment(new PowerUpRestPatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
        }

        public override Boolean IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return !Actor.IsHeld;
        }
    }
}
