﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequoiaEngine
{
    public class CollapsibleDrawer
    {
        public Button button;
        public Texture2D BackgroundTexture;
        public Vector2 Position;
        public Vector2 Size;
        public float Rotation;
        public Color SpriteColor = Color.White;
        public AnchorLocation AnchorLocation;
        public ScaleSize Scale;
        public Action OnButtonOpenPress;
        public Action OnButtonClosePress;
        public string Tag { get; private set; }

        public GameObject GameObject { get; private set; }
        public GameObject Parent { get; private set; }

        public CollapsibleDrawer(Vector2 position,
            float rotation,
            Vector2 size,
            Texture2D backgroundTexture,
            AnchorLocation anchorLocation = AnchorLocation.None,
            ScaleSize scale = ScaleSize.None,
            string openedButtonFilepath = "", 
            string closedButtonFilepath = "",
            GameObject parent = null,
            string tag = ""
            )
        {
           
            GameObject = new(new Transform(position, rotation, size));
            this.Position = position;
            this.BackgroundTexture = backgroundTexture;
            this.Size = size;
            this.AnchorLocation = anchorLocation;
            this.Scale = scale;
            this.Rotation = rotation;
            this.Parent = parent;
            this.Tag = tag;


            Setup();

            OnButtonOpenPress = () =>
            {
                Debug.WriteLine("OpenPressed!");
            };

            OnButtonClosePress = () => { Debug.WriteLine("ClosedPressed!"); };



            button = new Button(new Vector2(10.5f, 33.5f), 0f, Vector2.One, backgroundTextureName: closedButtonFilepath, onPress: OnButtonOpenPress, anchorLocation: AnchorLocation.TopRight, parent: GameObject);
        }


        public void Setup()
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

        }

        public void AddSubcomponentsToSystemManager(SystemManager systemManager)
        {
            systemManager.Add(button.GameObject);
        }


    }
}