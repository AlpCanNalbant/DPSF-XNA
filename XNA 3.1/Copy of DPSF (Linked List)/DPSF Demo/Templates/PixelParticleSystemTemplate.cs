﻿#region File Description
//===================================================================
// PixelParticleSystemTemplate.cs
// 
// This file provides the template for creating a new Pixel Particle
// System from scratch, by creating a very basic Pixel Particle System.
//
// First, it shows how to create a new Particle class so the user can 
// create Particles with whatever properties they need their Particles
// to contain. Next, it shows how to create the Particle Vertex structure, 
// which is used to draw the particles to the screen. Last, it shows 
// how to create the Particle System class itself.
//
// The spots that should be modified are marked with TODO statements.
//
// Copyright Daniel Schroeder 2008
//===================================================================
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace DPSF.ParticleSystems
{
    //-----------------------------------------------------------
    // TODO: Rename/Refactor the Particle class
    //-----------------------------------------------------------
    /// <summary>
    /// Create a new Particle class that inherits from DPSFParticle
    /// </summary>
    class PixelParticleSystemTemplateParticle : DPSFParticle
    {
        //-----------------------------------------------------------
        // TODO: Add in any properties that you want your Particles to have here.
        // NOTE: A Pixel Particle System requires the Particles to at least
        // have a Position and a Color.
        //-----------------------------------------------------------
        public Vector3 Position;        // The Position of the Particle in 3D space
        public Vector3 Velocity;        // The 3D Velocity of the Particle
        public Color Color;             // The Color of the Particle
        
        /// <summary>
        /// Resets the Particles variables to default values
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            //-----------------------------------------------------------
            // TODO: Reset your Particle properties to their default values here
            //-----------------------------------------------------------
            Position = Vector3.Zero;
            Velocity = Vector3.Zero;
            Color = Color.White;
        }

        /// <summary>
        /// Deep copy the ParticleToCopy's values into this Particle
        /// </summary>
        /// <param name="ParticleToCopy">The Particle whose values should be Copied</param>
        public override void CopyFrom(DPSFParticle ParticleToCopy)
        {
            // Cast the Particle from its base type to its actual type
            PixelParticleSystemTemplateParticle cParticleToCopy = (PixelParticleSystemTemplateParticle)ParticleToCopy;
            base.CopyFrom(cParticleToCopy);

            //-----------------------------------------------------------
            // TODO: Copy your Particle properties from the given Particle here
            //-----------------------------------------------------------
            Position = cParticleToCopy.Position;
            Velocity = cParticleToCopy.Velocity;
            Color = cParticleToCopy.Color;
        }
    }

    //-----------------------------------------------------------
    // TODO: Rename/Refactor the Particle Vertex struct
    //-----------------------------------------------------------
    /// <summary>
    /// Create a new structure that inherits from IDSPFParticleVertex to hold
    /// the Particle Vertex properties used to draw the Particle
    /// </summary>
    struct PixelParticleSystemTemplateParticleVertex : IDPSFParticleVertex
    {
        //===========================================================
        // TODO: Add any more Vertex variables needed to draw your Particles here.
        // Notice how Velocity is not included here, since the Velocity of the
        // Particle cannot be drawn; only its position can. Other drawable properties
        // a Particle might have are Color, Size, Rotation or Normal direction, 
        // Texture coordinates, etc.
        // NOTE: If you are using your own Shaders (i.e. Effect file) you may
        // specify whatever properties here that you wish, but if using the
        // default DPSFEffect.fx file, Pixel Vertices must have a Position
        // and Color.
        //===========================================================
        public Vector3 Position;        // The Position of the Particle in 3D space
        public Color Color;             // The Color of the Particle

        // Describe the vertex structure used to display a Particle
        private static readonly VertexElement[] msVertexElements =
        {
            new VertexElement(0, 0, VertexElementFormat.Vector3,
                                    VertexElementMethod.Default,
                                    VertexElementUsage.Position, 0),

            new VertexElement(0, 12, VertexElementFormat.Color,
                                    VertexElementMethod.Default,
                                    VertexElementUsage.Color, 0),

            //-----------------------------------------------------------
            // TODO: Add the VertexElements describing the Vertex variables you added here
            //-----------------------------------------------------------
        };

        //-----------------------------------------------------------
        // TODO: Change miSizeInBytes to reflect the total size of the msVertexElements
        // array if any new Vertex Elements were added to it
        //-----------------------------------------------------------
        // The size of the vertex structure in bytes
        private const int miSizeInBytes = 12 + 4;

        /// <summary>
        /// An array describing the attributes of each Vertex
        /// </summary>
        public VertexElement[] VertexElements
        {
            get { return PixelParticleSystemTemplateParticleVertex.msVertexElements; }
        }

        /// <summary>
        /// The Size of one Vertex Element in Bytes
        /// </summary>
        public int SizeInBytes
        {
            get { return PixelParticleSystemTemplateParticleVertex.miSizeInBytes; }
        }
    }

    //-----------------------------------------------------------
    // TODO: Rename/Refactor the Particle System class
    //-----------------------------------------------------------
    /// <summary>
    /// Create a new Particle System class that inherits from DPSF using 
    /// our created Particle class and Particle Vertex structure
    /// </summary>
    class PixelParticleSystemTemplate : DPSF<PixelParticleSystemTemplateParticle, PixelParticleSystemTemplateParticleVertex>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cGame">Handle to the Game object being used. Pass in null for this 
        /// parameter if not using a Game object.</param>
        public PixelParticleSystemTemplate(Game cGame) : base(cGame) { }

        //===========================================================
        // Structures and Variables
        //===========================================================

        //-----------------------------------------------------------
        // TODO: Place any Particle System properties here
        //-----------------------------------------------------------
        

        //===========================================================
        // Vertex Update and Overridden Particle System Functions
        //===========================================================

        /// <summary>
        /// Function to update the Vertex properties according to the Particle properties
        /// </summary>
        /// <param name="sVertexBuffer">The array containing the Vertices to be drawn</param>
        /// <param name="iIndex">The Index in the array where the Particle's Vertex info should be placed</param>
        /// <param name="Particle">The Particle to copy the information from</param>
        public virtual void UpdateVertexProperties(ref PixelParticleSystemTemplateParticleVertex[] sVertexBuffer, int iIndex, DPSFParticle Particle)
        {
            // Cast the Particle to the type it really is
            PixelParticleSystemTemplateParticle cParticle = (PixelParticleSystemTemplateParticle)Particle;

            //-----------------------------------------------------------
            // TODO: Copy the Particle's renderable properties to the Vertex Buffer
            //-----------------------------------------------------------
            sVertexBuffer[iIndex].Position = cParticle.Position;
            sVertexBuffer[iIndex].Color = cParticle.Color;
        }

        /// <summary>
        /// Function to set the Shaders global variables before drawing
        /// </summary>
        protected override void SetEffectParameters()
        {
            //-----------------------------------------------------------
            // TODO: Set any global Shader variables required before drawing
            //-----------------------------------------------------------
            // Specify the World, View, and Projection Matrices
            Effect.Parameters["xWorld"].SetValue(World);
            Effect.Parameters["xView"].SetValue(View);
            Effect.Parameters["xProjection"].SetValue(Projection);
        }

        /// <summary>
        /// Function to set the RenderState properties before drawing
        /// </summary>
        /// <param name="cRenderState">The RenderState used to draw</param>
        protected override void SetRenderState(RenderState cRenderState)
        {
            //-----------------------------------------------------------
            // TODO: Set any RenderState properties required before drawing
            //-----------------------------------------------------------
        }

        /// <summary>
        /// Function to reset any RenderState properties that were changed after drawing
        /// </summary>
        /// <param name="cRenderState">The RenderState used to draw</param>
        protected override void ResetRenderState(RenderState cRenderState)
        {
            //-----------------------------------------------------------
            // TODO: Reset any unusual RenderState properties that were changed
            //-----------------------------------------------------------
        }

        //===========================================================
        // Initialization Functions
        //===========================================================

        /// <summary>
        /// Function to Initialize the Particle System with default values
        /// </summary>
        /// <param name="cGraphicsDevice">The Graphics Device to draw to</param>
        /// <param name="cContentManager">The Content Manager to use to load Textures and Effect files</param>
        public override void AutoInitialize(GraphicsDevice cGraphicsDevice, ContentManager cContentManager)
        {
            //-----------------------------------------------------------
            // TODO: Change any Initialization parameters desired
            //-----------------------------------------------------------
            // Initialize the Particle System before doing anything else
            InitializePixelParticleSystem(cGraphicsDevice, cContentManager, 1000, 50000, UpdateVertexProperties);

            // Finish loading the Particle System in a separate function call, so if
            // we want to reset the Particle System later we don't need to completely 
            // re-initialize it, we can just call this function to reset it.
            LoadParticleSystem();
        }

        /// <summary>
        /// Load the Particle System Events and any other settings
        /// </summary>
        public void LoadParticleSystem()
        {
            //-----------------------------------------------------------
            // TODO: Setup the Particle System to achieve the desired result.
            // You may change all of the code in this function. It is just
            // provided to show you how to setup a simple particle system.
            //-----------------------------------------------------------

            // Set the Function to use to Initialize new Particles
            ParticleInitializationFunction = InitializeParticleProperties;

            // Remove all Events first so that none are added twice if this function is called again
            ParticleEvents.RemoveAllEvents();
            ParticleSystemEvents.RemoveAllEvents();

            // Make the Particles move according to their Velocity
            ParticleEvents.AddEveryTimeEvent(UpdateParticlePositionUsingVelocity);

            // Set the Particle System's Emitter to toggle on and off every 0.5 seconds
            ParticleSystemEvents.LifetimeData.EndOfLifeOption = CParticleSystemEvents.EParticleSystemEndOfLifeOptions.Repeat;
            ParticleSystemEvents.LifetimeData.Lifetime = 1.0f;
            ParticleSystemEvents.AddTimedEvent(0.0f, UpdateParticleSystemEmitParticlesAutomaticallyOn);
            ParticleSystemEvents.AddTimedEvent(0.5f, UpdateParticleSystemEmitParticlesAutomaticallyOff);

            // Setup the Emitter
            Emitter.ParticlesPerSecond = 2000;
            Emitter.PositionData.Position = new Vector3(0, 0, 0);
        }

        /// <summary>
        /// Function to Initialize a new Particle's properties
        /// </summary>
        /// <param name="cParticle">The Particle to be Initialized</param>
        public void InitializeParticleProperties(PixelParticleSystemTemplateParticle cParticle)
        {
            //-----------------------------------------------------------
            // TODO: Initialize all of the Particle's properties here.
            // In addition to initializing the Particle properties you added, you
            // must also initialize the Lifetime property that is inherited from DPSFParticle
            //-----------------------------------------------------------

            // Set the Particle's Lifetime (how long it should exist for)
            cParticle.Lifetime = 2.0f;

            // Set the Particle's initial Position to be wherever the Emitter is
            cParticle.Position = Emitter.PositionData.Position;

            // Set the Particle's Velocity
            Vector3 sVelocityMin = new Vector3(-50, 50, -50);
            Vector3 sVelocityMax = new Vector3(50, 100, 50);
            cParticle.Velocity = DPSFHelper.RandomVectorBetweenTwoVectors(sVelocityMin, sVelocityMax);

            // Adjust the Particle's Velocity direction according to the Emitter's Orientation
            cParticle.Velocity = Vector3.Transform(cParticle.Velocity, Emitter.OrientationData.Orientation);

            // Give the Particle a random Color
            cParticle.Color = DPSFHelper.RandomColor();
        }

        //===========================================================
        // Particle Update Functions
        //===========================================================

        //-----------------------------------------------------------
        // TODO: Place your Particle Update functions here, using the 
        // same function prototype as below (i.e. public void FunctionName(DPSFParticle, float))
        //-----------------------------------------------------------

        /// <summary>
        /// Update a Particle's Position according to its Velocity
        /// </summary>
        /// <param name="cParticle">The Particle to update</param>
        /// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
        public void UpdateParticlePositionUsingVelocity(PixelParticleSystemTemplateParticle cParticle, float fElapsedTimeInSeconds)
        {
            // Update the Particle's Position according to its Velocity
            cParticle.Position += cParticle.Velocity * fElapsedTimeInSeconds;
        }

        //===========================================================
        // Particle System Update Functions
        //===========================================================

        //-----------------------------------------------------------
        // TODO: Place your Particle System Update functions here, using 
        // the same function prototype as below (i.e. public void FunctionName(float))
        //-----------------------------------------------------------

        /// <summary>
        /// Sets the Emitter to Emit Particles Automatically
        /// </summary>
        /// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
        public void UpdateParticleSystemEmitParticlesAutomaticallyOn(float fElapsedTimeInSeconds)
        {
            Emitter.EmitParticlesAutomatically = true;
        }

        /// <summary>
        /// Sets the Emitter to not Emit Particles Automatically
        /// </summary>
        /// <param name="fElapsedTimeInSeconds">How long it has been since the last update</param>
        public void UpdateParticleSystemEmitParticlesAutomaticallyOff(float fElapsedTimeInSeconds)
        {
            Emitter.EmitParticlesAutomatically = false;
        }

        //===========================================================
        // Other Particle System Functions
        //===========================================================

        //-----------------------------------------------------------
        // TODO: Place any other functions here
        //-----------------------------------------------------------
    }
}
