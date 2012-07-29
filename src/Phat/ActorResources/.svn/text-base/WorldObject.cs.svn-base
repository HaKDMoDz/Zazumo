using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Phat.ActorResources
{
    [Serializable]
    public abstract class WorldObject : IWorldObject
    {
#if !WINDOWS_PHONE && !XBOX
        [NonSerialized]
#endif
        private String _id;

#if !MONO
        [ContentSerializerIgnore]
#endif
        public String Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String Behavior { get; set; }
        public Single X { get; set; }
        public Single Y { get; set; }
        public String Name { get; set; }
    }
}
