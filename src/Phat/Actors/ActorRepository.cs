using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Phat.Physics;
using Phat.Visual;

namespace Phat.Actors
{
    public class ActorRepository : IActorRepository
    {
        private readonly IDictionary<Guid, Actor> _actors;
        private readonly IVisualSubsystem _visualSubsystem;
        private readonly PhysicsSubsystem _physicsSubsystem;
        
        public ActorRepository()
        {
            _actors = new Dictionary<Guid, Actor>();
        }

        public ActorRepository(IVisualSubsystem visualSubsystem, PhysicsSubsystem physicsSubsystem)
            : this()
        {
            this._visualSubsystem = visualSubsystem;
            this._physicsSubsystem = physicsSubsystem;
        }

        public void AddActor(Actor actor)
        {
            _actors[actor.ActorId] = actor;
        }

        public Actor GetActorById(Guid id)
        {
            if (!_actors.ContainsKey(id))
                return null;

            return _actors[id];
        }

        public IEnumerable<Actor> GetActors()
        {
            return _actors.Values;
        }

        public IEnumerable<TActor> GetAllActors<TActor>() where TActor : Actor
        {
            return _actors.Values.Where(x => typeof(TActor).IsAssignableFrom(x.GetType())).Cast<TActor>();
        }

        public IEnumerable<VisualHitTestResult> GetActorByVisualHitTest(Vector3 traceFrom, Vector3 traceDirection, Boolean shouldStopOnFirstHit)
        {
            if (_visualSubsystem == null)
                throw new Exception("To get actors by a visual hit test please use a constructor of the actor repository that accepts an IVisualSubsystem as an argument.");

            return _visualSubsystem.GetActorByHitTest(traceFrom, traceDirection, shouldStopOnFirstHit);
        }

        public void RemoveActor(Actor actor)
        {
            _actors.Remove(actor.ActorId);
        }

        public IEnumerable<Actor> GetActorByPhysicalHitTest(Vector2 location)
        {
            return _physicsSubsystem.GetActorsAtLocation(location);
        }

        public Actor GetActorByName(String name)
        {
            return _actors.Select(x => x.Value)
                          .Where(x => x.Name == name)
                          .FirstOrDefault();
        }
    }
}
