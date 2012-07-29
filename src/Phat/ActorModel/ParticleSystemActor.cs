using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectMercury;

namespace Phat.ActorModel
{
    public abstract class ParticleSystemActor : EffectActor
    {
        public Int32 EmissionDuration { get; protected set; }
        public ParticleEffect Effect { get; protected set; }
    }
}
