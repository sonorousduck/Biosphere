using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequoiaEngine
{
    public class Canvas
    {
        public Texture2D BackgroundTexture;
        public Vector2 Position;
        public Vector2 Size;
        public float Rotation;
        public Color SpriteColor = Color.White;

        /// <summary>
        /// Instantiated differently because it is a entity instead of a prefab. I could have technically done it the same, but I think I like this way better..?
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="size"></param>
        /// <param name="backgroundTexture"></param>
        public Canvas(Vector2 position, float rotation, Vector2 size, Texture2D backgroundTexture)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Size = size;
            this.BackgroundTexture = backgroundTexture;
        }

        /// <summary>
        /// Uses a string path instead of a Texture2D directly
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="size"></param>
        /// <param name="backgroundTexture"></param>
        public Canvas(Vector2 position, float rotation, Vector2 size, string backgroundTextureName)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Size = size;
            this.BackgroundTexture = ResourceManager.Get<Texture2D>(backgroundTextureName);
        }




        public GameObject Create()
        {
            GameObject gameObject = new(new Transform(this.Position, this.Rotation, this.Size));

            gameObject.Add(new Sprite(this.BackgroundTexture, this.SpriteColor, 0f, true));




            return gameObject;
        }

    }
}
