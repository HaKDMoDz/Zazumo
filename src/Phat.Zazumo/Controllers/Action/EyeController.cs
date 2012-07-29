using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Actors;
using Microsoft.Xna.Framework;
using Phat.Zazumo.Actors;
using Phat.Animations;

namespace Phat.Zazumo.Controllers.Action
{
    public class EyeController : ActorController
    {
        private enum EyeMotionSegment
        {
            FadingIn,
            InitialCollapse,
            Waiting,
            Collapsing,
            Expanding,
            Moving
        }

        // Constants
        private TimeSpan FadeInTime = TimeSpan.FromMilliseconds(1000);
        private TimeSpan InitialCollapseTime = TimeSpan.FromMilliseconds(1500);
        private TimeSpan WaitTime = TimeSpan.FromMilliseconds(2000);
        private TimeSpan ExpandTime = TimeSpan.FromMilliseconds(400);
        private TimeSpan CollapseTime = TimeSpan.FromMilliseconds(400);
        private TimeSpan MovingTime = TimeSpan.FromMilliseconds(9000);
        private Single ExpandDistance = 1.0f;
        private Single RotationStep = 0.05f;

        // private fields
        private Random random;
        private Dictionary<EyeEnemy, Vector2> initialEyePositions;
        private Dictionary<EyeEnemy, Vector2> desiredEyePositions;
        private Dictionary<EyeEnemy, Single> eyeAngles;
        private Single collapseDistance;
        private EyeMotionSegment motionSegment;
        private Vector2[] path;
        private TimeSpan lifetime;
        private TimeSpan segmentLifeTime;

        protected override void OnInitialize()
        {
            random = new Random();
            motionSegment = EyeMotionSegment.FadingIn;
            initialEyePositions = new Dictionary<EyeEnemy, Vector2>();
            desiredEyePositions = new Dictionary<EyeEnemy, Vector2>();
            eyeAngles = new Dictionary<EyeEnemy, Single>();
        } 

        public override void  Update(TimeSpan ellapsedTime)
        {
            base.Update(ellapsedTime);

            InitializeWithActor();

            lifetime = lifetime.Add(ellapsedTime);
            segmentLifeTime = segmentLifeTime.Add(ellapsedTime);

            switch (motionSegment)
            {
                case EyeMotionSegment.FadingIn:
                    if (segmentLifeTime > FadeInTime)
                        EndFadeInSegment();
                    break;

                case EyeMotionSegment.InitialCollapse:
                    if (segmentLifeTime > InitialCollapseTime)
                    {
                        EndInitialCollapseSegment();
                        break;
                    }

                    CollapseEyes(InitialCollapseTime);
                    break;

                case EyeMotionSegment.Waiting:
                    if (segmentLifeTime > WaitTime)
                        EndWaitingSegment();
                    break;

                case EyeMotionSegment.Expanding:
                    if (segmentLifeTime > ExpandTime)
                    {
                        EndExpandingSegment();
                        break;
                    }

                    CollapseEyes(ExpandTime);

                    break;

                case EyeMotionSegment.Moving:
                    if (segmentLifeTime > MovingTime)
                    {
                        EndMovingSegment();
                        break;
                    }

                    MoveEyes(MovingTime);

                    break;

                case EyeMotionSegment.Collapsing:
                    if (segmentLifeTime > CollapseTime)
                    {
                        EndCollapseSegment();
                        break;
                    }

                    CollapseEyes(CollapseTime);
                    break;
                default:
                    break;
            }
        }

        private void MoveEyes(TimeSpan MovingTime)
        {
            // find center position.
            var t = (Single)(segmentLifeTime.TotalMilliseconds / MovingTime.TotalMilliseconds) * (path.Length);
            var segmentPosition = (Single)Math.Max(0f, t);

            Int32 pathSegmentIndex = (Int32)Math.Floor(segmentPosition);

            var a = (pathSegmentIndex - 1) % path.Length; if (a < 0) a += path.Length;
            var b = (pathSegmentIndex) % path.Length;
            var c = (pathSegmentIndex + 1) % path.Length;
            var d = (pathSegmentIndex + 2) % path.Length;

            var position = segmentPosition - (Single)Math.Floor(segmentPosition);
            var result = Vector2.CatmullRom(path[a], path[b], path[c], path[d], position);

            // Set eye locations.
            foreach (var eye in ((EyeFlockActor)Actor).Segments)
            {
                var angle = eyeAngles[eye];
                eye.SetLocation(
                    result.X + ((Single)Math.Cos(angle) * ExpandDistance),
                    result.Y + ((Single)Math.Sin(angle) * ExpandDistance),
                    0f
                );

                eyeAngles[eye] += RotationStep;
            }
        }

