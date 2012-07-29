using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Actors;

namespace Phat.Zazumo.Controllers.Action
{
    public class ClamController : PatternActorController<ClamActor>
    {
        protected override void OnActorInitialized()
        {
            base.OnActorInitialized();

            this.SetPatternSegment(new ClamDescendPatternSegment());
        }
    }

    public class ClamDescendPatternSegment : PatternSegment<ClamActor>
    {
        public TimeSpan Duration = TimeSpan.FromMilliseconds(2500);
        public const Single MovementStep = 0.05f;
        
        private Single totalTime = 0f;
        private Single baseX;

        public override void Begin()
        {
            this.baseX = Actor.Location.X;
        }

        public override void End()
        {
            Actor.Open();

            SetPatternSegment(new ClamRestPatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            totalTime += (Single)(ellapsedTime.TotalSeconds) * 3.0f;

            var y = Actor.Location.Y + MovementStep;
            var x = baseX + (Single)Math.Sin(totalTime) * 0.5f;
            
            Actor.SetLocation(x, y, 0f);
        }

        public override Boolean IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime >= Duration;
        }
    }

    public class ClamRestPatternSegment : PatternSegment<ClamActor>
    {
        public TimeSpan Duration = TimeSpan.FromMilliseconds(2000);

        public override void Begin()
        {
        }

        public override void End()
        {
            SetPatternSegment(new ClamAscendPatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {

        }

        public override Boolean IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime >= Duration;
        }
    }

    public class ClamAscendPatternSegment : PatternSegment<ClamActor>
    {
        public TimeSpan Duration = TimeSpan.FromMilliseconds(2000);
        public const Single MovementStep = 0.1f;

        public override void Begin()
        {
        }

        public override void End()
        {
            Actor.Destroy();
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            var y = Actor.Location.Y - MovementStep;
            var x = Actor.Location.X;

            Actor.SetLocation(x, y, 0f);
        }

        public override Boolean IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime >= Duration;
        }
    }
}
