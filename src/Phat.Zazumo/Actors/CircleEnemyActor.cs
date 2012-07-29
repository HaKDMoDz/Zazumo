using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Resources;
using Phat.Zazumo.Messages;
using Microsoft.Xna.Framework;

namespace Phat.Zazumo.Actors
{
    public class CircleFlockActor : Actor<CircleFlockActor>
    {
        private const Int32 EnemySpawnCount = 4;
        private const Single Radius = 1.25f;
        public CircleEnemy[] Segments { get; private set; }
        private readonly List<CircleEnemy> _deadList = new List<CircleEnemy>();

        public StartPosition StartPosition { get; private set; }

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            var enemyList = new List<CircleEnemy>();

            var data = (CircleShapeData)initializationData;
            this.StartPosition = data.StartPosition;

            for (Int32 i = 0; i < EnemySpawnCount; i++)
            {
                Vector2 position = Vector2.Zero;

                var angle = (Single)Math.PI / 2f / (Single)(EnemySpawnCount - 1) * (Single)i;
                Single y = (Single)Math.Sin(angle) * Radius;
                Single x = (Single)Math.Cos(angle) * Radius;

                switch (data.StartPosition)
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

                CircleEnemy enemy = ActorFactory.Create<CircleEnemy>(Resources.GetResource("CircleData"), position);
                enemy.CanDamagePlayer = false;
                enemy.Opacity = 0f;
                enemyList.Add(enemy);
                enemy.SetProperty("Center", new Vector2(0.5f, 0.5f));
                enemy.AnimateProperty("Rotation", 0f, (Single)Math.PI * 14f, TimeSpan.FromMilliseconds(7000), false);

                enemy.SetFlock(this);
            }

            Segments = enemyList.ToArray();
        }

        public void Hit(CircleEnemy hitEnemy)
        {
            NotifyDeath(hitEnemy);

            if (_deadList.Count >= EnemySpawnCount)
            {
                this.Publish(new EnemyFlockDestroyedEvent(hitEnemy.Location.X * Settings.MetersToPixels, hitEnemy.Location.Y * Settings.MetersToPixels));
                this.Publish(new BigPointsAwardedEvent(1500, hitEnemy.Location.X * Settings.MetersToPixels, hitEnemy.Location.Y * Settings.MetersToPixels, true));
                this.Destroy();
            }
            else
            {
                this.Publish(new EnemyDestoryedEvent(hitEnemy.Location.X * Settings.MetersToPixels, hitEnemy.Location.Y * Settings.MetersToPixels));
                this.Publish(new PointsAwardedEvent(100 * (_deadList.Count + 1), hitEnemy.Location.X * Settings.MetersToPixels, hitEnemy.Location.Y * Settings.MetersToPixels));
            }
        }

        public void NotifyDeath(CircleEnemy hitEnemy)
        {
            if (!_deadList.Contains(hitEnemy))
                _deadList.Add(hitEnemy);

            if (_deadList.Count >= EnemySpawnCount)
                this.Destroy();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }

    public class CircleEnemy : EnemyActor
    {
        public CircleFlockActor Flock { get; private set; }

        protected override void OnInitializing(object initializationData)
        {
            base.OnInitializing(initializationData);
            CanDamagePlayer = false;
        }

        public void SetFlock(CircleFlockActor flock)
        {
            this.Flock = flock;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (!IsDead)
                Flock.NotifyDeath(this);
        }

        protected override void HitInternal()
        {
            if (!CanDamagePlayer)
                return;

            this.IsDead = true;
            Flock.Hit(this);

            Destroy();
        }

        protected override void OnActorCollided(Phat.Messages.ActorCollidedEvent @event)
        {
            base.OnActorCollided(@event);

            if (IsDead)
                return;

            if (@event.OtherActor is WallVolume)
            {
                @event.Cancel = true;
            }
            else if (@event.OtherActor is ZazumoProjectileActor)
            {
                if (!CanDamagePlayer)
                {
                    @event.Cancel = true;
                    return;
                }

                this.IsDead = true;
                Flock.Hit(this);

                Destroy();
            }
        }
    }
}
