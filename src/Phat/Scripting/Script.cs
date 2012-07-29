using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Scripting
{
    public abstract class Script 
    {
        public abstract void BuildScript(ScriptActivityContainer container);
    }
}
