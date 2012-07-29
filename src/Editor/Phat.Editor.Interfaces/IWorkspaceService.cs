using System;
using Phat.Editor.Interfaces.DatabaseModel;

namespace Phat.Editor.Interfaces
{
    public interface IWorkspaceService
    {
        void OpenEditor(String editorViewName, Asset asset);
        void CloseEditor(Asset asset);
        void SaveCurrentEditor();
    }
}
