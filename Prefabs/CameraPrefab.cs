using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using SequoiaEngine;
using System;
using System.Diagnostics;

namespace Biosphere
{
    public static class CameraPrefab
    {
        public static GameObject Create()
        {
            GameObject camera = new GameObject(new Transform(Vector2.Zero, 0, Vector2.One));

            float maxZoomBounds = 2.5f;
            float minZoomBounds = 1f;

            float zoomSpeed = 0.25f;
            float movementAmount = 200f;


            float maxCameraX = 500f;
            float minCameraX = -500f;
            float maxCameraY = 500f;
            float minCameraY = -500f;



            camera.Add(new Rigidbody());
            //camera.Add(new CircleCollider(1, false));


            KeyboardInput keyboardInput = new KeyboardInput();
            keyboardInput.RegisterOnHeldAction("moveCameraUp", () =>
            {
            if (GameManager.Instance.Camera.Position.Y >= minCameraY)
            {
                    Vector2 movement = new Vector2(0f, -movementAmount) * GameManager.Instance.ElapsedMicroseconds;
                    movement.Round();

                    GameManager.Instance.Camera.Move(movement);
                    camera.GetComponent<Transform>().position = GameManager.Instance.Camera.Position;
                }
                
            });

            keyboardInput.RegisterOnHeldAction("moveCameraDown", () =>
            {
                if (GameManager.Instance.Camera.Position.Y <= maxCameraY)
                {
                    Vector2 movement = new Vector2(0f, movementAmount) * GameManager.Instance.ElapsedMicroseconds;
                    movement.Round();


                    GameManager.Instance.Camera.Move(movement);
                    camera.GetComponent<Transform>().position = GameManager.Instance.Camera.Position;
                }

            });

            keyboardInput.RegisterOnHeldAction("moveCameraLeft", () =>
            {
                Console.WriteLine(GameManager.Instance.Camera.Position.ToString());
                if (GameManager.Instance.Camera.Position.X >= minCameraX)
                {
                    Vector2 movement = new Vector2(-movementAmount, 0f) * GameManager.Instance.ElapsedMicroseconds;
                    movement.Round();
                    GameManager.Instance.Camera.Move(movement);
                    camera.GetComponent<Transform>().position = GameManager.Instance.Camera.Position;
                }
            });


            keyboardInput.RegisterOnHeldAction("moveCameraRight", () =>
            {
                if (GameManager.Instance.Camera.Position.X <= maxCameraX)
                {
                    Vector2 movement = new Vector2(movementAmount, 0f) * GameManager.Instance.ElapsedMicroseconds;
                    movement.Round();
                    GameManager.Instance.Camera.Move(movement);
                    camera.GetComponent<Transform>().position = GameManager.Instance.Camera.Position;
                }
            });

            


            keyboardInput.DefaultBindings.Add("moveCameraUp", Keys.W);
            keyboardInput.DefaultBindings.Add("moveCameraDown", Keys.S);
            keyboardInput.DefaultBindings.Add("moveCameraRight", Keys.D);
            keyboardInput.DefaultBindings.Add("moveCameraLeft", Keys.A);

            MouseInput mouseInput = new MouseInput();
            mouseInput.RegisterOnPressAction("zoomOut", () =>
            {
                float zoomOutAmount = zoomSpeed;
                GameManager.Instance.Camera.MaximumZoom = maxZoomBounds;
                GameManager.Instance.Camera.MinimumZoom = minZoomBounds;
                GameManager.Instance.Camera.ZoomOut(zoomSpeed);
            });

            mouseInput.RegisterOnPressAction("zoomIn", () =>
            {
                GameManager.Instance.Camera.MaximumZoom = maxZoomBounds;
                GameManager.Instance.Camera.MinimumZoom = minZoomBounds;
                GameManager.Instance.Camera.ZoomIn(zoomSpeed);
            });

            mouseInput.RegisterOnHeldAction("zoomOut", () =>
            {
                GameManager.Instance.Camera.ZoomOut(zoomSpeed * GameManager.Instance.ElapsedMicroseconds);
            });

            mouseInput.RegisterOnHeldAction("zoomIn", () =>
            {
                GameManager.Instance.Camera.ZoomIn(zoomSpeed * GameManager.Instance.ElapsedMicroseconds);

            });

            

            mouseInput.DefaultBindings.Add("zoomOut", MouseButton.ScrollWheelDown);
            mouseInput.DefaultBindings.Add("zoomIn", MouseButton.ScrollWheelUp);




            camera.Add(new CameraScript(camera));
            camera.Add(keyboardInput);
            camera.Add(mouseInput);


            return camera;
        }
    }
}