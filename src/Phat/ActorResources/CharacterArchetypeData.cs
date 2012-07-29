using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;

namespace Phat.ActorResources
{

#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class CharacterArchetypeData : ArchetypeData, IDrawable, IPhysicalObject
    {
        public String SpriteKey { get; set; }
        public Single Height { get; set; }
        public Single Width { get; set; }
        public String CollisionHullKey { get; set; }
        public Int16 CollisionGroup { get; set; }
    }
}
