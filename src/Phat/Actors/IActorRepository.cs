using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Phat.Visual;

namespace Phat
{
    public interface IActorRepository
    {
        void AddActor(Actor actor);
        Actor GetActorById(Guid id);
        Actor GetActorByName(String name);
        void RemoveActor(Actor actor);
        
        IEnumerable<Actor> GetActors();
        IEnumerable<TActor> GetAllActors<TActor>()
            where TActor : Actor;

        IEnumerable<VisualHitTestResult> GetActorByVisualHitTest(Vector3 traceFrom, Vector3 traceDirection, Boolean shouldStopOnFirstHit);
        IEnumerable<Actor> GetActorByPhysicalHitTest(Vector2 location);

        
    }
}
