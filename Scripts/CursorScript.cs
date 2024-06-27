using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using SequoiaEngine;
using System;
using System.Diagnostics;

namespace Biosphere
{
    public class CursorScript : Script
    {
        private Transform transform;
        private Sprite sprite;
        private MouseInput mouse;


        private float cameraSpeed = 600;


        public CursorScript(GameObject gameObject) : base(gameObject)
        {

        }

        public override void Start()
        {
            transform = gameObject.GetComponent<Transform>();
            sprite = gameObject.GetComponent<Sprite>();
            mouse = gameObject.GetComponent<MouseInput>();

            mouse.OnMouseMove += OnMouseMove;

            transform.position = mouse.Position;
        }

        public void OnMouseMove()
        {
            transform.position = mouse.Position * new Vector2(480, 270) / new Vector2(1280, 720);
        }


        public override void Update(GameTime gameTime)
        {
        }
    }
}