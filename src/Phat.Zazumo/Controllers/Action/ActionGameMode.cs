using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;
using Phat.Zazumo.Actors;
using Microsoft.Xna.Framework;
using Phat.Zazumo.Resources;
using Phat.ActorResources;
using Phat.Messages;
using Phat.Zazumo.Messages;
using Phat.Animations;

namespace Phat.Zazumo.Controllers.Action
{
    public class ActionGameMode : GameMode
    {
        private ZazumoActor _zazumo;
        private Rectangle _bounds;
        private CharacterArchetypeData zazumoData;
        private Random random;
        private Int32 _multiplier = 1;
        private TimeSpan _spawnRate = TimeSpan.FromSeconds(5.0);
        private TimeSpan _powerupRate = TimeSpan.FromSeconds(2.0);

        private LevelSpawnData[] _normalSpawnData;
        private TimeSpan _normalTotalTime;

        private FireButton _fireButton;
        private UIButtonActor __bomb1Button;
        private UIButtonActor __bomb2Button;
        private UIButtonActor __bomb3Button;
        private AmmoMeter _ammoMeter;
        private ZazumoShape _currentShape;

        private TimeSpan _powerUpTime = TimeSpan.Zero;
        private Int64 _score;
        private Int64 _lives;

        private UITextBlockActor _scoreTextBlock;
        private UITextBlockActor _livesTextBlock;

        private LevelSpawnData _growFrogData = new LevelSpawnData { SpawnType = SpawnType.Frog, Data = new FrogSpawnData { SizeAdjustment = FrogEffect.Grow } };
        private LevelSpawnData _shrinkFrogData = new LevelSpawnData { SpawnType = SpawnType.Frog, Data = new FrogSpawnData { SizeAdjustment = FrogEffect.Shrink } };
        private LevelSpawnData _ammoFrogData = new LevelSpawnData { SpawnType = SpawnType.Frog, Data = new FrogSpawnData { SizeAdjustment = FrogEffect.Ammo } };
        private LevelSpawnData _pointFrogData = new LevelSpawnData { SpawnType = SpawnType.Frog, Data = new FrogSpawnData { SizeAdjustment = FrogEffect.Points } };
        private LevelSpawnData _bombFrogData = new LevelSpawnData { SpawnType = SpawnType.Frog, Data = new FrogSpawnData { SizeAdjustment = FrogEffect.Bomb } };

        private Actor _background;
        private Actor _backgroundR;
        private Actor _backgroundCloud;
        private Actor _backgroundCloudR;

        private List<ZazumoShape> _levelShapeData = new List<ZazumoShape> { ZazumoShape.Star, ZazumoShape.Star, ZazumoShape.Star };

        private enum ZazumoActionMode
        {
            Normal,
            Shape,
            Boss
        }

        private ZazumoActionMode _mode;

