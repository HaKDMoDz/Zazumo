using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Messages;
using Phat.ActorResources;

namespace Phat.ActorModel
{
    public class TutorialTextTriggerVolume : TriggerVolume
    {
        private String _text;

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);
            
            var resource = (TutorialTriggerVolumeWorldObject)initializationData;

            this._text = resource.Text;
        }

        protected override void OnVolumeEntered(Actor actor)
        {
            base.OnVolumeEntered(actor);

            Publish(new TutorialTriggeredEvent(_text));
        }
    }
}
