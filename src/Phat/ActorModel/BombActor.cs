using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Messages;
using Phat;
using Microsoft.Xna.Framework;
using Phat.ActorResources;
using Phat.Animations;
using Microsoft.Xna.Framework.Graphics;

namespace Phat.ActorModel
{
    public class BombActor : PlaceableActor<BombActor>, ITouchable
    {
        private Int32 _bombIndex;
        private Single _countdown;
        private Int32 _previousCountdown;
        private IBombWorldObject _resource;
        private Boolean _isActive;
        private UITextBlockActor _countDownText;

        public Int32 BombIndex
        {
            get { return _bombIndex; }
        }

        public BombActor()
        {
        }

        public void BeginCountdown()
        {
            OnCountdownChanged((Int32)_countdown);
            
            RunLatent(t =>
            {
                if (_countdown > 0)
                {
                    _countdown -= (((Single)t / 10000000f) * 1.5f);

                    var newCountdown = (Int32)Math.Floor(_countdown);
                    if (newCountdown != _previousCountdown)
                    {
                        if (newCountdown >= 0)
                            OnCountdownChanged((Int32)Math.Floor(_countdown));
                    }
                    return ProcessState.Running;
                }
                else
                {
                    OnBombExploded();
                    return ProcessState.Completed;
                }
            });
        }

        public void SetBombIndex(Int32 index)
        {
            OnSetBombIndex(index);
        }

        public void Prime()
        {
            this.SetFrameSet(_resource.ActiveAnimation);
        }

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            this._resource = (IBombWorldObject)initializationData;
            this._countdown = _resource.Timer;
        }

        protected virtual void OnBombExploded()
        {
            if (_countDownText != null)
                _countDownText.Destroy();

            this.Publish(new BombExplodedEvent(this.ActorId, new Vector2(this.Location.X, this.Location.Y)));
            this.Destroy();
        }

        protected virtual void OnCountdownChanged(Int32 countdown)
        {
            _previousCountdown = countdown;

            if (_countDownText != null)
                _countDownText.Destroy();

            _countDownText = this.ActorFactory.Create<UITextBlockActor>();
            if (countdown == 2)
                _countDownText.Color = Color.Yellow;
            else if (countdown == 1)
                _countDownText.Color = Color.Orange;
            else if (countdown == 0)
                _countDownText.Color = Color.Red;

            _countDownText.FontKey = "BombFont";
            _countDownText.Text = (countdown + 1).ToString();
            _countDownText.Location = new Vector3(this.Location.X, this.Location.Y - 24, 0f);

            Storyboard sb = new Storyboard
            {
                Children = new StoryboardTarget[]
                    {
                        new StoryboardTarget { TargetActor = _countDownText, TargetProperty = "XOffset",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(200) }, Value = -25.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(300) }, Value = -10.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(400) }, Value = -21.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(1000) }, Value = -21.0f }                                    
                                }
                            }
                        },
                        
                        new StoryboardTarget { TargetActor = _countDownText, TargetProperty = "YOffset",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 0.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(200) }, Value = -25.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(300) }, Value = -10.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(400) }, Value = -21.0f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(1000) }, Value = -21.0f }

                                }
                            }
                        },

                        new StoryboardTarget { TargetActor = _countDownText, TargetProperty = "Scale",
                            Timeline = new SingleAnimationUsingKeyFrames
                            {
                                KeyFrames = new LinearSingleKeyFrame[] 
                                {
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(0) }, Value = 0.25f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(200) }, Value = 1.5f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(300) }, Value = 0.75f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(400) }, Value = 1.25f },
                                    new LinearSingleKeyFrame() { KeyTime = new KeyTime() { TimeSpan = TimeSpan.FromMilliseconds(1000) }, Value = 1.25f }
                                }
                            }
                        }
                    }
            };

            _countDownText.RunStoryboard(sb);


            this.Publish(new BombCountdownChangedEvent(this.ActorId, countdown));

            if (!_isActive)
            {
                this.SetFrameSet(_resource.ActiveAnimation);
                _isActive = true;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_countDownText != null)
                _countDownText.Destroy();
        }

        protected virtual void OnSetBombIndex(Int32 index)
        {
            this._bombIndex = index;
        }

        public void Touch(Actor other)
        {
            if (other is IMortal)
                ((IMortal)other).Kill();
        }
    }
}
