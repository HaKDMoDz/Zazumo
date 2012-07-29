using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;
using Phat;

namespace Phat.ActorModel
{
    public class SpotlightEffectActor : EffectActor
    {
        private Single _spotlightRadius;
        public Single SpotlightRadius 
        {
            get { return (Single)this.GetAnimatedValue("SpotlightRadius", _spotlightRadius); }
            set { _spotlightRadius = value; }
        }

        public SpotlightEffectActor()
        {
            _spotlightRadius = 50f;
        }        
    }
}
