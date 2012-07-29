using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class ScriptedSearchableWorldObjectArchetypeData : ConcreteWorldObjectArchetypeData
    {
        public String ScriptKey { get; set; }
    }
}
