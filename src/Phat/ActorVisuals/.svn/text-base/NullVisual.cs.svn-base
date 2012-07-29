using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Microsoft.Xna.Framework;

namespace Phat.ActorVisuals
{
    public class NullVisual : IActorVisual
    {
        private readonly Actor _actor;

        public NullVisual(Actor actor)
        {
            this._actor = actor;
        }

        public void Draw(TimeSpan ellapsedTime, ViewPort viewPort)
        {
            
        }

        public void HandleEvent(object @event)
        {
        }

        public Phat.Visual.VisualHitTestResult GetHitResult(Microsoft.Xna.Framework.Vector2 location, ViewPort viewPort)
        {
            return new Phat.Visual.VisualHitTestResult(this._actor, Vector2.Zero, false);
        }


        public void Initialize(object initializationData)
        {

        }
    }
}
