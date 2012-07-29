using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Actors;
using Microsoft.Xna.Framework;
using Phat.Messages;
using Phat.ActorResources;
using Phat.Animations;

namespace Phat.ActorModel
{
    public class BurgalsActor : Actor<BurgalsActor>, IMortal
    {
        private const Single Threshold = 5.0f;

        private const Int32 TileWidth = 25;
        private const Int32 TileHeight = 24;
        private const Int32 DirectionChangeThreshold = 3;

        private Single _speed; // Units per second.
        private Boolean _isMoving;
        private Boolean _isPaused;
        private Boolean _shouldChangeDirection;
        private ArrowDirection _newDirection;
        private ArrowDirection _currentDirection;
        private ArrowActor _touchedArrow = null;
        private Vector2 _previousVelocity;
        private Vector2 _currentVelocity;
        private IBurgalsData _resource;

        public BurgalsActor()
        {
            _speed = 20.0f;
            _isMoving = false;
            _currentDirection = ArrowDirection.Right;

            ((ILocatable)this).Location = new Vector3(-100f, -100f, 0f);

            this.Handle<ActorCollidedEvent>()
                .ApplyDefaultHandler(OnActorCollided);
        }

        public void MoveTo(Single x, Single y, Single z)
        {
            OnMoveTo(x, y, z);
        }

        public void MoveInDirection(Single x, Single y, Single z)
        {
            OnMoveInDirection(x, y, z);
        }

        protected override void OnSetLocation(float x, float y, float z)
        {
            base.OnSetLocation(x, y, z);
        }

        protected virtual void OnSetVelocity(Single x, Single y, Single z)
        {
            if (_isPaused)
                return;

            if (_currentVelocity == Vector2.Zero)
                this.SetFrameSet(_resource.WalkingRightFrameSet);

            _currentVelocity = new Vector2(x, y);

            if (x < 0)
                this.SetFrameSet(_resource.WalkingLeftFrameSet);
            else if (x > 0)
                this.SetFrameSet(_resource.WalkingRightFrameSet);

            this.Publish(new ActorVelocitySetEvent(ActorId, new Vector2(x, y)));
        }

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            _resource = initializationData as IBurgalsData;
            this._speed = _resource.Speed;

            this.RunLatent(t =>
            {
                // Speed Boost
                /*speedBoostCounter -= t;
                if (speedBoostCounter <= 0f)
                {
                    if (!_shouldChangeDirection && !_isPaused)
                    {
                        if (_currentVelocity.X < 0.0f)
                            this.MoveInDirection(-1f, 0f, 0f);
                        else if (_currentVelocity.X > 0.0f)
                            this.MoveInDirection(1f, 0f, 0f);
                        else if (_currentVelocity.Y < 0.0f)
                            this.MoveInDirection(0f, -1f, 0f);
                        else if (_currentVelocity.Y > 0.0f)
                            this.MoveInDirection(0f, 1f, 0f);
                    }

                    speedBoostCounter = TimeSpan.FromSeconds(1f).Ticks;
                }*/

                if (_shouldChangeDirection)
                {
                    var location = ((ILocatable)this).Location;
                    if (_newDirection == ArrowDirection.Up || _newDirection == ArrowDirection.Down)
                    {
                        // Check if pawn is near the horizontal center of the tile.
                        var pawnMidpoint = location.X + (Single)TileWidth / 2f;
                        var pawnMidpointTileCoordinate = pawnMidpoint % (Single)TileWidth;
                        var tileMidPointCoordinate = (Single)TileWidth / 2f;

                        if (Math.Abs(pawnMidpointTileCoordinate - tileMidPointCoordinate) <= DirectionChangeThreshold)
                        {
                            _shouldChangeDirection = false;

                            // Snap pawn to the horizontal center of the tile.
                            var horizontalTileIndex = (Int32)Math.Floor((Single)(pawnMidpoint) / (Single)TileWidth);

                            this.SetLocation(horizontalTileIndex * TileWidth, location.Y, 0f);

                            if (_newDirection == ArrowDirection.Up)
                            {
                                _currentDirection = ArrowDirection.Up;
                                this.MoveInDirection(0f, -1f, 0f);
                            }
                            else
                            {
                                _currentDirection = ArrowDirection.Down;
                                this.MoveInDirection(0f, 1f, 0f);
                            }

                            if (_touchedArrow != null)
                                _touchedArrow.Touch();
                        }
                    }

                    else
                    {
                        // Check if pawn is near the vertical center of the tile.
                        var pawnMidpoint = location.Y + (Single)TileHeight / 2f;
                        var pawnMidpointTileCoordinate = pawnMidpoint % (Single)TileHeight;
                        var tileMidPointCoordinate = (Single)TileHeight / 2f;

                        if (Math.Abs(pawnMidpointTileCoordinate - tileMidPointCoordinate) <= DirectionChangeThreshold)
                        {
                            _shouldChangeDirection = false;

                            // Snap pawn to the horizontal center of the tile.
                            var verticalTileIndex = (Int32)Math.Floor((Single)(pawnMidpoint) / (Single)TileHeight);

                            this.SetLocation(location.X, verticalTileIndex * TileHeight, 0f);

                            if (_newDirection == ArrowDirection.Left)
                            {
                                _currentDirection = ArrowDirection.Left;
                                this.MoveInDirection(-1f, 0f, 0f);
                            }
                            else
                            {
                                _currentDirection = ArrowDirection.Right;
                                this.MoveInDirection(1f, 0f, 0f);
                            }

                            if (_touchedArrow != null)
                                _touchedArrow.Touch();
                        }
                    }
                }

                return ProcessState.Running;
            });
        }

