using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.ActorVisuals
{
    public abstract class VisualBase : IActorVisual
    {
        public IResourceDictionary ResourceDictionary { get; private set; }

        public VisualBase(IResourceDictionary resourceDictionary)
        {
            this.ResourceDictionary = resourceDictionary;
        }

        public abstract void Draw(TimeSpan ellapsedTime, ViewPort viewPort);
        public abstract void HandleEvent(object @event);
        public abstract Phat.Visual.VisualHitTestResult GetHitResult(Microsoft.Xna.Framework.Vector2 location, ViewPort viewPort);
        public abstract void Initialize(Object initializationData);
    }
}
