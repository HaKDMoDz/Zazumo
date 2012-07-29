using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat
{
    public interface IResourceDictionary
    {
        Object GetResource(String resourceKey);

        void MergeResources(String sourceName, IDictionary<String, Object> resources);

        void ApplyCustomProcessor<TResource>(Func<TResource, Object> loadAction);

        void LoadPackage(String packageName);
        void UnloadPackage(String packageName);
    }
}
