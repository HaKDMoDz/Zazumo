using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class ArchetypeBasedRotatingArrowWorldObject : ArchetypeBasedArrowWorldObject, IRotatingArrowWorldObject
    {
        private RotatingArrowWorldObjectArchetypeData Data
        {
            get { return (RotatingArrowWorldObjectArchetypeData)base.ArchetypeData; }
        }

        public String UpSprite
        {
            get { return Data.UpSprite; }
        }

        public String DownSprite
        {
            get { return Data.DownSprite; }
        }

        public String LeftSprite
        {
            get { return Data.LeftSprite; }
        }

        public String RightSprite
        {
            get { return Data.RightSprite; }
        }
    }
}
