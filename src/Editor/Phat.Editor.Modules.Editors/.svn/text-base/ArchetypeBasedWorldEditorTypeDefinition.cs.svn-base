using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors
{
    public abstract class ArchetypeBasedWorldEditorTypeDefinition<TWorldObject, TArchetypeData> : IArchetypeBasedWorldEditorTypeDefinition
        where TWorldObject : ArchetypeBasedWorldObject, new()
        where TArchetypeData : WorldObjectArchetypeData, new()
    {
        public abstract WorldObjectViewModel CreateWorldObjectViewModel();

        public Type WorldObjectType
        {
            get { return typeof(TWorldObject); }
        }

        public Type ArchetypeDataType
        {
            get { return typeof(TArchetypeData); }
        }

        public String Name { get; private set; }

        public ArchetypeBasedWorldEditorTypeDefinition()
        {
            this.Name = String.Empty;
        }

        public IWorldObject GetDefaultedObject(String archetypeKey, WorldObjectArchetypeData data)
        {
            var obj = Activator.CreateInstance<TWorldObject>();
            obj.ArchetypeKey = archetypeKey;
            obj.SetArchetypeData(data);
            return obj;
        }
    }
}
