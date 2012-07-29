using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Resources;
using Microsoft.Xna.Framework;
using Phat.Zazumo.Messages;


namespace Phat.Zazumo.Actors
{
    public class ArrowFlockActor : Actor<ArrowFlockActor>
    {
        private const Int32 EnemySpawnCount = 6;

        public ArrowEnemy[] Segments { get; private set; }
        private readonly List<ArrowEnemy> _deadList = new List<ArrowEnemy>();

        private static Random rnd = new Random();

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            var enemyList = new List<ArrowEnemy>();

            var data = (ArrowShapeData)initializationData;
            
            var x = (Single)rnd.NextDouble() * 9.2f;

            for (Int32 i = 0; i < EnemySpawnCount; i++)
            {
                ArrowEnemy enemy = ActorFactory.Create<ArrowEnemy>(Resources.GetResource("ArrowData"), new Vector2(x, (Single)i * (-0.55f) + 11f));
                enemy.Direction = ((i % 2) == 0 ? MoveDirection.Left : MoveDirection.Right);
                enemy.BreakOffPoint = (Single)rnd.NextDouble() * 5f;
                enemyList.Add(enemy);
                enemy.SetFlock(this);
            }

            Segments = enemyList.ToArray();
        }

        public void Hit(ArrowEnemy hitEnemy)
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

        public void NotifyDeath(ArrowEnemy hitEnemy)
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

    public class ArrowEnemy : EnemyActor
    {
        public ArrowFlockActor Flock { get; private set; }

        public MoveDirection Direction { get; set; }
        public Single BreakOffPoint { get; set; }

        protected override void OnInitializing(object initializationData)
        {
            base.OnInitializing(initializationData);
            CanDamagePlayer = false;
        }

        public void SetFlock(ArrowFlockActor flock)
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