using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;

namespace Phat.ActorModel
{
    public class EffectActor : Actor<EffectActor>
    {
        private readonly List<Animation> _animations;

        public EffectActor()
        {
            _animations = new List<Animation>();
        }

        protected override void OnSetAnimation(Animation animation)
        {
            base.OnSetAnimation(animation);

            _animations.Add(animation);
        }

        protected Object GetAnimatedValue(String key, Object fallbackValue)
        {
            foreach (var animation in this._animations.ToArray())
            {
                if (animation.IsComplete == true)
                {
                    this._animations.Remove(animation);
                }
                else
                {
                    if (animation.AnimatedProperty == key)
                        return animation.AnimatedValue;
                }
            }

            return fallbackValue;
        }
    }
}