        private void EndMovingSegment()
        {
            segmentLifeTime = TimeSpan.Zero;
            BeginCollapseSegment();
        }

        private void EndExpandingSegment()
        {
            segmentLifeTime = TimeSpan.Zero;
            BeginMovingSegment();            
        }

        private void EndFadeInSegment()
        {
            segmentLifeTime = TimeSpan.Zero;
            collapseDistance = (new Vector2(0, 0) - path[0]).Length();

            foreach (var eye in ((EyeFlockActor)Actor).Segments)
            {
                eye.Opacity = 1.0f;
            }

            BeginInitialCollapseSegment();
        }

        private void EndInitialCollapseSegment()
        {
            segmentLifeTime = TimeSpan.Zero;
            BeginWaitingSegment();
        }

        private void EndCollapseSegment()
        {
            segmentLifeTime = TimeSpan.Zero;
            BeginWaitingSegment();
        }

        private void EndWaitingSegment()
        {
            segmentLifeTime = TimeSpan.Zero;
            BeginExpandingSegment();

            ((EyeFlockActor)Actor).StopFiring();
        }

        private void BeginMovingSegment()
        {
            motionSegment = EyeMotionSegment.Moving;

            var pathNumber = random.Next(1, 4);
            path = (Vector2[])ResourceDictionary.GetResource("EyePath" + pathNumber.ToString());

            foreach (var eye in ((EyeFlockActor)Actor).Segments)
            {
                eye.IsInvincible = false;
            }
        }

        private void BeginExpandingSegment()
        {
            motionSegment = EyeMotionSegment.Expanding;

            var expandAngle = (Single)Math.PI / 4f;

            foreach (var eye in ((EyeFlockActor)Actor).Segments)
            {
                initialEyePositions[eye] = new Vector2(eye.Location.X, eye.Location.Y);
                desiredEyePositions[eye] = new Vector2(eye.Location.X + ((Single)Math.Cos(expandAngle) * ExpandDistance), eye.Location.Y + ((Single)Math.Sin(expandAngle) * ExpandDistance));
                eyeAngles[eye] = expandAngle;
                expandAngle += (Single)(Math.PI / 2f);
            }
        }

        private void BeginWaitingSegment()
        {
            motionSegment = EyeMotionSegment.Waiting;

            ((EyeFlockActor)Actor).StartFiring();
            
            foreach (var eye in ((EyeFlockActor)Actor).Segments)
            {
                eye.AnimateProperty("Opacity", 0.5f, 1.0f, TimeSpan.FromMilliseconds(100));
                eye.Open();
            }
        }

        private void BeginInitialCollapseSegment()
        {
            motionSegment = EyeMotionSegment.InitialCollapse;

            foreach (var eye in ((EyeFlockActor)Actor).Segments)
            {
                eye.AnimateProperty("Opacity", 1.0f, 0.5f, TimeSpan.FromMilliseconds(100));
                eye.IsInvincible = true;
                eye.CanDamagePlayer = true;
                initialEyePositions[eye] = new Vector2(eye.Location.X, eye.Location.Y);
                desiredEyePositions[eye] = path[0];
            }
        }

        private void BeginCollapseSegment()
        {
            motionSegment = EyeMotionSegment.Collapsing;

            foreach (var eye in ((EyeFlockActor)Actor).Segments)
            {
                eye.AnimateProperty("Opacity", 1.0f, 0.5f, TimeSpan.FromMilliseconds(100));
                eye.IsInvincible = true;
                initialEyePositions[eye] = new Vector2(eye.Location.X, eye.Location.Y);
                desiredEyePositions[eye] = path[0];
                
                if (!eye.IsClosed)
                    eye.Close();
            }
        }        

        private void InitializeWithActor()
        {
            if (path == null)
            {
                path = (Vector2[])ResourceDictionary.GetResource("EyePath1");

                // Fade In.
                foreach (var eye in ((EyeFlockActor)Actor).Segments)
                {
                    eye.AnimateProperty("Opacity", 0f, 1f, FadeInTime);
                    eye.IsInvincible = true;
                }
            }
        }

        private void CollapseEyes(TimeSpan collapseDuration)
        {
            var t = (Single)(segmentLifeTime.TotalMilliseconds / collapseDuration.TotalMilliseconds);

            foreach (var eye in ((EyeFlockActor)Actor).Segments)
            {
                var x = ((1f - t) * initialEyePositions[eye].X) + (t * desiredEyePositions[eye].X);
                var y = ((1f - t) * initialEyePositions[eye].Y) + (t * desiredEyePositions[eye].Y);
                eye.SetLocation(x, y, 0f);
            }
        }
    }
}
