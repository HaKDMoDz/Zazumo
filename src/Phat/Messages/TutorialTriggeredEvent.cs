using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Messages
{
    public class TutorialTriggeredEvent
    {
        public String Text { get; private set; }
        
        public TutorialTriggeredEvent(String text)
        {
            this.Text = text;
        }
    }
}
