using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    public interface IWorldObject
    {
        String Id { get; set; }
        String Behavior { get; }
        String Name { get; }
        Single X { get; set; }
        Single Y { get; set; }        
    }
}
