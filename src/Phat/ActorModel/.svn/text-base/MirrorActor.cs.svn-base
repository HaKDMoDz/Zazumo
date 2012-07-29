using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.ActorResources;

namespace Phat.ActorModel
{
    public class MirrorActor : PlaceableActor<MirrorActor>
    {
        public MirrorDirection MirrorDirection { get; private set; }

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            var resource = (IMirrorWorldObject)initializationData;

            SetLocation(resource.X, resource.Y, 0f);

            this.MirrorDirection = resource.MirrorDirection;
        }
    }
}
