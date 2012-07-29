﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectMercury.Renderers;
using Phat.ActorModel;
using Microsoft.Xna.Framework;
using Phat.Visual;

namespace Phat.ActorVisuals
{
    public class ParticleEffectVisual: VisualBase 
    {
        private readonly SpriteBatchRenderer _renderer;
        private readonly ParticleSystemActor _actor;
        private Matrix world, view, projection;
        private Vector3 cameraPosition;
        private Boolean _isTriggered = false;

        public ParticleEffectVisual(ParticleSystemActor actor,  SpriteBatchRenderer renderer,  IResourceDictionary resourceDictionary)
            : base (resourceDictionary)
        {
            this._renderer = renderer;
            this._actor = actor;

            world = view = projection = Matrix.Identity;
            cameraPosition = Vector3.Up;
        }

        public override void Draw(TimeSpan ellapsedTime, ViewPort viewPort)
        {
            if (this._actor.Effect == null)
                return;

            Vector3 position = new Vector3(_actor.Location.X * Settings.MetersToPixels, _actor.Location.Y * Settings.MetersToPixels, 0f);

            _actor.Effect.Update((Single)ellapsedTime.TotalSeconds);

            //if (!_isTriggered)
            //{
                _actor.Effect.Trigger((Single)ellapsedTime.TotalSeconds, ref position);
                _isTriggered = true;
            //}

            this._renderer.RenderEffect(_actor.Effect, ref world, ref view, ref projection, ref cameraPosition);
        }

        public override VisualHitTestResult GetHitResult(Vector2 location, ViewPort viewPort)
        {
            return new VisualHitTestResult(_actor, Vector2.Zero, false);
        }

        public override void HandleEvent(Object @event)
        {

        }

        public override void Initialize(Object initializationData)
        {
            
        }
    }
}
