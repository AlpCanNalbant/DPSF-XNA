﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace DPSF.ParticleSystems
{
    /// <summary>
    /// Create a new Particle System class that inherits from a
    /// Default DPSF Particle System
    /// </summary>
    [Serializable]
    class ExplosionFlashParticleSystem : DefaultTexturedQuadTextureCoordinatesParticleSystem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ExplosionFlashParticleSystem(Game game) : base(game) { }

        //===========================================================
        // Structures and Variables
        //===========================================================

        /// <summary>
        /// The Color of the explosion.
        /// </summary>
        public Color ExplosionColor { get; set; }

        /// <summary>
        /// The Size of the individual Particles.
        /// </summary>
        public int ExplosionParticleSize { get; set; }

        /// <summary>
        /// The Intensity of the explosion.
        /// </summary>
        public int ExplosionIntensity { get; set; }

        Rectangle _flash1TextureCoordinates = new Rectangle(256, 0, 128, 128);
        Rectangle _flash2TextureCoordinates = new Rectangle(384, 0, 128, 128);
        Rectangle _flash3TextureCoordinates = new Rectangle(256, 128, 128, 128);
        Rectangle _flash4TextureCoordinates = new Rectangle(384, 128, 128, 128);

        //===========================================================
        // Overridden Particle System Functions
        //===========================================================
        protected override void SetRenderState(RenderState renderState)
        {
            base.SetRenderState(renderState);

            // Show the texture on both the front and back of the quad
            renderState.CullMode = CullMode.None;

            // Use additive blending
            renderState.DestinationBlend = Blend.One;
        }

        protected override void ResetRenderState(RenderState renderState)
        {
            base.ResetRenderState(renderState);

            // Restore the Cull Mode
            renderState.CullMode = CullMode.CullCounterClockwiseFace;
        }

        //===========================================================
        // Initialization Functions
        //===========================================================
        public override void AutoInitialize(GraphicsDevice graphicsDevice, ContentManager contentManager, SpriteBatch spriteBatch)
        {
            InitializeTexturedQuadParticleSystem(graphicsDevice, contentManager, 1000, 50000,
                                                UpdateVertexProperties, "Textures/ExplosionParticles");

            Name = "Explosion - Flash";
            LoadEvents();
        }

        public void LoadEvents()
        {
            // Specify the particle initialization function
            ParticleInitializationFunction = InitializeParticleExplosion;

            // Setup the behaviors that the particles should have
            ParticleEvents.RemoveAllEvents();
            ParticleEvents.AddEveryTimeEvent(UpdateParticleTransparencyWithQuickFadeInAndSlowFadeOut, 100);
            ParticleEvents.AddEveryTimeEvent(UpdateParticleToFaceTheCamera);
            ParticleEvents.AddEveryTimeEvent(UpdateParticleFlashSize);

            // Setup the emitter
            Emitter.PositionData.Position = new Vector3(0, 50, 0);
            Emitter.ParticlesPerSecond = 10000;
            Emitter.EmitParticlesAutomatically = false; // We will call the Explode() function to release a burst of particles instead of always emitting them

            // Set the default explosion settings
            ExplosionColor = new Color(255, 120, 0);
            ExplosionParticleSize = 70;
            ExplosionIntensity = 5;
        }

        public void SetupToAutoExplodeEveryInterval(float intervalInSeconds)
        {
            // Set the Particle System's Emitter to release a burst of particles after a set interval
            ParticleSystemEvents.RemoveAllEventsInGroup(1);
            ParticleSystemEvents.LifetimeData.EndOfLifeOption = CParticleSystemEvents.EParticleSystemEndOfLifeOptions.Repeat;
            ParticleSystemEvents.LifetimeData.Lifetime = intervalInSeconds;
            ParticleSystemEvents.AddTimedEvent(0.0f, UpdateParticleSystemToExplode, 0, 1);
        }

        public void InitializeParticleExplosion(DefaultTextureQuadTextureCoordinatesParticle particle)
        {
            particle.Lifetime = 0.2f;
            particle.Color = ExplosionColor;
            particle.Position = Emitter.PositionData.Position + new Vector3(RandomNumber.Next(-15, 15), RandomNumber.Next(-15, 15), RandomNumber.Next(-15, 15));
            particle.Size = particle.StartSize = 1;
            particle.EndSize = ExplosionParticleSize;

            // Randomly pick which texture coordinates to use for this particle
            Rectangle textureCoordinates;
            switch (RandomNumber.Next(0, 4))
            {
                default:
                case 0: textureCoordinates = _flash1TextureCoordinates; break;
                case 1: textureCoordinates = _flash2TextureCoordinates; break;
                case 2: textureCoordinates = _flash3TextureCoordinates; break;
                case 3: textureCoordinates = _flash4TextureCoordinates; break;
            }

            particle.SetTextureCoordinates(textureCoordinates, Texture.Width, Texture.Height);
        }

        //===========================================================
        // Particle Update Functions
        //===========================================================
        protected void UpdateParticleFlashSize(DefaultTextureQuadTextureCoordinatesParticle particle, float elapsedTimeInSeconds)
        {
            // Have the particle reach its full size when it reaches half its lifetime, and then shrink back to nothing
            particle.Size = MathHelper.Lerp(particle.StartSize, particle.EndSize, DPSFHelper.InterpolationAmountForEqualLerpInAndLerpOut(particle.NormalizedElapsedTime));
        }

        //===========================================================
        // Particle System Update Functions
        //===========================================================
        protected void UpdateParticleSystemToExplode(float elapsedTimeInSeconds)
        {
            Explode();
        }

        //===========================================================
        // Other Particle System Functions
        //===========================================================

        /// <summary>
        /// Start the explosion.
        /// </summary>
        public void Explode()
        {
            this.Emitter.BurstParticles = this.ExplosionIntensity;
        }

        /// <summary>
        /// Change the color of the explosion to a random color.
        /// </summary>
        public void ChangeExplosionColor()
        {
            ExplosionColor = DPSFHelper.RandomColor();
        }

        /// <summary>
        /// Change the color of the explosion to the given color.
        /// </summary>
        /// <param name="color">The color the explosion should be.</param>
        public void ChangeExplosionColor(Color color)
        {
            ExplosionColor = color;
        }
    }
}