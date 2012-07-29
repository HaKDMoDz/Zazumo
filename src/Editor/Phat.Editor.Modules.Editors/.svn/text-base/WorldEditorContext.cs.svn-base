using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Phat.ActorResources;
using Microsoft.Practices.Prism.ViewModel;
using Phat.Editor.Infrastructure;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors
{
    [Export(typeof(IWorldEditorContext))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class WorldEditorContext : NotificationObject, IWorldEditorContext
    {
        private readonly ViewFactory _viewFactory;
        private readonly List<WorldObjectViewModel> _worldObjects;

        private UserControl _rootView;
        private UserControl _rootToolsView;

        public WorldResource World { get; set; }
        public UserControl EditorView { get; private set; }
        public UserControl PropertiesView { get; private set; }
        public UserControl EditorToolsView { get; private set; }
                
        [ImportingConstructor]
        public WorldEditorContext(ViewFactory viewFactory)
        {
            this._viewFactory = viewFactory;
            this._worldObjects = new List<WorldObjectViewModel>();
        }
        
        public void SetEditor(String viewName)
        {
            var view = this._viewFactory.GetView(viewName);
            ((IWorldEditor)view.DataContext).Context = this;
            ((IWorldEditor)view.DataContext).Initialize();
            this.EditorView = (UserControl)view;
            this._rootView = view;
            RaisePropertyChanged(() => EditorView);
        }

        public void OpenChildEditor(String viewName, WorldObjectViewModel childViewModel)
        {
            var view = this._viewFactory.GetView(viewName);
            ((IWorldChildEditor)view.DataContext).Context = this;
            ((IWorldChildEditor)view.DataContext).ChildViewModel = childViewModel;

            this.EditorView = (UserControl)view;
            ((IWorldChildEditor)view.DataContext).Initialize();

            RaisePropertyChanged(() => EditorView);
        }

        public void SetEditorTools(String viewName)
        {
            var view = this._viewFactory.GetView(viewName);
            this.EditorToolsView = (UserControl)view;
            this._rootToolsView = view;
            ((IWorldEditor)this.EditorView.DataContext).Tools = view.DataContext;
            RaisePropertyChanged(() => EditorToolsView);
        }

        public void ShowProperties(String viewName, IWorldObject obj)
        {
            var view = this._viewFactory.GetView(viewName);
            ((WorldObjectPropertiesViewModel)(view.DataContext)).Context = this;
            ((WorldObjectPropertiesViewModel)(view.DataContext)).MoveModelToView(obj as IWorldObject);
            
            this.PropertiesView = (UserControl)view;
            RaisePropertyChanged(() => PropertiesView);
        }

        public void NotifyWorldObjectPropertyChanged(String id, String propertyName, Object value)
        {
            this._worldObjects.Where(x => x.Id == id).Single().SetValue(propertyName, value);
            RootEditor.MarkChanges();
        }

        public void NotifyPropertyWindowWorlObjectChanged(String id, String propertyName, Object value)
        {
            if (this.PropertiesView == null)
                return;

            ((WorldObjectPropertiesViewModel)(this.PropertiesView.DataContext)).SetProperty(propertyName, value);
            RootEditor.MarkChanges();
        }

        public IEnumerable<WorldObjectViewModel> GetAllObjects()
        {
            return this._worldObjects;
        }

        public void AddWorldObjectViewModel(WorldObjectViewModel viewModel)
        {
            this._worldObjects.Add(viewModel);
        }

        public void RemoveWorldObjectViewModel(WorldObjectViewModel viewModel)
        {
            this._worldObjects.Remove(viewModel);
        }

        public WorldEditorViewModel RootEditor { get; set; }

        public void CloseChildEditor()
        {
            this.EditorView = _rootView;
            SetEditorTools(ViewNames.WorldEditorTools_World);
            RaisePropertyChanged(() => EditorView);
        }

        public void ClearProperties()
        {
            this.PropertiesView = null;
            RaisePropertyChanged(() => PropertiesView);
        }
    }
}