        protected virtual void OnActorCollided(ActorCollidedEvent @event)
        {
            if (@event.OtherActor is ArrowActor)
            {
                this.Publish(new ArrowTouchedEvent());
                var arrow = @event.OtherActor as ArrowActor;

                switch (arrow.ArrowDirection)
                {
                    case ArrowDirection.Up:
                        _shouldChangeDirection = true;
                        _newDirection = ArrowDirection.Up;
                        break;

                    case ArrowDirection.Down:
                        _shouldChangeDirection = true;
                        _newDirection = ArrowDirection.Down;
                        break;

                    case ArrowDirection.Left:
                        _shouldChangeDirection = true;
                        _newDirection = ArrowDirection.Left;
                        break;

                    case ArrowDirection.Right:
                        _shouldChangeDirection = true;
                        _newDirection = ArrowDirection.Right;
                        break;

                    default:
                        throw new Exception("Player touched an arrow with an invalid direction.");
                }

                _touchedArrow = arrow;
            }
            else if (@event.OtherActor is MirrorActor)
            {
                this.Publish(new MirrorTouchedEvent());
                var xDir = -1.0f;
                var yDir = -1.0f;             

                var mirror = @event.OtherActor as MirrorActor;
                switch (mirror.MirrorDirection)
                {
                    case MirrorDirection.Up:
                        _shouldChangeDirection = true;
                        switch (_currentDirection)
                        {
                            case ArrowDirection.Up:
                                _newDirection = ArrowDirection.Right;
                                xDir = -1f; yDir = -1f;
                                break;
                            case ArrowDirection.Down:
                                _newDirection = ArrowDirection.Left;
                                xDir = 1f; yDir = 1f;
                                break;
                            case ArrowDirection.Left:
                                _newDirection = ArrowDirection.Down;
                                xDir = -1f; yDir = -1f;
                                break;
                            case ArrowDirection.Right:
                                _newDirection = ArrowDirection.Up;
                                xDir = 1f; yDir = 1f;
                                break;
                            default:
                                break;
                        }
                        break;

                    case MirrorDirection.Down:
                        _shouldChangeDirection = true;
                        switch (_currentDirection)
                        {
                            case ArrowDirection.Up:
                                _newDirection = ArrowDirection.Left;
                                xDir = 1f; yDir = -1f;
                                break;
                            case ArrowDirection.Down:
                                _newDirection = ArrowDirection.Right;
                                xDir = -1f; yDir = 1f;
                                break;
                            case ArrowDirection.Left:
                                _newDirection = ArrowDirection.Up;
                                xDir = -1f; yDir = 1f;
                                break;
                            case ArrowDirection.Right:
                                _newDirection = ArrowDirection.Down;
                                xDir = 1f; yDir = -1f;
                                break;
                            default:
                                break;
                        }
                        break;

                    default:
                        throw new Exception("Player touched a mirror with an invalid direction.");
                }

                _touchedArrow = null;

                Storyboard sb = new Storyboard
                {
                    Children = new StoryboardTarget[]
                    {
                        new StoryboardTarget { TargetActor = @event.OtherActor, TargetProperty = "XOffset",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(150) }, Value = 8.0f * xDir },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(300) }, Value = 0.0f }
                                }
                            }
                        },
                        
                        new StoryboardTarget { TargetActor = @event.OtherActor, TargetProperty = "YOffset",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(150) }, Value = 8.0f * yDir },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(300) }, Value = 0.0f }
                                }
                            }
                        }

                    }
                };

                this.RunStoryboard(sb);

            }
            else
            {
                var touchable = @event.OtherActor as ITouchable;

                if (@event.OtherActor is TerrainActor)
                {
                    touchable.Touch(this);
                    return;
                }

                if (touchable != null)
                    touchable.Touch(this);

                _touchedArrow = null;
            }
        }

        protected virtual void OnMoveInDirection(Single x, Single y, Single z)
        {
            if (_isPaused)
                return;

            var direction = new Vector3(x, y, z);
            direction.Normalize();
            direction = direction * _speed;

            if (x < 0)
                _currentDirection = ArrowDirection.Left;
            else if (x > 0)
                _currentDirection = ArrowDirection.Right;
            else if (y < 0)
                _currentDirection = ArrowDirection.Up;
            else if (y > 0)
                _currentDirection = ArrowDirection.Down;

            this.SetVelocity(direction.X, direction.Y, 0f);

            if (!_isMoving)
            {
                base.RunLatent(t =>
                {
                    _isMoving = true;

                    if (!_isMoving)
                        return ProcessState.Completed;
                    else
                        return ProcessState.Running;
                });
            }
        }

        protected virtual void OnMoveTo(Single x, Single y, Single z)
        {
            base.RunLatent(t =>
            {
                // get direction vector.
                var currentLocation = ((ILocatable)this).Location;
                var direction = new Vector3(x - currentLocation.X, y - currentLocation.Y, z - currentLocation.Z);
                direction.Normalize();
                var secondsEllapsed = (Single)t / (Single)10000000.0;
                var distance = _speed * secondsEllapsed;
                var newLocation = new Vector3(currentLocation.X + direction.X * distance, currentLocation.Y + direction.Y * distance, currentLocation.Z + direction.Z * distance);

                this.SetLocation(newLocation.X, newLocation.Y, newLocation.Z);
                var distanceFromDestination = Math.Sqrt((x - newLocation.X) * (x - newLocation.X) + (y - newLocation.Y) * (y - newLocation.Y) + (z - newLocation.Z) * (z - newLocation.Z));

                if (distanceFromDestination < Threshold)
                    return ProcessState.Completed;
                else
                    return ProcessState.Running;
            });
        }

        public void Kill()
        {
            this.SetVelocity(0f, 0f, 0f);
            Publish(new PlayerDiedEvent());
            this.SetFrameSet(_resource.DeathFrameSet);
        }

        public void Stop()
        {
            this.SetVelocity(0f, 0f, 0f);
            this.SetFrameSet(_resource.StandingFrameSet);
        }

        public void Pause()
        {
            _previousVelocity = _currentVelocity;
            _currentVelocity = Vector2.Zero;
            this.SetVelocity(0f, 0f, 0f);
            _isPaused = true;
            this.SetFrameSet(_resource.StandingFrameSet);
        }

        public void Resume()
        {
            _isPaused = false;
            switch (_currentDirection)
            {
                case ArrowDirection.Up:
                    MoveInDirection(0f, -1f, 0f);
                    this.SetFrameSet(_resource.WalkingRightFrameSet);
                    break;
                case ArrowDirection.Down:
                    MoveInDirection(0f, 1f, 0f);
                    this.SetFrameSet(_resource.WalkingRightFrameSet);
                    break;
                case ArrowDirection.Left:
                    MoveInDirection(-1f, -0, 0f);
                    this.SetFrameSet(_resource.WalkingLeftFrameSet);
                    break;
                case ArrowDirection.Right:
                    MoveInDirection(1f, 0f, 0f);
                    this.SetFrameSet(_resource.WalkingRightFrameSet);
                    break;
                default:
                    break;
            }
        }
    }
}
