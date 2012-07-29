using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class WorldEditorWorldToolsViewModel : NotificationObject
    {
        private Boolean _isGridSnappingEnabled;
        public Boolean IsGridSnappingEnabled
        {
            get { return _isGridSnappingEnabled; }
            set
            {
                _isGridSnappingEnabled = value;
                RaisePropertyChanged(() => IsGridSnappingEnabled);
            }
        }

        private Int32 _gridSnapX;
        public Int32 GridSnapX            
        {
            get { return _gridSnapX; }
            set
            {
                _gridSnapX = value;
                RaisePropertyChanged(() => GridSnapX);
            }
        }

        private Int32 _gridSnapY;
        public Int32 GridSnapY
        {
            get { return _gridSnapY; }
            set
            {
                _gridSnapY = value;
                RaisePropertyChanged(() => GridSnapY);
            }
        }

        public WorldEditorWorldToolsViewModel()
        {
            this._isGridSnappingEnabled = true;
            this.GridSnapX = 25;
            this.GridSnapY = 24;
        }
    }
}
