using System;
using System.Net;
using Phat.ActorResources;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Phat.Animations
{
    [Serializable]
    public class Storyboard : ResourceModel
    {
        private readonly AnimationClock _clock;

        [ContentSerializerIgnore]
        public Boolean ShouldAutoRepeat { get; set; }

        public StoryboardTarget[] Children { get; set; }

        public Storyboard()
        {
            _clock = new AnimationClock();
        }

        public void Begin(IProcessRunner processRunner, IActorRepository repository)
        {
            _clock.Reset();
            Double duration = TimeSpan.FromMilliseconds(0).TotalMilliseconds;

            foreach (var child in Children)
            {
                Actor target = null;

                if (!String.IsNullOrEmpty(child.TargetName))
                    target = repository.GetActorByName(child.TargetName);
                else if (child.TargetActor != null)
                    target = child.TargetActor;
                else
                    throw new Exception("A storyboard child was found that did not specify an actor name or actor.");

                var targetProperty = child.TargetProperty;
                child.Timeline.SetActor(target);
                child.Timeline.ApplyAnimationClock(_clock);
                child.Timeline.BeginAnimation(targetProperty);
                duration = Math.Max(child.Timeline.GetNaturalDuration().TotalMilliseconds, duration);
            }

            processRunner.ScheduleProcess(new AnimationClockController(_clock, TimeSpan.FromMilliseconds(duration)));
        }

        public void Begin(IProcessRunner processRunner)
        {
            _clock.Reset();
            Double duration = TimeSpan.FromMilliseconds(0).TotalMilliseconds;

            foreach (var child in Children)
            {
                Actor target = child.TargetActor;

                var targetProperty = child.TargetProperty;
                child.Timeline.SetActor(target);
                child.Timeline.ApplyAnimationClock(_clock);
                child.Timeline.BeginAnimation(targetProperty);
                duration = Math.Max(child.Timeline.GetNaturalDuration().TotalMilliseconds, duration);
            }

            processRunner.ScheduleProcess(new AnimationClockController(_clock, TimeSpan.FromMilliseconds(duration)));
        }

        public AnimationClockController Begin(IProcessRunner processRunner, Actor target)
        {
            _clock.Reset();
            Double duration = TimeSpan.FromMilliseconds(0).TotalMilliseconds;

            foreach (var child in Children)
            {
                var targetProperty = child.TargetProperty;
                child.Timeline.SetActor(target);
                child.Timeline.ApplyAnimationClock(_clock);
                child.Timeline.BeginAnimation(targetProperty);
                duration = Math.Max(child.Timeline.GetNaturalDuration().TotalMilliseconds, duration);
            }

            var controller = new AnimationClockController(_clock, TimeSpan.FromMilliseconds(duration), ShouldAutoRepeat);

            processRunner.ScheduleProcess(controller);

            return controller;
        }

        public Storyboard Clone()
        {
            List<StoryboardTarget> clonedChildren = new List<StoryboardTarget>();
            foreach (var child in Children)
            {
                clonedChildren.Add((StoryboardTarget)child.Clone());
            }

            return new Storyboard
            {
                Key = String.Empty,
                Children = clonedChildren.ToArray()
            };
        }
    }

    public class AnimationClockController : IProcess
    {
        private readonly AnimationClock _clock;
        private readonly TimeSpan _duration;
        private readonly Boolean _shouldAutoRepeat;

        private Boolean _shouldStop;

        public void Stop()
        {
            this._shouldStop = true;
        }

        public AnimationClockController(AnimationClock clock, TimeSpan duration)
            : this (clock, duration, false)
        {
        }

        public AnimationClockController(AnimationClock clock, TimeSpan duration, Boolean shouldAutoRepeat)
        {
            this._clock = clock;
            this._duration = duration;
            this._shouldAutoRepeat = shouldAutoRepeat;
        }


        public ProcessState Run(long ticksSinceLastRun)
        {
            if (this._shouldStop)
                return ProcessState.Completed;

            _clock.Update(ticksSinceLastRun);

            if (!_shouldAutoRepeat)
            {
                if (_clock.TimeSpan.TotalMilliseconds > _duration.TotalMilliseconds)
                    return ProcessState.Completed;
                else
                    return ProcessState.Running;
            }
            else
            {
                if (_clock.TimeSpan.TotalMilliseconds > _duration.TotalMilliseconds)
                    this._clock.Reset();

                return ProcessState.Running;
            }
        }
    }
}
