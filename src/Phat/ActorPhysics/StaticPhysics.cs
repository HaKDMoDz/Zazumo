using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Physics;
using Phat;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision.Shapes;
using Phat.ActorResources;

namespace Phat.ActorPhysics
{
    public class StaticPhysics : PhysicsBase
    {
        private readonly Actor _actor;
        private IPhysicalObject _resource;
        private Body _body;

        public StaticPhysics(Actor actor, IResourceDictionary resourceDictionary)
            : base (resourceDictionary)
        {
            this._actor = actor;
        }

        public override Body CreateBody(World world)
        {
            var vertices = new Vertices((Vector2[])this.ResourceDictionary.GetResource(_resource.CollisionHullKey));

            var body = new Body(world);
            body.BodyType = BodyType.Static;

            var shape = new PolygonShape(vertices, 1.0f);

            var fixture = new Fixture(body, shape, 1.0f);
            fixture.CollisionGroup = _resource.CollisionGroup;
            fixture.UserData = new CollisionData { Actor = _actor };

            _body = body;
            return body;
        }

        public override void RemoveBodies(World world)
        {
            world.RemoveBody(_body);
        }

        public override void Initialize(Object initializationData)
        {
            this._resource = (IPhysicalObject)initializationData;
        }
    }
}
