using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.ActorModel
{
    public class ExplosionActor : Actor<ExplosionActor>
    {
        public Int64 _lifetime;

        public ExplosionActor()
        {
            
        }
        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            this.SetFrameSet("Core.FrameSets.Explosion");

            /*this.RunLatent(t =>
                {
                    _lifetime += t;

                    this.Opacity = 1f - (_lifetime / (0.25f * 10000000f));

                    if (_lifetime > 0.25f * 10000000f)
                    {
                        Destroy();
                        return ProcessState.Completed;
                    }

                    return ProcessState.Running;
                });*/
        }
    }
}
