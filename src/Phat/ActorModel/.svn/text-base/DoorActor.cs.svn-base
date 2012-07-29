using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Animations;

namespace Phat.ActorModel
{
    public class DoorActor : PlaceableActor<DoorActor>, ITouchable
    {
        Boolean _isOpen;

        public void Touch(Actor other)
        {
            this.OnTouch(other);
        }

        public void Open()
        {
            this._isOpen = true;

            Storyboard sb = new Storyboard
            {
                Children = new StoryboardTarget[]
                    {
                       new StoryboardTarget { TargetActor= this, TargetProperty = "Opacity",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new DiscreteSingleKeyFrame[] 
                                {
                                    new DiscreteSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 0f },
                                    new DiscreteSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(100) }, Value = 1f },
                                    new DiscreteSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(150) }, Value = 0f },
                                    new DiscreteSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(200) }, Value = 1f },
                                    new DiscreteSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(250) }, Value = 0f },
                                    new DiscreteSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(300) }, Value = 0.5f }
                                }
                            }
                        }
                    }
            };

            this.RunStoryboard(sb);

            In(350).Milliseconds.Run(() => this.Destroy());
        }

        private void OnTouch(Actor other)
        {
            if (_isOpen)
                return;

            var mortal = other as IMortal;

            if (mortal == null)
                return;

            mortal.Kill();
        }
    }
}
