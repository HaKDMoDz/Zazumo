using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;

namespace Phat.Zazumo.Resources
{
    public enum FrogEffect
    {
        Shrink,
        Grow,
        Ammo,
        Points,
        Bomb
    }

    public enum MovementDirection
    {
        LeftToRight,
        RightToLeft
    }

    public class FrogSpawnData
    {
        public Single Y { get; set; }
        public MovementDirection Direction { get; set; }
        public FrogEffect SizeAdjustment { get; set; }
    }

    public class FrogData : CharacterArchetypeData
    {
        public FrogEffect FrogEffect { get; set; }
    }
}
