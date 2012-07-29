using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;

namespace Phat.ActorModel
{
    public class ShockwaveEffectActor : EffectActor
    {
        private Single _shockwaveDistance;
        public Single ShockwaveDistance
        {
            get { return (Single)this.GetAnimatedValue("ShockwaveDistance", _shockwaveDistance); }
            set { _shockwaveDistance = value; }
        }

        public Single ShockwaveWidth { get; set; }

        public ShockwaveEffectActor()
        {
            this.ShockwaveWidth = 30f;

            this.ShockwaveDistance = 10;
        }        
    }
}
