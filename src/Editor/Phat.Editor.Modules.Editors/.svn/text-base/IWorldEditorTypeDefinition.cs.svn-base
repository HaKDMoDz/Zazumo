using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors
{
    public interface IWorldEditorTypeDefinition
    {
        /// <summary>
        /// Gets the type of <see cref="IWorldObject"/> the definitions are being set for.
        /// </summary>
        Type WorldObjectType { get; }

        /// <summary>
        /// Gets the displayed name of the world object type.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Gets a world object with default values.
        /// </summary>
        /// <returns></returns>
        IWorldObject GetDefaultedObject();

        WorldObjectViewModel CreateWorldObjectViewModel();
    }

    public interface IArchetypeBasedWorldEditorTypeDefinition
    {
        /// <summary>
        /// Gets the type of <see cref="IWorldObject"/> the definitions are being set for.
        /// </summary>
        Type WorldObjectType { get; }

        Type ArchetypeDataType { get; }
        
        /// <summary>
        /// Gets a world object with default values.
        /// </summary>
        /// <returns></returns>
        IWorldObject GetDefaultedObject(String archetypeKey, WorldObjectArchetypeData data);

        WorldObjectViewModel CreateWorldObjectViewModel();
    }
}
