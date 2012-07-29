using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Actors
{
    public interface IActorEventSubscriptionProvider
    {
        void AddHandler(IActorEventHandler handler);
        void HandleEvent(Object @event);
    }
}
