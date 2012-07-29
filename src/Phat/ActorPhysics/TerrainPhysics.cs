using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Physics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Phat;
using Phat.ActorModel;
using FarseerPhysics.Common;
using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework;
using Phat.Actors;
using Phat.ActorResources;

namespace Phat.ActorPhysics
{
    public class TerrainPhysics : PhysicsBase
    {
        private TerrainWorldObject _resource;
        private readonly TerrainActor _actor;
        private readonly List<Body> _bodies;

        public TerrainPhysics(TerrainActor actor, IResourceDictionary resourceDictionary)
            : base(resourceDictionary)
        {
            this._actor = actor;
            this._bodies = new List<Body>();
        }

        public override Body CreateBody(World world)
        {
            Int32 count = 0;
            foreach (var key in _resource.TileDefinitionKeys)
            {
                var tileDefinition = (TerrainTileDefinitionResource)ResourceDictionary.GetResource(key);

                if (!String.IsNullOrEmpty(tileDefinition.CollisionHullKey))
                {
                    var vertices = new Vertices((Vector2[])this.ResourceDictionary.GetResource(tileDefinition.CollisionHullKey));

                    var body = new Body(world);
                    _bodies.Add(body);
                    body.BodyType = BodyType.Static;

                    var location = ((ILocatable)_actor).Location;
                    body.Position = new Vector2(((count % _resource.Columns) * _resource.TileWidth + location.X) / Settings.MetersToPixels, ((Int32)(count / _resource.Columns) * _resource.TileHeight + location.Y) / Settings.MetersToPixels);
                    
                    var shape = new PolygonShape(vertices, 0f);
                    var fixture = new Fixture(body, shape, 1.0f);
                    
                    fixture.UserData = new CollisionData { Actor = _actor, Data = tileDefinition.CollisionData };
                }
                
                count++;
            }
            return null;
        }

        public override void RemoveBodies(World world)
        {
            foreach (var body in _bodies)
                world.RemoveBody(body);

            _bodies.Clear();
        }

        public override void Initialize(Object initializationData)
        {
            _resource = (TerrainWorldObject)initializationData;
        }
    }
}
