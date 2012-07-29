using System;
using System.Net;

namespace Phat.Animations
{
    public class DiscreteSingleKeyFrame : SingleKeyFrame
    {
        public override Single InterpolateValue(Single baseValue, Single keyFrameProgress)
        {
            return baseValue;   
        }

        public override SingleKeyFrame Clone()
        {
            return new DiscreteSingleKeyFrame
            {
                KeyTime = new KeyTime { TimeSpan = this.KeyTime.TimeSpan },
                Value = this.Value
            };
        }
    }
}
