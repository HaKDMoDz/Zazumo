using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Phat.Editor.Interfaces
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared)]
    public class BehaviorRegistry
    {
        private readonly List<String> _registeredBehaviors;

        public BehaviorRegistry()
        {
            this._registeredBehaviors = new List<String>();
        }

        public void RegisterBehavior(String behavior)
        {
            this._registeredBehaviors.Add(behavior);
        }

        public IEnumerable<String> GetRegisteredBehaviors()
        {
            return this._registeredBehaviors.OrderBy(x => x).AsEnumerable();
        }
    }
}
