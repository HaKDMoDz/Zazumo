using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Actors;

namespace Phat.Zazumo.Controllers.Action
{
    public class ArrowController : PatternActorController<ArrowFlockActor>
    {
        private const Single MovementStep = 0.05f;
        private const Single HorizontalMovementStep = 0.05f;

        protected override void OnActorInitialized()
        {
            base.OnActorInitialized();

            foreach (var enemy in ((ArrowFlockActor)Actor).Segments)
            {
                enemy.CanDamagePlayer = true;
            }
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            base.Update(ellapsedTime);

            foreach (var enemy in ((ArrowFlockActor)Actor).Segments)
            {
                enemy.SetLocation(enemy.Location.X, enemy.Location.Y - MovementStep, 0f);

                if (enemy.Location.Y < enemy.BreakOffPoint)
                {
                    if (enemy.Direction == Resources.MoveDirection.Left)
                        enemy.SetLocation(enemy.Location.X - HorizontalMovementStep, enemy.Location.Y, 0f);
                    else
                        enemy.SetLocation(enemy.Location.X + HorizontalMovementStep, enemy.Location.Y, 0f);
                }

                
                if (enemy.Location.Y < -1f)
                    enemy.Destroy();
            }
        }
    }
}
