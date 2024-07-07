using Microsoft.Xna.Framework;
using SequoiaEngine;

namespace Biosphere
{
    public class CameraScript : Script
    {
        private Transform transform;
        private MouseInput mouse;
        private Rigidbody rb;
        private GameObject followObject;

        float minX;
        float maxX;
        float minY;
        float maxY;

        private Vector2 currentDirection = new Vector2();

        private float cameraSpeed = 600;


        public CameraScript(GameObject gameObject) : base(gameObject)
        {
        }

        public void SetFollow(GameObject objectToFollow)
        {
            this.followObject = objectToFollow;
        }

        public override void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            transform = gameObject.GetComponent<Transform>();
            mouse = gameObject.GetComponent<MouseInput>();
            minX = 0;
            minY = 0;
            maxX = 640;
            maxY = 360;
        }

        public override void Update(GameTime gameTime)
        {
            if (followObject == null) return;
            transform.position = new Vector2(followObject.GetComponent<Transform>().position.X - 240, followObject.GetComponent<Transform>().position.Y - 135);
            if (transform.position.X < minX)
            {
                transform.position = new Vector2(minX, transform.position.Y);
            }
            else if (transform.position.X > maxX)
            {
                transform.position = new Vector2(maxX, transform.position.Y);
            }

            if (transform.position.Y < minY)
            {
                transform.position = new Vector2(transform.position.X, minY);
            }
            else if (transform.position.Y > maxY)
            {
                transform.position = new Vector2(transform.position.X, maxY);
            }

        }
    }
}