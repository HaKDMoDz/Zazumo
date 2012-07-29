using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#if WINDOWS_PHONE
using Microsoft.Xna.Framework.Input.Touch;
#endif
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.IO;
using Phat;
using Phat.Messages;
using Phat.ActorResources;
using Phat.Visual.Xna;
using Phat.ActorModel;
using Phat.ActorPhysics;
using Phat.ActorVisuals;
using Phat.Zazumo.Controllers.Splash;
using Phat.Zazumo.Resources;
using Phat.Zazumo.Actors;
using Phat.Zazumo.Physics;
using Phat.Zazumo.Visuals;

namespace Phat.Zazumo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : PhatGame
    {
        Color[] backgroundColors = { Color.Black, Color.CornflowerBlue, Color.Pink, Color.Olive, Color.Red, Color.Purple, Color.Brown, Color.Navy, Color.White, Color.Yellow, Color.Teal, Color.Orange};

        public Game1()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
                        
            InitializeAndRun<SplashGameMode, SaveData>();

            Phat.Settings.MetersToPixels = (Single)GraphicsDevice.PresentationParameters.BackBufferWidth / 10f;
        }

        protected override void LoadResources(IResourceDictionary resources)
        {
            base.LoadResources(resources);

            #region Physics
            // Physics
            Dictionary<String, Object> physics = new Dictionary<String, Object>();
            physics.Add("TileCollisionHull", new Vector2[] 
            { 
                new Vector2(0f, 0f), 
                new Vector2(1f, 0f), 
                new Vector2(1f, 1f), 
                new Vector2(0f, 1f) 
            });

            physics.Add("UnitCollisionHull", new Vector2[] 
            { 
                new Vector2(0f, 0f), 
                new Vector2(1f, 0f), 
                new Vector2(1f, 1f), 
                new Vector2(0f, 1f) 
            });

            physics.Add("WormSegmentCollisionHull", new Vector2[] 
            { 
                new Vector2(0f, 0f), 
                new Vector2(0.5f, 0f), 
                new Vector2(0.5f, 0.5f), 
                new Vector2(0f, 0.5f) 
            });

            physics.Add("ZazumoNormalHull", new Vector2[] 
            { 
                new Vector2(0.35f, 0.35f), 
                new Vector2(0.65f, 0.35f), 
                new Vector2(0.65f, 0.65f), 
                new Vector2(0.35f, 0.65f) 
            });


            physics.Add("WormholeCollisionHull", new Vector2[] 
            { 
                new Vector2(0f, 0f), 
                new Vector2(0.5f, 0f), 
                new Vector2(0.5f, 0.5f), 
                new Vector2(0f, 0.5f) 
            });
            #endregion

            #region GameData
            // Game data
            Dictionary<String, Object> gameData = new Dictionary<String, Object>();
            gameData.Add("ZazumoData", new ZazumoArchetypeData 
            { 
                Height = 0.75f, 
                Width = 0.465f, 
                Damping = 2.5f,
                Speed = 40.0f,
                BulletSpeed = 5.0f,
                SpriteKey = "Zazumo.Sprites.Zazumo" 
            });                       

            #region Shape Enemies
            gameData.Add("TriangleData", new CharacterArchetypeData
            {
                Height = 0.333f,
                Width = 0.5f,
                SpriteKey = "Zazumo.Sprites.TriangleShape",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("SquareData", new CharacterArchetypeData
            {
                Height = 0.5f,
                Width = 0.5f,
                SpriteKey = "Zazumo.Sprites.SquareShape",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });


            gameData.Add("SquiggleData", new CharacterArchetypeData
            {
                Height = 0.5f,
                Width = 0.5f,
                SpriteKey = "Zazumo.Sprites.SquiggleShape",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("ArrowData", new CharacterArchetypeData
            {
                Height = 0.6f,
                Width = 0.8f,
                SpriteKey = "Zazumo.Sprites.ArrowShape",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("CircleData", new CharacterArchetypeData
            {
                Height = 0.5f,
                Width = 0.5f,
                SpriteKey = "Zazumo.Sprites.CircleShape",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("DiamondData", new CharacterArchetypeData
            {
                Height = 0.333f,
                Width = 0.666f,
                SpriteKey = "Zazumo.Sprites.DiamondShape",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });
            #endregion

            #region Mini Bosses
            gameData.Add("WormHeadData", new CharacterArchetypeData
            {
                Height = 0.65f,
                Width = 0.65f,
                SpriteKey = "Zazumo.Sprites.Wormhead",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("WormSegmentData", new CharacterArchetypeData
            {
                Height = 0.5f,
                Width = 0.5f,
                SpriteKey = "Zazumo.Sprites.WormSegment",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("EyeData", new CharacterArchetypeData
            {
                Height = 0.5f,
                Width = 1.2f,
                SpriteKey = "Zazumo.Sprites.Eye",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("BoulderData", new CharacterArchetypeData
            {
                Height = 1.5f,
                Width = 1.0f,
                SpriteKey = "Zazumo.Sprites.Boulder",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("SquidData", new CharacterArchetypeData
            {
                Height = 1.75f,
                Width = 1.0f,
                SpriteKey = "Zazumo.Sprites.Squid1",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("SquidArmData", new CharacterArchetypeData
            {
                Height = 0.3f,
                Width = 0.3f,
                SpriteKey = "Zazumo.Sprites.SquidBall",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            #endregion

            #region Power Ups
            gameData.Add("ClamData", new ClamData
            {
                Height = 1.0f,
                Width = 1.0f,
                SpriteKey = "Zazumo.Sprites.Clam1",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1,
                Shape = ZazumoShape.Star,
                HitPoints = 10
            });

            gameData.Add("StarPowerUpData", new PowerUpData
            {
                Height = 1.0f,
                Width = 1.0f,
                SpriteKey = "Zazumo.Sprites.StarPickup",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1,
                Shape = ZazumoShape.Star
            });

            gameData.Add("PurpleFrogData", new FrogData
            {
                Height = 1.4f,
                Width = 1.0f,
                FrogEffect = Resources.FrogEffect.Shrink,
                SpriteKey = "Zazumo.Sprites.PurpleFrog",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("YellowFrogData", new FrogData
            {
                Height = 1.4f,
                Width = 1.0f,
                FrogEffect = Resources.FrogEffect.Ammo,
                SpriteKey = "Zazumo.Sprites.AmmoFrog1",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("GreenFrogData", new FrogData
            {
                Height = 1.4f,
                Width = 1.0f,
                FrogEffect = Resources.FrogEffect.Grow,
                SpriteKey = "Zazumo.Sprites.GreenFrog",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("BlueFrogData", new FrogData
            {
                Height = 1.4f,
                Width = 1.0f,
                FrogEffect = Resources.FrogEffect.Bomb,
                SpriteKey = "Zazumo.Sprites.BlueFrog1",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });

            gameData.Add("BlackFrogData", new FrogData
            {
                Height = 1.4f,
                Width = 1.0f,
                FrogEffect = Resources.FrogEffect.Points,
                SpriteKey = "Zazumo.Sprites.BlackFrog1",
                CollisionHullKey = "TileCollisionHull",
                CollisionGroup = 1
            });


            #endregion

            #region Wormholes
            gameData.Add("StarWormhole", new WormholeData
            {
                Height = 1f,
                Width = 1f,
                SpriteKey = "Zazumo.Sprites.StarWormhole",
                CollisionGroup = 1,
                CollisionHullKey = "WormholeCollisionHull"
            });

            gameData.Add("SwirlWormhole", new WormholeData
            {
                Height = 1f,
                Width = 1f,
                SpriteKey = "Zazumo.Sprites.SwirlWormhole",
                CollisionGroup = 1,
                CollisionHullKey = "WormholeCollisionHull"
            });
            #endregion

            gameData.Add("Bullet", new ProjectileData
            {
                Height = 0.325f,
                Width = 0.325f,
                Speed = 1.0f,
                CollisionGroup = 2,
                SpriteKey = "Zazumo.Sprites.Bullet"
            });

            gameData.Add("StarBullet", new ProjectileData
            {
                Height = 0.3f,
                Width = 0.3f,
                Speed = 2.0f,
                CollisionGroup = 2,
                SpriteKey = "Zazumo.Sprites.StarBullet"
            });

            gameData.Add("EyeBullet", new ProjectileData
            {
                Height = 0.15f,
                Width = 0.15f,
                Speed = 2.0f,
                CollisionGroup = 1,
                SpriteKey = "Zazumo.Sprites.EyeBullet"
            });

            gameData.Add("HorizontalWall", new VolumeWorldObject
            {
                Width = GraphicsDevice.PresentationParameters.BackBufferWidth / Phat.Settings.MetersToPixels,
                Height = 0.01f
            });

            gameData.Add("VerticalWall", new VolumeWorldObject
            {
                Height = GraphicsDevice.PresentationParameters.BackBufferHeight / Phat.Settings.MetersToPixels,
                Width = 0.01f
            });
            #endregion

            #region Paths
            gameData.Add("WormPath1", new Vector2[]
            {
                new Vector2(-1.5f,0.5f),
                new Vector2(0.0f, 1.0f),
                new Vector2(4.5f, 1.5f),
                new Vector2(6.5f, 2.5f),
                new Vector2(4.5f, 3.5f),
                new Vector2(2.5f, 2.5f),
                /*new Vector2(4.5f, 1.5f),
                new Vector2(6.5f, 2.5f),                */
            });

            gameData.Add("EyePath1", new Vector2[]
            {
                new Vector2(4.5f, 2.5f),
                new Vector2(7f, 0.5f),
                new Vector2(8.5f, 2.5f),
                new Vector2(7f, 4.5f),
                new Vector2(4.5f, 2.5f),
                new Vector2(3f, 0.5f),
                new Vector2(0.5f, 2.5f),
                new Vector2(3f, 4.5f)                
            });

            gameData.Add("EyePath2", new Vector2[]
            {
                new Vector2(4.5f, 2.5f),
                new Vector2(3f, 0.5f),
                new Vector2(0.5f, 2.5f),
                new Vector2(3f, 4.5f),
                new Vector2(4.5f, 2.5f),
                new Vector2(7f, 0.5f),
                new Vector2(8.5f, 2.5f),
                new Vector2(7f, 4.5f),

            });

            gameData.Add("EyePath3", new Vector2[]
            {
                new Vector2(4.5f, 2.5f),
                new Vector2(7f, 4.5f),                
                new Vector2(8.5f, 2.5f),
                new Vector2(7f, 0.5f),
                new Vector2(4.5f, 2.5f),
                new Vector2(3f, 4.5f),
                new Vector2(0.5f, 2.5f),
                new Vector2(3f, 0.5f),                               
            });

            gameData.Add("EyePath4", new Vector2[]
            {
                new Vector2(4.5f, 2.5f),
                new Vector2(3f, 4.5f),
                new Vector2(0.5f, 2.5f),
                new Vector2(3f, 0.5f),                
                new Vector2(4.5f, 2.5f),
                new Vector2(7f, 4.5f),
                new Vector2(8.5f, 2.5f),
                new Vector2(7f, 0.5f),
            });
            #endregion            


            #region LevelSpawnData
            gameData.Add("Level1NormalSpawnData", new LevelSpawnData[]
            {
                new LevelSpawnData { SpawnType = SpawnType.Triangle, Data = new TriangleShapeData { StartPosition = StartPosition.Left } },
                new LevelSpawnData { SpawnType = SpawnType.Triangle, Data = new TriangleShapeData { StartPosition = StartPosition.Left } },
                new LevelSpawnData { SpawnType = SpawnType.Triangle, Data = new TriangleShapeData { StartPosition = StartPosition.Right } },
                new LevelSpawnData { SpawnType = SpawnType.Triangle, Data = new TriangleShapeData { StartPosition = StartPosition.Right } },
                new LevelSpawnData { SpawnType = SpawnType.Square, Data = new SquareShapeData { StartPosition = StartPosition.Top } },
                new LevelSpawnData { SpawnType = SpawnType.Square, Data = new SquareShapeData { StartPosition = StartPosition.Top } },
                new LevelSpawnData { SpawnType = SpawnType.Square, Data = new SquareShapeData { StartPosition = StartPosition.Bottom } },
                new LevelSpawnData { SpawnType = SpawnType.Square, Data = new SquareShapeData { StartPosition = StartPosition.Bottom } },
                new LevelSpawnData { SpawnType = SpawnType.Squiggle, Data = new SquiggleShapeData { StartPosition = StartPosition.Left } },
                new LevelSpawnData { SpawnType = SpawnType.Squiggle, Data = new SquiggleShapeData { StartPosition = StartPosition.Left } },
                new LevelSpawnData { SpawnType = SpawnType.Squiggle, Data = new SquiggleShapeData { StartPosition = StartPosition.Right } },
                new LevelSpawnData { SpawnType = SpawnType.Squiggle, Data = new SquiggleShapeData { StartPosition = StartPosition.Right } },
                new LevelSpawnData { SpawnType = SpawnType.Arrow, Data = new ArrowShapeData { } },
                new LevelSpawnData { SpawnType = SpawnType.Arrow, Data = new ArrowShapeData { } },
                new LevelSpawnData { SpawnType = SpawnType.Arrow, Data = new ArrowShapeData { } },
                new LevelSpawnData { SpawnType = SpawnType.Arrow, Data = new ArrowShapeData { } },
                new LevelSpawnData { SpawnType = SpawnType.Circle, Data = new CircleShapeData { StartPosition = StartPosition.TopLeft } },
                new LevelSpawnData { SpawnType = SpawnType.Circle, Data = new CircleShapeData { StartPosition = StartPosition.TopRight } },
                new LevelSpawnData { SpawnType = SpawnType.Circle, Data = new CircleShapeData { StartPosition = StartPosition.BottomLeft } },
                new LevelSpawnData { SpawnType = SpawnType.Circle, Data = new CircleShapeData { StartPosition = StartPosition.BottomRight } },
                new LevelSpawnData { SpawnType = SpawnType.Diamond, Data = new DiamondShapeData { } },
                new LevelSpawnData { SpawnType = SpawnType.Diamond, Data = new DiamondShapeData { } },
                new LevelSpawnData { SpawnType = SpawnType.Diamond, Data = new DiamondShapeData { } },
                new LevelSpawnData { SpawnType = SpawnType.Diamond, Data = new DiamondShapeData { } },
            });
            #endregion

            // Fonts
            var fontDictionary = new Dictionary<String, Object>();
            fontDictionary.Add("SmallPointsFont", Content.Load<SpriteFont>("Fonts/SmallPoints"));
            fontDictionary.Add("BigPointsFont", Content.Load<SpriteFont>("Fonts/BigPoints"));
            fontDictionary.Add("ScoreFont", Content.Load<SpriteFont>("Fonts/Score"));
            fontDictionary.Add("MainFont", Content.Load<SpriteFont>("Fonts/MainFont"));
            

            resources.MergeResources("Fonts", fontDictionary);
            resources.MergeResources("GameData", gameData);
            resources.MergeResources("Physics", physics);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var colorIndex = (Int32)(Math.Floor(gameTime.TotalGameTime.TotalSeconds / 5f));
            colorIndex = colorIndex % backgroundColors.Length;
            var nextColorIndex = (colorIndex + 1) % backgroundColors.Length;
            var colorInterpolationPosition = (Single)((gameTime.TotalGameTime.TotalSeconds / 5f) - (Math.Floor(gameTime.TotalGameTime.TotalSeconds / 5f)));
            
            var c1 = backgroundColors[colorIndex].ToVector3();
            var c2 = backgroundColors[nextColorIndex].ToVector3();
            
#if !MONO
            BackgroundColor = new Color(
                (c2.X * colorInterpolationPosition + c1.X * (1f - colorInterpolationPosition)),
                (c2.Y * colorInterpolationPosition + c1.Y * (1f - colorInterpolationPosition)),
                (c2.Z * colorInterpolationPosition + c1.Z * (1f - colorInterpolationPosition))
            );
#else
            BackgroundColor = Color.White;
#endif
        }

        protected override void RegisterPhysics(IActorPhysicsFactory physicsFactory, IResourceDictionary resourceDictionary)
        {
            base.RegisterPhysics(physicsFactory, resourceDictionary);

            physicsFactory.RegisterPhysicsFactory<ZazumoActor>(x => new ZazumoPhysics(x, resourceDictionary));
            physicsFactory.RegisterPhysicsFactory<TriangleEnemy>(x => new CharacterPhysics(x, resourceDictionary));
            physicsFactory.RegisterPhysicsFactory<EnemyActor>(x => new StaticPhysics(x, resourceDictionary));
            physicsFactory.RegisterPhysicsFactory<FrogActor>(x => new StaticPhysics(x, resourceDictionary));
            physicsFactory.RegisterPhysicsFactory<PowerUpActor>(x => new StaticPhysics(x, resourceDictionary));
            physicsFactory.RegisterPhysicsFactory<ProjectileActor>(x => new ProjectilePhysics(x, resourceDictionary));
            physicsFactory.RegisterPhysicsFactory<WormholeActor>(x => new StarGatePhysics(x, resourceDictionary));
        }

        protected override void RegisterVisuals(IActorVisualFactory actorVisualFactory, IResourceDictionary resourceDictionary, SpriteBatch spriteBatch)
        {
            base.RegisterVisuals(actorVisualFactory, resourceDictionary, spriteBatch);

            actorVisualFactory.RegisterVisualFactory<WormActor>(x => new NullVisual(x));
            actorVisualFactory.RegisterVisualFactory<EyeFlockActor>(x => new NullVisual(x));
            actorVisualFactory.RegisterVisualFactory<BoulderFlockActor>(x => new NullVisual(x));
            actorVisualFactory.RegisterVisualFactory<WormFlockActor>(x => new NullVisual(x));
            actorVisualFactory.RegisterVisualFactory<TriangleFlockActor>(x => new NullVisual(x));
            actorVisualFactory.RegisterVisualFactory<ArrowFlockActor>(x => new NullVisual(x));
            actorVisualFactory.RegisterVisualFactory<CircleFlockActor>(x => new NullVisual(x));
            actorVisualFactory.RegisterVisualFactory<DiamondFlockActor>(x => new NullVisual(x));
            actorVisualFactory.RegisterVisualFactory<SquareFlockActor>(x => new NullVisual(x));
            actorVisualFactory.RegisterVisualFactory<SquiggleFlockActor>(x => new NullVisual(x));
            actorVisualFactory.RegisterVisualFactory<AmmoMeter>(x => new AmmoMeterVisual(x, spriteBatch, resourceDictionary));
        }           
    }
}