        protected override void OnInitialize(Object initializationData)
        {
            random = new Random();
            _mode = ZazumoActionMode.Normal;
            Subscribe<PlayerDiedEvent>(OnPlayerDied);
            Subscribe<ZazumoShapeChangedEvent>(OnShapeChanged);
            Subscribe<WormholeClosedEvent>(OnWormholeClosed);
            Subscribe<MiniBossDestroyedEvent>(OnMiniBossDestroyed);
            Subscribe<EnemyDestoryedEvent>(OnEnemyDestroyed);
            Subscribe<EnemyFlockDestroyedEvent>(OnEnemyFlockDestroyed);
            Subscribe<AmmoDepletedEvent>(OnAmmoDepleted);
            Subscribe<ZazumoSizeChangedEvent>(OnSizeChanged);
            Subscribe<PointsAwardedEvent>(OnPointsAwarded);
            Subscribe<BigPointsAwardedEvent>(OnBigPointsAwarded);
            Subscribe<BombAwardedEvent>(OnBombAwarded);
            Subscribe<BombDeployedEvent>(OnBombDeployed);

            this._currentShape = ZazumoShape.None;

            ResourceDictionary.LoadPackage("Packages/Zazumo");
            ResourceDictionary.LoadPackage("Packages/Level1");

            this._lives = 5;

            _bounds = new Rectangle(0, 0, 10, 6);

            this._scoreTextBlock = ActorFactory.Create<UITextBlockActor>(new Object(), new Vector2(25f, 25f));
            this._scoreTextBlock.FontKey = "ScoreFont";
            this._scoreTextBlock.Text = "0";
            this._scoreTextBlock.TextAlignment = UITextBlockActor.Alignment.Right;

            this._livesTextBlock = ActorFactory.Create<UITextBlockActor>(new Object(), new Vector2(60f, 425f));
            this._livesTextBlock.FontKey = "MainFont";
            this._livesTextBlock.Text = "X" + (_lives - 1).ToString();
            this._livesTextBlock.Color = Color.Black;
            this._livesTextBlock.TextAlignment = UITextBlockActor.Alignment.Left;

            this._background = ActorFactory.Create<DecoratorActor>(new Drawable { Height = _bounds.Height, Width = _bounds.Width, SpriteKey = "Level1.Sprites.Level1Background" }, new Vector2(_bounds.Left, _bounds.Top));
            this._backgroundR = ActorFactory.Create<DecoratorActor>(new Drawable { Height = _bounds.Height, Width = _bounds.Width, SpriteKey = "Level1.Sprites.Level1BackgroundR" }, new Vector2(_bounds.Right, _bounds.Top));
            this._backgroundCloud = ActorFactory.Create<DecoratorActor>(new Drawable { Height = _bounds.Height, Width = _bounds.Width, SpriteKey = "Level1.Sprites.Level1Cloud" }, new Vector2(_bounds.Left, _bounds.Top));
            this._backgroundCloudR = ActorFactory.Create<DecoratorActor>(new Drawable { Height = _bounds.Height, Width = _bounds.Width, SpriteKey = "Level1.Sprites.Level1CloudR" }, new Vector2(_bounds.Right, _bounds.Top));
            this._background.Opacity = 0.75f;
            this._backgroundR.Opacity = 0.75f;
            this._backgroundCloud.Opacity = 0.75f;
            this._backgroundCloudR.Opacity = 0.75f;


            ActorFactory.Create<DecoratorActor>(new Drawable { Height = 0.75f, Width = 0.375f, SpriteKey = "Zazumo.Sprites.RemainingLives" }, new Vector2(0.4f, _bounds.Bottom - 1.05f)).Opacity = 0.75f;


            WormholeActor wormhole;
            wormhole = ActorFactory.Create<WormholeActor>(new WormholeData { Width = 1.0f, Height = 1.0f, SpriteKey = "Zazumo.Sprites.StarWormhole", Size = 1, CollisionHullKey = "WormholeCollisionHull", CollisionGroup = 1, Shape = ZazumoShape.Star, 
                MiniBossData = new LevelSpawnData { SpawnType = SpawnType.Eye, Data = new EyeData { HitPoints = 3 } } }, new Vector2(0.5f, 0.5f));
            wormhole.SetProperty("Center", new Vector2(0.5f, 0.5f));
            
            wormhole = ActorFactory.Create<WormholeActor>(new WormholeData { Width = 2.5f, Height = 2.5f, SpriteKey = "Zazumo.Sprites.StarWormhole", Size = 4, CollisionHullKey = "WormholeCollisionHull", CollisionGroup = 1, Shape = ZazumoShape.Star, MiniBossData = new LevelSpawnData { SpawnType = SpawnType.Boulder, Data = new BoulderData { HitPoints = 3, EnemyCount = 4 } } }, new Vector2(3f, 1f));
            wormhole.SetProperty("Center", new Vector2(0.5f, 0.5f));
            
            wormhole = ActorFactory.Create<WormholeActor>(new WormholeData { Width = 3f, Height = 3f, SpriteKey = "Zazumo.Sprites.StarWormhole", Size = 5, CollisionHullKey = "WormholeCollisionHull", CollisionGroup = 1, Shape = ZazumoShape.Star, MiniBossData = new LevelSpawnData { SpawnType = SpawnType.Eye, Data = new EyeData { HitPoints = 3 } } }, new Vector2(6f, 3f));
            wormhole.SetProperty("Center", new Vector2(0.5f, 0.5f));

            ActorFactory.Create<WallVolume>(new VolumeWorldObject { Width = _bounds.Width, Height = 0.1f }, new Vector2(_bounds.Left + 0.2f, _bounds.Top + 0.2f));
            ActorFactory.Create<WallVolume>(new VolumeWorldObject { Width = 0.1f, Height = _bounds.Height }, new Vector2(_bounds.Left + 0.2f, _bounds.Top + 0.2f));
            ActorFactory.Create<WallVolume>(new VolumeWorldObject { Width = _bounds.Width, Height = 0.1f }, new Vector2(_bounds.Left + 0.2f, _bounds.Bottom - 0.2f));
            ActorFactory.Create<WallVolume>(new VolumeWorldObject { Width = 0.1f, Height = _bounds.Height }, new Vector2(_bounds.Right - 0.2f, _bounds.Top + 0.2f));

            ActorFactory.Create<DecoratorActor>(new Drawable { Height = 0.25f, Width = 0.25f, SpriteKey = "Zazumo.Sprites.TopLeftCornerBorder" }, new Vector2(_bounds.Left + 0.19f, _bounds.Top + 0.2f));
            ActorFactory.Create<DecoratorActor>(new Drawable { Height = 0.05f, Width = _bounds.Width - 0.8f, SpriteKey = "Zazumo.Sprites.HorizontalBorder" }, new Vector2(_bounds.Left + 0.4f, _bounds.Top + 0.2f));
            ActorFactory.Create<DecoratorActor>(new Drawable { Height = 0.25f, Width = 0.25f, SpriteKey = "Zazumo.Sprites.TopRightBorder" }, new Vector2(_bounds.Right - 0.415f, _bounds.Top + 0.2f));
            ActorFactory.Create<DecoratorActor>(new Drawable { Height = _bounds.Height - 0.8f, Width = 0.05f, SpriteKey = "Zazumo.Sprites.VerticalBorder" }, new Vector2(_bounds.Right - 0.25f, _bounds.Top + 0.4f));
            ActorFactory.Create<DecoratorActor>(new Drawable { Height = 0.25f, Width = 0.25f, SpriteKey = "Zazumo.Sprites.BottomRightBorder" }, new Vector2(_bounds.Right - 0.415f, _bounds.Bottom - 0.425f));
            ActorFactory.Create<DecoratorActor>(new Drawable { Height = 0.05f, Width = _bounds.Width - 0.8f, SpriteKey = "Zazumo.Sprites.HorizontalBorder" }, new Vector2(_bounds.Left + 0.4f, _bounds.Bottom - 0.25f));
            ActorFactory.Create<DecoratorActor>(new Drawable { Height = 0.25f, Width = 0.25f, SpriteKey = "Zazumo.Sprites.BottomLeftBorder" }, new Vector2(_bounds.Left + 0.19f, _bounds.Bottom - 0.425f));
            ActorFactory.Create<DecoratorActor>(new Drawable { Height = _bounds.Height - 0.8f, Width = 0.05f, SpriteKey = "Zazumo.Sprites.VerticalBorder" }, new Vector2(_bounds.Left + 0.2f, _bounds.Top + 0.4f));

            
            _normalSpawnData = (LevelSpawnData[])ResourceDictionary.GetResource("Level1NormalSpawnData");
            CreateZazumo();

            CreateClam();
        }

