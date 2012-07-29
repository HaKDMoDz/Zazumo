using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using System.Windows.Controls;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors
{
    public interface IWorldEditorContext
    {
        WorldResource World { get; set; }

        UserControl EditorView { get; }
        UserControl EditorToolsView { get; }
        UserControl PropertiesView { get; }

        void SetEditor(String viewName);
        void OpenChildEditor(String viewName, WorldObjectViewModel childViewModel);
        void CloseChildEditor();

        void SetEditorTools(String viewName);
                
        void ShowProperties(String viewName, IWorldObject obj);
        void ClearProperties();
        void NotifyWorldObjectPropertyChanged(String id, String propertyName, Object value);
        void NotifyPropertyWindowWorlObjectChanged(String id, String propertyName, Object value);

        void AddWorldObjectViewModel(WorldObjectViewModel viewModel);
        void RemoveWorldObjectViewModel(WorldObjectViewModel viewModel);

        WorldEditorViewModel RootEditor { get; set; }
        IEnumerable<WorldObjectViewModel> GetAllObjects();       
    }
}
