using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Messages;
using Phat.ActorResources;

namespace Phat.ActorModel
{
    public class RetractableSpikeActor : PlaceableActor<RetractableSpikeActor>, ITouchable
    {
        private IRetractableSpikeWorldObject _resource;

        private Boolean _isRaised;

        public Boolean IsRaised { get { return _isRaised; } }

        public void Toggle()
        {
            this.OnToggle();   
        }

        protected override void  OnInitializing(Object initializationData)
        {
 	         base.OnInitializing(initializationData);

             this._resource = initializationData as IRetractableSpikeWorldObject;
             _isRaised = _resource.SpikePosition == SpikePosition.Up;

             if (_isRaised)
                 base.SetSprite(_resource.SpikeUpSprite);
             else
                 base.SetSprite(_resource.SpikeDownSprite);
        }

        protected virtual void OnToggle()
        {
            this._isRaised = !_isRaised;

            if (_isRaised)
            {
                base.SetFrameSet("Core.FrameSets.SpikeRaiseFrameset");
                In(200).Milliseconds.Run(() => base.SetSprite(_resource.SpikeUpSprite));
            }
            else
            {
                base.SetFrameSet("Core.FrameSets.SpikeLowerFrameset");
                In(200).Milliseconds.Run(() => base.SetSprite(_resource.SpikeDownSprite));
            }
        }

        public void Touch(Actor other)
        {
            this.OnTouch(other);
        }

        private void OnTouch(Actor other)
        {
            var mortal = other as IMortal;

            if (mortal == null)
                return;

            if (this.IsRaised)
                mortal.Kill();
        }
    }
}
