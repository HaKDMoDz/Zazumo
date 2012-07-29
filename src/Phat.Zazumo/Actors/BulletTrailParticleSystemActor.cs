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
    public class BulletTrailParticleSystemActor : ParticleSystemActor
    {
        public ParticleEffect CreateEffect(IResourceDictionary resourceDictionary)
        {
            var effect = new ParticleEffect
            {
                Emitters = new EmitterCollection
                                {            
                                     new PointEmitter
                                        {
                                            Budget = 32,
                                            Term = 1.0f,                                            
                                            ParticleTexture = (Texture2D)resourceDictionary.GetResource("Zazumo.Textures.Particles.BulletTrail"),
                                            BlendMode = EmitterBlendMode.Alpha,
                                            ReleaseColour = new ColourRange { Red = 0.0f, Green = 0.5f, Blue = 0.1f },
                                            ReleaseOpacity = 0.0f,
                                            ReleaseQuantity = 1,
                                            ReleaseRotation = new RotationRange 
                                                                {
                                                                    Roll = new Range(0f, 1.5f)
                                                                },
                                            ReleaseScale = new Range(20f, 20f),
                                            ReleaseSpeed = new Range(0f, 100f),
                                            
                                            Modifiers = new ModifierCollection
                                                            {
                                                                new ScaleInterpolator3
                                                                {
                                                                      FinalScale = 0f,
                                                                      InitialScale = 20f,
                                                                      Median = 0.25f,
                                                                      MedianScale = 20f
                                                                },

                                                                new ColourInterpolator2
                                                                    {
                                                                         FinalColour = new Vector3(1f, 0.25f, 0f),
                                                                         InitialColour = new Vector3(0f, 0.5f, 1f)
                                                                    },

                                                                new OpacityInterpolator3
                                                                {
                                                                     InitialOpacity = 0.0f,
                                                                     FinalOpacity = 0.5f,
                                                                     Median = 0.1f,
                                                                     MedianOpacity = 1.0f                                                                      
                                                                }
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