        public override void Update(TimeSpan ellapsedTime)
        {
            base.Update(ellapsedTime);

            var BX = this._background.Location.X - 0.0075f;
            var BRX = this._backgroundR.Location.X - 0.0075f;
            var BCX = this._backgroundCloud.Location.X - 0.0175f;
            var BCRX = this._backgroundCloudR.Location.X - 0.0175f;

            if (BX < -10f)
                BX += 20f;

            if (BRX < -10f)
                BRX += 20f;

            if (BCX < -10f)
                BCX += 20f;

            if (BCRX < -10f)
                BCRX += 20f;

            this._background.SetLocation(BX, 0f, 0f);
            this._backgroundR.SetLocation(BRX, 0f, 0f);
            this._backgroundCloud.SetLocation(BCX, 0f, 0f);
            this._backgroundCloudR.SetLocation(BCRX, 0f, 0f);

            _powerUpTime = _powerUpTime.Add(ellapsedTime);
            if (_powerUpTime > _powerupRate)
            {
                if (_mode == ZazumoActionMode.Shape)
                {
                    var r = random.Next(0, 7);
                    if (r == 1)
                        Spawn(_shrinkFrogData);
                    else if (r == 2)
                        Spawn(_ammoFrogData);
                    else if (r == 3)
                        Spawn(_bombFrogData);
                    else if (r == 4)
                        Spawn(_pointFrogData);
                    else
                        Spawn(_growFrogData);                    
                }

                _powerUpTime = TimeSpan.Zero;
            }

            _normalTotalTime = _normalTotalTime.Add(ellapsedTime);

            if (_normalTotalTime > _spawnRate)
            {
                if (_mode != ZazumoActionMode.Boss)
                {
                    var index = random.Next(0, _normalSpawnData.Length);
                    Spawn(_normalSpawnData[index]);
                }

                _normalTotalTime = TimeSpan.Zero;
            }

            ViewPort.Left = (Int32)(Math.Min(_bounds.Right + 0.0f - (ViewPort.ResolutionX / Phat.Settings.MetersToPixels), Math.Max(_bounds.Left - 0.0f, (_zazumo.Location.X + zazumoData.Width / 2f) - (ViewPort.ResolutionX / 2f / Phat.Settings.MetersToPixels))) * Phat.Settings.MetersToPixels);
            ViewPort.Top = (Int32)(Math.Min(_bounds.Bottom + 0.0f - (ViewPort.ResolutionY / Phat.Settings.MetersToPixels), Math.Max(_bounds.Top - 0.0f, (_zazumo.Location.Y + zazumoData.Height / 2f) - (ViewPort.ResolutionY / 2f / Phat.Settings.MetersToPixels))) * Phat.Settings.MetersToPixels);
        }

