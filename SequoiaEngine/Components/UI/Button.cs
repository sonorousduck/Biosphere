using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequoiaEngine
{
    public class Button : Component
    {

        Action onPress;

        public string SpriteImageUnpressedPath;
        public string SpriteImagePressedPath;
        public string SpriteImageHoverPath;

        public Texture2D SpriteImageUnpressed = null;
        public Texture2D SpriteImagePressed = null;
        public Texture2D SpriteImageHover = null;


        /// <summary>
        /// If true, button will behave as a checkbox.
        /// </summary>
        public bool ToggleMode;

        // Button value when in toggle mode
        private bool isChecked = false;




        public Button(string spriteImageUnpressedPath = "", string spriteImagePressedPath = "", string spriteImageHoverPath = "")
        {
            if (spriteImageUnpressedPath != "")
            {
                SpriteImageUnpressed = ResourceManager.Manager.Load<Texture2D>(spriteImageUnpressedPath);
                this.SpriteImageUnpressedPath = spriteImageUnpressedPath;
            }

            if (spriteImagePressedPath != "")
            {
                SpriteImagePressed = ResourceManager.Manager.Load<Texture2D>(spriteImagePressedPath);
                this.SpriteImagePressedPath = spriteImagePressedPath;
            }

            if (spriteImageHoverPath != "")
            {
                SpriteImageHover = ResourceManager.Manager.Load<Texture2D>(spriteImageHoverPath);
                this.SpriteImageHoverPath = spriteImageHoverPath;
            }
        }

        public Button(Texture2D spriteImageUnpressed = null, Texture2D spriteImagePressed = null, Texture2D spriteImageHover = null)
        {
            SpriteImageUnpressed = spriteImageUnpressed;
            SpriteImagePressed = spriteImagePressed;
            SpriteImageHover = spriteImageHover;
        }

    }
}
