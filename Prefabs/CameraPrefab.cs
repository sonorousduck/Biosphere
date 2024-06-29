using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using SequoiaEngine;
using System;

namespace Biosphere
{
    public static class CameraPrefab
    {
        public static GameObject Create()
        {
            GameObject camera = new GameObject();
            

            camera.Add(new Transform(Vector2.Zero, 0, Vector2.One));
            camera.Add(new Rigidbody());
            camera.Add(new CircleCollider(1, false));
            camera.Add(new MouseInput());

            float movementAmount = 100f;

            KeyboardInput keyboardInput = new KeyboardInput();
            keyboardInput.RegisterOnHeldAction("moveCameraUp", () =>
            {
                camera.GetComponent<Transform>().position += new Vector2(0f, movementAmount) * GameManager.Instance.ElapsedMicroseconds;
                GameManager.Instance.Camera.Move(new Vector2(0f, -movementAmount) * GameManager.Instance.ElapsedMicroseconds);
            });

            keyboardInput.RegisterOnHeldAction("moveCameraDown", () =>
            {
                camera.GetComponent<Transform>().position += new Vector2(0f, -movementAmount) * GameManager.Instance.ElapsedMicroseconds;
                GameManager.Instance.Camera.Move(new Vector2(0f, movementAmount) * GameManager.Instance.ElapsedMicroseconds);

            });

            keyboardInput.RegisterOnHeldAction("moveCameraLeft", () =>
            {
                camera.GetComponent<Transform>().position += new Vector2(-movementAmount, 0f) * GameManager.Instance.ElapsedMicroseconds;
                GameManager.Instance.Camera.Move(new Vector2(-movementAmount, 0f) * GameManager.Instance.ElapsedMicroseconds);

            });

            keyboardInput.RegisterOnHeldAction("moveCameraRight", () =>
            {
                camera.GetComponent<Transform>().position += new Vector2(movementAmount, 0f) * GameManager.Instance.ElapsedMicroseconds;
                GameManager.Instance.Camera.Move(new Vector2(movementAmount, 0f) * GameManager.Instance.ElapsedMicroseconds);
            });

            keyboardInput.DefaultBindings.Add("moveCameraUp", Keys.W);
            keyboardInput.DefaultBindings.Add("moveCameraDown", Keys.S);
            keyboardInput.DefaultBindings.Add("moveCameraRight", Keys.D);
            keyboardInput.DefaultBindings.Add("moveCameraLeft", Keys.A);


            camera.Add(new CameraScript(camera));
            camera.Add(keyboardInput);


            return camera;
        }
    }
}