using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Messages;
using Phat.ActorResources;
using Phat.Animations;

namespace Phat.ActorModel
{
    public abstract class ArrowActor : PlaceableActor<ArrowActor>
    {
        public ArrowDirection ArrowDirection { get; protected set; }

        public void Touch()
        {
            this.OnTouch();
        }

        protected virtual void OnTouch()
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
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(100) }, Value = -7.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(200) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(300) }, Value = -7.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(400) }, Value = 0.0f }
                                }
                            }
                        },
                        
                        new StoryboardTarget { TargetActor = this, TargetProperty = "YOffset",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(100) }, Value = -7.0f},
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(200) }, Value = 0.0f},
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(300) }, Value = -7.0f},
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(400) }, Value = 0.0f}
                                }
                            }
                        },

                        new StoryboardTarget { TargetActor = this, TargetProperty = "HeightOffset",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(100) }, Value = 14.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(200) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(300) }, Value = 14.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(400) }, Value = 0.0f }
                                }
                            }
                        },
                        
                        new StoryboardTarget { TargetActor = this, TargetProperty = "WidthOffset",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(100) }, Value = 14.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(200) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(300) }, Value = 14.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(400) }, Value = 0.0f }
                                }
                            }
                        }
                    }
            };

            this.RunStoryboard(sb);
        }

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            var resource = (IArrowWorldObject)initializationData;

            SetLocation(resource.X, resource.Y, 0f);
            this.ArrowDirection = resource.ArrowDirection;
        }
    }
}
