using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Resources;
using Microsoft.Xna.Framework;
using Phat.Zazumo.Messages;

namespace Phat.Zazumo.Actors
{
    public class SquareFlockActor : Actor<SquareFlockActor>
    {
        private const Int32 EnemySpawnCount = 10;

        public SquareEnemy[] Segments { get; private set; }
        private readonly List<SquareEnemy> _deadList = new List<SquareEnemy>();

        public StartPosition StartPosition { get; private set; }

        private static Random rnd = new Random();

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            var enemyList = new List<SquareEnemy>();

            var data = (SquareShapeData)initializationData;
            StartPosition = data.StartPosition;

            var x = (Single)rnd.NextDouble() * 9.5f;

            for (Int32 i = 0; i < EnemySpawnCount; i++)
            {
                SquareEnemy enemy = ActorFactory.Create<SquareEnemy>(Resources.GetResource("SquareData"), new Vector2(x, (Single)i * (-0.55f) + (StartPosition == StartPosition.Top ? (-0.5f) : 11f)));
                enemyList.Add(enemy);
                enemy.SetFlock(this);
            }

            Segments = enemyList.ToArray();
        }

        public void Hit(SquareEnemy hitEnemy)
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

        public void NotifyDeath(SquareEnemy hitEnemy)
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

    public class SquareEnemy : EnemyActor
    {
        public SquareFlockActor Flock { get; private set; }

        protected override void OnInitializing(object initializationData)
        {
            base.OnInitializing(initializationData);
            CanDamagePlayer = false;
        }

        public void SetFlock(SquareFlockActor flock)
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
