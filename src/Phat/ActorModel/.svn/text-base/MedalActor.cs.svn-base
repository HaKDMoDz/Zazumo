using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Messages;
using Microsoft.Xna.Framework;
using Phat.Animations;
using Phat.ActorResources;

namespace Phat.ActorModel
{
    public class MedalActor : PlaceableActor<MedalActor>, ITouchable
    {
        public void Touch(Actor other)
        {
            this.OnTouch(other);
        }

        private void OnTouch(Actor other)
        {
            this.Publish(new MedalCollectedEvent(this.ActorId));
            var decoratorData = (ConcreteWorldObjectArchetypeData)((ArchetypeResource)Resources.GetResource("Core.Archetypes.MedalPickup")).Data;
            var worldObject = new ArchetypeBasedConcreteWorldObject();
            worldObject.SetArchetypeData(decoratorData);
            var decorator = this.ActorFactory.Create<DecoratorActor>(worldObject, new Vector2(this.Location.X - 13, this.Location.Y - 20));
            
            Storyboard sb = new Storyboard
            {
                Children = new StoryboardTarget[]
                    {
                       new StoryboardTarget { TargetActor = decorator, TargetProperty = "YOffset",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(400) }, Value = -30.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(1650) }, Value = -30.0f }
                                }
                            }
                        },

                        new StoryboardTarget { TargetActor = decorator, TargetProperty = "Opacity",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new DiscreteSingleKeyFrame[] 
                                {
                                    new DiscreteSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 1f },
                                    new DiscreteSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(400) }, Value = 1f },
                                    new DiscreteSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(650) }, Value = 0f },
                                    new DiscreteSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(900) }, Value = 1f },
                                    new DiscreteSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(1150) }, Value = 0f },
                                    new DiscreteSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(1400) }, Value = 1f },
                                }
                            }
                        },

                    }
            };

            decorator.RunStoryboard(sb);

            In(1650).Milliseconds.Run(() => decorator.Destroy());


            this.Destroy();
        }
    }
}
