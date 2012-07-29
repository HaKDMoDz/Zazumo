using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Messages;
using Phat.ActorResources;

namespace Phat.ActorModel
{
    public class RetractableSpikeButtonActor : PlaceableActor<RetractableSpikeButtonActor>, ITouchable
    {
        private Boolean _isDepressed;
        private IRetractableSpikeButtonWorldObject _resource;

        public void Touch(Actor other)
        {
            OnTouch(other);
        }

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            this._resource = initializationData as IRetractableSpikeButtonWorldObject;
        }

        protected virtual void OnTouch(Actor other)
        {
            this.Publish(new RetractableSpikeButtonToggledEvent(this.ActorId));

            this._isDepressed = !this._isDepressed;

            if (!_isDepressed)
            {
                this.SetSprite(_resource.SpriteKey);                
            }
            else
            {
                this.SetSprite(_resource.AlternateSprite);
            }
        }
    }
}
