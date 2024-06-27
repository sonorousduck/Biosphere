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

        }


        public override void Update(GameTime gameTime)
        {
            transform.position = InputManager.Instance.MousePositionState.Position * new Vector2(480, 270) / new Vector2(1280, 720);
        }
    }
}