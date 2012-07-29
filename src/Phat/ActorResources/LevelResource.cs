using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class LevelResource : ResourceModel
    {
        public String Name { get; set; }

        public String World { get; set; }

        public Int32 UpArrows { get; set; }
        public Int32 DownArrows { get; set; }
        public Int32 LeftArrows { get; set; }
        public Int32 RightArrows { get; set; }
        public Int32 Bombs { get; set; }
    }
}
