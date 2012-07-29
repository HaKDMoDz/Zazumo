using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.ActorModel
{
    public class UserArrowActor : ArrowActor
    {
        private Int32 _remainingHits;

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            _remainingHits = 3;
        }

        private long _timeToDestroy = 4000000;

        protected override void OnTouch()
        {
            base.OnTouch();

            _remainingHits--;
            this.Opacity -= 0.2f;

            if (_remainingHits == 0)
            {
                RunLatent(x =>
                    {
                        _timeToDestroy -= x;

                        if (_timeToDestroy < 0)
                        {
                            this.Destroy();
                            return ProcessState.Completed;
                        }
                        else
                            return ProcessState.Running;

                    });
            }
        }
    }
}
