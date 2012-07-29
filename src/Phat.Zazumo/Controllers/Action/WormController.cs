using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Actors;
using Microsoft.Xna.Framework;
using Phat.Zazumo.Actors;

namespace Phat.Zazumo.Controllers.Action
{
    public class WormController : ActorController
    {
        Vector2[] path;
        TimeSpan lifetime;

        protected override void OnInitialize()
        {

        }

        public override void Update(TimeSpan ellapsedTime)
        {
            base.Update(ellapsedTime);

            foreach (var worm in ((WormFlockActor)Actor).Segments)
            {

                if (path == null)
                    path = ((WormActor)worm).Path;

                lifetime = lifetime.Add(ellapsedTime);
                var t = (Single)lifetime.TotalMilliseconds / 1000f;

                // Move Segments.
                for (Int32 wormSegmentIndex = 0; wormSegmentIndex < ((WormActor)worm).Segments.Length; wormSegmentIndex++)
                {
                    var segmentPosition = Math.Max(0f, (t - (wormSegmentIndex) * 0.1f));
                    Int32 pathSegmentIndex = (Int32)Math.Floor(segmentPosition);

                    Int32 a = 0, b = 0, c = 0, d = 0;

                    a = (pathSegmentIndex - 1) % (path.Length - 2) + 2;
                    b = (pathSegmentIndex) % (path.Length - 2) + 2;
                    c = (pathSegmentIndex + 1) % (path.Length - 2) + 2;
                    d = (pathSegmentIndex + 2) % (path.Length - 2) + 2;

                    var position = segmentPosition - (Single)Math.Floor(segmentPosition);
                    var result = Vector2.CatmullRom(path[a], path[b], path[c], path[d], position);
                    ((WormActor)worm).Segments[wormSegmentIndex].SetLocation(result.X, result.Y, 0f);
                }
            }
        }
    }
}
