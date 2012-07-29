using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Phat
{
    [Serializable]
    public abstract class Animation
    {
#if !WINDOWS_PHONE && !XBOX
        [NonSerialized]
#endif
        private Boolean _isComplete;

#if !MONO
        [ContentSerializerIgnore]
#endif
        public Boolean IsComplete 
        { 
            get { return _isComplete; } 
            set { _isComplete = value; } 
        }

#if !WINDOWS_PHONE && !XBOX
        [NonSerialized]
#endif
        private Actor _actor;

#if !MONO
        [ContentSerializerIgnore]
#endif
        public Actor Actor
        {
            get { return _actor; }
            set { _actor = value; }
        }


        private Boolean _shouldAutoRepeat;

#if !MONO
        [ContentSerializerIgnore]
#endif
        public Boolean ShouldAutoRepeat
        {
            get { return _shouldAutoRepeat; }
            set { _shouldAutoRepeat = value; }
        }

#if !WINDOWS_PHONE && !XBOX
        [NonSerialized]
#endif
        private Object _animatedValue;

#if !MONO
        [ContentSerializerIgnore]
#endif
        public Object AnimatedValue 
        {
            get { return _animatedValue; }
            set { _animatedValue = value; }
        }

        public abstract String AnimatedProperty { get; }

        public void SetActor(Actor actor)
        {
            this._actor = actor;
        }

        public void Run(Int64 ticks)
        {
            IsComplete = RunImplementaion(ticks);                        
        }

        public abstract void Initialize();
        protected abstract Boolean RunImplementaion(Int64 ticks);
    }

}
