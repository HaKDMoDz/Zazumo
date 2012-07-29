using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace Phat.Editor.Infrastructure
{
    /// <summary>
    /// Creates factories from the container.  This is explicitly registered vs. exported to avoid registering the container in the container.
    /// </summary>
    public class ViewFactory
    {
        private readonly CompositionContainer container;

        public ViewFactory(CompositionContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Returns the view that matches the supplied view name.
        /// </summary>
        /// <seealso cref="ExportViewAttribute"/>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public UserControl GetView(String viewName)
        {
            var view = this.container.GetExportedValue<UserControl>(viewName);
            if (view == null)
            {
                throw new InvalidOperationException(string.Format("Unable to locate view with name {0}.", viewName));
            }

            return view;
        }
    }
}
