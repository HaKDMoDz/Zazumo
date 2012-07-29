using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;
using ProjectMercury;
using ProjectMercury.Emitters;
using Microsoft.Xna.Framework.Graphics;
using ProjectMercury.Modifiers;
using Microsoft.Xna.Framework;

namespace Phat.Zazumo.Actors
{
    public class PowerUpTrailParticleSystemActor : ParticleSystemActor
    {
        public ParticleEffect CreateEffect(IResourceDictionary resourceDictionary)
        {
            var effect = new ParticleEffect
            {
                Emitters = new EmitterCollection
                                {            
                                     new PointEmitter
                                        {
                                            Budget = 15,
                                            Term = 0.75f,                                            
                                            ParticleTexture = (Texture2D)resourceDictionary.GetResource("Zazumo.Textures.Particles.Star"),
                                            BlendMode = EmitterBlendMode.Alpha,
                                            ReleaseColour = new ColourRange { Red = 1.0f, Green = 0.88f, Blue = 0.75f },
                                            ReleaseOpacity = 1.0f,
                                            ReleaseQuantity = 1,
                                            ReleaseRotation = new RotationRange 
                                                                {
                                                                    Roll = new Range(0f, 0f)
                                                                },
                                            ReleaseScale = new Range(24f, 72f),
                                            ReleaseSpeed = new Range(32f, 160f),
                                            
                                            Modifiers = new ModifierCollection
                                                            {
                                                                new ScaleInterpolator3
                                                                {
                                                                      FinalScale = 16f,
                                                                      InitialScale = 48f,
                                                                      Median = 0.4f,
                                                                      MedianScale = 64f
                                                                },

                                                                new OpacityInterpolator2
                                                                {
                                                                     FinalOpacity = 0f,
                                                                     InitialOpacity = 1f
                                                                },

                                                                new ColourInterpolator2
                                                                {
                                                                    InitialColour = new Vector3(1f, 0.5f, 1f),
                                                                    FinalColour = new Vector3(1f, 0.5f, 0f)
                                                                },

                                                                new DampingModifier
                                                                {
                                                                     DampingCoefficient = 5f
                                                                }
                                                            },
                                        },                           

                                        new PointEmitter
                                        {
                                            Budget = 100,
                                            Term = 1.0f,                                            
                                            ParticleTexture = (Texture2D)resourceDictionary.GetResource("Zazumo.Textures.Particles.Star"),
                                            BlendMode = EmitterBlendMode.Alpha,
                                            ReleaseColour = new ColourRange { Red = 1.0f, Green = 1f, Blue = 1f },
                                            ReleaseOpacity = 1.0f,
                                            ReleaseQuantity = 5,
                                            ReleaseRotation = new RotationRange 
                                                                {
                                                                    Roll = new Range(0f, 0f)
                                                                },
                                            ReleaseScale = new Range(16f, 16f),
                                            ReleaseSpeed = new Range(0f, 48f),
                                            
                                            Modifiers = new ModifierCollection
                                                            {
                                                                new OpacityInterpolator2
                                                                {
                                                                     FinalOpacity = 0f,
                                                                     InitialOpacity = 1f
                                                                },

                                                                new ColourInterpolator2
                                                                {
                                                                    InitialColour = new Vector3(1f, 0.5f, 1f),
                                                                    FinalColour = new Vector3(0.75f, 0.25f, 0f)
                                                                },

                                                            },
                                        },                           
                                },
            };

            foreach (var emitter in effect.Emitters)
            {
                emitter.Initialise();
            }

            return effect;
        }

        protected override void OnInitializing(object initializationData)
        {
            base.OnInitializing(initializationData);
            base.Effect = CreateEffect(Resources);
        }
    }
}
