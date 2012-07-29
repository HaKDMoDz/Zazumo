using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Actors;
using Phat.Zazumo.Actors;
using Phat.Zazumo.Resources;

namespace Phat.Zazumo.Controllers.Action
{
    public class FrogController : PatternActorController<FrogActor>
    {
        protected override void OnActorInitialized()
        {
            base.OnActorInitialized();
            this.SetPatternSegment(new FrogRestPatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            base.Update(ellapsedTime);

            if (Actor.Location.Y < -2)
                Actor.Destroy();
        }
    }

    public class FrogRestPatternSegment : PatternSegment<FrogActor>
    {
        private TimeSpan PauseDuration = TimeSpan.FromMilliseconds(500);

        public override void Begin()
        {
            if (Actor.Adjustment == FrogEffect.Grow)
            {
                Actor.SetSprite("Zazumo.Sprites.GrowFrog3");
            }
            else if (Actor.Adjustment == FrogEffect.Shrink)
            {
                Actor.SetSprite("Zazumo.Sprites.ShrinkFrog1");
            }
            else if (Actor.Adjustment == FrogEffect.Ammo)
            {
                Actor.SetSprite("Zazumo.Sprites.AmmoFrog1");
            }
            else if (Actor.Adjustment == FrogEffect.Bomb)
            {
                Actor.SetSprite("Zazumo.Sprites.BlueFrog1");
            }
            else if (Actor.Adjustment == FrogEffect.Points)
            {
                Actor.SetSprite("Zazumo.Sprites.BlackFrog1");
            }

        }

        public override void End()
        {
            SetPatternSegment(new FrogJumpPatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            
        }

        public override Boolean IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime >= PauseDuration;
        }
    }

    public class FrogJumpPatternSegment : PatternSegment<FrogActor>
    {
        public TimeSpan JumpDuration = TimeSpan.FromMilliseconds(500);
        public const Single MovementStep = 0.05f;

        public override void Begin()
        {
            if (Actor.Adjustment == FrogEffect.Grow)
            {
                Actor.SetFrameSet("Zazumo.FrameSets.GrowFrog");
            }
            else if (Actor.Adjustment == FrogEffect.Shrink)
            {
                Actor.SetFrameSet("Zazumo.FrameSets.ShrinkFrog");
            }
            else if (Actor.Adjustment == FrogEffect.Ammo)
            {
                Actor.SetFrameSet("Zazumo.FrameSets.AmmoFrog");
            }
            else if (Actor.Adjustment == FrogEffect.Bomb)
            {
                Actor.SetFrameSet("Zazumo.FrameSets.BlueFrog");
            }
            else if (Actor.Adjustment == FrogEffect.Points)
            {
                Actor.SetFrameSet("Zazumo.FrameSets.BlackFrog");
            }
        }

        public override void End()
        {
            SetPatternSegment(new FrogRestPatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            var y = Actor.Location.Y - MovementStep;
            var x = Actor.Location.X;

            Actor.SetLocation(x, y, 0f);
        }

        public override Boolean IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime >= JumpDuration;            
        }
    }
}
