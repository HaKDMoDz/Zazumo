using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportArchetypeDataEditorAttribute : ExportAttribute
    {
        public Type ArchetypeDataType { get; private set; }

        public ExportArchetypeDataEditorAttribute(Type archetypeDataType)
            : base(typeof(ArchetypeDataViewModel))
        {
            this.ArchetypeDataType = archetypeDataType;
        }
    }

    /// <summary>
    /// MEF metadata interface, to allow us to work with compile-safe data types.
    /// </summary>
    public interface IArchetypeDataEditorMetaData
    {
        Type ArchetypeDataType { get; }
    }
}
