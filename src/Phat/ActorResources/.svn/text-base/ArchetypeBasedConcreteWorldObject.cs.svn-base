using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class ArchetypeBasedConcreteWorldObject : ArchetypeBasedWorldObject, IConcreteWorldObject
    {
        private ConcreteWorldObjectArchetypeData Data
        {
            get { return (ConcreteWorldObjectArchetypeData)base.ArchetypeData; }
        }

        public String SpriteKey
        {
            get { return Data.SpriteKey; }
        }

        public Single Width
        {
            get { return Data.Width; }
        }

        public Single Height
        {
            get { return Data.Height; }
        }

        public String CollisionHullKey
        {
            get { return Data.CollisionHullKey; }
        }
    }
}
