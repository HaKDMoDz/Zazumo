using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Actors;

namespace Phat.Zazumo.Controllers.Action
{
    public class BoulderController : PatternActorController<BoulderFlockActor>
    {
        protected override void OnActorInitialized()
        {
            base.OnActorInitialized();
            this.SetPatternSegment(new BoulderFadeInPatternSegment());
        }
    }

    public class BoulderFadeInPatternSegment : PatternSegment<BoulderFlockActor>
    {
        private Random r;
        private TimeSpan FadeInDuration = TimeSpan.FromMilliseconds(1000);

        public BoulderFadeInPatternSegment()
        {
            r = new Random();
        }

        public override void Begin()
        {
            var index = 0;
            var segmentSize = (10.0f / (Single)Actor.Segments.Count()) - 1.0f;

            foreach (var boulder in Actor.Segments)
            {
                var position = (Single)r.NextDouble() * segmentSize + (Single)index * (segmentSize) + (Single)index * 1.0f;
                boulder.AnimateProperty("Opacity", 0.0f, 1.0f, FadeInDuration);
                boulder.SetLocation(position, 0f, 0f);
                boulder.IsInvincible = false;
                boulder.CanDamagePlayer = false;

                index++;
            }
        }

        public override void End()
        {
            SetPatternSegment(new BoulderShakePatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
        }

        public override Boolean IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime >= FadeInDuration;
        }
    }

    public class BoulderShakePatternSegment : PatternSegment<BoulderFlockActor>
    {
        private TimeSpan ShakeDuration = TimeSpan.FromMilliseconds(500);

        private Random r;

        public BoulderShakePatternSegment()
        {
            r = new Random();
        }


        public override void Begin()
        {
            foreach (var boulder in Actor.Segments)
            {
                boulder.IsInvincible = false;
                boulder.CanDamagePlayer = true;
            }
        }

        public override void End()
        {
            SetPatternSegment(new BoulderDropPatterSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            foreach (var boulder in Actor.Segments)
            {
                var currentPosition = boulder.Location;

                var xOffset = ((Single)r.NextDouble() - 0.5f) * 0.1f;
                var yOffset = ((Single)r.NextDouble() - 0.5f) * 0.1f;

                boulder.SetLocation(currentPosition.X + xOffset, currentPosition.Y + yOffset, 0f);
            }
        }

        public override Boolean IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime >= ShakeDuration;
        }
    }

    public class BoulderDropPatterSegment : PatternSegment<BoulderFlockActor>
    {
        private readonly TimeSpan DropDuration = TimeSpan.FromMilliseconds(500);
        public const Single MovementStep = 0.75f;

        public override void Begin()
        {
            
        }

        public override void End()
        {
            SetPatternSegment(new BoulderFadeInPatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            foreach (var boulder in Actor.Segments)
            {
                var y = boulder.Location.Y + MovementStep;
                var x = boulder.Location.X;

                boulder.SetLocation(x, y, 0f);
            }
        }

        public override bool IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime > DropDuration;
        }
    }

}
