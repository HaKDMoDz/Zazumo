using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;
using Phat.Messages;
using Microsoft.Xna.Framework;
using Phat.Animations;
using Phat.Zazumo.Controllers.Action;
using Phat.Zazumo.Controllers.Title;

#if WINDOWS_PHONE
using Microsoft.Xna.Framework.Input.Touch;
#endif

namespace Phat.Zazumo.Controllers.Splash
{
    public class SplashGameMode : GameMode
    {
        private UIBackgroundActor _splashScreen;

        protected override void OnInitialize(Object initializationData)
        {
#if WINDOWS_PHONE
            TouchPanel.EnabledGestures = GestureType.Tap;
#endif
            ResourceDictionary.LoadPackage(@"Packages/Splash");

            _splashScreen = ActorFactory.Create<UIBackgroundActor>("Splash.Archetypes.SplashBackground", new Vector2(0f, 0f));
            _splashScreen.Name = "Background";
            FadeIn();

            Subscribe<PanelTapEvent>(x => OnPanelTap(x));

            In(5).Seconds.Run(() =>
            {
                _splashScreen.Destroy();
                Push<TitleGameMode>();
            });           

        }

        private void OnPanelTap(PanelTapEvent @event)
        {
            if (_splashScreen == null)
                return;
            
            FadeOut();

            In(250).Milliseconds.Run(() =>
            {
                _splashScreen.Destroy();
                Push<TitleGameMode>();
            });
        }

        protected override void OnSuspend()
        {
            base.OnSuspend();
            ResourceDictionary.UnloadPackage(@"Packages/Splash");
        }

        private void FadeOut()
        {
            RunStoryboard((Storyboard)ResourceDictionary.GetResource("Splash.Storyboards.FadeOut"));
        }

        private void FadeIn()
        {
            RunStoryboard((Storyboard)ResourceDictionary.GetResource("Splash.Storyboards.FadeIn"));
        }
    }
}
