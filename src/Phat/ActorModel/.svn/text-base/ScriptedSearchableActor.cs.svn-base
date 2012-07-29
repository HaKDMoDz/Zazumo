using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;

namespace Phat.ActorModel
{
    public class ScriptedSearchableActor : Actor<ScriptedSearchableActor>, ISearchable
    {
        IScriptedSearchableWorldObject _resource;

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            _resource = (IScriptedSearchableWorldObject)initializationData;
        }

        public void Search()
        {

        }
    }
}
