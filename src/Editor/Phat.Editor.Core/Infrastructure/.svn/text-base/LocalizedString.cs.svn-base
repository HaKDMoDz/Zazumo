using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Editor.Infrastructure
{
    /// <summary>
    /// Represents an lookup entry in a dictionary for a localized string.
    /// </summary>
    public class LocalizedString
    {
        public String Text { get; private set; }

        public LocalizedString(Type source, String key)
        {
            global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager(source.Name, source.Assembly);
            var resourceMan = temp;

            Text = resourceMan.GetString(key, System.Threading.Thread.CurrentThread.CurrentUICulture);
        }

        public LocalizedString(Type source, String resourceName, String key)
        {
            global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager(resourceName, source.Assembly);
            var resourceMan = temp;

            Text = resourceMan.GetString(key, System.Threading.Thread.CurrentThread.CurrentUICulture);
        }

    }
}
