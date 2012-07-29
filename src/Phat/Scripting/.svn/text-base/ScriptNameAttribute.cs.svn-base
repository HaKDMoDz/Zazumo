using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Scripting
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public class ScriptNameAttribute : Attribute
    {
        public String ScriptName { get; private set; }
        
        public ScriptNameAttribute(String scriptName)
        {
            this.ScriptName = scriptName;
        }
    }
}
