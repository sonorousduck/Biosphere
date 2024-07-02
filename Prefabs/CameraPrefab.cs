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
            GameObject camera = new GameObject();

            float maxZoomBounds = 5f;
            float minZoomBounds = 0.5f;

            float zoomSpeed = 2f;
            float movementAmount = 200f;


            float maxCameraX = 500f;
            float minCameraX = -500f;
            float maxCameraY = 500f;
            float minCameraY = -500f;



            camera.Add(new Transform(Vector2.Zero, 0, Vector2.One));
            camera.Add(new Rigidbody());
            camera.Add(new CircleCollider(1, false));


            KeyboardInput keyboardInput = new KeyboardInput();
            keyboardInput.RegisterOnHeldAction("moveCameraUp", () =>
            {
                if (GameManager.Instance.Camera.Position.Y >= minCameraY)
                {
                    camera.GetComponent<Transform>().position += new Vector2(0f, movementAmount) * GameManager.Instance.ElapsedMicroseconds;
                    GameManager.Instance.Camera.Move(new Vector2(0f, -movementAmount) * GameManager.Instance.ElapsedMicroseconds);
                }
                
            });

            keyboardInput.RegisterOnHeldAction("moveCameraDown", () =>
            {
                if (GameManager.Instance.Camera.Position.Y <= maxCameraY)
                {
                    camera.GetComponent<Transform>().position += new Vector2(0f, -movementAmount) * GameManager.Instance.ElapsedMicroseconds;
                    GameManager.Instance.Camera.Move(new Vector2(0f, movementAmount) * GameManager.Instance.ElapsedMicroseconds);
                }

            });

            keyboardInput.RegisterOnHeldAction("moveCameraLeft", () =>
            {
                Console.WriteLine(GameManager.Instance.Camera.Position.ToString());
                if (GameManager.Instance.Camera.Position.X >= minCameraX)
                {
                    camera.GetComponent<Transform>().position += new Vector2(-movementAmount, 0f) * GameManager.Instance.ElapsedMicroseconds;
                    GameManager.Instance.Camera.Move(new Vector2(-movementAmount, 0f) * GameManager.Instance.ElapsedMicroseconds);
                }
            });


            keyboardInput.RegisterOnHeldAction("moveCameraRight", () =>
            {
                if (GameManager.Instance.Camera.Position.X <= maxCameraX)
                {
                    camera.GetComponent<Transform>().position += new Vector2(movementAmount, 0f) * GameManager.Instance.ElapsedMicroseconds;
                    GameManager.Instance.Camera.Move(new Vector2(movementAmount, 0f) * GameManager.Instance.ElapsedMicroseconds);
                }
            });

            


            keyboardInput.DefaultBindings.Add("moveCameraUp", Keys.W);
            keyboardInput.DefaultBindings.Add("moveCameraDown", Keys.S);
            keyboardInput.DefaultBindings.Add("moveCameraRight", Keys.D);
            keyboardInput.DefaultBindings.Add("moveCameraLeft", Keys.A);


            MouseInput mouseInput = new MouseInput();
            mouseInput.RegisterOnPressAction("zoomOut", () =>
            {
                GameManager.Instance.Camera.ZoomOut(zoomSpeed * GameManager.Instance.ElapsedMicroseconds);
                GameManager.Instance.Camera.MaximumZoom = maxZoomBounds;
                GameManager.Instance.Camera.MinimumZoom = minZoomBounds;
            });

            mouseInput.RegisterOnPressAction("zoomIn", () =>
            {
                GameManager.Instance.Camera.ZoomIn(zoomSpeed * GameManager.Instance.ElapsedMicroseconds);
                GameManager.Instance.Camera.MaximumZoom = maxZoomBounds;
                GameManager.Instance.Camera.MinimumZoom = minZoomBounds;

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