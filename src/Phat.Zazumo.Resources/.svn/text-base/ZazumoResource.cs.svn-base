using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;

namespace Phat.Zazumo.Resources
{
    public interface IZazumoData : IDrawable
    {
        Single Damping { get; }
        Single Speed { get; }
        Single BulletSpeed { get; }
    }

#if !WINDOWS_PHONE
    [Serializable]
#endif
    public class ZazumoArchetypeData : CharacterArchetypeData, IZazumoData
    {
        public Single Damping { get; set; }
        public Single Speed { get; set; }
        public Single BulletSpeed { get; set; }
    }
}