        private void Spawn(LevelSpawnData spawnInfo)
        {
            switch (spawnInfo.SpawnType)
            {
                case SpawnType.Worm:
                    SpawnWorm((WormSpawnData)spawnInfo.Data);
                    break;
                case SpawnType.Frog:
                    SpawnFrog((FrogSpawnData)spawnInfo.Data);
                    break;

                case SpawnType.Boulder:
                    SpawnBoulder((BoulderData)spawnInfo.Data);
                    break;

                case SpawnType.Eye:
                    SpawnEye((EyeData)spawnInfo.Data);
                    break;

                case SpawnType.Triangle:
                    SpawnTriangle((TriangleShapeData)spawnInfo.Data);
                    break;

                case SpawnType.Square:
                    SpawnSquare((SquareShapeData)spawnInfo.Data);
                    break;
                    
                case SpawnType.Squiggle:
                    SpawnSquiggle((SquiggleShapeData)spawnInfo.Data);
                    break;

                case SpawnType.Arrow:
                    SpawnArrow((ArrowShapeData)spawnInfo.Data);
                    break;

                case SpawnType.Circle:
                    SpawnCircle((CircleShapeData)spawnInfo.Data);
                    break;

                case SpawnType.Diamond:
                    SpawnDiamond((DiamondShapeData)spawnInfo.Data);
                    break;


                default:
                    break;
            }
        }


        private void SpawnBoss(SpawnType spawnType)
        {
            switch (spawnType)
            {
                case SpawnType.Squid:
                    var controller = AddActorController<SquidController>(ActorFactory.Create<SquidBoss>(ResourceDictionary.GetResource("SquidData"), new Vector2(7f, 1.75f)));
                    controller.Zazumo = this._zazumo;
                    break;
            }
        }


        private void SpawnWorm(WormSpawnData data)
        {
            var path = (Vector2[])ResourceDictionary.GetResource(data.MotionPathKey);

            var worm = ActorFactory.Create<WormFlockActor>(data, new Vector2(path[0].X, path[0].Y));
            var controller = AddActorController<WormController>(worm);
        }

        private void SpawnEye(EyeData data)
        {
            var eye = ActorFactory.Create<EyeFlockActor>(data, Vector2.Zero);
            var controller = AddActorController<EyeController>(eye);
        }

