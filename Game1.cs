using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using SequoiaEngine;
using System.Collections.Generic;


namespace Biosphere
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly ScreenManager screenManager;


        private Dictionary<ScreenEnum, SequoiaEngine.Screen> screens;
        private SequoiaEngine.Screen currentScreen;
        private ScreenEnum nextScreen;
        private bool newScreenFocused;
        const int VIRTUAL_WIDTH = 480;
        const int VIRTUAL_HEIGHT = 270; // Aspect ratio of 16:9

        InputManager inputManager;
        
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            screenManager = new ScreenManager();
            Components.Add(screenManager);
            screens = new();

            inputManager = new();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            ResourceManager.Manager = Content;

            Window.AllowUserResizing = true;

            screens.Add(ScreenEnum.Test, new TestScreen(this, ScreenEnum.Test));
            currentScreen = screens[ScreenEnum.Test];
            nextScreen = ScreenEnum.Test;



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (ScreenEnum screen in screens.Keys)
            {
                screens[screen].Initialize(GraphicsDevice, _graphics, Window);
            }

            foreach (ScreenEnum screen in screens.Keys)
            {
                screens[screen].LoadContent();
                screens[screen].SetupGameObjects();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            inputManager.Update();
            // Diplays FPS
            /*            Debug.WriteLine(1 / gameTime.ElapsedGameTime.TotalSeconds);
            */

            if (newScreenFocused)
            {
                currentScreen.OnScreenFocus();
                newScreenFocused = false;
            }

            currentScreen.Update(gameTime);

            if (screens[nextScreen] != currentScreen)
            {
                currentScreen.OnScreenDefocus();
                newScreenFocused = true;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            currentScreen.Draw(gameTime);

            currentScreen = screens[nextScreen];

            base.Draw(gameTime);
        }
    }
}
