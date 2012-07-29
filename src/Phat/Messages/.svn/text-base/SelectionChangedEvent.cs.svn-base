using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.Messages
{
    public class SelectionChangedEvent
    {
        public Actor Sender { get; private set; }
        public Int32 SelectedIndex { get; set; }

        public SelectionChangedEvent(Actor sender, Int32 selectedIndex)
        {
            this.Sender = sender;
            this.SelectedIndex = selectedIndex;
        }
    }
}
