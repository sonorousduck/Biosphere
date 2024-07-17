using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using SequoiaEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Biosphere
{
    public class CursorScript : Script
    {
        private Transform transform;
        private Sprite sprite;
        private MouseInput mouse;

        private List<GameObject> collidingWithGame;
        private List<GameObject> collidingWithUI;


        private float cameraSpeed = 600;


        public CursorScript(GameObject gameObject) : base(gameObject)
        {

        }

        public override void Start()
        {
            transform = gameObject.GetComponent<Transform>();
            sprite = gameObject.GetComponent<Sprite>();
            mouse = gameObject.GetComponent<MouseInput>();
            collidingWithGame = new List<GameObject>();
            collidingWithUI = new List<GameObject>();

        }


        public override void Update(GameTime gameTime)
        {
            transform.position = GameManager.Instance.Camera.ScreenToWorld(InputManager.Instance.MousePositionState.Position);

            //transform.position = camera.ScreenToWorld(test);
        }

        public override void OnCollisionStart(GameObject other)
        {
            base.OnCollisionStart(other);

            if (other != null)
            {
                RectangleCollider collider = other.GetComponent<RectangleCollider>();
                Transform colliderTransform = other.GetComponent<Transform>();
                collider.IsColliding = true;

                if (!colliderTransform.IsHUD)
                {
                    collidingWithGame.Add(other);
                }
                else
                {
                    collidingWithUI.Add(other);

                    if (other.TryGetComponent(out Hoverable hoverable))
                    {
                        hoverable.OnHoverStart?.Invoke();
                    }

                }
            }
        }

        public override void OnCollisionEnd(GameObject other)
        {
            base.OnCollisionEnd(other);

            if (other != null)
            {
                RectangleCollider collider = other.GetComponent<RectangleCollider>();
                Transform colliderTransform = other.GetComponent<Transform>();

                collider.IsColliding = false;
                
                if (!colliderTransform.IsHUD)
                {
                    collidingWithGame.Remove(other);
                }
                else
                {
                    collidingWithUI.Remove(other);

                    if (other.TryGetComponent(out Hoverable hoverable))
                    {
                        hoverable.OnHoverEnd?.Invoke();
                    }
                }
            }
        }


        public void OnClick()
        {
            if (collidingWithUI.Count > 0)
            {
                if (collidingWithUI[0].TryGetComponent(out ButtonComponent button))
                {
                    button.OnPress?.Invoke();
                }
            }
        }

    }
}