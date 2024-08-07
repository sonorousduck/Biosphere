﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using SequoiaEngine;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using Biosphere.Prefabs.UI;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Microsoft.Xna.Framework.Audio;



namespace Biosphere
{
    class TestScreen : Screen
    {

        private RenderingSystem renderingSystem;
        private SpritesheetAnimationSystem animationSystem;
        private PhysicsSystem physicsEngine;
        private ParticleRenderingSystem particleRenderer;
        private ParticleSystem particleSystem;
        private InputSystem inputSystem;
        private ScriptSystem scriptSystem;
        private AnimatedSystem animatedSystem;
        private TimeManager timeManager;

        private AudioSystem audioSystem;

        private TiledRenderingSystem tiledRenderingSystem;



        private RenderTarget2D mainRenderTarget;
        // private LightRenderingSystem lightRenderer;

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
            animatedSystem = new AnimatedSystem(systemManager);
            audioSystem = new AudioSystem(systemManager);
            tiledRenderingSystem = new TiledRenderingSystem(systemManager);

            animationSystem = new SpritesheetAnimationSystem(systemManager);
            timeManager = new TimeManager(systemManager);

            camera = CameraPrefab.Create();

            mainRenderTarget = new RenderTarget2D(graphicsDevice, 640, 360, false, SurfaceFormat.Color, DepthFormat.None, graphics.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
            tileRenderTarget = new RenderTarget2D(graphicsDevice, 640, 360, false, SurfaceFormat.Color, DepthFormat.None, graphics.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
            hudRenderTarget = new RenderTarget2D(graphicsDevice, 640, 360, false, SurfaceFormat.Color, DepthFormat.None, graphics.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
            systemManager.Add(camera);
        }


        public override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.SetRenderTarget(tileRenderTarget);
            graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp, transformMatrix: GameManager.Instance.Camera.GetViewMatrix());


            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);



            graphics.GraphicsDevice.SetRenderTarget(mainRenderTarget);
            graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            graphics.GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp, transformMatrix: GameManager.Instance.Camera.GetViewMatrix());


