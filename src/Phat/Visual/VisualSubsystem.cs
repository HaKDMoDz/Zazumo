using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phat.ActorVisuals;
using Phat.ActorModel;

namespace Phat.Visual.Xna
{
    public class VisualSubsystem : IVisualSubsystem
    {
        private readonly Dictionary<Int32, List<IActorVisual>> _actorVisuals;
        private readonly Dictionary<Actor, IActorVisual> _actorVisualMap;
        private readonly Object _lockObject;
        private readonly IActorVisualFactory _viewFactory;
        private readonly GraphicsDevice _graphics;
        private readonly ViewPort _viewPort;

        public VisualSubsystem(IActorVisualFactory viewFactory, GraphicsDevice graphics, ViewPort viewPort)
        {
            _lockObject = new Object();
            _actorVisuals = new Dictionary<Int32, List<IActorVisual>>();
            _actorVisualMap = new Dictionary<Actor, IActorVisual>();
            _graphics = graphics;
            _viewFactory = viewFactory;
            _viewPort = viewPort;
        }

        public void Draw(TimeSpan ellapsedTime)
        {
            lock (_lockObject)
            {
                foreach (var layer in _actorVisuals.OrderBy(x => x.Key))
                {
                    foreach (var actor in layer.Value)
                    {
                        actor.Draw(ellapsedTime, _viewPort);
                    }
                }
            }
        }

        public IActorVisual AddActor(Actor actor)
        {
            if (actor is EffectActor)
                return AddActor(actor, 20);
            else if (actor is UIBackgroundActor)
                return AddActor(actor, 0);
            else if (actor is UIActor)
                return AddActor(actor, 30);
            else if (actor is ProjectileActor)
                return AddActor(actor, 10);
            else if (actor is ICharacterActor)
                return AddActor(actor, 10);
            else
                return AddActor(actor, 5);
        }

        public IActorVisual AddActor(Actor actor, Int32 layer)
        {
            IActorVisual visual = BuildActorVisual(actor, layer);
            return visual;
        }

        public IActorVisual AddActor(Actor actor, Object initializationData)
        {
            if (actor is EffectActor)
                return AddActor(actor, initializationData, 20);
            else if (actor is UIBackgroundActor)
                return AddActor(actor, initializationData, 0);
            else if (actor is UIActor)
                return AddActor(actor, initializationData, 30);
            else if (actor is ProjectileActor)
                return AddActor(actor, initializationData, 10);
            else if (actor is ICharacterActor)
                return AddActor(actor, initializationData, 10);
            else
                return AddActor(actor, initializationData, 0);
        }

        public IActorVisual AddActor(Actor actor, Object initializationData, Int32 layer)
        {
            IActorVisual visual = BuildActorVisual(actor, layer);
            visual.Initialize(initializationData);
            return visual;
        }


        private IActorVisual BuildActorVisual(Actor actor, Int32 layer)
        {
            IActorVisual visual = null;

            lock (_lockObject)
            {
                var view = _viewFactory.BuildVisual(actor);

                visual = view as IActorVisual;

                if (!_actorVisuals.ContainsKey(layer))
                    _actorVisuals[layer] = new List<IActorVisual>();

                _actorVisuals[layer].Add(visual);

                _actorVisualMap.Add(actor, visual);
            }

            return visual;
        }

        public void Remove(Actor actor)
        {
            if (_actorVisualMap.ContainsKey(actor))
            {
                var visual = _actorVisualMap[actor];

                foreach (var layer in _actorVisuals)
                {
                    if (layer.Value.Contains(visual))
                    {
                        layer.Value.Remove(visual);
                    }
                }

                _actorVisualMap.Remove(actor);
            }
        }


        public IEnumerable<VisualHitTestResult> GetActorByHitTest(Vector3 traceFrom, Vector3 traceDirection, Boolean shouldStopOnFirstHit)
        {
            List<VisualHitTestResult> results = new List<VisualHitTestResult>();

            foreach (var layer in _actorVisuals)
            {
                foreach (var visual in layer.Value)
                {
                    var hitResult = visual.GetHitResult(new Vector2(traceFrom.X, traceFrom.Y), _viewPort);

                    if (hitResult.IsHit)
                    {
                        results.Add(hitResult);

                        if (shouldStopOnFirstHit)
                            return results;
                    }
                }
            }

            return results;
        }

        public void SetZIndex(IActorVisual visual, Int32 index)
        {
            /*this._actorVisuals.Remove(visual);

            index = Math.Min(index, this._actorVisuals.Count());
            this._actorVisuals.Insert(index, visual);*/
        }
    }
}
