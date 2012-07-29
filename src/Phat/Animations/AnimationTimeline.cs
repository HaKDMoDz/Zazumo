using System;
using System.Net;
using System.Reflection;

namespace Phat.Animations
{
    [Serializable]
    public abstract class AnimationTimeline : Animation
    {
        private Object _initialValue;
        private String _targetProperty;
        private AnimationClock _clock;
        private TimeSpan _duration;
        
        public virtual Object GetCurrentValue(Object defaultOriginValue, Object defaultDestinationValue, AnimationClock animationClock)
        {
            return null;
        }

        public void ApplyAnimationClock(AnimationClock clock)
        {
            this._clock = clock;
        }

        public virtual void BeginAnimation(String targetProperty)
        {
            switch (targetProperty)
            {
                case "X":
                    this._initialValue = Actor.Location.X;
                    break;

                default:
                    break;
            }
            
            this._targetProperty = targetProperty;
            this._duration = GetNaturalDuration();
            Actor.SetAnimation(this);
        }

        public override void Initialize()
        {
        }

        public abstract TimeSpan GetNaturalDuration();

        protected override Boolean RunImplementaion(long ticks)
        {
            var value = GetCurrentValue(_initialValue, null, _clock);
            this.AnimatedValue = value;
            
            switch (_targetProperty)
            {
                case "X":
                    this.Actor.Location = new Microsoft.Xna.Framework.Vector3((Single)value, Actor.Location.Y, 0f);
                    break;

                default:
                    break;
            }

            Boolean iterationComplete = false;
            if (_clock.TimeSpan.TotalMilliseconds > _duration.TotalMilliseconds)
                iterationComplete = true;

            if (!this.ShouldAutoRepeat)
                return iterationComplete;

            if (iterationComplete)
                _clock.Reset();

            return false;
        }

        public override String AnimatedProperty
        {
            get { return _targetProperty; }
        }

        public abstract AnimationTimeline Clone();
        
    }
}
