using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Phat.ActorResources;
using System.IO;

#if MONO
using System.Runtime.Serialization.Formatters.Binary;
#endif

namespace Phat
{
    /// <summary>
    /// Represents an indexable set of user defined data.
    /// </summary>
    public class ResourceDictionary : IResourceDictionary
    {
        private readonly Dictionary<String, Object> _resources;
        private readonly Dictionary<String, List<String>> _resourcePackages;
        private readonly ContentManager _contentManager;
        private readonly List<String> _loadedPackages;
        private readonly Dictionary<Type, Delegate> _loadActions;
        private readonly Dictionary<Type, Delegate> _unloadActions;

        public ResourceDictionary(ContentManager contentManager)
        {
            _resources = new Dictionary<String, Object>();
            _contentManager = contentManager;
            _loadedPackages = new List<String>();
            _resourcePackages = new Dictionary<String, List<String>>();
            _loadActions = new Dictionary<Type, Delegate>();
            _unloadActions = new Dictionary<Type, Delegate>();
        }

        public Object GetResource(String resourceKey)
        {
            if (!_resources.ContainsKey(resourceKey))
                throw new Exception(String.Format("The key {0} was not found in the currently loaded resources.", resourceKey));

            return _resources[resourceKey];
        }

        public void MergeResources(String sourceName, IDictionary<String, Object> resources)
        {
            _loadedPackages.Add(sourceName);
            _resourcePackages[sourceName] = new List<String>();

            foreach (var pair in resources)
            {
                if (this._loadActions.ContainsKey(pair.Value.GetType()))
                {
                    var action = this._loadActions[pair.Value.GetType()];
                    var result = action.DynamicInvoke(pair.Value);
                    _resources.Add(pair.Key, result);
                }
                else
                {
                    _resources.Add(pair.Key, pair.Value);
                }

                _resourcePackages[sourceName].Add(pair.Key);
            }
        }

        public void LoadPackage(String packageName)
        {
            if (this._loadedPackages.Contains(packageName))
                return;
           
#if !MONO
            var package = _contentManager.Load<ResourceModel[]>(packageName);
#else
            ResourceModel[] package;

            using (var file = File.OpenRead(Path.Combine("Content", packageName) + ".dat"))
            {
                var serializer = new BinaryFormatter();
                package = (ResourceModel[])serializer.Deserialize(file);
            }

#endif

            var newDictionary = new Dictionary<String, Object>();
            foreach (var r in package)
            {
                newDictionary.Add(r.Key, r);
            }

            this.MergeResources(packageName, newDictionary);
        }

        public void UnloadPackage(String packageName)
        {
            if (!this._loadedPackages.Contains(packageName))
                throw new Exception(String.Format("An attempt was made to unload the package {0}, which was not loaded.", packageName));


            foreach (var resourceKey in _resourcePackages[packageName])
            {
                var resource = _resources[resourceKey];
                
                // XNA doesn't let you reload textures after they have been disposed.
                //if (resource is IDisposable)
                //    ((IDisposable)resource).Dispose();

                _resources.Remove(resourceKey);
            }

            _resourcePackages.Remove(packageName);

            _loadedPackages.Remove(packageName);
        }

        public void ApplyCustomProcessor<TResource>(Func<TResource, Object> loadAction)
        {
            this._loadActions[typeof(TResource)] = loadAction;
        }
    }
}
