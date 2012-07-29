using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectMercury;
using Phat.ActorModel;
using ProjectMercury.Emitters;
using Microsoft.Xna.Framework.Graphics;
using ProjectMercury.Modifiers;
using Microsoft.Xna.Framework;
using ProjectMercury.Controllers;

namespace Phat.Zazumo.Actors
{
    public class ExplosionParticleSystemActor : ParticleSystemActor
    {
        public ParticleEffect CreateEffect(IResourceDictionary resourceDictionary)
        {
            var effect = new ParticleEffect
            {
                Emitters = new EmitterCollection
                                {            
                                     new PointEmitter
                                        {
                                            Name = "Sparks",
                                            Budget = 35,
                                            Term = 0.75f,                                            
                                            ParticleTexture = (Texture2D)resourceDictionary.GetResource("Zazumo.Textures.Particles.Sparks"),
                                            BlendMode = EmitterBlendMode.Alpha,
                                            ReleaseColour = new ColourRange { Red = 1f, Green = new Range(0.4f, 0.8f), Blue = 0f },
                                            ReleaseOpacity = 1.0f,
                                            ReleaseQuantity = 35,
                                            ReleaseRotation = new RotationRange 
                                                                {
                                                                    Roll = new Range(0f, 0f)
                                                                },
                                            ReleaseScale = new Range(3f, 7f),
                                            ReleaseSpeed = new Range(0f, 250f),
                                            
                                            Modifiers = new ModifierCollection
                                                            {
                                                                new OpacityInterpolator2
                                                                {
                                                                     FinalOpacity = 0f,
                                                                     InitialOpacity = 1f
                                                                },
                                                                
                                                                new DampingModifier
                                                                {
                                                                     DampingCoefficient = 2f
                                                                }
                                                            },
                                        },                           
                                },
            };

            effect.Emitters["Sparks"].Controllers.AddFirst(new CooldownController { CooldownPeriod = 5.0f });

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
