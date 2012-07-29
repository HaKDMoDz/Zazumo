using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    public interface IBombWorldObject : IConcreteWorldObject
    {
        String ActiveAnimation { get; }
        Int32 Timer { get; }
    }
}
