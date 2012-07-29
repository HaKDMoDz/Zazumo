using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Actors;

namespace Phat.Zazumo.Controllers.Action
{
    public abstract class PatternActorController<TActor> : ActorController
        where TActor : Actor
    {
        private Boolean _isInitialized;
        private TimeSpan _segmentLifeTime;
        private PatternSegment<TActor> _currentPatternSegment;
        
        protected override void OnInitialize()
        {
            
        }

        protected virtual void OnActorInitialized()
        {

        }

        public override void Update(TimeSpan ellapsedTime)
        {
            base.Update(ellapsedTime);

            if (!_isInitialized)
            {
                _isInitialized = true;
                OnActorInitialized();
            }

            _segmentLifeTime = _segmentLifeTime.Add(ellapsedTime);

            if (_currentPatternSegment == null)
                return;

            if (_currentPatternSegment.IsComplete(_segmentLifeTime))
            {
                _segmentLifeTime = TimeSpan.Zero;
                var endingSegment = _currentPatternSegment;
                _currentPatternSegment.End();

                if (_currentPatternSegment == endingSegment)
                {
                    _currentPatternSegment = null;
                    return;
                }
            }

            _currentPatternSegment.Update(ellapsedTime);
        }

        public void SetPatternSegment(PatternSegment<TActor> patternSegment)
        {
            patternSegment.ParentController = this;
            patternSegment.Actor = (TActor)this.Actor;
            patternSegment.Begin();
            _currentPatternSegment = patternSegment;
        }
    }

    public abstract class PatternSegment<TActor>
        where TActor : Actor
    {
        public PatternActorController<TActor> ParentController { get; set; }
        public TActor Actor { get; set; }
        
        public abstract void Begin();
        public abstract void End();
        public abstract void Update(TimeSpan ellapsedTime);
        public abstract Boolean IsComplete(TimeSpan ellapsedSegmentTime);

        public void SetPatternSegment(PatternSegment<TActor> pattern)
        {
            ParentController.SetPatternSegment(pattern);
        }
    }
}
