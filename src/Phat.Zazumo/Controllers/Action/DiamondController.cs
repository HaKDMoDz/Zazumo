using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Actors;
using Phat.Zazumo.Resources;

namespace Phat.Zazumo.Controllers.Action
{
    public class DiamondController : PatternActorController<DiamondFlockActor>
    {
        private const Single MovementStep = 0.05f;

        protected override void OnActorInitialized()
        {
            base.OnActorInitialized();

            foreach (var enemy in ((DiamondFlockActor)Actor).Segments)
            {
                enemy.CanDamagePlayer = true;
            }
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            base.Update(ellapsedTime);

            foreach (var enemy in ((DiamondFlockActor)Actor).Segments)
            {
                switch (enemy.StartPosition)
                {
                    case StartPosition.TopLeft:
                        enemy.SetLocation(enemy.Location.X + MovementStep, enemy.Location.Y + MovementStep, 0f);
                        break;
                    case StartPosition.TopRight:
                        enemy.SetLocation(enemy.Location.X - MovementStep, enemy.Location.Y + MovementStep, 0f);
                        break;
                    case StartPosition.BottomLeft:
                        enemy.SetLocation(enemy.Location.X + MovementStep, enemy.Location.Y - MovementStep, 0f);
                        break;
                    case StartPosition.BottomRight:
                        enemy.SetLocation(enemy.Location.X - MovementStep, enemy.Location.Y - MovementStep, 0f);
                        break;
                    default:
                        break;
                }

                if (enemy.Location.X < -2f || enemy.Location.X > 12f || enemy.Location.Y < -2f || enemy.Location.Y > 8f)
                    enemy.Destroy();
            }
        }
    }
}
