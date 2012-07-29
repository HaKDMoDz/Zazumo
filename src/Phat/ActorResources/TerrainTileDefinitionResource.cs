using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class TerrainTileDefinitionResource : ResourceModel
    {
        public String SpriteKey { get; set; }
        public String CollisionHullKey { get; set; }
        public String CollisionData { get; set; }
        public Int32 Layer { get; set; }
    }
}
