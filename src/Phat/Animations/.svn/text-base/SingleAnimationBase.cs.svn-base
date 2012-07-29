using System;
using System.Net;
using System.Reflection;

namespace Phat.Animations
{
    [Serializable]
    public abstract class SingleAnimationBase : AnimationTimeline
    {
        protected abstract Single GetCurrentValue(Single defaultOriginValue, Single defaultDestinationValue, AnimationClock animationClock);

        public SingleAnimationBase()
        {

        }

        public override Object GetCurrentValue(Object defaultOriginValue, Object defaultDestinationValue, AnimationClock animationClock)
        {
            return GetCurrentValue(Convert.ToSingle(defaultOriginValue), 0f, animationClock);
        }
    }
}
