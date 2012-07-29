using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Messages;
using Phat.Zazumo.Resources;
using Microsoft.Xna.Framework;
using Phat.ActorModel;

namespace Phat.Zazumo.Actors
{
    public class ClamActor : CharacterActor<ClamActor>
    {
        private BubbleParticleSystem _bubbles;
        private Int32 _hitpoints;
        private readonly List<ClamItem> _items = new List<ClamItem>();

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            var data = initializationData as ClamData;
            _hitpoints = data.HitPoints;

            _bubbles = ActorFactory.Create<BubbleParticleSystem>(new Object(), new Vector2(7f, 4f));
            _bubbles.AttachTo(this, new Vector2(0.2f, 0.3f));
        }

        public void Open()
        {
            this.SetFrameSet("Zazumo.FrameSets.ClamOpen");

            In(550).Milliseconds.Run(() =>
            {
                this.SetSprite("Zazumo.Sprites.Clam6");

                foreach (var item in _items)
                {
                    if (item is ClamShapeItem)
                    {
                        PowerUpActor itemActor = null;

                        switch (((ClamShapeItem)item).Shape)
                        {
                            case ZazumoShape.None:
                                break;
                            case ZazumoShape.Star:
                                itemActor = ActorFactory.Create<PowerUpActor>(Resources.GetResource("StarPowerUpData"), new Vector2(this.Location.X, this.Location.Y + 0.35f));                                
                                break;
                            case ZazumoShape.Pear:
                                break;
                            case ZazumoShape.Swirl:
                                break;
                            default:
                                break;
                        }

                        itemActor.AnimateProperty("Opacity", 0f, 1f, TimeSpan.FromSeconds(2));
                        itemActor.SetProperty("Center", new Vector2(0.5f, 0.5f));
                    }
                }
            });
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            this._bubbles.Destroy();
            this._bubbles = null;
        }


        protected override void OnActorCollided(ActorCollidedEvent @event)
        {
            base.OnActorCollided(@event);

        }

        public void AddItem(ClamItem item)
        {
            _items.Add(item);
        }
    }

    public abstract class ClamItem
    {

    }

    public class ClamShapeItem : ClamItem
    {
        public ZazumoShape Shape { get; set; }
    }
}