        private void SpawnBoulder(BoulderData data)
        {
            var enemy = ActorFactory.Create<BoulderFlockActor>(data, Vector2.Zero);
            var controller = AddActorController<BoulderController>(enemy);
        }

        private void SpawnTriangle(TriangleShapeData data)
        {
            var triangle = ActorFactory.Create<TriangleFlockActor>(data, Vector2.Zero);
            var controller = AddActorController<TriangleController>(triangle);
        }

        private void SpawnArrow(ArrowShapeData data)
        {
            var arrow = ActorFactory.Create<ArrowFlockActor>(data, Vector2.Zero);
            var controller = AddActorController<ArrowController>(arrow);
        }

        private void SpawnCircle(CircleShapeData data)
        {
            var flock = ActorFactory.Create<CircleFlockActor>(data, Vector2.Zero);
            var controller = AddActorController<CircleController>(flock);
        }

        private void SpawnDiamond(DiamondShapeData data)
        {
            var flock = ActorFactory.Create<DiamondFlockActor>(data, Vector2.Zero);
            var controller = AddActorController<DiamondController>(flock);
        }

        private void SpawnSquare(SquareShapeData data)
        {
            var enemy = ActorFactory.Create<SquareFlockActor>(data, Vector2.Zero);
            var controller = AddActorController<SquareController>(enemy);
        }

        private void SpawnSquiggle(SquiggleShapeData data)
        {
            var enemy = ActorFactory.Create<SquiggleFlockActor>(data, Vector2.Zero);
            var controller = AddActorController<SquiggleController>(enemy);
        }

        private void SpawnFrog(FrogSpawnData data)
        {
            FrogActor frog = null;

            switch (data.SizeAdjustment)
            {
                case FrogEffect.Shrink:
                    var resource = ResourceDictionary.GetResource("PurpleFrogData");
                    frog = ActorFactory.Create<FrogActor>(resource, new Vector2(random.Next((Int32)_bounds.Left + 1, (Int32)_bounds.Right - 1), _bounds.Bottom + 1f));
                    frog.SetFrameSet("Zazumo.FrameSets.ShrinkFrog");
                    break;
                case FrogEffect.Grow:
                    frog = ActorFactory.Create<FrogActor>(ResourceDictionary.GetResource("GreenFrogData"),
                                                          new Vector2(random.Next((Int32)_bounds.Left + 1, (Int32)_bounds.Right - 1),
                                                          _bounds.Bottom + 1f));
                    frog.SetFrameSet("Zazumo.FrameSets.GrowFrog");
                    break;
                case FrogEffect.Ammo:
                    frog = ActorFactory.Create<FrogActor>(ResourceDictionary.GetResource("YellowFrogData"),
                                      new Vector2(random.Next((Int32)_bounds.Left + 1, (Int32)_bounds.Right - 1),
                                      _bounds.Bottom + 1f));
                    frog.SetFrameSet("Zazumo.FrameSets.AmmoFrog");

                    break;
                case FrogEffect.Points:
                    frog = ActorFactory.Create<FrogActor>(ResourceDictionary.GetResource("BlackFrogData"),
                                      new Vector2(random.Next((Int32)_bounds.Left + 1, (Int32)_bounds.Right - 1),
                                      _bounds.Bottom + 1f));
                    frog.SetFrameSet("Zazumo.FrameSets.BlackFrog");

                    break;
                case FrogEffect.Bomb:
                    frog = ActorFactory.Create<FrogActor>(ResourceDictionary.GetResource("BlueFrogData"),
                                      new Vector2(random.Next((Int32)_bounds.Left + 1, (Int32)_bounds.Right - 1),
                                      _bounds.Bottom + 1f));
                    frog.SetFrameSet("Zazumo.FrameSets.BlueFrog");

                    break;
                default:
                    break;
            }
            var controller = AddActorController<FrogController>(frog);
        }

        private void CreateZazumo()
        {
            zazumoData = (CharacterArchetypeData)ResourceDictionary.GetResource("ZazumoData");
            _zazumo = ActorFactory.Create<ZazumoActor>(zazumoData, new Vector2(_bounds.Left + _bounds.Width / 2f - zazumoData.Width / 2f, _bounds.Top + _bounds.Height / 2 - zazumoData.Height / 2f));
            AddActorController<ZazumoController>(_zazumo);
        }

