using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;
using Phat.Zazumo.Messages;

namespace Phat.Zazumo.Actors
{
    public class AmmoMeter : UIActor
    {
        private Single _ammoLevel;

            public Single AmmoLevel
        {
            get {
                 return this._ammoLevel;
                }

            set
            {
                if (_ammoLevel > 0f && value <= 0f)
                {
                    this._ammoLevel = 0f;
                    Publish(new AmmoDepletedEvent());
                }
                else
                    this._ammoLevel = value;
            }
        }
        
        public AmmoMeter()
        {
            _ammoLevel = 1f;
        }
    }
}
