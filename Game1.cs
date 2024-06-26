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
        
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            screenManager = new ScreenManager();
            Components.Add(screenManager);


            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            Window.AllowUserResizing = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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
