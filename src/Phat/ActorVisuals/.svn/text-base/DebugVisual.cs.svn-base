using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Phat.Visual;
using Microsoft.Xna.Framework;

namespace Phat.ActorVisuals
{
    public class DebugVisual : VisualBase
    {
        protected readonly Actor _actor;
        protected readonly SpriteBatch _spriteBatch;

        private Object _resource;


        public DebugVisual(Actor actor, SpriteBatch spriteBatch, IResourceDictionary resourceDictionary)
            : base(resourceDictionary)
        {
            this._actor = actor;
            this._spriteBatch = spriteBatch;
        }

        public override void HandleEvent(Object @event)
        {
               
        }

        public override VisualHitTestResult GetHitResult(Vector2 location, ViewPort viewPort)
        {
            return new VisualHitTestResult(this._actor, new Vector2(0f, 0f), false);
        }

        public override void Draw(TimeSpan ellapsedTime, ViewPort viewPort)
        {
           
        }

        public override void Initialize(Object initializationData)
        {
            _resource = initializationData;
        }
    }
}
