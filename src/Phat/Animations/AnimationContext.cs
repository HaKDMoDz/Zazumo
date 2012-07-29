using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat
{
    public class AnimationContext
    {
        public Action Callback { get; private set; }

        public void OnCompletion(Action callback)
        {
            this.Callback = callback;
        }
    }
}
