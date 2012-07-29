using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Phat.Zazumo.Resources;
using Phat.Zazumo.Messages;

namespace Phat.Zazumo.Actors
{
    public class TriangleFlockActor : Actor
    {
        private const Int32 EnemySpawnCount = 8;

        public TriangleEnemy[] Segments { get; private set; }
        private readonly List<TriangleEnemy> _deadList = new List<TriangleEnemy>();

        public StartPosition StartPosition { get; private set; }

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            var enemyList = new List<TriangleEnemy>();

            var data = (TriangleShapeData)initializationData;
            StartPosition = data.StartPosition;
          
            for (Int32 i = 0; i < EnemySpawnCount; i++)
            {
                TriangleEnemy triangle = ActorFactory.Create<TriangleEnemy>(Resources.GetResource("TriangleData"), new Vector2(data.StartPosition == StartPosition.Left ? 0.25f : 9.25f, (Single)i * (5.75f / 8f) + 0.25f));
                triangle.Opacity = 0.0f;
                enemyList.Add(triangle);
                triangle.SetFlock(this);
            }

            Segments = enemyList.ToArray();
        }

        public void Hit(TriangleEnemy hitEnemy)
        {
            NotifyDeath(hitEnemy);

            if (_deadList.Count >= EnemySpawnCount)
            {
                this.Publish(new EnemyFlockDestroyedEvent(hitEnemy.Location.X * Settings.MetersToPixels, hitEnemy.Location.Y * Settings.MetersToPixels));
                this.Publish(new BigPointsAwardedEvent(1000, hitEnemy.Location.X * Settings.MetersToPixels, hitEnemy.Location.Y * Settings.MetersToPixels, true));

                this.Destroy();
            }
            else
            {
                this.Publish(new EnemyDestoryedEvent(hitEnemy.Location.X * Settings.MetersToPixels, hitEnemy.Location.Y * Settings.MetersToPixels));
                this.Publish(new PointsAwardedEvent(100 * (_deadList.Count + 1), hitEnemy.Location.X * Settings.MetersToPixels, hitEnemy.Location.Y * Settings.MetersToPixels));

            }
        }

        public void NotifyDeath(TriangleEnemy hitEnemy)
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

    public class TriangleEnemy : EnemyActor
    {
        public TriangleFlockActor Flock { get; private set; }
        

        protected override void OnInitializing(object initializationData)
        {
            base.OnInitializing(initializationData);
            CanDamagePlayer = false;
        }

        public void SetFlock(TriangleFlockActor flock)
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
            if (IsDead)
                return;

            base.OnActorCollided(@event);

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
