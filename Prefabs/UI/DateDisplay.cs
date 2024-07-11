using SequoiaEngine;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;

namespace Biosphere.Prefabs.UI
{
    public static class DateDisplay
    {
        public static GameObject Create()
        {
            GameObject go = new GameObject(new Transform(new Vector2(GameManager.Instance.RenderWidth, 8f), 0.0f, Vector2.One, true));

            Text text = new Text("00/0000", Color.White, Color.Transparent, ResourceManager.Get<BitmapFont>("default_pixel_18"), isHUDElement: true);

            // NOTICE: Added an extra zero for a little more padding on the string positioning
            go.GetComponent<Transform>().position.X -= ResourceManager.Get<BitmapFont>("default_pixel_18").MeasureString("22/22222").Width;

            go.Add(new DateDisplayScript(go));


            go.Add(text);

            return go;
        }
    }
}
