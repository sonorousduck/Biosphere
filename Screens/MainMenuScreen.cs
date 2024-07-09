using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SequoiaEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosphere
{
    public class MainMenuScreen : Screen
    {
        private RenderingSystem renderingSystem;
        private FontRenderingSystem fontRenderingSystem;
        private PhysicsSystem physicsSystem;
        private InputSystem inputSystem;
        private ScriptSystem scriptSystem;
        private AnimatedSystem animatedSystem;

        private RenderTarget2D mainRenderTarget;
        private RenderTarget2D hudRenderTarget;

        private GameObject camera;

        public MainMenuScreen(Game game, ScreenEnum screenEnum) : base(game, screenEnum) { }


        public override void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameWindow window)
        {
            base.Initialize(graphicsDevice, graphics, window);

            physicsSystem = new(systemManager);
            inputSystem = new(systemManager);
            scriptSystem = new(systemManager);
            animatedSystem = new(systemManager);

            camera = CameraPrefab.Create();

            mainRenderTarget = new RenderTarget2D(graphicsDevice, 640, 360, false, SurfaceFormat.Color, DepthFormat.None, graphics.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
            hudRenderTarget = new RenderTarget2D(graphicsDevice, 640, 360, false, SurfaceFormat.Color, DepthFormat.None, graphics.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);

            systemManager.Add(camera);
        }

        public override void LoadContent()
        {
            renderingSystem = new RenderingSystem(systemManager, window.ClientBounds.Height, camera, new Vector2(window.ClientBounds.Width, window.ClientBounds.Height));
            fontRenderingSystem = new FontRenderingSystem(systemManager, camera);

            ResourceManager.Load<Texture2D>("Sprites/UI/MainMenuButton", "mainMenuButton");
            ResourceManager.Load<Texture2D>("Sprites/UI/MainMenuButtonHover", "mainMenuButton_hover");
            ResourceManager.Load<Texture2D>("Sprites/UI/MainMenuButtonPressed", "mainMenuButton_pressed");
            ResourceManager.Load<Texture2D>("Sprites/Default", "default");


        }

        public override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.SetRenderTarget(mainRenderTarget);
            graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() {  DepthBufferEnable = true };
            graphics.GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointWrap, transformMatrix: GameManager.Instance.Camera.GetViewMatrix());
            renderingSystem.Draw(gameTime, spriteBatch);
            fontRenderingSystem.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);

            graphics.GraphicsDevice.SetRenderTarget(hudRenderTarget);
            graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            graphics.GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);


            renderingSystem.Draw(gameTime, spriteBatch, true);
            fontRenderingSystem.Draw(gameTime, spriteBatch, true);


            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);


            spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointWrap);

            spriteBatch.Draw(hudRenderTarget, GameManager.Instance.DestinationRectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(mainRenderTarget, GameManager.Instance.DestinationRectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            spriteBatch.End();
        }

        public override void OnScreenDefocus()
        {
            Debug.WriteLine("MainMenuScreen was unloaded");
        }

        public override void OnScreenFocus()
        {
            inputSystem.Start();
            physicsSystem.Start();
            inputSystem.Start();
            scriptSystem.Start();
            animatedSystem.Start();
        }

        public override void SetupGameObjects()
        {
            Action onButtonPress = () =>
            {
                Debug.WriteLine("Pressed Button!");
            };
            
            systemManager.Add(CursorPrefab.Create(new Vector2(100, 100), Vector2.One));


            Canvas canvas = new Canvas(new Vector2(320, 180), 0, new Vector2(640, 360), backgroundTextureName: "default");
            Canvas canvas1 = new Canvas(new Vector2(-120, 0), 0, new Vector2(240, 240), backgroundTextureName: "default", anchorLocation: AnchorLocation.MiddleRight, parent: canvas.GameObject);
            canvas1.GameObject.GetComponent<Sprite>().color = Color.Green;
            
            Button button = new Button(new Vector2(0, 34.5f), 0, Vector2.One, backgroundTextureName: "mainMenuButton", hoverBackgroundName: "mainMenuButton_hover", pressedBackgroundName: "mainMenuButton_pressed", anchorLocation: AnchorLocation.TopMiddle, parent: canvas1.GameObject, onPress: onButtonPress, tag: "TestButton");
            Button button1 = new Button(new Vector2(0, 0.5f), 0, Vector2.One, backgroundTextureName: "mainMenuButton", hoverBackgroundName: "mainMenuButton_hover", pressedBackgroundName: "mainMenuButton_pressed", anchorLocation: AnchorLocation.MiddleMiddle, parent: canvas1.GameObject, onPress: onButtonPress, tag: "TestButton");
            Button button2 = new Button(new Vector2(0, -34.5f), 0, Vector2.One, backgroundTextureName: "mainMenuButton", hoverBackgroundName: "mainMenuButton_hover", pressedBackgroundName: "mainMenuButton_pressed", anchorLocation: AnchorLocation.BottomMiddle, parent: canvas1.GameObject, onPress: onButtonPress, tag: "TestButton");


            systemManager.Add(canvas.GameObject);
            systemManager.Add(canvas1.GameObject);
            systemManager.Add(button.GameObject);
            systemManager.Add(button1.GameObject);
            systemManager.Add(button2.GameObject);
        }
    }
}
