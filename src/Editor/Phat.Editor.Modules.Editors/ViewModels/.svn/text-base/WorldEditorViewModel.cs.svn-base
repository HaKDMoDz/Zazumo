using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.ProfessionalBurglar.Resources;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class WorldEditorViewModel : EditorViewModel<WorldResource>
    {
        private readonly IWorldEditorContext _context;

        public IWorldEditorContext Context
        {
            get { return _context; }
        }

        [ImportingConstructor]
        public WorldEditorViewModel(IWorldEditorContext context)
        {
            this._context = context;
            this._context.RootEditor = this;
        }

        protected override WorldResource MoveViewToModel()
        {
            WorldResource world = new WorldResource();

            world.Key = this.Asset.Key;

            List<IWorldObject> worldObjects = new List<IWorldObject>();
            foreach (var viewModel in this._context.GetAllObjects())
            {
                worldObjects.Add(viewModel.MoveViewToModel());
            }

            world.Objects = worldObjects.ToArray();

            return world;
        }

        public void MarkChanges()
        {
            this.MarkUnsaved();
        }

        protected override void MoveModelToView(WorldResource model)
        {
            _context.World = model;
            _context.SetEditor(ViewNames.WorldEditor_World);
            _context.SetEditorTools(ViewNames.WorldEditorTools_World);
        }
    }
}
