﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SequoiaEngine;

namespace Biosphere
{
    public static class CursorPrefab
    {
        public static GameObject Create(Vector2 position, Vector2 size)
        {
            GameObject gameObject = new(new Transform(position, 0, Vector2.One));

            gameObject.Add(new Rigidbody());
            gameObject.Add(new Sprite(ResourceManager.Get<Texture2D>("cursor"), Color.White));
            gameObject.Add(new RectangleCollider(size * gameObject.GetComponent<Sprite>().size, false));
            gameObject.Add(new CursorScript(gameObject));
            gameObject.Add(new MouseInput());

            return gameObject;

        }
    }
}