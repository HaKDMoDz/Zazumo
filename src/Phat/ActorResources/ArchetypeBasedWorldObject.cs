using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Phat.ActorResources
{
    [Serializable]
    public abstract class ArchetypeBasedWorldObject : IWorldObject
    {
        public String ArchetypeKey { get; set; }

#if !WINDOWS_PHONE && !XBOX
        [NonSerialized]
#endif
        private WorldObjectArchetypeData _archetypeData;

#if !MONO
        [ContentSerializerIgnore]
#endif
        public WorldObjectArchetypeData ArchetypeData { get { return _archetypeData; } }

        public virtual void SetArchetypeData(WorldObjectArchetypeData data)
        {
            this._archetypeData = data;
        }

        public String Behavior
        {
            get { return ArchetypeData.Behavior; }
        }

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

        public String Name { get; set; }
        public Single X { get; set; }
        public Single Y { get; set; }
    }
}
