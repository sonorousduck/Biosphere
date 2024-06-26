using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace SequoiaEngine
{
    public enum ScreenEnum
    {
        Default,
        Game,
        MainMenu,
        Controls,
        Credits,
        PauseScreen,
        Quit,
        CameraTest,
        Test
    }

    public partial class Screen : GameScreen
    {
        protected ScreenEnum screenName;
        protected ScreenEnum currentScreen;



        protected Screen(Game game, ScreenEnum screenEnum) : base(game)
        {

            this.screenName = screenEnum;
            this.currentScreen = screenEnum;
        }

/*        public ScreenEnum Update(GameTime gameTime)
        {
            systemManager.Update(gameTime);

            return currentScreen;
        }*/

        protected void SetCurrentScreen(ScreenEnum screenEnum)
        {
            currentScreen = screenEnum;
        }

        public delegate void SetCurrentScreenDelegate(ScreenEnum screenEnum);
    }
}
