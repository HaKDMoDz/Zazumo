using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Messages;

namespace Phat.ActorModel
{
   public class Volume : Actor<Volume>
    {       
        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            base.OnInitializing(initializationData);
            this.Handle<ActorCollidedEvent>()
                .ApplyDefaultHandler(x =>
                {
                    x.Cancel = false;
                });
        }
    }
}
