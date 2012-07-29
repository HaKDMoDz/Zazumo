
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
    public class FrameSetEditorViewModel : EditorViewModel<FrameSetResource>
    {
        private readonly IPackageRepository _repository;
        private readonly SpriteLoader _spriteLoader;
        private readonly DispatcherTimer _timer;

        private DelegateCommand _deleteFrameCommand;
        public ICommand DeleteFrame
        {
            get { return _deleteFrameCommand; }
        }

        private DelegateCommand _addFrameCommand;
        public ICommand AddFrame
        {
            get { return _addFrameCommand; }
        }

        private SpriteViewModel _animation;
        public SpriteViewModel Animation
        {
            get { return _animation; }
            set
            {
                this._animation = value;
                RaisePropertyChanged(() => Animation);
            }
        }

        private readonly ObservableCollection<SpriteViewModel> _frames;
        public IEnumerable<SpriteViewModel> Frames
        {
            get { return _frames; }
        }

        private SpriteViewModel _selectedFrame;
        public SpriteViewModel SelectedFrame
        {
            get { return _selectedFrame; }
            set
            {
                if (value == _selectedFrame)
                    return;

                this._selectedFrame = value;
                RaisePropertyChanged(() => SelectedFrame);
            }
        }

        private readonly ObservableCollection<Asset> _sprites;
        public IEnumerable<Asset> Sprites
        {
            get { return _sprites; }
        }

        private Asset _selectedSprite;
        public Asset SelectedSprite
        {
            get { return _selectedSprite; }
            set
            {
                if (value == _selectedSprite)
                    return;

                this._selectedSprite = value;
                RaisePropertyChanged(() => SelectedSprite);
            }
        }

        private Single _frameDuration;
        public Single FrameDuration
        {
            get { return _frameDuration; }
            set
            {
                if (value == _frameDuration)
                    return;

                this._frameDuration = value;
                this.MarkUnsaved();
                this._timer.Stop();
                this._timer.Interval = TimeSpan.FromSeconds(_frameDuration);
                this._timer.Start();
                RaisePropertyChanged(() => FrameDuration);
            }
        }

        [ImportingConstructor]
        public FrameSetEditorViewModel(IPackageRepository repository, SpriteLoader spriteLoader)
        {
            this._repository = repository;
            this._spriteLoader = spriteLoader;
            this._frames = new ObservableCollection<SpriteViewModel>();
            this._timer = new DispatcherTimer(DispatcherPriority.Render);
            this._frameDuration = 1f;
            
            this._sprites = new ObservableCollection<Asset>();

            foreach (var asset in repository.Assets.Where(x => x.Type == EditorAssetTypes.Sprite).OrderBy(x => x.Key))
            {
                _sprites.Add(asset);
            }

            this.SelectedSprite = _sprites.FirstOrDefault();

            this._addFrameCommand = new DelegateCommand(AddFrameExecute, AddFrameCanExecute);
            this._deleteFrameCommand = new DelegateCommand(DeleteFrameExecute, DeleteFrameCanExecute);

            this._timer.Interval = TimeSpan.FromSeconds(_frameDuration);
            this._timer.Tick += new EventHandler(_timer_Tick);
            this._timer.Start();
        }
        
        protected override FrameSetResource MoveViewToModel()
        {
            var model = new FrameSetResource();
            model.Key = this.Asset.Key;

            var frameKeys = new List<String>();
            foreach (var frame in Frames)
            {
                frameKeys.Add(frame.SpriteKey);
            }

            model.FrameKeys = frameKeys.ToArray();
            model.FrameDuration = FrameDuration;

            return model;
        }

        protected override void MoveModelToView(FrameSetResource model)
        {
            if (model.FrameKeys == null)
                return;

            foreach (var frameKey in model.FrameKeys)
            {
                _frames.Add(_spriteLoader.LoadSprite(frameKey));
            }

            this.FrameDuration = model.FrameDuration;            
        }

        public void AddFrameExecute()
        {
            var newFrame = _spriteLoader.LoadSprite(this.SelectedSprite.Key);
            this._frames.Add(newFrame);
            this.MarkUnsaved();
        }

        public Boolean AddFrameCanExecute()
        {
            return SelectedSprite != null;
        }

        public void DeleteFrameExecute()
        {
            this._frames.Remove(this.SelectedFrame);
            this.MarkUnsaved();
        }

        public Boolean DeleteFrameCanExecute()
        {
            return this.SelectedFrame != null;
        }

        private Int32 _frameIndex;

        void _timer_Tick(object sender, EventArgs e)
        {
            if (_frames.Count == 0)
                return;

            _frameIndex++;
            _frameIndex %= _frames.Count;

            Animation = _frames[_frameIndex];
        }
    }
}
