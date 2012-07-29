using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Actors
{
    public abstract class ActorController : BaseController
    {
        public Actor Actor { get; set; }
    }
}
