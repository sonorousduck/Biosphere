﻿using Microsoft.Xna.Framework;
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

        Anchor anchor;
        Scale scale;



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
            Action onHover = null,
            Action onHoverEnd = null,
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

            if (hoverBackground != null)
            {
                if (onHover == null)
                {
                    onHover = () =>
                    {
                        GameObject.GetComponent<Sprite>().sprite = GameObject.GetComponent<ButtonComponent>().SpriteImageHover;
                    };

                    onHoverEnd = () =>
                    {
                        GameObject.GetComponent<Sprite>().sprite = GameObject.GetComponent<ButtonComponent>().SpriteImageUnpressed;
                    };
                }
            }

            this.AnchorLocation = anchorLocation;
            this.Scale = scale;
            
            GameObject = new(new Transform(this.Position, this.Rotation, this.Size), parent, tag);
            
            Setup(toggleModeActive, onPress, onRelease, onHover, onHoverEnd);
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
            Action onHover = null,
            Action onHoverEnd = null,
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

                if (onHover == null)
                {
                    onHover = () =>
                    {
                        GameObject.GetComponent<Sprite>().sprite = GameObject.GetComponent<ButtonComponent>().SpriteImageHover;
                    };

                    onHoverEnd = () =>
                    {
                        GameObject.GetComponent<Sprite>().sprite = GameObject.GetComponent<ButtonComponent>().SpriteImageUnpressed;
                    };
                }
            }

            this.AnchorLocation = anchorLocation;
            this.Scale = scale;

            GameObject = new(new Transform(this.Position, this.Rotation, this.Size, true), parent, tag);

            Setup(toggleModeActive, onPress, onRelease, onHover, onHoverEnd);
        }


        private void Setup(bool toggleModeActive, Action onPress, Action onRelease, Action onHover, Action onHoverEnd)
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
                this.anchor = new(this.AnchorLocation);
                GameObject.Add(anchor);


                GameObject.GetComponent<Transform>().position += anchor.GetAnchorPoint(GameObject);
            }

            if (!this.Scale.Equals(ScaleSize.None))
            {
                this.scale = new(this.Scale);
                GameObject.Add(scale);

                GameObject.GetComponent<Transform>().scale *= scale.GetScaleModifier(GameObject);
            }


            ButtonComponent button = new(BackgroundTexture, PressedBackgroundTexture, HoverBackgroundTexture, toggleModeActive, onPress, onRelease, onHover, onHoverEnd);

            GameObject.Add(button);


            RectangleCollider collider = new RectangleCollider(this.GameObject.GetComponent<Transform>().scale * this.GameObject.GetComponent<Sprite>()?.size ?? Vector2.One, false);
            collider.LayersToCollideWith = CollisionLayer.UI;
            collider.IsHud = true;
            GameObject.Add(collider);

            GameObject.Add(new Rigidbody());
        }

        // This should be called if the parent changes
        public void AdjustLocation()
        {
            GameObject.GetComponent<Transform>().position = Position;

            if (!this.AnchorLocation.Equals(AnchorLocation.None))
            {
                GameObject.GetComponent<Transform>().position += anchor.GetAnchorPoint(GameObject);
            }

            if (!this.Scale.Equals(ScaleSize.None))
            {
                GameObject.GetComponent<Transform>().scale *= scale.GetScaleModifier(GameObject);
            }
        }

    }
}
