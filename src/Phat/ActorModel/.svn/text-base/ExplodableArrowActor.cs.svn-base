using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorModel
{
    public class ExplodableArrowActor : ArrowActor, IExplodable
    {   
        public void Explode()
        {
            this.OnExplode();            
        }

        protected virtual void OnExplode()
        {
            this.Destroy();
        }
    }
}
