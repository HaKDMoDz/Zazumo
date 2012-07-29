using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorResources;

namespace Phat.Zazumo.Resources
{
#if !WINDOWS_PHONE
    [Serializable]
#endif

    public enum SpawnType
    {
        Frog,

        Triangle,
        Square,
        Diamond,
        Arrow,
        Circle,
        Squiggle,

        Worm,
        Eye,
        Boulder,        

        Squid
    }

    public enum StartPosition
    {
        Top,
        Left,
        Right,
        Bottom,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public enum MoveDirection
    {
        Left,
        Right,
    }
   
   
    public class LevelSpawnData
    {
        public SpawnType SpawnType { get; set; }
        public Object Data { get; set; }        
    }

    public class WormSpawnData
    {
        public String MotionPathKey { get; set; }
        public Int32 SegmentCount { get; set; }
    }

    public class WormholeData : IDrawable, IPhysicalObject
    {
        public ZazumoShape Shape { get; set; }
        public String SpriteKey { get; set; }
        public Single Height { get; set; }
        public Single Width { get; set; }
        public Int32 Size { get; set; }
        public String CollisionHullKey { get; set; }
        public Int16 CollisionGroup { get; set; }
        public LevelSpawnData MiniBossData { get; set; }
    }

    public class TriangleShapeData
    {
        public StartPosition StartPosition { get; set; }
    }

    public class SquareShapeData
    {
        public StartPosition StartPosition { get; set; }
    }

    public class SquiggleShapeData
    {
        public StartPosition StartPosition { get; set; }
    }

    public class ArrowShapeData
    {
    }

    public class CircleShapeData
    {
        public StartPosition StartPosition { get; set; }
    }

    public class DiamondShapeData
    {
    }

    public class EyeData
    {
        public Int32 HitPoints { get; set; }
    }

    public class BoulderData
    {
        public Int32 HitPoints { get; set; }
        public Int32 EnemyCount { get; set; }
    }
}
