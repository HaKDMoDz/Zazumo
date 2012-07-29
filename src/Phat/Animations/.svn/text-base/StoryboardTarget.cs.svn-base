using System;
using System.Net;
using Microsoft.Xna.Framework.Content;

namespace Phat.Animations
{
    [Serializable]
    public class StoryboardTarget
    {

#if !WINDOWS_PHONE && !XBOX
        [NonSerialized]
#endif
        private Actor _targetActor;

#if !MONO
        [ContentSerializerIgnore]
#endif
        public Actor TargetActor
        {
            get { return _targetActor; }
            set { _targetActor = value; }
        }

        public String TargetName { get; set; }
        public String TargetProperty { get; set; }

        public AnimationTimeline Timeline { get; set; }

        public StoryboardTarget Clone()
        {
            return new StoryboardTarget
            {
                TargetName = this.TargetName,
                TargetProperty = this.TargetProperty,
                TargetActor = this.TargetActor,
                Timeline = (AnimationTimeline)this.Timeline.Clone()
            };
        }
    }
}
