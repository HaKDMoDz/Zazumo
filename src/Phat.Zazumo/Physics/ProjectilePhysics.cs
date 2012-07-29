﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorPhysics;
using Phat.Zazumo.Resources;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using Phat.Actors;
using FarseerPhysics.Collision.Shapes;
using Phat.Physics;
using FarseerPhysics.Dynamics.Contacts;

namespace Phat.Zazumo.Physics
{
    public class ProjectilePhysics : PhysicsBase
    {
        private readonly Actor _actor;
        private ProjectileData _resource;
        private Body _body;

        public ProjectilePhysics(Actor actor, IResourceDictionary resourceDictionary)
            : base(resourceDictionary)
        {
            this._actor = actor;
        }

        public override Body CreateBody(World world)
        {
            var unitHull = (Vector2[])ResourceDictionary.GetResource("UnitHull");

            var scaledVertices = new List<Vector2>();
            foreach (var vertex in unitHull)
            {
                scaledVertices.Add(new Vector2(vertex.X * _resource.Width, vertex.Y * _resource.Height));
            }
            
            var vertices = new Vertices(scaledVertices);

            var body = new Body(world);
            body.BodyType = BodyType.Dynamic;
            body.IgnoreGravity = true;
            body.FixedRotation = true;
            body.IsBullet = true;
            body.Mass = 0;
            
            var location = ((ILocatable)_actor).Location;
            body.Position = new Vector2(location.X, location.Y);

            var shape = new PolygonShape(vertices, 1f);

            var fixture = body.CreateFixture(shape, 0f);
            fixture.UserData = new CollisionData { Actor = _actor };
            fixture.CollisionGroup = _resource.CollisionGroup;            
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
            this._resource = (ProjectileData)initializationData;
        }

        public override void RemoveBodies(World world)
        {
            world.RemoveBody(_body);
        }
    }
}
