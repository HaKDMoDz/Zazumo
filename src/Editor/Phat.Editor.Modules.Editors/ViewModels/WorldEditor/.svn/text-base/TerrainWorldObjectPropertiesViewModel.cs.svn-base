using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.ActorResources;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class TerrainWorldObjectPropertiesViewModel : WorldObjectPropertiesViewModel
    {
        private Int32 _columns;
        public Int32 Columns
        {
            get { return _columns; }
            set
            {
                if (value == _columns)
                    return;

                this._columns = value;
                RaisePropertyChanged(() => Columns);
                NotifyWorldObjectValueChanged("Columns", value);
            }
        }

        private Int32 _rows;
        public Int32 Rows
        {
            get { return _rows; }
            set
            {
                if (value == _rows)
                    return;

                this._rows = value;
                RaisePropertyChanged(() => Rows);
                NotifyWorldObjectValueChanged("Rows", value);
            }
        }

        private Int32 _tileWidth;
        public Int32 TileWidth
        {
            get { return _tileWidth; }
            set
            {
                if (value == _tileWidth)
                    return;

                this._tileWidth = value;
                RaisePropertyChanged(() => TileWidth);
                NotifyWorldObjectValueChanged("TileWidth", value);
            }
        }

        private Int32 _tileHeight;
        public Int32 TileHeight
        {
            get { return _tileHeight; }
            set
            {
                if (value == _tileHeight)
                    return;

                this._tileHeight = value;
                RaisePropertyChanged(() => TileHeight);
                NotifyWorldObjectValueChanged("TileHeight", value);
            }
        }

        public override void MoveModelToView(IWorldObject model)
        {
            base.MoveModelToView(model);

            var m = model as TerrainWorldObject;

            this.Rows = m.Rows;
            this.Columns = m.Columns;
            this.TileHeight = m.TileHeight;
            this.TileWidth = m.TileWidth;
        }    
    }
}
