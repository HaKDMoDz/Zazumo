using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors
{
    public abstract class WorldEditorTypeDefinition<TWorldObject> : IWorldEditorTypeDefinition
        where TWorldObject : IWorldObject, new()
    {
        protected abstract void SetDefaultValues(TWorldObject obj);

        public Type WorldObjectType
        {
            get { return typeof(TWorldObject); }
        }

        public String Name { get; private set; }

        public WorldEditorTypeDefinition(String name)
        {
            this.Name = name;
        }
        
        public IWorldObject GetDefaultedObject()
        {
            var obj = Activator.CreateInstance<TWorldObject>();
            SetDefaultValues(obj);
            return obj;
        }

        public abstract WorldObjectViewModel CreateWorldObjectViewModel();
}
}
