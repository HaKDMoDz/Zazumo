﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces;
using Phat.ActorResources;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    public class ArchetypeBasedMirrorWorldObjectViewModel : ArchetypeBasedConcreteWorldObjectViewModel
    {
        public ArchetypeBasedMirrorWorldObjectViewModel(IPackageRepository packageRepository, SpriteLoader spriteLoader)
            : base(packageRepository, spriteLoader)
        {
        }

        public override IWorldObject MoveViewToModel()
        {
            var model = new ArchetypeBasedMirrorWorldObject();

            model.SetArchetypeData(Data);
            model.ArchetypeKey = ArchetypeKey;
            model.Id = this.Id;
            model.Name = this.Name;
            model.X = this.X;
            model.Y = this.Y;

            return model;
        }
    }
}
