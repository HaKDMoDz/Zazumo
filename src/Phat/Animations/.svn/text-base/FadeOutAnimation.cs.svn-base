using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phat.Animations
{
    public class FadeOutAnimation : Animation
    {
        private readonly TimeSpan _duration;

        public FadeOutAnimation(TimeSpan duration)
        {
            if (duration.TotalMilliseconds == 0)
                throw new Exception("animation duration can not be 0ms.");

            this._duration = duration;
        }

        public override void Initialize()
        {
            // Actor.Opacity = 1f;
        }

        protected override Boolean RunImplementaion(Int64 ticks)
        {
            var ellapsedMilliseconds = ((Single)ticks / 10000f);
            var opacityChangePercentage = ellapsedMilliseconds / (Single)_duration.TotalMilliseconds;

            Actor.Opacity -= opacityChangePercentage;

            if (this.Actor.Opacity <= 0f)
            {
                this.Actor.Opacity = 0f;
                return true;
            }

            return false;
        }

        public override String AnimatedProperty
        {
            get
            {
                return "Opacity";
            }            
        }
    }
}
