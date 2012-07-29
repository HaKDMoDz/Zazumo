using System;
using System.Net;
using Microsoft.Xna.Framework;

namespace Phat.Animations
{
    public class SplineSingleKeyFrame : SingleKeyFrame
    {
        public Single X1 { get; set; }
        public Single X2 { get; set; }
        public Single Y1 { get; set; }
        public Single Y2 { get; set; }
        
        public override Single InterpolateValue(Single baseValue, Single keyFrameProgress)
        {
            var result = Vector2.Hermite(Vector2.Zero, Vector2.UnitX, new Vector2(X1, Y1), new Vector2(X2, Y2), keyFrameProgress);
            return ((1f - result.X) * baseValue) + (result.X * this.Value);
        }

        public override SingleKeyFrame Clone()
        {
            return new SplineSingleKeyFrame
            {
                X1 = this.X1,
                X2 = this.X2,
                Y1 = this.Y1,
                Y2 = this.Y2,
                KeyTime = new KeyTime { TimeSpan = this.KeyTime.TimeSpan },
                Value = this.Value
            };
        }
    }
}
