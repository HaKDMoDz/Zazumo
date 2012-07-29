using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class Texture2DResource : ResourceModel
    {
        public String Path { get; set; }
    }
}