            tiledRenderingSystem.Draw(gameTime, spriteBatch);
            renderingSystem.Draw(gameTime, spriteBatch);
            particleRenderer.Draw(gameTime, spriteBatch);
            fontRenderingSystem.Draw(gameTime, spriteBatch);


            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);

            graphics.GraphicsDevice.SetRenderTarget(hudRenderTarget);
            graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            graphics.GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            tiledRenderingSystem.Draw(gameTime, spriteBatch, true);
            renderingSystem.Draw(gameTime, spriteBatch, true);
            particleRenderer.Draw(gameTime, spriteBatch, true);
            fontRenderingSystem.Draw(gameTime, spriteBatch, true);


            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);



            spriteBatch.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointWrap);

            spriteBatch.Draw(tileRenderTarget, GameManager.Instance.DestinationRectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(mainRenderTarget, GameManager.Instance.DestinationRectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(hudRenderTarget, GameManager.Instance.DestinationRectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            spriteBatch.End(); 
        }


        public override void LoadContent()
        {

            ResourceManager.Load<Texture2D>("Sprites/ForestStoreTile", "forestStoreTile");
            ResourceManager.Load<Texture2D>("Sprites/ForestTile", "forestTile");
            ResourceManager.Load<Texture2D>("Sprites/MainInventoryBackground", "mainInventoryBackground");
            ResourceManager.Load<Texture2D>("Sprites/MountainsStoreTile", "mountainsStoreTile");
            ResourceManager.Load<Texture2D>("Sprites/MountainTile", "mountainTile");
            ResourceManager.Load<Texture2D>("Sprites/NewTileLocation", "newTileLocation");
            ResourceManager.Load<Texture2D>("Sprites/PlainsStoreTile", "plainsStoreTile");
            ResourceManager.Load<Texture2D>("Sprites/PlainsTemp", "plainsTemp");
            ResourceManager.Load<Texture2D>("Sprites/box", "box");
            ResourceManager.Load<Texture2D>("Sprites/UI/CloseDrawer", "closeDrawer");
            ResourceManager.Load<Texture2D>("Sprites/UI/OpenDrawer", "openDrawer");
            ResourceManager.Load<Texture2D>("Sprites/UI/LeftSideDrawer", "leftSideDrawer");
            ResourceManager.Load<Texture2D>("Sprites/Default", "default");
            ResourceManager.Load<Texture2D>("Sprites/UI/SlowTimeButton", "slowTimeButton");
            ResourceManager.Load<Texture2D>("Sprites/UI/SlowTimeButtonHover", "slowTimeButton_hover");
            ResourceManager.Load<Texture2D>("Sprites/UI/SlowTimeButtonPressed", "slowTimeButton_pressed");
            
            ResourceManager.Load<Texture2D>("Sprites/UI/DoubleSlow", "doubleSlow");
            ResourceManager.Load<Texture2D>("Sprites/UI/DoubleSlowHover", "doubleSlow_hover");
            ResourceManager.Load<Texture2D>("Sprites/UI/DoubleSlowPressed", "doubleSlow_pressed");

            ResourceManager.Load<Texture2D>("Sprites/UI/Slow", "slow");
            ResourceManager.Load<Texture2D>("Sprites/UI/SlowHover", "slow_hover");
            ResourceManager.Load<Texture2D>("Sprites/UI/SlowPressed", "slow_pressed");

            ResourceManager.Load<Texture2D>("Sprites/UI/Play", "play");
            ResourceManager.Load<Texture2D>("Sprites/UI/PlayHover", "play_hover");
            ResourceManager.Load<Texture2D>("Sprites/UI/PlayPressed", "play_pressed");

            ResourceManager.Load<Texture2D>("Sprites/UI/Fast", "fast");
            ResourceManager.Load<Texture2D>("Sprites/UI/FastHover", "fast_hover");
            ResourceManager.Load<Texture2D>("Sprites/UI/FastPressed", "fast_pressed");

            ResourceManager.Load<Texture2D>("Sprites/UI/DoubleFast", "doubleFast");
            ResourceManager.Load<Texture2D>("Sprites/UI/DoubleFastHover", "doubleFast_hover");
            ResourceManager.Load<Texture2D>("Sprites/UI/DoubleFastPressed", "doubleFast_pressed");


            ResourceManager.Load<Texture2D>("Sprites/UI/SpeedTimeButton", "speedTimeButton");
            ResourceManager.Load<Texture2D>("Sprites/UI/SpeedTimeButtonHover", "speedTimeButton_hover");
            ResourceManager.Load<Texture2D>("Sprites/UI/SpeedTimeButtonPressed", "speedTimeButton_pressed");

            ResourceManager.Load<Texture2D>("Spritesheets/Animals/BunnySpritesheet", "bunny_spritesheet");




            ResourceManager.Load<BitmapFont>("Fonts/Default_Pixel_18", "default_pixel_18");


            ResourceManager.Load<SoundEffect>("SoundEffects/UI/MouseClick", "mouseClick");
            ResourceManager.Load<SoundEffect>("SoundEffects/UI/ButtonClick", "buttonClick");


            renderingSystem = new RenderingSystem(systemManager, window.ClientBounds.Height, camera, new Vector2(window.ClientBounds.Width, window.ClientBounds.Height));
            renderingSystem.debugMode = false;
            fontRenderingSystem = new FontRenderingSystem(systemManager, camera);

            //lightRenderer = new LightRenderingSystem(systemManager, camera, graphicsDevice);
            //lightRenderer.globalLightLevel = 0f;



        }

        public override void OnScreenDefocus()
        {
            Debug.WriteLine("Test Screen was unloaded");
        }

        public override void OnScreenFocus()
        {
            systemManager.StartSystems();
/*            inputSystem.Start();
            physicsEngine.Start();
            inputSystem.Start();
            scriptSystem.Start();
            particleSystem.Start();
            particleRenderer.Start();
            animationSystem.Start();
            animatedSystem.Start();
            timeManager.Start();*/
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
            Texture2DRegion textureRegion = new(_particleTexture);


            ParticleEffect _particleEffect = new()
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
                        },

                        }
                    }
                }
            };



           
            SequoiaEngine.Particle particle = new(_particleTexture, _particleEffect);

            testParticles.Add(particle);

            systemManager.Add(testParticles);

            GameObject cursor = CursorPrefab.Create(new Vector2(100, 100), Vector2.One);

            systemManager.Add(cursor);

            Action<GameObject> onButtonPress = (GameObject button) =>
            {
                Debug.WriteLine("Pressed Button!");
            };


            Canvas canvas = new(new Vector2(200, 200), 0, Vector2.One, ResourceManager.Get<Texture2D>("mountainsStoreTile"));
            Canvas canvasTest = new(new Vector2(0, 0), 0, Vector2.One, ResourceManager.Get<Texture2D>("cursor"), AnchorLocation.MiddleLeft, ScaleSize.None, canvas.GameObject);
            Button button = new(new Vector2(0, 0), 0, Vector2.One, "newTileLocation", anchorLocation: AnchorLocation.TopLeft, parent: canvas.GameObject, onPress: onButtonPress, tag: "TestButton");
            CollapsibleDrawer drawer = new(new Vector2(88, 175), 0, Vector2.One, ResourceManager.Get<Texture2D>("leftSideDrawer"), openedButtonFilepath: "openDrawer", closedButtonFilepath: "closeDrawer", tag: "LeftDrawer");




            systemManager.Add(canvasTest.GameObject);
            systemManager.Add(canvas.GameObject);
            systemManager.Add(button.GameObject);
            systemManager.Add(drawer.GameObject);
            drawer.AddSubcomponentsToSystemManager(systemManager);

            systemManager.Add(DateDisplay.Create());
            systemManager.Add(SeasonDisplay.Create());
            //systemManager.Add(SelectedItemCursorChild.Create(cursor));

            GameObject parentObject = SpeedControl.Create(systemManager);

            systemManager.Add(parentObject);


            //GameObject tiledMap = new();

            //TiledMapComponent tiledMapComponent = new(Content.Load<TiledMap>("Maps/Test"));

            //tiledMap.Add(tiledMapComponent);

            //systemManager.Add(tiledMap);




            GameObject go = new GameObject(new Transform(new Vector2(300, 200), 0, Vector2.One));

            TextureAtlas atlas = TextureAtlas.Create("Atlas/Bunny", ResourceManager.Get<Texture2D>("bunny_spritesheet"), 48, 48);
            SequoiaEngine.AnimatedSprite animatedSprite = new SequoiaEngine.AnimatedSprite(atlas);
            animatedSprite.CreateAnimation("walk");
            animatedSprite.Animations["walk"].AddFrame(0, 0.1f);
            animatedSprite.Animations["walk"].AddFrame(1, 0.1f);
            animatedSprite.Animations["walk"].AddFrame(2, 0.1f);
            animatedSprite.Animations["walk"].AddFrame(3, 0.1f);
            animatedSprite.Animations["walk"].AddFrame(4, 0.1f);
            animatedSprite.Animations["walk"].AddFrame(5, 0.1f);
            animatedSprite.Animations["walk"].AddFrame(6, 0.1f);
            animatedSprite.Animations["walk"].AddFrame(7, 0.1f);
            animatedSprite.Animations["walk"].PlayForever = true;



            animatedSprite.Animations["walk"].Callbacks.Add(7, (go) =>
            {
                go.GetComponent<AnimationController>().AnimationTree.Tree[0].Animations[0].Play("idle");
            });


            animatedSprite.CreateAnimation("idle");
            animatedSprite.Animations["idle"].AddFrame(8, 0.1f);
            animatedSprite.Animations["idle"].AddFrame(9, 0.1f);
            animatedSprite.Animations["idle"].AddFrame(10, 0.1f);
            animatedSprite.Animations["idle"].AddFrame(11, 0.5f);

            animatedSprite.Animations["idle"].Callbacks.Add(3, (go) =>
            {
                go.GetComponent<AnimationController>().AnimationTree.Tree[0].Animations[0].Play("walk");
            });


            // Right now, the animation tree does absolutely nothing. Instead of the AnimatedSprite having an array of Animations with names
            // that functionality should be taken by the Animation Tree and is linked to the frames.
            // Then, when we build our links, we can easily go between the "walk" and "idle" animation without a clunky lookup

            AnimationTree animationTree = new AnimationTree();

            animatedSprite.Play("walk");
            Node node = new Node(new List<Link>(), new List<SequoiaEngine.AnimatedSprite> { animatedSprite });

            animationTree.AddNode(node);


            AnimationController animationController = new AnimationController(animationTree, 1.0f);


            go.Add(animationController);

            systemManager.Add(go);
            //GameObject test = new(new Transform(new Vector2(ResourceManager.Get<Texture2D>("mountainsStoreTile").Width / 2f, ResourceManager.Get<Texture2D>("mountainsStoreTile").Height / 2f), 0f, Vector2.One));

            //test.Add(new Sprite(ResourceManager.Get<Texture2D>("mountainsStoreTile"), Color.White, 1.0f, true));
            //systemManager.Add(test);
        }
    }
}