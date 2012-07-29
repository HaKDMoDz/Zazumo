using System;
using System.Net;

namespace Phat.Animations
{
    [Serializable]
    public class AnimationClock
    {
        private Int64 _ticks;
        public TimeSpan TimeSpan { get; private set; }
        
        public void Reset()
        {
            _ticks = 0;
            TimeSpan = TimeSpan.Zero;
        }

        public void Update(Int64 ticks)
        {
            _ticks += ticks;

            TimeSpan = TimeSpan.FromTicks(_ticks);
        }
    }
}