        public void OnPlayerDied(PlayerDiedEvent @event)
        {
            _zazumo.Destroy();
            CreateZazumo();

            if (_mode == ZazumoActionMode.Shape)
                _mode = ZazumoActionMode.Normal;

            foreach (var actor in ActorRepository.GetAllActors<EnemyActor>())
            {
                if (!actor.IsMiniBoss)
                    actor.Destroy();
            }

            foreach (var actor in ActorRepository.GetAllActors<FrogActor>())
            {
                actor.Destroy();
            }

            foreach (var actor in ActorRepository.GetAllActors<PowerUpActor>())
            {
                actor.SetZIndex(100);
            }

            this._multiplier = 1;

            this._lives--;

            if (_lives == 0)
                this.Pop();

            this._livesTextBlock.Text = "X" + (_lives - 1).ToString();
        }

        public void OnShapeChanged(ZazumoShapeChangedEvent @event)
        {
            if (@event.Shape == ZazumoShape.None)
            {
                foreach (var actor in ActorRepository.GetAllActors<FrogActor>())
                {
                    actor.Destroy();
                }

                if (this._mode == ZazumoActionMode.Shape)
                    this._mode = ZazumoActionMode.Normal;

                if (_fireButton != null)
                {
                    _fireButton.Destroy();
                    _fireButton = null;
                }

                if (_ammoMeter != null)
                {
                    _ammoMeter.Destroy();
                    _ammoMeter = null;
                }

                StopWormholes();

            }
            else
            {
                _fireButton = ActorFactory.Create<FireButton>(new UIResource { X = 0.86f, Y = 0.78f, Width = 0.1f, Height = 0.175f, SpriteKey = "Zazumo.Sprites.FireButton" });
                _ammoMeter = ActorFactory.Create<AmmoMeter>(new UIResource { X = 0.875f, Y = 0.15f, Width = 0.09f, Height = 0.6f, SpriteKey = "Zazumo.Sprites.AmmoMeter" });

                _fireButton.AnimateProperty("Opacity", 0.0f, 0.5f, TimeSpan.FromMilliseconds(500));
                _ammoMeter.AnimateProperty("Opacity", 0.0f, 0.5f, TimeSpan.FromMilliseconds(500));
                _ammoMeter.AmmoLevel = @event.AmmoLevel;
                _zazumo.SetAmmo(_ammoMeter);

                this._currentShape = @event.Shape;

                if (this._mode == ZazumoActionMode.Normal)
                    this._mode = ZazumoActionMode.Shape;


                foreach (var wormhole in ActorRepository.GetAllActors<WormholeActor>())
                {
                    if (wormhole.Shape == @event.Shape)
                        wormhole.AnimateProperty("Rotation", 0f, (Single)Math.PI * 2f, TimeSpan.FromSeconds(5), true);
                }

                foreach (var wormhole in ActorRepository.GetAllActors<PowerUpActor>())
                {
                    if (wormhole.Shape == @event.Shape)
                        wormhole.AnimateProperty("Rotation", 0f, (Single)Math.PI * 2f, TimeSpan.FromSeconds(5), true);
                }
            }
        }

        public void OnSizeChanged(ZazumoSizeChangedEvent @event)
        {

        }

        public void StopWormholes()
        {
            foreach (var wormhole in ActorRepository.GetAllActors<WormholeActor>())
            {
                wormhole.StopAnimations();
                In(100).Milliseconds.Run(() => wormhole.SetProperty("Rotation", 0f));
            }

            foreach (var powerUp in ActorRepository.GetAllActors<PowerUpActor>())
            {
                powerUp.StopAnimations();
                In(100).Milliseconds.Run(() => powerUp.SetProperty("Rotation", 0f));
            }
        }

        public void OnWormholeClosed(WormholeClosedEvent @event)
        {
            this._mode = ZazumoActionMode.Normal;
            _zazumo.ReleaseShape();

            Spawn(@event.MiniBossData);
        }

