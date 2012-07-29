using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;

namespace Phat.Messages
{
    public class UIButtonPressedEvent : ActorEvent
    {
        public UIButtonActor Button { get; private set; }

        public UIButtonPressedEvent(UIButtonActor button)
            : base (button.ActorId)
        {
            this.Button = button;
        }
    }
}
