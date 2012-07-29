using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Actors;
using Microsoft.Xna.Framework;
using Phat.Zazumo.Resources;

namespace Phat.Zazumo.Controllers.Action
{
    public class CircleController : PatternActorController<CircleFlockActor>
    {
        protected override void OnActorInitialized()
        {
            base.OnActorInitialized();
            this.SetPatternSegment(new CircleFadeInPatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            base.Update(ellapsedTime);

            foreach (var enemy in ((CircleFlockActor)Actor).Segments)
            {
                if (enemy.Location.X < -1f || enemy.Location.X > 10f || enemy.Location.Y < -1f || enemy.Location.Y > 6f)
                    enemy.Destroy();
            }
        }
    }

    public class CircleFadeInPatternSegment : PatternSegment<CircleFlockActor>
    {
        private readonly TimeSpan Duration = TimeSpan.FromMilliseconds(1000);

        public override void Begin()
        {
            foreach (var enemy in Actor.Segments)
            {
                enemy.AnimateProperty("Opacity", 0.0f, 1.0f, Duration);
            }
        }

        public override void End()
        {
            foreach (var enemy in Actor.Segments)
            {
                enemy.CanDamagePlayer = true;
            }

            base.SetPatternSegment(new CircleMovePatternSegment());
        }

        public override void Update(TimeSpan ellapsedTime)
        {

        }

        public override Boolean IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime >= Duration;
        }
    }


    public class CircleMovePatternSegment : PatternSegment<CircleFlockActor>
    {
        private const Single Step = 0.05f;

        private TimeSpan currentEllapsedTime = TimeSpan.Zero;
        private Single radius;


        public override void Begin()
        {
            radius = 1.25f;
        }

        public override void End()
        {

        }

        public override void Update(TimeSpan ellapsedTime)
        {
            currentEllapsedTime = currentEllapsedTime.Add(ellapsedTime);

            radius += Step;

            for (Int32 index = 0; index < Actor.Segments.Length; index++)
            {
                Vector2 position = Vector2.Zero;

                var angle = (Single)Math.PI / 2f / (Single)(Actor.Segments.Length - 1) * (Single)index;
                Single y = (Single)Math.Sin(angle) * radius;
                Single x = (Single)Math.Cos(angle) * radius;

                switch (Actor.StartPosition)
                {

                    case StartPosition.TopLeft:
                        position = new Vector2(0.5f + x, 0.5f + y);
                        break;
                    case StartPosition.TopRight:
                        position = new Vector2(9f - x, 0.5f + y);
                        break;
                    case StartPosition.BottomLeft:
                        position = new Vector2(0.5f + x, 5f - y);
                        break;
                    case StartPosition.BottomRight:
                        position = new Vector2(9f - x, 5f - y);
                        break;
                }

                Actor.Segments[index].SetLocation(position.X, position.Y, 0f);
            }            
        }

        public override bool IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return false;   
        }
    }
}
