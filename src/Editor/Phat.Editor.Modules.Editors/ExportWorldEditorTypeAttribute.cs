using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Phat.Editor.Modules.Editors
{
    /// <summary>
    /// MEF Metadata attribute to define world objects that can be added to the world editor.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportWorldEditorTypeAttribute : ExportAttribute
    {
        /// <summary>
        /// Gets the name of the view will be used to display object properties.
        /// </summary>
        public String PropertiesView { get; private set; }

        /// <summary>
        /// Gets the name of the custom editor view.
        /// </summary>
        public String CustomEditorView { get; set; }

        /// <summary>
        /// Gets the name of the custom editor tools view.
        /// </summary>
        public String CustomEditorToolsView { get; set; }

        public ExportWorldEditorTypeAttribute(String propertiesView)
            : base(typeof(IWorldEditorTypeDefinition))
        {
            this.PropertiesView = propertiesView;
        }
    }

    /// <summary>
    /// MEF metadata interface, to allow us to work with compile-safe data types.
    /// </summary>
    public interface IWorldEditorTypeMetaData
    {
        String PropertiesView { get; }
        String CustomEditorView { get; }
        String CustomEditorToolsView { get; }
    }
}
