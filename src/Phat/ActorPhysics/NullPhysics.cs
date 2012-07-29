using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Physics;
using FarseerPhysics.Dynamics;

namespace Phat.ActorPhysics
{
    public class NullPhysics : IActorPhysics
    {
        public Body CreateBody(World world)
        {
            return null;
        }

        public void RemoveBodies(World world)
        {
        }


        public void Initialize(Object initializationData)
        {
        }

        public void HandleEvent(object @event)
        {
            
        }
    }
}
