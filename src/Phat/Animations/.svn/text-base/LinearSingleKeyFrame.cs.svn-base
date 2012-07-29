using System;
using System.Net;

namespace Phat.Animations
{
    [Serializable]
    public class LinearSingleKeyFrame : SingleKeyFrame
    {
        public override Single InterpolateValue(Single baseValue, Single keyFrameProgress)
        {
            return ((1f - keyFrameProgress) * baseValue) + (keyFrameProgress * this.Value);            
        }

        public override SingleKeyFrame Clone()
        {
            return new LinearSingleKeyFrame
            {
                KeyTime = new KeyTime { TimeSpan = this.KeyTime.TimeSpan },
                Value = this.Value
            };
        }

    }
}