        public void OnMiniBossDestroyed(MiniBossDestroyedEvent @event)
        {
            var wormholes = ActorRepository.GetAllActors<WormholeActor>();

            if (wormholes.Count() > 0)
            {
                CreateClam();
            }
            else
            {
                // Start boss battle.
                _mode = ZazumoActionMode.Boss;

                foreach (var enemy in ActorRepository.GetAllActors<EnemyActor>())
                    enemy.Destroy();

                CreateClam();

                In(3).Seconds.Run(() => SpawnBoss(SpawnType.Squid));
            }
        }

        public void OnBombAwarded(BombAwardedEvent @event)
        {
            if (__bomb3Button != null)
                return;

            if (__bomb2Button != null)
            {
                __bomb3Button = ActorFactory.Create<UIButtonActor>(new UIResource { X = 0.53f, Y = 0.78f, Width = 0.1f, Height = 0.175f, SpriteKey = "Zazumo.Sprites.BombButton" });
                return;
            }

            if (__bomb1Button != null)
            {
                __bomb2Button = ActorFactory.Create<UIButtonActor>(new UIResource { X = 0.64f, Y = 0.78f, Width = 0.1f, Height = 0.175f, SpriteKey = "Zazumo.Sprites.BombButton" });
                return;
            }

            __bomb1Button = ActorFactory.Create<UIButtonActor>(new UIResource { X = 0.75f, Y = 0.78f, Width = 0.1f, Height = 0.175f, SpriteKey = "Zazumo.Sprites.BombButton" });
        }

        public void OnBombDeployed(BombDeployedEvent @event)
        {
            Boolean hasBomb = false;

            if (__bomb3Button != null)
            {
                __bomb3Button.Destroy();
                __bomb3Button = null;
                hasBomb = true;
            }

            if (__bomb2Button != null && ! hasBomb)
            {
                __bomb2Button.Destroy();
                __bomb2Button = null;
                hasBomb = true; 
            }

            if (__bomb1Button != null && !hasBomb)
            {
                __bomb1Button.Destroy();
                __bomb1Button = null;
                hasBomb = true;
            }

            if (!hasBomb)
                return;

            var owlBomb = ActorFactory.Create<OwlBomb>(new CharacterArchetypeData
                {
                    CollisionGroup = 8,
                    Height = 5f,
                    Width = 9f,
                    SpriteKey = "Zazumo.Sprites.OwlBomb",
                    CollisionHullKey = "TileCollisionHull",
                }, new Vector2(0.5f, 6f));

            this.AddActorController<OwlBombController>(owlBomb);

            In(500).Milliseconds.Run(() =>
                {
                    foreach (var enemy in ActorRepository.GetAllActors<EnemyActor>())
                    {
                        enemy.Hit();
                    }

                    foreach (var projectile in ActorRepository.GetAllActors<ProjectileActor>())
                    {
                        projectile.Destroy();
                    }
                });
        }

        public void OnPointsAwarded(PointsAwardedEvent @event)
        {
            var scoreIndicator = ActorFactory.Create<UITextBlockActor>(new Object(), new Vector2(@event.X, @event.Y));
            scoreIndicator.FontKey = "SmallPointsFont";
            scoreIndicator.Text = String.Format("+{0} X{1}", @event.Score, this._multiplier);

            scoreIndicator.AnimateProperty("YOffset", 0.0f, -100f, TimeSpan.FromSeconds(2.0));
            scoreIndicator.AnimateProperty("Opacity", 1.0f, 0.0f, TimeSpan.FromSeconds(2.0));
            In(2).Seconds.Run(() => scoreIndicator.Destroy());

            this._score += (@event.Score * this._multiplier);
            this._scoreTextBlock.Text = this._score.ToString();
        }

