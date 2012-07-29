using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Resources;
using Phat.Zazumo.Messages;
using Microsoft.Xna.Framework;

namespace Phat.Zazumo.Actors
{
    public class DiamondFlockActor : Actor<DiamondFlockActor>
    {
        private const Int32 EnemySpawnCount = 8;
        private const Single Radius = 1.25f;
        public DiamondEnemy[] Segments { get; private set; }
        private readonly List<DiamondEnemy> _deadList = new List<DiamondEnemy>();


        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            var enemyList = new List<DiamondEnemy>();

            var data = (DiamondShapeData)initializationData;

            for (Int32 i = 0; i < EnemySpawnCount; i++)
            {
                Vector2 position = Vector2.Zero;
                StartPosition startPosition = StartPosition.TopLeft;
                Single angle = 0.0f;

                switch (i % 4)
                {
                    case 0:
                        startPosition = StartPosition.TopLeft;
                        position = new Vector2(-1f - (Single)Math.Floor((Single)i / 4f) * 0.5f, -1f - (Single)Math.Floor((Single)i / 4f) * 1.0f);
                        angle = (Single)Math.PI * -3f / 4f;
                        break;
                    case 1:
                        startPosition = StartPosition.TopRight;
                        position = new Vector2(10.666f + (Single)Math.Floor((Single)i / 4f) * 0.5f, -1f - (Single)Math.Floor((Single)i / 4f) * 1.0f);
                        angle = (Single)Math.PI * 7f / 4f;
                        break;
                    case 2:
                        startPosition = StartPosition.BottomRight;
                        position = new Vector2(10.666f + (Single)Math.Floor((Single)i / 4f) * 0.5f, 6.333f + (Single)Math.Floor((Single)i / 4f) * 1.0f);
                        angle = (Single)Math.PI * 1f / 4f;
                        break;
                    case 3:
                        startPosition = StartPosition.BottomLeft;
                        position = new Vector2(-1f - (Single)Math.Floor((Single)i / 4f) * 0.5f, 6.333f + (Single)Math.Floor((Single)i / 4f) * 1.0f);
                        angle = (Single)Math.PI * 3f / 4f;
                        break;
                }

                DiamondEnemy enemy = ActorFactory.Create<DiamondEnemy>(Resources.GetResource("DiamondData"), position);
                enemy.StartPosition = startPosition;
                enemy.SetProperty("Center", new Vector2(0.5f, 0.5f));
                enemy.SetProperty("Rotation", angle);
                enemyList.Add(enemy);
                enemy.SetFlock(this);
            }

            Segments = enemyList.ToArray();
        }

        public void Hit(DiamondEnemy hitEnemy)
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

        public void NotifyDeath(DiamondEnemy hitEnemy)
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

    public class DiamondEnemy : EnemyActor
    {
        public DiamondFlockActor Flock { get; private set; }
        public StartPosition StartPosition { get; set; }
        
        protected override void OnInitializing(object initializationData)
        {
            base.OnInitializing(initializationData);
            CanDamagePlayer = false;
        }

        public void SetFlock(DiamondFlockActor flock)
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
