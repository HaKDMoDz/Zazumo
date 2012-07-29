using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Messages;

namespace Phat.ActorModel
{
    public class TriggerVolume : Volume
    {       
        protected override void OnInitializing(object initializationData)
        {
            base.OnInitializing(initializationData);
            this.Handle<ActorCollidedEvent>()
                .ApplyDefaultHandler(x => 
                    {
                        OnVolumeEntered(x.OtherActor);
                        x.Cancel = true;
                    });
        }


        protected virtual void OnVolumeEntered(Actor actor)
        {
            
        }
    }
}
