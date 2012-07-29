using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;
using Phat.Messages;

namespace Phat.Zazumo.Actors
{
    public class ZazumoProjectileActor : ProjectileActor
    {
        public enum BulletSources
        {
            Zazumo,
            Enemy
        }

        public BulletSources BulletSource { get; set; }

        protected override void OnInitializing(object initializationData)
        {
            base.OnInitializing(initializationData);

            Handle<ActorCollidedEvent>()
                .ApplyDefaultHandler(OnActorCollided);
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (var actor in base.AttachedActors)
                actor.Destroy();
        }

        public void OnActorCollided(ActorCollidedEvent @event)
        {
            if (@event.OtherActor is WormholeActor)
            {
                @event.Cancel = true;
                return;
            }
            else if (@event.OtherActor is FrogActor)
            {
                @event.Cancel = true;
                return;
            }
            else if (@event.OtherActor is ProjectileActor)
            {
                @event.Cancel = true;
                return;
            }
            else if (@event.OtherActor is PowerUpActor)
            {
                @event.Cancel = true;
                return;
            }

            if (this.BulletSource == BulletSources.Zazumo)
            {
                if (@event.OtherActor is ZazumoActor)
                {
                    @event.Cancel = true;
                    return;
                }
            }
            else
            {
                if (@event.OtherActor is EnemyActor)
                {
                    @event.Cancel = true;
                    return;
                }
            }

            if (@event.OtherActor is WallVolume)
            {

            }


            this.Destroy();
        }

        public Int32 BulletPower { get; set; }
    }
}
