using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;

namespace Phat.Zazumo.Resources
{
    public class ProjectileData : IDrawable
    {
        public String SpriteKey { get; set; }
        public Single Height { get; set; }
        public Single Width { get; set; }
        public Single Speed { get; set; }
        public Int16 CollisionGroup { get; set; }
    }
}
