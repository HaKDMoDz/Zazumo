using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Visual;
using Microsoft.Xna.Framework;

namespace Phat
{
    public interface IActorVisual
    {
        void Draw(TimeSpan ellapsedTime, ViewPort viewPort);
        void HandleEvent(Object @event);
        VisualHitTestResult GetHitResult(Vector2 location, ViewPort viewPort);
        void Initialize(Object initializationData);
    }
}
