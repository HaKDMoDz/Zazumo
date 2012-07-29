using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Physics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using Phat;
using Phat.ActorModel;
using Phat.Actors;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Phat.ActorPhysics;
using Phat.ActorResources;

namespace Phat.ActorPhysics
{
    public class CharacterPhysics : PhysicsBase
    {
        private readonly Actor _actor;
        private CharacterArchetypeData _resource;
        private Body _body;

        public CharacterPhysics(Actor actor, IResourceDictionary resourceDictionary)
            : base(resourceDictionary)
        {
            this._actor = actor;
        }

        public override Body CreateBody(World world)
        {
            var unithull = new Vertices((Vector2[])this.ResourceDictionary.GetResource(_resource.CollisionHullKey));
            
            var scaledVertices = new List<Vector2>();
            foreach (var vertex in unithull)
            {
                scaledVertices.Add(new Vector2(vertex.X * _resource.Width, vertex.Y * _resource.Height));
            }

            var vertices = new Vertices(scaledVertices);

            var body = new Body(world);
            body.BodyType = BodyType.Dynamic;
            body.IgnoreGravity = true;
            body.LinearDamping = 0.0f;
            body.FixedRotation = true;
            body.Mass = 0;
            var location = ((ILocatable)_actor).Location;
            body.Position = new Vector2(location.X, location.Y);

            var shape = new PolygonShape(vertices, 1f);

            var fixture = body.CreateFixture(shape, 0f);
            fixture.CollisionGroup = _resource.CollisionGroup;
            fixture.UserData = new CollisionData { Actor = _actor };
            fixture.OnCollision += OnCollision;
            _body = body;
            return body;
        }

        public Boolean OnCollision(Fixture fixtureA, Fixture fixtureB, Contact manifold)
        {
            return true;
        }

        public override void Initialize(Object initializationData)
        {
            this._resource = (CharacterArchetypeData)initializationData;
        }

        public override void RemoveBodies(World world)
        {
            world.RemoveBody(_body);
        }
    }
}
