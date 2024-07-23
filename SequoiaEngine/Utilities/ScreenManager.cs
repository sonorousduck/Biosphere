using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequoiaEngine
{
    public class ScreenManager
    {
        public static ScreenManager Instance { get; private set; }

        public Screen CurrentScreen { get; private set; }
        public ScreenEnum NextScreen { get; private set; }
        public ScreenEnum CurrentScreenEnum { get; private set; }
        public bool NewScreenFocused { get; private set; }
        public Dictionary<ScreenEnum, Screen> Screens { get; private set; } = new Dictionary<ScreenEnum, Screen>();

        public RenderTarget2D CurrentScreenRenderTarget { get; private set; }
        public RenderTarget2D NextScreenRenderTarget { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public ScreenManager()
        {
            Instance = this;
            CurrentScreenRenderTarget = new RenderTarget2D(GameManager.Instance.GraphicsDevice, 640, 360, false, SurfaceFormat.Color, DepthFormat.None, GameManager.Instance.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
            NextScreenRenderTarget = new RenderTarget2D(GameManager.Instance.GraphicsDevice, 640, 360, false, SurfaceFormat.Color, DepthFormat.None, GameManager.Instance.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
            SpriteBatch = new(GameManager.Instance.GraphicsDevice);
        }

        public void SetNextScreen(ScreenEnum screen)
        {
            NextScreen = screen;
        }

        public void SetCurrentScreen(ScreenEnum newScreen = ScreenEnum.None)
        {
            if (!newScreen.Equals(ScreenEnum.None))
            {
                NextScreen = newScreen;
                CurrentScreen = Screens[newScreen];
                CurrentScreenEnum = newScreen;
            }

            else
            {
                CurrentScreen = Screens[NextScreen];
                CurrentScreenEnum = NextScreen;
            }

            NewScreenFocused = true;
        }

        public void Update(GameTime gameTime)
        {
            if (NewScreenFocused)
            {
                CurrentScreen.Start();
                CurrentScreen.OnScreenFocus();
                NewScreenFocused = false;
            }

            CurrentScreen.Update(gameTime);

            if (CurrentScreenEnum != NextScreen)
            {
                CurrentScreen.OnScreenDefocus();
                NewScreenFocused = true;
            }
        }

        public void Draw(GameTime gameTime)
        {
            GameManager.Instance.GraphicsDevice.SetRenderTarget(CurrentScreenRenderTarget);
            GameManager.Instance.GraphicsDevice.Clear(Color.Transparent);

            SpriteBatch.Begin(SpriteSortMode.Immediate);

            CurrentScreen.Draw(gameTime);

            SpriteBatch.End();

        }


        public void PreUpdate()
        {
            if (CurrentScreenEnum != NextScreen)
            {
                SetCurrentScreen();
            }
        }

    }
}
