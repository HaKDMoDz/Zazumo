using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows;

namespace Phat.Editor.Infrastructure
{
    /// <summary>
    /// MEF Metadata attribute to ensure views are registered appropriately
    /// </summary>
    /// <remarks>
    /// This works in conjunction with <see cref="ViewFactory"/> to locate views from the <see cref="System.ComponentModel.Composition.Hosting.CompositionContainer"/>
    /// </remarks>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportViewAttribute : ExportAttribute
    {
        public ExportViewAttribute(String viewName)
            : base(viewName, typeof(UserControl))
        {
        }
    }
}
