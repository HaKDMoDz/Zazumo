using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Actors;
using Phat.Zazumo.Messages;
using Microsoft.Xna.Framework;

namespace Phat.Zazumo.Controllers.Action
{
    public class SquidController : PatternActorController<SquidBoss>, IHandle<BossHitEvent>
    {
        protected override void OnActorInitialized()
        {
            base.OnActorInitialized();

            var index = 0;

            ((SquidBoss)Actor).BuildArms();

            foreach (var segment in ((SquidBoss)Actor).Segments)
            {
                if (index < ((SquidBoss)Actor).Segments.Count() / 2)
                    segment.SetLocation(Actor.Location.X - (Single)index * 0.2f - 0.0f, Actor.Location.Y + 0.95f, 0f);
                else
                    segment.SetLocation(Actor.Location.X + ((Single)index - ((SquidBoss)Actor).Segments.Count() / 2) * 0.2f + 0.7f, Actor.Location.Y + 0.95f, 0f);

                index++;
            }

            SetPatternSegment(new SquidFadeInPattern());
        }

        public void Handle(BossHitEvent @event)
        {
            // this.SetPatternSegment(
        }

        public ZazumoActor Zazumo { get; set; }
    }

    public class SquidFadeInPattern : PatternSegment<SquidBoss>
    {
        private readonly TimeSpan FadeInDuration = TimeSpan.FromMilliseconds(1000);

        public override void Begin()
        {
            Actor.AnimateProperty("Opacity", 0.0f, 1.0f, FadeInDuration);

            foreach (var segment in Actor.Segments)
            {
                segment.AnimateProperty("Opacity", 0.0f, 1.0f, FadeInDuration);
            }
        }

        public override void End()
        {
            Actor.CanDamagePlayer = true;
            SetPatternSegment(new SquidMoveAndFlailPattern(new SquidArmFlailSubPattern(Actor)));
        }

        public override void Update(TimeSpan ellapsedTime) { }

