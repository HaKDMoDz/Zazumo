using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics.Contacts;
using Phat.Actors;
using Phat.Messages;
using Phat.ActorModel;

namespace Phat.Physics
{
    public class PhysicsSubsystem :  IProcess
    {
        private readonly World _world;
        private readonly Dictionary<Actor, IActorPhysics> _actorMap;
        private readonly Dictionary<Actor, Body> _actorBodies;
        private readonly List<Actor> _dynamicActors;
        private readonly List<Func<Boolean>> _queuedActions;
        private readonly IActorPhysicsFactory _physicsFactory;
        private readonly IResourceDictionary _resources;
        private readonly IBus _bus;
        private readonly List<Contact> _currentContacts;

        public PhysicsSubsystem(IActorPhysicsFactory physicsFactory, IResourceDictionary resources, IBus bus)
        {
            _world = new World(Vector2.Zero);
            _actorMap = new Dictionary<Actor, IActorPhysics>();
            _actorBodies = new Dictionary<Actor, Body>();
            _dynamicActors = new List<Actor>();
            _queuedActions = new List<Func<Boolean>>();
            _physicsFactory = physicsFactory;
            _resources = resources;
            _bus = bus;
            _currentContacts = new List<Contact>();

            _world.ContactManager.BeginContact += OnBeginContact;
            //_world.ContactManager.EndContact += OnEndContact;
        }

        public void Initialize()
        {
            
        }        

        public IActorPhysics AddActor(Actor actor, Object initializationData)
        {
            var actorPhysics = _physicsFactory.BuildPhysicsFixture(actor);
            actorPhysics.Initialize(initializationData);
            
            var body = actorPhysics.CreateBody(_world);
            _actorMap[actor] = actorPhysics;

            if (body == null)
                return actorPhysics;

            body.Position = new Vector2(actor.Location.X, actor.Location.Y);
            _actorBodies[actor] = body;
            
            if (body.BodyType != BodyType.Static)
            {
                _dynamicActors.Add(actor);
            }

            return actorPhysics;
        }

        public void RemoveActor(Actor actor)
        {
            if (_actorMap.ContainsKey(actor))
            {
                var actorPhysics = _actorMap[actor];
                actorPhysics.RemoveBodies(_world);
                _actorMap.Remove(actor);
            }

            if (_actorBodies.ContainsKey(actor))
                _actorBodies.Remove(actor);

            if (_dynamicActors.Contains(actor))
                _dynamicActors.Remove(actor);
        }

        public Boolean OnBeginContact(Contact contact)
        {
            var data1 = (CollisionData)contact.FixtureA.UserData;
            var data2 = (CollisionData)contact.FixtureB.UserData;

            var collision = new Collision(data1.Actor, data2.Actor);

            var ev1 = new ActorCollidedEvent(data1.Actor.ActorId, data2.Actor, data2.Data);
            var ev2 = new ActorCollidedEvent(data2.Actor.ActorId, data1.Actor, data1.Data);
            _bus.Publish(ev1, true);
            _bus.Publish(ev2, true);

            return !ev1.Cancel && !ev2.Cancel;
        }
        
        public ProcessState Run(long ticksSinceLastRun)
        {   
            _world.Step(ticksSinceLastRun / 10000000.0f);
         
            for (Int32 index = _queuedActions.Count - 1; index >= 0; index--)
            {
                if (_queuedActions[index].Invoke())
                    _queuedActions.RemoveAt(index);
            }

            foreach (var actor in _dynamicActors)
            {
                ((ILocatable)actor).Location = new Vector3(_actorBodies[actor].Position.X, _actorBodies[actor].Position.Y, 0f);
            }

            return ProcessState.Running;            
        }

        public void ApplyForce(Actor actor, Vector2 force)
        {
            _queuedActions.Add(() =>
            {
                if (actor == null)
                    return true;

                if (!_actorMap.ContainsKey(actor))
                    return false;

                var body = _actorBodies[actor];
                body.ApplyLinearImpulse(ref force);
                return true;
            });
        }

        public void SetPosition(Actor actor, Vector2 position)
        {
            if (actor == null)
                return;

            if (!_actorBodies.ContainsKey(actor))
                return;

            var body = _actorBodies[actor];

            if (body != null)
                body.Position = position;

            ((ILocatable)actor).Location = new Vector3(position.X, position.Y, 0f);
        }

        public void SetVelocity(Actor actor, Vector2 velocity)
        {
            _queuedActions.Add(() => 
                {
                    if (actor == null)
                        return true;

                    if (!_actorMap.ContainsKey(actor))
                        return false;

                    var body = _actorBodies[actor];

                    body.LinearVelocity = velocity;
                    return true;
                });
        }

        public IEnumerable<Actor> GetActorsAtLocation(Vector2 location)
        {
            foreach (var body in _world.BodyList)
            {
                foreach (var fixture in body.FixtureList)
                {
                    if (fixture.TestPoint(ref location))
                        yield return ((CollisionData)fixture.UserData).Actor;
                    
                }
            }
        }
    }
}
