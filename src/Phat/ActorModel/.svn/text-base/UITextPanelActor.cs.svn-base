using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Animations;

namespace Phat.ActorModel
{
    public class UITextPanelActor : Actor<UITextPanelActor>
    {
        public String Text { get; set; }

        public Single Width { get; set; }
        public Single Height { get; set; }
        public Single X { get; set; }
        public Single Y { get; set; }
        
        public void Open()
        {
            Storyboard sb = new Storyboard
            {
                Children = new StoryboardTarget[]
                    {
                        new StoryboardTarget { TargetActor = this, TargetProperty = "XOffset",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = -25.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(200) }, Value = -300.0f }
                                }
                            }
                        },
                        
                        new StoryboardTarget { TargetActor = this, TargetProperty = "YOffset",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(200) }, Value = -200.0f }                                   
                                }
                            }
                        },

                        new StoryboardTarget { TargetActor = this, TargetProperty = "Height",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 50.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(200) }, Value = 400.0f }
                                }
                            }
                        },

                        new StoryboardTarget { TargetActor = this, TargetProperty = "Width",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 50.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(200) }, Value = 600f }
                                }
                            }
                        }
                    }
            };

            this.RunStoryboard(sb);
        }
    }
}
