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
    public class VolumePhysics : PhysicsBase
    {
        private readonly Actor _actor;
        private VolumeWorldObject _resource;
        private Body _body;

        public VolumePhysics(Actor actor, IResourceDictionary resourceDictionary)
            : base(resourceDictionary)
        {
            this._actor = actor;
        }

        public override Body CreateBody(World world)
        {
            var vertices = new Vertices((Vector2[])this.ResourceDictionary.GetResource("UnitHull"));

            var scaledVertices = new List<Vector2>();
            foreach (var v in vertices)
            {
                scaledVertices.Add(new Vector2(v.X * _resource.Width, v.Y * _resource.Height));
            }

            var body = new Body(world);
            body.BodyType = BodyType.Static;

            var shape = new PolygonShape(new Vertices(scaledVertices), 0.0f);            
            
            var fixture = new Fixture(body, shape, 1.0f);
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
            this._resource = (VolumeWorldObject)initializationData;
        }
    }
}
