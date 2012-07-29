using System;
using System.Net;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Phat.Animations
{
    [Serializable]
    public class SingleAnimationUsingKeyFrames : SingleAnimationBase
    {
#if !WINDOWS_PHONE && !XBOX
        [NonSerialized]
#endif
        private Single baseValue;

#if !WINDOWS_PHONE && !XBOX
        [NonSerialized]
#endif
        private Int32 _currentKeyFrameIndex;

#if !WINDOWS_PHONE && !XBOX
        [NonSerialized]
#endif
        private Double _lastKeyFrameTime;
        
        public SingleAnimationUsingKeyFrames()
        {
            baseValue = Single.PositiveInfinity;
            _currentKeyFrameIndex = 0;
            _lastKeyFrameTime = 0;
        }

        public SingleKeyFrame[] KeyFrames { get; set; }


        protected override Single GetCurrentValue(Single defaultOriginValue, Single defaultDestinationValue, AnimationClock animationClock)
        {
            if (baseValue == Single.PositiveInfinity)
            {
                baseValue = defaultOriginValue;
            }

            if (_currentKeyFrameIndex >= (KeyFrames.Count()))
            {
                if (animationClock.TimeSpan.Seconds == 0)
                    this._currentKeyFrameIndex = 0;
                else
                    return baseValue;
            }

            // check if key frame is complete.
            if (animationClock.TimeSpan.TotalMilliseconds >= KeyFrames[_currentKeyFrameIndex].KeyTime.TimeSpan.TotalMilliseconds)
            {
                _lastKeyFrameTime = KeyFrames[_currentKeyFrameIndex].KeyTime.TimeSpan.TotalMilliseconds;
                var value = KeyFrames[_currentKeyFrameIndex].Value;
                baseValue = value;
                _currentKeyFrameIndex++;

                return value;
            }

            var currentKeyFrame = KeyFrames[_currentKeyFrameIndex];

            var ellapsedTime = animationClock.TimeSpan.TotalMilliseconds - _lastKeyFrameTime;
            var totalTime = currentKeyFrame.KeyTime.TimeSpan.TotalMilliseconds - _lastKeyFrameTime;

            if (totalTime == 0)
            {
                return currentKeyFrame.InterpolateValue(baseValue, 1.0f);
            }
            else
            {
                return currentKeyFrame.InterpolateValue(baseValue, (Single)(ellapsedTime / totalTime ));
            }
        }

        public override TimeSpan GetNaturalDuration()
        {
            return KeyFrames.Last().KeyTime.TimeSpan;
        }

        public override AnimationTimeline Clone()
        {
            List<SingleKeyFrame> keyFrames = new List<SingleKeyFrame>();

            foreach (var keyframe in KeyFrames)
            {
                keyFrames.Add(keyframe.Clone());
            }
            return new SingleAnimationUsingKeyFrames
            {
                 KeyFrames = keyFrames.ToArray()
            };
        }
    }
}
