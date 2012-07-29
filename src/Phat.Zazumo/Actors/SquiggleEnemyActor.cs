using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Resources;
using Phat.Zazumo.Messages;
using Microsoft.Xna.Framework;

namespace Phat.Zazumo.Actors
{
    public class SquiggleFlockActor : Actor<SquiggleFlockActor>
    {
        private const Int32 EnemySpawnCount = 8;

        public SquiggleEnemy[] Segments { get; private set; }
        private readonly List<SquiggleEnemy> _deadList = new List<SquiggleEnemy>();

        public StartPosition StartPosition { get; private set; }
        
        private static Random rnd = new Random();

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            var enemyList = new List<SquiggleEnemy>();

            var data = (SquiggleShapeData)initializationData;
            StartPosition = data.StartPosition;

            var y = (Single)rnd.NextDouble() * 5.5f;

            for (Int32 i = 0; i < EnemySpawnCount; i++)
            {
                SquiggleEnemy enemy = ActorFactory.Create<SquiggleEnemy>(Resources.GetResource("SquiggleData"), new Vector2((Single)i * (-0.55f) + (StartPosition == StartPosition.Left ? (-0.5f) : 14f), y));
                enemy.IsEven = (i % 2) == 0;
                enemyList.Add(enemy);
                enemy.SetFlock(this);
                enemy.SetProperty("Center", new Vector2(0.5f, 0.5f));
                enemy.AnimateProperty("Rotation", 0f, (Single)Math.PI * 14f, TimeSpan.FromMilliseconds(7000), false);
            }

            Segments = enemyList.ToArray();
        }

        public void Hit(SquiggleEnemy hitEnemy)
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

        public void NotifyDeath(SquiggleEnemy hitEnemy)
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

    public class SquiggleEnemy : EnemyActor
    {
        public Boolean IsEven { get; set; }

        public SquiggleFlockActor Flock { get; private set; }

        protected override void OnInitializing(object initializationData)
        {
            base.OnInitializing(initializationData);
            CanDamagePlayer = false;
        }

        public void SetFlock(SquiggleFlockActor flock)
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
