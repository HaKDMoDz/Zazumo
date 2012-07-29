using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Actors;
using Phat.Zazumo.Resources;

namespace Phat.Zazumo.Controllers.Action
{
    public class SquiggleController: PatternActorController<SquiggleFlockActor>
    {
        private const Single MovementStep = 0.05f;
        private Single yBase = 0f;
        private Single distance = 0f;

        protected override void OnActorInitialized()
        {
            base.OnActorInitialized();

            foreach (var enemy in ((SquiggleFlockActor)Actor).Segments)
            {
                enemy.CanDamagePlayer = true;
                yBase = enemy.Location.Y;
            }
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            distance += 0.05f;
            base.Update(ellapsedTime);

            foreach (var enemy in ((SquiggleFlockActor)Actor).Segments)
            {
                Single yOffset= 0f;

                if (enemy.IsEven)
                {
                    yOffset = ((Single)Math.Min(Math.Max(Math.Cos(distance), -0.5f), 0.5f)) * 1.1f;
                }
                else
                {
                    yOffset = ((Single)Math.Min(Math.Max(Math.Cos(distance + Math.PI), -0.5f), 0.5f)) * 1.1f;
                }

                if (enemy.Flock.StartPosition == StartPosition.Left)
                    enemy.SetLocation(enemy.Location.X + MovementStep, yBase + yOffset, 0f);
                else if (enemy.Flock.StartPosition == StartPosition.Right)
                    enemy.SetLocation(enemy.Location.X - MovementStep, yBase + yOffset, 0f);


                if (enemy.Flock.StartPosition == StartPosition.Left && enemy.Location.X > 11f)
                    enemy.Destroy();

                if (enemy.Flock.StartPosition == StartPosition.Right && enemy.Location.X < 0f)
                    enemy.Destroy();
            }            
        }
    }
}
