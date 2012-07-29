
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;
using Phat.ActorResources;
using Phat.Editor.Interfaces.DatabaseModel;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using System.Windows.Threading;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class LevelEditorViewModel : EditorViewModel<LevelResource>
    {
        private readonly IPackageRepository _repository;

        private String _name;
        public String Name
        {
            get { return _name; }
            set
            {
                if (value == _name)
                    return;

                this._name = value;
                this.MarkUnsaved();
                RaisePropertyChanged(() => Name);
            }
        }

        private readonly ObservableCollection<Asset> _worlds;
        public IEnumerable<Asset> Worlds
        {
            get { return _worlds; }
        }

        private Asset _selectedWorld;
        public Asset SelectedWorld
        {
            get { return _selectedWorld; }
            set
            {
                if (value == _selectedWorld)
                    return;

                this._selectedWorld = value;
                RaisePropertyChanged(() => SelectedWorld);
            }
        }

        private Int32 _upArrows;
        public Int32 UpArrows
        {
            get { return _upArrows; }
            set
            {
                if (value == _upArrows)
                    return;

                this._upArrows = value;
                this.MarkUnsaved();
                RaisePropertyChanged(() => UpArrows);
            }
        }

        private Int32 _downArrows;
        public Int32 DownArrows
        {
            get { return _downArrows; }
            set
            {
                if (value == _downArrows)
                    return;

                this._downArrows = value;
                this.MarkUnsaved();
                RaisePropertyChanged(() => DownArrows);
            }
        }

        private Int32 _leftArrows;
        public Int32 LeftArrows
        {
            get { return _leftArrows; }
            set
            {
                if (value == _leftArrows)
                    return;

                this._leftArrows = value;
                this.MarkUnsaved();
                RaisePropertyChanged(() => LeftArrows);
            }
        }

        private Int32 _rightArrows;
        public Int32 RightArrows
        {
            get { return _rightArrows; }
            set
            {
                if (value == _rightArrows)
                    return;

                this._rightArrows = value;
                this.MarkUnsaved();
                RaisePropertyChanged(() => RightArrows);
            }
        }

        private Int32 _bombs;
        public Int32 Bombs
        {
            get { return _bombs; }
            set
            {
                if (value == _bombs)
                    return;

                this._bombs = value;
                this.MarkUnsaved();
                RaisePropertyChanged(() => Bombs);
            }
        }

        [ImportingConstructor]
        public LevelEditorViewModel(IPackageRepository repository)
        {
            this._repository = repository;

            this._worlds = new ObservableCollection<Asset>();

            foreach (var asset in repository.Assets.Where(x => x.Type == EditorAssetTypes.World).OrderBy(x => x.Key))
            {
                _worlds.Add(asset);
            }

            this.SelectedWorld = _worlds.FirstOrDefault();
        }

        protected override LevelResource MoveViewToModel()
        {
            var model = new LevelResource();
            model.Key = this.Asset.Key;

            model.Name = Name;
            model.World = SelectedWorld.Key;
            model.UpArrows = UpArrows;
            model.DownArrows = DownArrows;
            model.LeftArrows = LeftArrows;
            model.RightArrows = RightArrows;
            model.Bombs = Bombs;

            return model;
        }

        protected override void MoveModelToView(LevelResource model)
        {
            this.Name = model.Name;
            this.SelectedWorld = _worlds.Where(x => x.Key == model.World).FirstOrDefault();
            this.UpArrows = model.UpArrows;
            this.DownArrows = model.DownArrows;
            this.LeftArrows = model.LeftArrows;
            this.RightArrows = model.RightArrows;
            this.Bombs = model.Bombs;
        }
    }
}
