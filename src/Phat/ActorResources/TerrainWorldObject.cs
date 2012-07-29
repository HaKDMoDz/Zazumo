using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class TerrainWorldObject : WorldObject
    {
        public Int32 Rows { get; set; }
        public Int32 Columns { get; set; }
        public Int32 TileHeight { get; set; }
        public Int32 TileWidth { get; set; }

        public String[] TileDefinitionKeys { get; set; }
    }
}
