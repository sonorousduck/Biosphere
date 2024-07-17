using MonoGame.Extended.BitmapFonts;
using SequoiaEngine;
using Microsoft.Xna.Framework;

namespace Biosphere
{
    public static class SeasonDisplay
    {
        public static GameObject Create()
        {
            GameObject gameObject = new GameObject(new Transform(new Vector2(GameManager.Instance.RenderWidth, 24f), 0.0f, Vector2.One, true));
            Text text = new Text("Summer", Color.White, Color.Transparent, ResourceManager.Get<BitmapFont>("default_pixel_18"));

            // NOTICE: Added an extra zero for a little more padding on the string positioning
            gameObject.GetComponent<Transform>().position.X -= ResourceManager.Get<BitmapFont>("default_pixel_18").MeasureString("22/22222").Width;

            gameObject.Add(new SeasonDisplayScript(gameObject));


            gameObject.Add(text);



            return gameObject;
        }
    }
}
