using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using SequoiaEngine;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers.Containers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended;

namespace CrowEngine
{
    class TestScreen : Screen
    {

        private RenderingSystem renderingSystem;
        private AnimationSystem animationSystem;
        private PhysicsSystem physicsEngine;
        private ParticleRenderingSystem particleRenderer;
        private ParticleSystem particleSystem;
        private InputSystem inputSystem;
        private ScriptSystem scriptSystem;
       // private LightRenderingSystem lightRenderer;
        RenderTarget2D mainRenderTarget;
        private GameObject camera;
        private FontRenderingSystem fontRenderingSystem;


        private Texture2D tilesetTexture;

        private RenderTarget2D renderTarget;
        private RenderTarget2D lightRenderTarget;
        private RenderTarget2D tileRenderTarget;
        private RenderTarget2D hudRenderTarget;

        private GameObject player;

        public TestScreen(Game game, ScreenEnum screenEnum) : base(game, screenEnum) { }

        public override void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameWindow window)
        {
            base.Initialize(graphicsDevice, graphics, window);

            physicsEngine = new PhysicsSystem(systemManager);
            inputSystem = new InputSystem(systemManager);
            scriptSystem = new ScriptSystem(systemManager);
            particleSystem = new ParticleSystem(systemManager);
            particleRenderer = new ParticleRenderingSystem(systemManager);

            animationSystem = new AnimationSystem(systemManager);

            camera = new GameObject();
            camera.Add(new Transform(Vector2.Zero, 0, Vector2.One));
            camera.Add(new Rigidbody());
            camera.Add(new CircleCollider(1, false));
            mainRenderTarget = new RenderTarget2D(graphicsDevice, 480, 270, false, SurfaceFormat.Color, DepthFormat.None, graphics.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
            tileRenderTarget = new RenderTarget2D(graphicsDevice, 480, 270, false, SurfaceFormat.Color, DepthFormat.None, graphics.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
            hudRenderTarget = new RenderTarget2D(graphicsDevice, 480, 270, false, SurfaceFormat.Color, DepthFormat.None, graphics.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
            systemManager.Add(camera);
        }

        public override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.SetRenderTarget(tileRenderTarget);
            graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);


            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);



            graphics.GraphicsDevice.SetRenderTarget(mainRenderTarget);
            graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            graphics.GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);
            renderingSystem.Draw(gameTime, spriteBatch);
            particleRenderer.Draw(gameTime, spriteBatch);
            fontRenderingSystem.Draw(gameTime, spriteBatch);


            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);

            graphics.GraphicsDevice.SetRenderTarget(hudRenderTarget);
            graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            graphics.GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);



            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(tileRenderTarget, new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(mainRenderTarget, new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(hudRenderTarget, new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.End(); 
        }


        public override void LoadContent()
        {

            renderingSystem = new RenderingSystem(systemManager, window.ClientBounds.Height, camera, new Vector2(window.ClientBounds.Width, window.ClientBounds.Height));
            fontRenderingSystem = new FontRenderingSystem(systemManager, camera);
            //lightRenderer = new LightRenderingSystem(systemManager, camera, graphicsDevice);
            //lightRenderer.globalLightLevel = 0f;

        }

        public override void OnScreenDefocus()
        {
            Debug.WriteLine("Default Screen was unloaded");
        }

        public override void OnScreenFocus()
        {
            Debug.WriteLine("Default Screen was loaded");
        }

        /// <summary>
        /// Note, while this one creates gameObjects manually inline, this should really be done in a separate file, in a static class.
        /// The reason this is done this way here, is so that any naming conventions you'd like to have don't conflict
        /// </summary>
        public override void SetupGameObjects()
        {
            GameObject testParticles = new();

            Texture2D _particleTexture = new(GraphicsDevice, 1, 1);
            _particleTexture.SetData(new[] { Color.White });
            TextureRegion2D textureRegion = new(_particleTexture);


            ParticleEffect _particleEffect = new(autoTrigger: false)
            {
                Position = new Vector2(200, 200),
                Emitters = new List<ParticleEmitter>
                {
                    new(textureRegion, 500, TimeSpan.FromSeconds(2.5),
                       Profile.Ring(10, Profile.CircleRadiation.In))


                    {
                        Parameters = new ParticleReleaseParameters
                        {
                            Speed = new Range<float>(0f, 50f),
                            Quantity = 3,
                            Rotation = new Range<float>(-1f, 1f),
                            Scale = new Range<float>(2.0f, 2.0f)
                        },
                        Modifiers =
                        {
                            new AgeModifier
                            {
                                Interpolators =
                                {
                                    new ColorInterpolator
                                    {
                                        StartValue = Color.Yellow.ToHsl(),
                                        EndValue = Color.Blue.ToHsl()
                                    }
                                }
                            },
                            new RotationModifier {RotationRate = -2.1f},
                            new LinearGravityModifier {Direction = -Vector2.UnitY, Strength = 30f},
                            new VelocityColorModifier
                        {
                            StationaryColor = Color.Green.ToHsl(),
                            VelocityColor = Color.Blue.ToHsl(),
                            VelocityThreshold = 80f
                        }
                        }
                    }
                }
            };



           
            SequoiaEngine.Particle particle = new(_particleTexture, _particleEffect);

            testParticles.Add(particle);

            systemManager.Add(testParticles);




        }
    }
}