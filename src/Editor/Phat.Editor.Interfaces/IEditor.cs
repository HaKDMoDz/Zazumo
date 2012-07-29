using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces.DatabaseModel;
using System.Windows.Input;

namespace Phat.Editor.Interfaces
{
    public interface IEditor
    {
        Asset Asset { get; set; }
        String Title { get; }
        Boolean HasUnsavedChanges { get; }

        void Save();

        ICommand CloseCommand { get; }
        

    }
}
