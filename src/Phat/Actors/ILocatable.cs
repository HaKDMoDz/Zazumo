using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Phat.Actors
{   
    public interface ILocatable
    {
        Vector3 Location { get; set; }
    }
}
