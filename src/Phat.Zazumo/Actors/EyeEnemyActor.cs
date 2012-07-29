using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Phat.Zazumo.Messages;
using Phat.Zazumo.Resources;

namespace Phat.Zazumo.Actors
{ 
    public class EyeFlockActor : Actor
    {
        private TimeSpan _fireDelay = TimeSpan.FromMilliseconds(500);

        private Boolean _isFiring;
        private Int64 _lastFired;
        private Single _fireAngle;
        private Single _bulletSpeed = 3.0f;
        private Single _fireAngleStep = 0.3f;
        private Int32 _bulletCount = 5;

        private const Int32 EnemySpawnCount = 4;

        public EyeEnemy[] Segments { get; private set; }
        private readonly List<EyeEnemy> _deadList = new List<EyeEnemy>();

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            var segmentsList = new List<EyeEnemy>();

            for (Int32 i = 0; i < 4; i++)
            {
                EyeEnemy eye = ActorFactory.Create<EyeEnemy>(Resources.GetResource("EyeData"), new Vector2((i / 2) == 0 ? 0f : 9.0f, (i %2 ) == 0 ? 0f : 5.5f));
                eye.Opacity = 0.0f;
                eye.IsInvincible = true;
                segmentsList.Add(eye);
                eye.SetFlock(this);

                eye.HitPoints = ((EyeData)initializationData).HitPoints;
            }

            _fireAngle = 0.0f;
            segmentsList.Reverse();
            Segments = segmentsList.ToArray();
        }
        
        public void StartFiring()
        {
            if (_isFiring == true)
                return;

            _isFiring = true;

            RunLatent(t =>
            {
                _lastFired += t;

                if (_lastFired > _fireDelay.Ticks)
                {
                    Fire();
                    _lastFired = 0;
                }

                if (_isFiring)
                    return ProcessState.Running;
                else
                    return ProcessState.Completed;
            }
            );
        }

        public void StopFiring()
        {
            if (_isFiring == false)
                return;

            _isFiring = false;

            In((Int32)_fireDelay.TotalMilliseconds).Milliseconds.Run(() => _lastFired = _fireDelay.Ticks + 1);
        }

        private void Fire()
        {
            for (Int32 i = 0; i < _bulletCount; i ++)
            {
                var bulletAngle = _fireAngle + (Single)i * ((Single)Math.PI * 2f / (Single)(_bulletCount));

                var bulletDirection = new Vector2((Single)Math.Cos(bulletAngle), (Single)Math.Sin(bulletAngle) * -1.0f);
                bulletDirection.Normalize();
                var bullet = ActorFactory.Create<ZazumoProjectileActor>(Resources.GetResource("EyeBullet"), new Vector2(5.1f, 2.7f));
                bullet.BulletSource = ZazumoProjectileActor.BulletSources.Enemy;
                bullet.SetFrameSet("Zazumo.FrameSets.EnemyBullet");
                var bulletVelocity = bulletDirection * _bulletSpeed;
                bullet.SetVelocity(bulletVelocity.X, bulletVelocity.Y, 0f);                
            }

            _fireAngle += _fireAngleStep;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (var segment in Segments)
            {
                segment.Destroy();
            }
        }

        public void Hit(EyeEnemy hitEnemy)
        {
            if (!_deadList.Contains(hitEnemy))
                _deadList.Add(hitEnemy);


            if (_deadList.Count >= EnemySpawnCount)
            {
                this.Destroy();
                this.Publish(new MiniBossDestroyedEvent());
                this.Publish(new EnemyFlockDestroyedEvent(hitEnemy.Location.X * Settings.MetersToPixels, hitEnemy.Location.Y * Settings.MetersToPixels));
                this.Publish(new BigPointsAwardedEvent(5000, hitEnemy.Location.X * Settings.MetersToPixels, hitEnemy.Location.Y * Settings.MetersToPixels, true));

            }
            else
            {
                this.Publish(new EnemyDestoryedEvent(hitEnemy.Location.X * Settings.MetersToPixels, hitEnemy.Location.Y * Settings.MetersToPixels));
                this.Publish(new PointsAwardedEvent(500, hitEnemy.Location.X * Settings.MetersToPixels, hitEnemy.Location.Y * Settings.MetersToPixels));
            }
        }
    }

    public class EyeEnemy : EnemyActor
    {
        public Int32 HitPoints { get; set; }

        public EyeFlockActor Flock { get; private set; }
        public Boolean IsClosed { get; private set; }
                
        protected override void OnInitializing(object initializationData)
        {
            base.OnInitializing(initializationData);
            CanDamagePlayer = false;
            IsMiniBoss = true;
        }

        public void Close()
        {
            IsClosed = true;
            SetFrameSet("Zazumo.FrameSets.EyeClose");
            In(200).Milliseconds.Run(() => SetSprite("Zazumo.Sprites.Eye6"));
        }

        public void Open()
        {
            IsClosed = false;
            SetFrameSet("Zazumo.FrameSets.EyeOpen");
            In(200).Milliseconds.Run(() => SetSprite("Zazumo.Sprites.Eye"));
        }

        public void SetFlock(EyeFlockActor flock)
        {
            this.Flock = flock;
        }

        protected override void HitInternal()
        {
            base.HitInternal();

            HitPoints--;

            if (HitPoints == 0)
            {
                Flock.Hit(this);
                this.Destroy();
            }

            IsInvincible = true;
            Close();
        }

        protected override void OnActorCollided(Phat.Messages.ActorCollidedEvent @event)
        {
            base.OnActorCollided(@event);

            if (IsInvincible)
            {
                @event.Cancel = true;
                return;
            }

            if (@event.OtherActor is ZazumoProjectileActor)
            {
                HitPoints--;

                if (HitPoints == 0)
                {
                    Flock.Hit(this);
                    this.Destroy();
                }

                IsInvincible = true;
                Close();
            }
        }
    }
}
