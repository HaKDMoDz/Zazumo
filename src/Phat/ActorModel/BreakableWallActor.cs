using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.ActorModel
{
    public class BreakableWallActor : PlaceableActor<BreakableWallActor>, IExplodable, ITouchable
    {
        public void Explode()
        {
            this.OnExplode();
        }

        protected virtual void OnExplode()
        {
            this.Destroy();
        }

        public void Touch(Actor other)
        {
            this.OnTouch(other);
        }

        private void OnTouch(Actor other)
        {
            var mortal = other as IMortal;

            if (mortal == null)
                return;

            mortal.Kill();
        }    
    }
}
