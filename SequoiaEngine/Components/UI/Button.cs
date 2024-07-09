﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequoiaEngine
{
    public class ButtonComponent : Component
    {

        public Action OnPress;
        public Action OnRelease;
        public Action OnHover;
        public Action OnHoverEnd;

        public string SpriteImageUnpressedPath;
        public string SpriteImagePressedPath;
        public string SpriteImageHoverPath;

        public Texture2D SpriteImageUnpressed = null;
        public Texture2D SpriteImagePressed = null;
        public Texture2D SpriteImageHover = null;

        public AnchorLocation AnchorLocation;


        /// <summary>
        /// If true, button will behave as a checkbox.
        /// </summary>
        public bool ToggleMode;

        // Button value when in toggle mode
        private bool isChecked = false;




        public ButtonComponent(string spriteImageUnpressedPath = "", string spriteImagePressedPath = "", string spriteImageHoverPath = "", bool toggleModeActive = false, Action onPress = null, Action onRelease = null, Action onHover = null, Action onHoverEnd = null)
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

            this.ToggleMode = toggleModeActive;
            this.OnPress = onPress;
            this.OnRelease = onRelease;
            this.OnHover = onHover;
            this.OnHoverEnd = onHoverEnd;
        }

        public ButtonComponent(Texture2D spriteImageUnpressed = null, Texture2D spriteImagePressed = null, Texture2D spriteImageHover = null, bool toggleModeActive = false, Action onPress = null, Action onRelease = null, Action onHover = null, Action onHoverEnd = null)
        {
            SpriteImageUnpressed = spriteImageUnpressed;
            SpriteImagePressed = spriteImagePressed;
            SpriteImageHover = spriteImageHover;
            this.ToggleMode = toggleModeActive;
            this.OnPress = onPress;
            this.OnRelease = onRelease;
            this.OnHover = onHover;
            this.OnHoverEnd = onHoverEnd;
        }

    }
}
