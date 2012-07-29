using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Actors;
using Phat.ActorModel;
using Phat.ActorResources;
using Microsoft.Xna.Framework;

namespace Phat.Zazumo.Controllers.Action
{
    public class OwlBombController : PatternActorController<OwlBomb>
    {
        private readonly Single MovementStep = -0.3f;



        protected override void OnActorInitialized()
        {
            base.OnActorInitialized();
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            base.Update(ellapsedTime);

            if (Actor.Location.Y < -6)
                Actor.Destroy();

            Actor.SetLocation(Actor.Location.X, Actor.Location.Y + MovementStep, 0f);
        }
    }
}
