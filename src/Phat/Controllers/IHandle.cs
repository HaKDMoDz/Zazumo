using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat
{
    public interface IHandle<TEvent>
    {
        void Handle(TEvent @event);
    }
}