        public void OnEnemyDestroyed(EnemyDestoryedEvent @event)
        {
            var explosion = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 0.75f, Width = 0.75f, SpriteKey = "Zazumo.Sprites.Explosion1" }, new Vector2(@event.X / Settings.MetersToPixels, @event.Y / Settings.MetersToPixels));
            explosion.SetFrameSet("Zazumo.FrameSets.Explosion");
            In(550).Milliseconds.Run(() => explosion.Destroy());
        }

        public void OnBigPointsAwarded(BigPointsAwardedEvent @event)
        {
            if (this._multiplier < 8 && @event.IncreasesMultiplier)
                this._multiplier++;

            var scoreIndicator = ActorFactory.Create<UITextBlockActor>(new Object(), new Vector2(@event.X, @event.Y));
            scoreIndicator.FontKey = "BigPointsFont";
            scoreIndicator.Text = String.Format("+{0} X{1}", @event.Score, this._multiplier);

            scoreIndicator.AnimateProperty("Opacity", 1.0f, 0.0f, TimeSpan.FromSeconds(2.0));
            In(2).Seconds.Run(() => scoreIndicator.Destroy());

            var storyboard = new Storyboard
            {
                Children = new StoryboardTarget[]
                {
                    new StoryboardTarget 
                    {
                        TargetActor = scoreIndicator, 
                        TargetProperty = "Scale",
                        Timeline = new SingleAnimationUsingKeyFrames
                        {
                             KeyFrames = new SingleKeyFrame[]
                             {
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(0) }, Value = 1.0f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(0.3) }, Value = 1.25f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(0.6) }, Value = 0.75f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(0.9) }, Value = 1.25f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(1.2) }, Value = 0.75f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(1.5) }, Value = 1.25f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(1.8) }, Value = 0.75f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(2) }, Value = 1.25f },
                             }
                        }
                    },

                    new StoryboardTarget 
                    {
                        TargetActor = scoreIndicator, 
                        TargetProperty = "YOffset",
                        Timeline = new SingleAnimationUsingKeyFrames
                        {
                             KeyFrames = new SingleKeyFrame[]
                             {
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(0) }, Value = 0.0f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(0.3) }, Value = -5f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(0.6) }, Value = 5f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(0.9) }, Value = -5f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(1.2) }, Value = 5f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(1.5) }, Value = -5f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(1.8) }, Value = 5f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(2) }, Value = -5f },
                             }
                        }
                    },


                    new StoryboardTarget 
                    {
                        TargetActor = scoreIndicator, 
                        TargetProperty = "XOffset",
                        Timeline = new SingleAnimationUsingKeyFrames
                        {
                             KeyFrames = new SingleKeyFrame[]
                             {
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(0) }, Value = 0.0f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(0.3) }, Value = -20f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(0.6) }, Value = 20f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(0.9) }, Value = -20f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(1.2) }, Value = 20f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(1.5) }, Value = -20f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(1.8) }, Value = 20f },
                                 new LinearSingleKeyFrame { KeyTime = new KeyTime { TimeSpan = TimeSpan.FromSeconds(2) }, Value = -20f },
                             }
                        }
                    },                 
                }
            };

            this.RunStoryboard(storyboard);

            this._score += (@event.Score * this._multiplier);
            this._scoreTextBlock.Text = this._score.ToString();
        }

        public void OnEnemyFlockDestroyed(EnemyFlockDestroyedEvent @event)
        {
            var explosion = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 0.75f, Width = 0.75f, SpriteKey = "Zazumo.Sprites.Explosion1" }, new Vector2(@event.X / Settings.MetersToPixels, @event.Y / Settings.MetersToPixels));
            explosion.SetFrameSet("Zazumo.FrameSets.Explosion");
            In(550).Milliseconds.Run(() => explosion.Destroy());
        }

        private void OnAmmoDepleted(AmmoDepletedEvent @event)
        {
            if (this._mode == ZazumoActionMode.Boss)
            {
                this._zazumo.DestroyPowerUp();
            }
        }

        private void CreateClam()
        {
            var clam = ActorFactory.Create<ClamActor>(ResourceDictionary.GetResource("ClamData"), new Vector2(4.5f, -1.0f));

            if (this._mode == ZazumoActionMode.Normal)
            {
                clam.AddItem(new ClamShapeItem { Shape = ActorRepository.GetAllActors<WormholeActor>().First().Shape });
            }
            else
            {
                foreach (var shape in _levelShapeData)
                {
                    clam.AddItem(new ClamShapeItem { Shape = shape });
                }
            }

            AddActorController<ClamController>(clam);

            In(4).Seconds.Run(() =>
                {
                    foreach (var item in this.ActorRepository.GetAllActors<PowerUpActor>())
                    {
                        AddActorController<PowerUpController>(item);
                    }
                });
        }
    }
}
