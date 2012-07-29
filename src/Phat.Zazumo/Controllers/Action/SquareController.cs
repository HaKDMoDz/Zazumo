using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Actors;
using Phat.Zazumo.Resources;

namespace Phat.Zazumo.Controllers.Action
{
    public class SquareController : PatternActorController<SquareFlockActor>
    {
        private const Single MovementStep = 0.05f;

        protected override void OnActorInitialized()
        {
            base.OnActorInitialized();

            foreach (var enemy in ((SquareFlockActor)Actor).Segments)
            {
                enemy.CanDamagePlayer = true;
            }
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            base.Update(ellapsedTime);

            foreach (var enemy in ((SquareFlockActor)Actor).Segments)
            {
                if (enemy.Flock.StartPosition == StartPosition.Top)
                    enemy.SetLocation(enemy.Location.X, enemy.Location.Y + MovementStep, 0f);
                else if (enemy.Flock.StartPosition == StartPosition.Bottom)
                    enemy.SetLocation(enemy.Location.X, enemy.Location.Y - MovementStep, 0f);


                if (enemy.Flock.StartPosition == StartPosition.Top && enemy.Location.Y > 10f)
                    enemy.Destroy();

                if (enemy.Flock.StartPosition == StartPosition.Bottom && enemy.Location.Y < 0f)
                    enemy.Destroy();
            }            
        }
    }
}
