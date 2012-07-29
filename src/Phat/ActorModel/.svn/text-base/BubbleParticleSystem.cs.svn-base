using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectMercury;
using ProjectMercury.Emitters;
using Microsoft.Xna.Framework.Graphics;
using ProjectMercury.Modifiers;
using Microsoft.Xna.Framework;
using ProjectMercury.Controllers;

namespace Phat.ActorModel
{
    public class BubbleParticleSystem : ParticleSystemActor
    {
        public ParticleEffect CreateEffect(IResourceDictionary resourceDictionary)
        {
            var effect = new ParticleEffect
            {    
                            Emitters = new EmitterCollection
                                {            
                                     new BoxEmitter
                                        {
                                            Name = "SmallBubbles",
                                            Budget = 50,
                                            Height = 5f,
                                            Width = 100f,
                                            Depth = 0f,                                               
                                            Term = 1.0f,                                            
                                            ParticleTexture = (Texture2D)resourceDictionary.GetResource("Zazumo.Textures.Particles.SmallBubbles"),
                                            BlendMode = EmitterBlendMode.Alpha,
                                            ReleaseColour = new ColourRange { Red = 1.0f, Green = 1.0f, Blue = 1.0f },
                                            ReleaseOpacity = 1.0f,
                                            ReleaseQuantity = 3,
                                            ReleaseRotation = new RotationRange 
                                                                {
                                                                    Roll = new Range(0f, 1.5f)
                                                                },
                                            ReleaseScale = new Range(40f, 56f),
                                            ReleaseSpeed = new Range(0f, 25f),
                                            
                                            Modifiers = new ModifierCollection
                                                            {
                                                                new OpacityInterpolator2
                                                                    {
                                                                        InitialOpacity = 1.0f,
                                                                        FinalOpacity = 0.5f
                                                                    },
                                                                new LinearGravityModifier
                                                                {
                                                                    GravityVector = new Vector3(0f, -1f, 0f),
                                                                    Strength = 150f
                                                                },
                                                            },

                                        },                           

                                    new CircleEmitter 
                                        {
                                            Name = "Bubbles",
                                            Budget = 50,
                                            Term = 1.5f,
                                            Radiate = true,
                                            Radius = 50f,
                                            Shell = true,
                                            ParticleTexture = (Texture2D)resourceDictionary.GetResource("Zazumo.Textures.Particles.Bubble"),
                                            BlendMode = EmitterBlendMode.Alpha,
                                            ReleaseColour = new ColourRange { Blue = new Range(0.66f, 1.0f), Red = 57f, Green = new Range(0.24f, 0.64f) },
                                            ReleaseOpacity = 1.0f,
                                            ReleaseQuantity = 2,
                                            ReleaseRotation = new RotationRange 
                                                                {
                                                                    Roll = 0f
                                                                },
                                            ReleaseScale = new Range(24f, 40f),
                                            ReleaseSpeed = new Range(0f, 25f),
                                            
                                            Modifiers = new ModifierCollection
                                                            {
                                                                new OpacityInterpolator2
                                                                    {
                                                                         InitialOpacity = 1.0f,
                                                                        FinalOpacity = 0.5f
                                                                    },
                                                                new LinearGravityModifier
                                                                {
                                                                    GravityVector = new Vector3(0f, -1f, 0f),
                                                                    Strength = 150f
                                                                }
                                                            },

                                        },                             
      
                                       
                                },
            };

            effect.Emitters["Bubbles"].Controllers.AddFirst(new CooldownController { CooldownPeriod = 0.15f });
            effect.Emitters["SmallBubbles"].Controllers.AddFirst(new CooldownController { CooldownPeriod = 0.1f });

            foreach (var emitter in effect.Emitters)
            {
                emitter.Initialise();
                // emitter.Controllers.AddLast(new CooldownController { CooldownPeriod = 0.5f });
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
