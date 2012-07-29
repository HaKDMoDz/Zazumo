using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.ActorResources;

namespace Phat.ActorModel
{
    public abstract class PlaceableActor<TActor> : Actor<TActor>
        where TActor : PlaceableActor<TActor>
    {
        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            SetLocation(((IWorldObject)initializationData).X, ((IWorldObject)initializationData).Y, 0f);
        }
    }
}
