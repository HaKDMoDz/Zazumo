using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Actors
{
    public interface IActorFactoryProvider
    {
        void SetActorFactory(IActorFactory actorFactory);
    }
}
