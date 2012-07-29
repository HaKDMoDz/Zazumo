using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Messages;
using Phat.ActorResources;

namespace Phat.ActorModel
{
    public class RotatingArrowActor : ArrowActor
    {
        private Single _animationSpeed;
        private Int64 _frameTickCounter;
        private IRotatingArrowWorldObject _resource;

        public void Spin()
        {
            OnSpin();
        }

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            _resource = (IRotatingArrowWorldObject)initializationData;
            this._animationSpeed = 1.0f;

            switch (ArrowDirection)
            {
                case ArrowDirection.Up:
                    SetSprite(_resource.UpSprite);
                    break;

                case ArrowDirection.Down:
                    SetSprite(_resource.DownSprite);
                    break;

                case ArrowDirection.Left:
                    SetSprite(_resource.LeftSprite);
                    break;

                case ArrowDirection.Right:
                    SetSprite(_resource.RightSprite);
                    break;

                default:
                    break;
            }
        }

        protected virtual void OnSpin()
        {
            base.RunLatent(t =>
                {
                    _frameTickCounter += t;
                    if (_frameTickCounter > (_animationSpeed * 10000000f))
                    {
                        switch (ArrowDirection)
                        {
                            case ArrowDirection.Up:
                                this.ArrowDirection = ArrowDirection.Right;
                                SetSprite(_resource.RightSprite);
                                break;

                            case ArrowDirection.Down:
                                this.ArrowDirection = ArrowDirection.Left;
                                SetSprite(_resource.LeftSprite);
                                break;

                            case ArrowDirection.Left:
                                this.ArrowDirection = ArrowDirection.Up;
                                SetSprite(_resource.UpSprite);
                                break;

                            case ArrowDirection.Right:
                                this.ArrowDirection = ArrowDirection.Down;
                                SetSprite(_resource.DownSprite);
                                break;

                            default:
                                break;
                        }
                        
                        _frameTickCounter = 0;
                    }

                    return ProcessState.Running;
                });
        }
    }
}
