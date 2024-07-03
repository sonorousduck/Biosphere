using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SequoiaEngine
{
    public class Button
    {
        public Texture2D BackgroundTexture;
        public Texture2D PressedBackgroundTexture;
        public Texture2D HoverBackgroundTexture;
        public Vector2 Position;
        public Vector2 Size;
        public float Rotation;
        public Color SpriteColor = Color.White;
        public AnchorLocation AnchorLocation;
        public ScaleSize Scale;



        public GameObject GameObject { get; private set; }


        public Button(Vector2 position, 
            float rotation, 
            Vector2 size, 
            Texture2D backgroundTexture, 
            Texture2D pressedBackground = null, 
            Texture2D hoverBackground = null, 
            AnchorLocation anchorLocation = AnchorLocation.None, 
            ScaleSize scale = ScaleSize.None,
            bool toggleModeActive = false,
            Action onPress = null,
            Action onRelease = null,
            GameObject parent = null,
            string tag = ""
            )
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Size = size;
            this.BackgroundTexture = backgroundTexture;
            this.PressedBackgroundTexture = pressedBackground;
            this.HoverBackgroundTexture = hoverBackground;
            this.AnchorLocation = anchorLocation;
            this.Scale = scale;
            
            GameObject = new(new Transform(this.Position, this.Rotation, this.Size), parent, tag);
            
            Setup(toggleModeActive, onPress, onRelease);
        }

        public Button(Vector2 position, 
            float rotation, 
            Vector2 size, 
            string backgroundTextureName, 
            string pressedBackgroundName = "", 
            string hoverBackgroundName = "", 
            AnchorLocation anchorLocation = AnchorLocation.None, 
            ScaleSize scale = ScaleSize.None,
            bool toggleModeActive = false,
            Action onPress = null,
            Action onRelease = null,
            GameObject parent = null,
            string tag = ""
            )
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Size = size;
            this.BackgroundTexture = ResourceManager.Get<Texture2D>(backgroundTextureName);

            if (pressedBackgroundName != "")
            {
                this.PressedBackgroundTexture = ResourceManager.Get<Texture2D>(pressedBackgroundName);
            }

            if (hoverBackgroundName != "")
            {
                this.HoverBackgroundTexture = ResourceManager.Get<Texture2D>(hoverBackgroundName);
            }

            this.AnchorLocation = anchorLocation;
            this.Scale = scale;

            GameObject = new(new Transform(this.Position, this.Rotation, this.Size, true), parent, tag);

            Setup(toggleModeActive, onPress, onRelease);
        }


        private void Setup(bool toggleModeActive, Action onPress, Action onRelease)
        {
            float spriteDrawLocationModification = 1.0f;

            GameObject parent = this.GameObject.GetParent();

            if (parent != null && parent.TryGetComponent(out Sprite parentBackground))
            {
                spriteDrawLocationModification = parentBackground.renderDepth - 0.001f;
            }

            GameObject.Add(new Sprite(this.BackgroundTexture, this.SpriteColor, spriteDrawLocationModification, true));

            if (!this.AnchorLocation.Equals(AnchorLocation.None))
            {
                Anchor anchor = new(this.AnchorLocation);
                GameObject.Add(anchor);


                GameObject.GetComponent<Transform>().position += anchor.GetAnchorPoint(GameObject);
            }

            if (!this.Scale.Equals(ScaleSize.None))
            {
                Scale scale = new(this.Scale);
                GameObject.Add(scale);

                GameObject.GetComponent<Transform>().scale *= scale.GetScaleModifier(GameObject);
            }


            ButtonComponent button = new(BackgroundTexture, PressedBackgroundTexture, HoverBackgroundTexture, toggleModeActive, onPress, onRelease);

            GameObject.Add(button);


            RectangleCollider collider = new RectangleCollider(this.GameObject.GetComponent<Transform>().scale * this.GameObject.GetComponent<Sprite>()?.size ?? Vector2.One, false);
            collider.LayersToCollideWith = CollisionLayer.UI;
            GameObject.Add(collider);

            GameObject.Add(new Rigidbody());

        }

    }
}
