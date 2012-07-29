using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Infrastructure;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces.DatabaseModel;
using System.Windows.Controls;

namespace Phat.Editor.Interfaces
{
    [Export(typeof(IWorkspaceService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class WorkspaceService : IWorkspaceService
    {
        private readonly ViewFactory _viewFactory;
        private readonly IRegionManager _regionManager;
        private readonly Dictionary<String, UserControl> _openEditors;

        [ImportingConstructor]
        public WorkspaceService(ViewFactory viewFactory, IRegionManager regionManager)
        {
            this._viewFactory = viewFactory;
            this._regionManager = regionManager;

            this._openEditors = new Dictionary<String, UserControl>();
        }

        public void OpenEditor(String editorViewName, Asset asset)
        {
            if (!_openEditors.ContainsKey(asset.Key))
            {
                var view = this._viewFactory.GetView(editorViewName);
                var editor = ((IEditor)view.DataContext);
                editor.Asset = asset;
                _regionManager.Regions[RegionName.Content.ToString()].Add(view, asset.Key);
                _openEditors[asset.Key] = view;
            }            

            _regionManager.Regions[RegionName.Content.ToString()].Activate(_openEditors[asset.Key]);
        }

        public void CloseEditor(Asset asset)
        {
            var view = _openEditors[asset.Key];

            _regionManager.Regions[RegionName.Content.ToString()].Remove(view);
            _openEditors.Remove(asset.Key);
        }

        public void SaveCurrentEditor()
        {
            foreach (var view in _regionManager.Regions[RegionName.Content.ToString()].ActiveViews)
            {
                var dataContext = ((UserControl)view).DataContext;

                var editor = dataContext as IEditor;

                if (editor == null)
                    return;

                editor.Save();
            }
        }
    }
}
