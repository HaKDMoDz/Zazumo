using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;
using Microsoft.Xna.Framework;
using Phat.Messages;
using Phat.Zazumo.Controllers.Action;
using Phat.ActorResources;
using Phat.Zazumo.Actors;

namespace Phat.Zazumo.Controllers.Title
{
    public class TitleGameMode : GameMode
    {
        private Actor Z1;
        private Actor A;
        private Actor Z2;
        private Actor U;
        private Actor M;
        private Actor O;

        private readonly Single Step = 0.075f;
        private readonly Single Distance = 0.15f;
        private readonly Single phaseShift = 1.0f;
        private Single phase = 0.0f;
        

        protected override void OnInitialize(Object initializationData)
        {
            ResourceDictionary.LoadPackage(@"Packages/Title");

#if WINDOWS_PHONE
            TouchPanel.EnabledGestures = GestureType.Tap;
#endif
            Subscribe<PanelTapEvent>(x => OnPanelTap(x));

            Show();
        }

        protected override void OnResume(object popState)
        {
            base.OnResume(popState);

            Show();
        }

        private void Show()
        {
            ActorFactory.Create<UIBackgroundActor>("Title.Archetypes.TitleBackground", new Vector2(0f, 0f));

            Actor actor ;

            actor = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1.25f, Width = 0.625f, SpriteKey = "Title.Sprites.VillageGirl1" }, new Vector2(3.9f, 1.8f));
            actor.SetFrameSet("Title.FrameSets.VillageGirlDance");

            actor = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1.25f, Width = 0.625f, SpriteKey = "Title.Sprites.VillageGirl1R" }, new Vector2(5.4f, 1.8f));
            actor.SetFrameSet("Title.FrameSets.VillageGirlDanceR");

            actor = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1f, Width = 1f, SpriteKey = "Title.Sprites.Chief1" }, new Vector2(3.5f, 3.0f));
            actor.SetFrameSet("Title.FrameSets.ChiefDance");

            actor = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1.25f, Width = 0.625f, SpriteKey = "Title.Sprites.Villager1" }, new Vector2(3.125f, 3.2f));
            actor.SetFrameSet("Title.FrameSets.VillagerDance");

            actor = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1.25f, Width = 0.625f, SpriteKey = "Title.Sprites.Villager1" }, new Vector2(2.5f, 3.5f));
            actor.SetFrameSet("Title.FrameSets.VillagerDance");

            actor = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1f, Width = 1f, SpriteKey = "Title.Sprites.Chief1R" }, new Vector2(5.5f, 3.0f));
            actor.SetFrameSet("Title.FrameSets.ChiefDanceR");

            actor = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1.25f, Width = 0.625f, SpriteKey = "Title.Sprites.Villager1R" }, new Vector2(6.325f, 3.2f));
            actor.SetFrameSet("Title.FrameSets.VillagerDanceR");

            actor = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1.25f, Width = 0.625f, SpriteKey = "Title.Sprites.Villager1R" }, new Vector2(7.0f, 3.5f));
            actor.SetFrameSet("Title.FrameSets.VillagerDanceR");

            Z1 = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1.5f, Width = 1.5f, SpriteKey = "Title.Sprites.Z" }, new Vector2(1.75f, 0.25f));
            A = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1.0f, Width = 1.0f, SpriteKey = "Title.Sprites.A" }, new Vector2(3.25f, 0.5f));
            Z2 = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1.0f, Width = 1.0f, SpriteKey = "Title.Sprites.Z" }, new Vector2(4.25f, 0.5f));
            U = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1.0f, Width = 1.0f, SpriteKey = "Title.Sprites.U" }, new Vector2(5.25f, 0.5f));
            M = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1.0f, Width = 1.0f, SpriteKey = "Title.Sprites.M" }, new Vector2(6.25f, 0.5f));
            O = ActorFactory.Create<DecoratorActor>(new Drawable { Height = 1.0f, Width = 1.0f, SpriteKey = "Title.Sprites.O" }, new Vector2(7.25f, 0.5f));

            var start = ActorFactory.Create<UITextBlockActor>(new Object(), new Vector2(0f, 300f));
            start.Text = "START NEW GAME";
            start.FontKey = "MainFont";
            start.Color = Color.Black;
            start.TextAlignment = UITextBlockActor.Alignment.Center;


            var resume = ActorFactory.Create<UITextBlockActor>(new Object(), new Vector2(0f, 350f));
            resume.Text = "RESUME GAME";
            resume.FontKey = "MainFont";
            resume.Color = Color.Black;
            resume.TextAlignment = UITextBlockActor.Alignment.Center;

            var leaderboards = ActorFactory.Create<UITextBlockActor>(new Object(), new Vector2(0f, 400f));
            leaderboards.Text = "LEADERBOARDS";
            leaderboards.FontKey = "MainFont";
            leaderboards.Color = Color.Black;
            leaderboards.TextAlignment = UITextBlockActor.Alignment.Center;

        }

        public override void Update(TimeSpan ellapsedTime)
        {
            base.Update(ellapsedTime);

            phase += Step;

            Z1.SetProperty("YOffset", (Single)Math.Sin((phase + phaseShift * 0)) * Distance);
            A.SetProperty("YOffset", (Single)Math.Sin((phase + phaseShift * 1)) * Distance);
            Z2.SetProperty("YOffset", (Single)Math.Sin((phase + phaseShift * 2)) * Distance);
            U.SetProperty("YOffset", (Single)Math.Sin((phase + phaseShift * 3)) * Distance);
            M.SetProperty("YOffset", (Single)Math.Sin((phase + phaseShift * 4)) * Distance);
            O.SetProperty("YOffset", (Single)Math.Sin((phase + phaseShift * 5)) * Distance);
        }

        protected override void OnSuspend()
        {         
            base.OnSuspend();
         }

        private void OnPanelTap(PanelTapEvent @event)
        {
            Push<ActionGameMode>();
        }
    }
}
