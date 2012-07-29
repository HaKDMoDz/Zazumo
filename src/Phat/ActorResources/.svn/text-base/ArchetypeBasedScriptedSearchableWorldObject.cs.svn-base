using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class ArchetypeBasedScriptedSearchableWorldObject : ArchetypeBasedConcreteWorldObject, IScriptedSearchableWorldObject
    {
        private ScriptedSearchableWorldObjectArchetypeData Data
        {
            get { return (ScriptedSearchableWorldObjectArchetypeData)base.ArchetypeData; }
        }

        public String ScriptKey
        {
            get { return Data.ScriptKey; }
        }
    }
}
