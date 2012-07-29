using System;
using System.Net;

namespace Phat.Animations
{
    [Serializable]
    public abstract class SingleKeyFrame
    {
        public KeyTime KeyTime { get; set; }
        public Single Value { get; set; }

        public abstract Single InterpolateValue(Single baseValue, Single keyFrameProgress);

        public abstract SingleKeyFrame Clone();
    }
}
