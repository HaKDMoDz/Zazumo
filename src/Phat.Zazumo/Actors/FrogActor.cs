using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;
using Phat.Messages;
using Phat.Zazumo.Resources;
using Phat.Zazumo.Messages;
using Microsoft.Xna.Framework;

namespace Phat.Zazumo.Actors
{
    public class FrogActor : CharacterActor<FrogActor>
    {
        private FrogData _data;
        private Boolean _isPickedUp;

        public FrogEffect Adjustment { get; set; }
        
        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            _isPickedUp = false;

            this._data = (FrogData)initializationData;
            this.Adjustment = _data.FrogEffect;

            this.Handle<ActorCollidedEvent>()
                .ApplyDefaultHandler(OnActorCollided);
        }

        public void OnActorCollided(ActorCollidedEvent @event)
        {
            if (@event.OtherActor is ZazumoActor)
            {
                @event.Cancel = true;

                if (_isPickedUp)
                    return;

                _isPickedUp = true;
                this.Destroy();

                if (_data.FrogEffect == FrogEffect.Grow)
                    ((ZazumoActor)@event.OtherActor).Grow();
                else if (_data.FrogEffect == FrogEffect.Shrink)
                    ((ZazumoActor)@event.OtherActor).Shrink();
                else if (_data.FrogEffect == FrogEffect.Ammo)
                    ((ZazumoActor)@event.OtherActor).GetAmmo();
                else if (_data.FrogEffect == FrogEffect.Points)
                    this.Publish(new BigPointsAwardedEvent(5000, this.Location.X * Settings.MetersToPixels, this.Location.Y * Settings.MetersToPixels, false));
                else if (_data.FrogEffect == FrogEffect.Bomb)
                    this.Publish(new BombAwardedEvent());

                var pickupEffect = ActorFactory.Create<BubbleParticleSystem>(new Object(), new Vector2(this.Location.X, this.Location.Y));
                In(1).Seconds.Run(() => pickupEffect.Destroy());

            }
            else if (@event.OtherActor is ZazumoProjectileActor)
            {
                @event.Cancel = true;
            }
        }
    }
}
