using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Physics;
using FarseerPhysics.Dynamics;

namespace Phat.ActorPhysics
{
    public abstract class PhysicsBase : IActorPhysics
    {
        public IResourceDictionary ResourceDictionary { get; private set;}

        public PhysicsBase(IResourceDictionary resourceDictionary)
        {
            this.ResourceDictionary = resourceDictionary;
        }

        public abstract Body CreateBody(FarseerPhysics.Dynamics.World world);
        public abstract void RemoveBodies(FarseerPhysics.Dynamics.World world);
        public abstract void Initialize(object initializationData);

        public virtual void HandleEvent(object @event)
        {
            
        }
    }
}