        public override bool IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime > FadeInDuration;
        }
    }

    public class SquidMoveAndFlailPattern : PatternSegment<SquidBoss>
    {
        private TimeSpan MoveDuration = TimeSpan.FromMilliseconds(3500);
        public const Single MovementStep = 0.1f;
        public const Single ArmAngleStep = 0.125f;

        private Boolean _isMovingUp;
        private SquidArmFlailSubPattern _armPattern;


        public SquidMoveAndFlailPattern(SquidArmFlailSubPattern armPattern)
        {
            this._armPattern = armPattern;
        }

        public override void Begin()
        {
            Actor.ArmAngleA = new Vector2(-1.75f, 0f);
            Actor.ArmAngleB = Vector2.Zero;
            Actor.ArmAngleC = new Vector2(0.2f * (Actor.Segments.Count() / 2f), 0f);
            Actor.ArmAngleD = new Vector2(0.2f * (Actor.Segments.Count() / 2f), 0f);
            _isMovingUp = true;

            MoveDuration = MoveDuration.Subtract(TimeSpan.FromMilliseconds((Actor.AngerLevel - 1f) * 3000));
        }

        public override void End()
        {            
            if (Actor.HitPoints > 0)
                SetPatternSegment(new SquidStopAndShootPattern(_armPattern));
            else
                SetPatternSegment(new SquidMoveToCenterPattern(_armPattern));
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            if (_isMovingUp)
            {
                Actor.SetLocation(Actor.Location.X, Actor.Location.Y - MovementStep * Actor.AngerLevel, 0f);

                if (Actor.Location.Y < 0.5f)
                {
                    _isMovingUp = false;
                }
            }
            else
            {
                Actor.SetLocation(Actor.Location.X, Actor.Location.Y + MovementStep * Actor.AngerLevel, 0f);

                if (Actor.Location.Y > 4.0f)
                {
                    _isMovingUp = true;
                }
            }

            _armPattern.Update();
        }

        public override bool IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime > MoveDuration || Actor.HitPoints == 0;
        }
    }

    public class SquidStopAndShootPattern : PatternSegment<SquidBoss>
    {
        private TimeSpan ShootDuration = TimeSpan.FromMilliseconds(2500);
        private SquidArmFlailSubPattern _armPattern;

        public SquidStopAndShootPattern(SquidArmFlailSubPattern armPattern)
        {
            this._armPattern = armPattern;
        }

        public override void Begin()
        {
            Actor.OpenMouth();
            Actor.StartMassFiring();
            ShootDuration = ShootDuration.Add(TimeSpan.FromMilliseconds((Actor.AngerLevel - 1f) * 3000));
        }

        public override void End()
        {
            if (Actor.HitPoints > 0)
                Actor.CloseMouth();

            Actor.StopFiring();

            if (Actor.HitPoints > 0)
                SetPatternSegment(new SquidMoveAndFlailPattern(_armPattern));
            else
                SetPatternSegment(new SquidMoveToCenterPattern(_armPattern));
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            this._armPattern.Update();
        }

        public override bool IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime > ShootDuration || Actor.HitPoints == 0;
        }
    }

    public class SquidArmFlailSubPattern
    {
        public const Single ArmAngleStep = 0.125f;
        private Boolean _isArmAngleIncreasing;
        private Single _armAngle = 0f;
        private readonly SquidBoss _actor;

        public SquidArmFlailSubPattern(SquidBoss actor)
        {
            this._armAngle = 0.0f;
            this._isArmAngleIncreasing = true;
            this._actor = actor;
        }

        public void Update()
        {
            if (_isArmAngleIncreasing)
            {
                _armAngle += ArmAngleStep;

                if (_armAngle > (Math.PI / 2f))
                    _isArmAngleIncreasing = false;
            }
            else
            {
                _armAngle -= ArmAngleStep;

                if (_armAngle < (-Math.PI / 2f))
                    _isArmAngleIncreasing = true;
            }

            Int32 index = 0;
            var armLength = 0.2f * (_actor.Segments.Count() / 2f);

            _actor.ArmAngleC = new Vector2(armLength * (Single)Math.Cos(_armAngle), armLength * (Single)Math.Sin(_armAngle));
            _actor.ArmAngleD = new Vector2((armLength + 0.0f) * (Single)Math.Cos(_armAngle), (armLength + 0.0f) * (Single)Math.Sin(_armAngle));

            foreach (var segment in (_actor.Segments))
            {
                if (index < (_actor.Segments.Count() / 2))
                {
                    var segmentPosition = Vector2.CatmullRom(_actor.ArmAngleA, _actor.ArmAngleB, _actor.ArmAngleC, _actor.ArmAngleD, ((Single)index) / (Single)_actor.Segments.Count() * 2.0f);
                    segment.SetLocation(_actor.Location.X - segmentPosition.X, _actor.Location.Y + segmentPosition.Y + 0.95f, 0f);
                }
                else
                {
                    var segmentPosition = Vector2.CatmullRom(_actor.ArmAngleA, _actor.ArmAngleB, _actor.ArmAngleC, _actor.ArmAngleD, (((Single)index) - ((Single)_actor.Segments.Count() / 2f)) / (Single)_actor.Segments.Count() * 2.0f);
                    segment.SetLocation(_actor.Location.X + segmentPosition.X + 0.7f, _actor.Location.Y - segmentPosition.Y + 0.95f, 0f);
                }

                index++;
            }
        }
    }

    public class SquidMoveToCenterPattern : PatternSegment<SquidBoss>
    {
        public const Single MovementStep = 0.05f;
        public const Single DesiredX = 4.5f;
        public const Single DesiredY = 2.125f;

        private readonly SquidArmFlailSubPattern _armPattern;

        private Vector2 _velocity;

        public SquidMoveToCenterPattern(SquidArmFlailSubPattern armPattern)
        {
            this._armPattern = armPattern;
        }

        public override void Begin()
        {
            Actor.IsInvincible = true;
            var direction = (new Vector3(DesiredX, DesiredY, 0)) - Actor.Location;
            direction.Normalize();
            this._velocity = new Vector2(direction.X * MovementStep, direction.Y * MovementStep);
        }

        public override void End()
        {
            SetPatternSegment(new SquidBrainWigglePattern(this._armPattern));
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            Actor.SetLocation(Actor.Location.X + this._velocity.X, Actor.Location.Y + this._velocity.Y, 0f);
            _armPattern.Update();
        }

        public override bool IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return (Actor.Location.X - DesiredX) <= MovementStep && (Actor.Location.Y - DesiredY) <= MovementStep;
        }
    }

    public class SquidBrainWigglePattern : PatternSegment<SquidBoss>
    {
        private readonly SquidArmFlailSubPattern _armPattern;

        public TimeSpan Duration = TimeSpan.FromSeconds(1.0);

        public SquidBrainWigglePattern(SquidArmFlailSubPattern armPattern)
        {
            this._armPattern = armPattern;
        }

        public override void Begin()
        {
            Actor.SetFrameSet("Zazumo.FrameSets.SquidBrainWiggle");
        }

        public override void End()
        {
            SetPatternSegment(new SquidArmRetractPattern());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            this._armPattern.Update();
        }

        public override Boolean IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime > Duration;
        }
    }

    public class SquidArmRetractPattern : PatternSegment<SquidBoss>
    {
        public TimeSpan Duration = TimeSpan.FromMilliseconds(750);
        private Single _totalTime = 0f;

        public override void Begin()
        {
            Actor.SetSprite("Zazumo.Sprites.Squid1");

            foreach (var segment in Actor.Segments)
            {
                segment.AnimateProperty("Opacity", 1.0f, 0.0f, Duration);
            }
        }

        public override void End()
        {
            SetPatternSegment(new SquidRegrowArmsPattern());
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            Int32 index = 0;
            var armLength = 0.2f * (Actor.Segments.Count() / 2f);

            _totalTime += (Single)ellapsedTime.TotalSeconds;
            Single retractionPosition = 1.0f - (_totalTime / (Single)Duration.TotalSeconds);
            
            foreach (var segment in (Actor.Segments))
            {
                if (index < (Actor.Segments.Count() / 2))
                {
                    var segmentPosition = Vector2.CatmullRom(Actor.ArmAngleA, Actor.ArmAngleB, Actor.ArmAngleC, Actor.ArmAngleD, ((Single)index) / ((Single)Actor.Segments.Count() * 2.0f) * retractionPosition);
                    segment.SetLocation(Actor.Location.X - segmentPosition.X, Actor.Location.Y + segmentPosition.Y + 0.95f, 0f);
                }
                else
                {
                    var segmentPosition = Vector2.CatmullRom(Actor.ArmAngleA, Actor.ArmAngleB, Actor.ArmAngleC, Actor.ArmAngleD, ((((Single)index) - ((Single)Actor.Segments.Count() / 2f)) / (Single)Actor.Segments.Count() * 2.0f) * retractionPosition);
                    segment.SetLocation(Actor.Location.X + segmentPosition.X + 0.7f, Actor.Location.Y - segmentPosition.Y + 0.95f, 0f);
                }

                index++;
            }
        }

        public override bool IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime > Duration;
        }
    }

    public class SquidRegrowArmsPattern : PatternSegment<SquidBoss>
    {
        public TimeSpan Duration = TimeSpan.FromMilliseconds(750);
        private Single _totalTime = 0f;

        public override void Begin()
        {
            Actor.RebuildArms();

            foreach (var segment in Actor.Segments)
            {
                segment.AnimateProperty("Opacity", 0.0f, 1.0f, Duration);
            }
        }

        public override void End()
        {
            SetPatternSegment(new SquidMoveToTopPattern(new SquidSpinArmsSubPattern(Actor)));
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            var index = 0;
            _totalTime += (Single)ellapsedTime.TotalSeconds;
            Single regrowPosition = (_totalTime / (Single)Duration.TotalSeconds);

            foreach (var segment in ((SquidBoss)Actor).Segments)
            {
                if (index < ((SquidBoss)Actor).Segments.Count() / 2)
                    segment.SetLocation(Actor.Location.X - (Single)index * 0.2f * regrowPosition - 0.0f, Actor.Location.Y + 0.95f, 0f);
                else
                    segment.SetLocation(Actor.Location.X + ((Single)index - ((SquidBoss)Actor).Segments.Count() / 2) * 0.2f * regrowPosition + 0.7f, Actor.Location.Y + 0.95f, 0f);

                index++;
            }
        }

        public override bool IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return ellapsedSegmentTime > Duration;
        }
    }

    public class SquidSpinArmsSubPattern
    {
        private Single _armAngle;
        private const Single ArmAngleStep = 0.1f;

        private readonly SquidBoss _actor;
        private Boolean _stopOnZeroDegrees;

        public SquidSpinArmsSubPattern(SquidBoss actor)
        {
            this._actor = actor;
        }

        public void Update()
        {
            var index = 0;

            var radians = _armAngle / (Single)Math.PI;

            if (_stopOnZeroDegrees)
            {
                if (radians - Math.Floor(radians) > 0.1f)
                    _armAngle += ArmAngleStep;
            }
            else
                _armAngle += ArmAngleStep;

            
            foreach (var segment in _actor.Segments)
            {
                if (index < (_actor.Segments.Count() / 2))
                    segment.SetLocation(_actor.Location.X + 0.4f - ((Single)index + 2f) * 0.2f * (Single)Math.Cos(_armAngle), _actor.Location.Y + 0.95f + (Single)Math.Sin(_armAngle) * ((Single)index + 2f) * 0.2f, 0f);
                else
                {
                    var newIndex = (Single)index - (Single)_actor.Segments.Count() / 2f;
                    segment.SetLocation(_actor.Location.X + 0.4f + (newIndex + 2f) * 0.2f * (Single)Math.Cos(_armAngle), _actor.Location.Y + 0.95f - (Single)Math.Sin(_armAngle) * (2f + newIndex) * 0.2f, 0f);
                }

                index++;
            }
        }

        public void StopOnZeroDegrees()
        {
            this._stopOnZeroDegrees = true;            
        }
    }

    public class SquidMoveToTopPattern : PatternSegment<SquidBoss>
    {
        public const Single MovementStep = 0.05f;
        public const Single DesiredX = 4.5f;
        public const Single DesiredY = 0.5f;

        private readonly SquidSpinArmsSubPattern _armPattern;

        private Vector2 _velocity;

        public SquidMoveToTopPattern(SquidSpinArmsSubPattern armPattern)
        {
            this._armPattern = armPattern;
        }

        public override void Begin()
        {
            Actor.IsInvincible = true;
            var direction = (new Vector3(DesiredX, DesiredY, 0)) - Actor.Location;
            direction.Normalize();
            this._velocity = new Vector2(direction.X * MovementStep, direction.Y * MovementStep);
        }

        public override void End()
        {
            SetPatternSegment(new SquidMoveAroundScreenPattern(_armPattern));
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            Actor.SetLocation(Actor.Location.X + this._velocity.X, Actor.Location.Y + this._velocity.Y, 0f);
            _armPattern.Update();
        }

        public override bool IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return (Actor.Location.X - DesiredX) <= MovementStep && (Actor.Location.Y - DesiredY) <= MovementStep;
        }
    }

    public class SquidMoveAroundScreenPattern : PatternSegment<SquidBoss>
    {
        public const Single AngleStep = -0.05f;
        public const Single Radius = 1.625f;

        private readonly SquidSpinArmsSubPattern _armPattern;

        private Single _angle;

        public SquidMoveAroundScreenPattern(SquidSpinArmsSubPattern armPattern)
        {
            this._armPattern = armPattern;
        }

        public override void Begin()
        {
            Actor.StartSlowFiring(((SquidController)base.ParentController).Zazumo);

            Actor.ResetHitPoints();
            Actor.IsInvincible = false;

            this._angle = (Single)(-Math.PI / 2.0);
        }

        public override void End()
        {
            Actor.StopFiring();
            SetPatternSegment(new SquidMoveToRightPattern(this._armPattern));
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            this._angle += AngleStep;
            Actor.SetLocation((Single)Math.Cos(_angle) * Radius + 4.5f, (Single)Math.Sin(_angle) * Radius + 2.125f, 0f);
            _armPattern.Update();
        }

        public override bool IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return Actor.HitPoints == 0;
        }
    }

    public class SquidMoveToRightPattern : PatternSegment<SquidBoss>
    {
        public const Single MovementStep = 0.05f;
        public const Single DesiredX = 7.0f;
        public const Single DesiredY = 1.75f;

        private readonly SquidSpinArmsSubPattern _armPattern;

        private Vector2 _velocity;

        public SquidMoveToRightPattern(SquidSpinArmsSubPattern armPattern)
        {
            this._armPattern = armPattern;
        }

        public override void Begin()
        {
            _armPattern.StopOnZeroDegrees();
            Actor.IsInvincible = true;
            var direction = (new Vector3(DesiredX, DesiredY, 0)) - Actor.Location;
            direction.Normalize();
            this._velocity = new Vector2(direction.X * MovementStep, direction.Y * MovementStep);
        }

        public override void End()
        {
            Actor.IsInvincible = false;
            Actor.ResetHitPoints();
            SetPatternSegment(new SquidMoveAndFlailPattern(new SquidArmFlailSubPattern(Actor)));
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            Actor.SetLocation(Actor.Location.X + this._velocity.X, Actor.Location.Y + this._velocity.Y, 0f);
            _armPattern.Update();
        }

        public override bool IsComplete(TimeSpan ellapsedSegmentTime)
        {
            return (Math.Abs(Actor.Location.X - DesiredX)) <= MovementStep && Math.Abs((Actor.Location.Y - DesiredY)) <= MovementStep;
        }
    }
}
