using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Phat.Visual;

namespace Phat
{
    public interface IVisualSubsystem
    {
        IActorVisual AddActor(Actor actor);
        IActorVisual AddActor(Actor actor, Int32 layer);
        IActorVisual AddActor(Actor actor, Object initializationData);
        IActorVisual AddActor(Actor actor, Object initializationData, Int32 layer);

        void Draw(TimeSpan ellapsedTime);
        IEnumerable<VisualHitTestResult> GetActorByHitTest(Vector3 traceFrom, Vector3 traceDirection, Boolean shouldStopOnFirstHit);

        void SetZIndex(IActorVisual actor, Int32 index);

        void Remove(Actor actor);
    }
}
