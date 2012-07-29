using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class UIResource : ArchetypeData, IDrawable
    {
        public Single X { get; set; }
        public Single Y { get; set; }
        public String Name { get; set; }

        public String SpriteKey { get; set; }
        public Single Width { get; set; }
        public Single Height { get; set; }
    }
}
