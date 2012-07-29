using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    public class ArchetypeBasedConcreteWorldObjectViewModel : WorldObjectViewModel
    {
        private readonly IPackageRepository _packageRepository;
        private ConcreteWorldObjectArchetypeData _archetypeData;
        private String _archetypeKey;
        private readonly SpriteLoader _spriteLoader;

        public String ArchetypeKey
        {
            get { return _archetypeKey; }
        }

        protected WorldObjectArchetypeData Data
        {
            get { return _archetypeData; }
        }

        
        public Single Width
        {
            get { return _archetypeData.Width; }
        }

        public Single VisualWidth
        {
            get { return _archetypeData.Width * Phat.Settings.MetersToPixels; }
        }

        public Single VisualHeight
        {
            get { return _archetypeData.Height * Phat.Settings.MetersToPixels; }
        }

        public Single Height
        {
            get { return _archetypeData.Height; }
        }

        public String SpriteKey
        {
            get { return _archetypeData.SpriteKey; }
        }

        private SpriteViewModel _sprite;
        public SpriteViewModel Sprite
        {
            get { return _sprite; }
            set
            {
                this._sprite = value;
                RaisePropertyChanged(() => Sprite);
            }
        }

        public ArchetypeBasedConcreteWorldObjectViewModel(IPackageRepository packageRepository, SpriteLoader spriteLoader)
        {
            this._packageRepository = packageRepository;
            this._spriteLoader = spriteLoader;
        }

        public override void SetModel(IWorldObject model)
        {
            var m = model as ArchetypeBasedConcreteWorldObject;

            this._archetypeKey = m.ArchetypeKey;

            if (m.ArchetypeData != null)
                this._archetypeData = (ConcreteWorldObjectArchetypeData)m.ArchetypeData;
            else
            {
                var archetype = _packageRepository.Assets.Where(x => x.Key == _archetypeKey).Single();
                this._archetypeData = (ConcreteWorldObjectArchetypeData)_packageRepository.GetAssetData<ArchetypeResource>(archetype.Id).Data;
                m.SetArchetypeData(this._archetypeData);
            }

            this.Sprite = _spriteLoader.LoadSprite(_archetypeData.SpriteKey);

            base.SetModel(model);
        }

        public override IWorldObject MoveViewToModel()
        {
            var model = new ArchetypeBasedConcreteWorldObject();

            model.SetArchetypeData(_archetypeData);
            model.ArchetypeKey = _archetypeKey;
            model.Id = this.Id;
            model.Name = this.Name;
            model.X = this.X;
            model.Y = this.Y;
                        
            return model;
        }
    }
}
