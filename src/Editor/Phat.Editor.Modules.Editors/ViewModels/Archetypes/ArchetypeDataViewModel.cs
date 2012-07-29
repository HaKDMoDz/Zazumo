using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using Microsoft.Practices.Prism.ViewModel;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    public abstract class ArchetypeDataViewModel : NotificationObject
    {
        public ArchetypeEditorViewModel Parent { get; set; }

        public abstract ArchetypeData MoveViewToModel();
        public abstract void MoveModelToView(ArchetypeData model);

        protected void MarkUnsaved()
        {
            Parent.MarkUnsaved();
        }        
    }
}
