using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using SequoiaEngine.Utilities;

namespace SequoiaEngine
{
    public class PhysicsSystem : System
    {
        /*        public static int PHYSICS_DIMENSION_WIDTH = 2000;
                public static int PHYSICS_DIMENSION_HEIGHT = 2000; // These should match up with rendering
                public static float GRAVITY = -9.81f;*/

        /*        private Quadtree quadtree;
                private Quadtree staticTree;*/


        public const float GRAVITY_CONST = -9.81f;

        private Grid grid;
        private Grid staticGrid;

        public Vector2 Dimensions;
        public Vector2 GridSize;
        public Vector2 GridStartPosition;


        public PhysicsSystem(SystemManager systemManager) : base(systemManager, typeof(Transform), typeof(Rigidbody), typeof(Collider))
        {
            //staticTree = new Quadtree(PHYSICS_DIMENSION_WIDTH, PHYSICS_DIMENSION_HEIGHT);
            Dimensions = new Vector2(2000, 2000);
            GridSize = new Vector2(16, 16);
            GridStartPosition = new Vector2(-500, -500);

            grid = new Grid(GridStartPosition, Dimensions, GridSize, false);
            staticGrid = new Grid(GridStartPosition, Dimensions, GridSize, true);
        }

        public PhysicsSystem(SystemManager systemManager, Vector2 dimensions, Vector2 gridSize) : base(systemManager, typeof(Transform), typeof(Rigidbody), typeof(Collider))
        {
            //staticTree = new Quadtree(PHYSICS_DIMENSION_WIDTH, PHYSICS_DIMENSION_HEIGHT);
            Dimensions = dimensions;
            GridSize = gridSize;
            GridStartPosition = new Vector2(-10, -10);
        }

        public PhysicsSystem(SystemManager systemManager, Vector2 dimensions, Vector2 gridSize, Vector2 gridStartPos) : base(systemManager, typeof(Transform), typeof(Rigidbody), typeof(Collider))
        {
            //staticTree = new Quadtree(PHYSICS_DIMENSION_WIDTH, PHYSICS_DIMENSION_HEIGHT);
            Dimensions = dimensions;
            GridSize = gridSize;
            GridStartPosition = gridStartPos;
        }


        protected override void Add(GameObject gameObject)
        {
            base.Add(gameObject);

            if (gameObject.ContainsComponentOfParentType<Collider>() && gameObject.GetComponent<Collider>().isStatic)
            {
                staticGrid.Insert(gameObject);
            }
            else if (gameObject.ContainsComponentOfParentType<Collider>())
            {
                grid.Insert(gameObject);
            }
        }


        /// <summary>
        /// This section updates all the rigidbody positions, and calls the Collision events from a component's script, if it has one
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (gameObjects.Count == 0) return; // i.e. we don't want to have to do the work to clear the grid everytime if we don't have to

            grid.Clear();

            foreach ((uint id, GameObject gameObject) in gameObjects)
            {
                if (!gameObject.IsEnabled()) return;

                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                Transform transform = gameObject.GetComponent<Transform>();
                Collider genericCollider = gameObject.GetComponent<Collider>();
                transform.previousPosition = transform.position;

                if (rb.usesGravity)
                {
                    rb.velocity += new Vector2(0, GRAVITY_CONST * (GameManager.Instance.ElapsedMicroseconds) * rb.gravityScale);
                }

                rb.velocity += new Vector2(rb.acceleration.X * GameManager.Instance.ElapsedMicroseconds, rb.acceleration.Y * MathF.Pow(GameManager.Instance.ElapsedMicroseconds, 2f));

                transform.position += rb.velocity * GameManager.Instance.ElapsedMicroseconds;

                while (rb.ScriptedMovementLength() > 0)
                {
                    Vector2 scriptedMovement = rb.GetNextScriptedMovement();
                    transform.position += scriptedMovement;
                }

                if (!genericCollider.isStatic)
                {
                    grid.Insert(gameObject);
                }
                else if (staticGrid.ShouldRebuild)
                {
                    staticGrid.Insert(gameObject);
                }
            }

            staticGrid.ShouldRebuild = false;


            foreach ((uint id, GameObject gameObject) in gameObjects)
            {
                if (!gameObject.IsEnabled()) return;

                UpdateGameObject(gameObject);

            }

        }


        private void UpdateGameObject(GameObject gameObject)
        {
            HashSet<GameObject> possibleCollisions = grid.GetPossibleCollisions(ref gameObject);
            possibleCollisions.UnionWith(staticGrid.GetPossibleCollisions(ref gameObject));

            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            List<uint> collisionsThisFrame = new();



            foreach (GameObject possibleCollision in possibleCollisions)
            {
                if (HasCollision(gameObject, possibleCollision))
                {
                    collisionsThisFrame.Add(possibleCollision.Id);
                    // On Collision Start
                    if (!rb.currentlyCollidingWith.Contains(possibleCollision.Id))
                    {
                        if (gameObject.ContainsComponentOfParentType<Script>())
                        {
                            gameObject.GetComponent<Script>().OnCollisionStart(possibleCollision);
                        }
                    }
                    else // On Collision
                    {
                        if (gameObject.ContainsComponentOfParentType<Script>())
                        {
                            gameObject.GetComponent<Script>().OnCollision(possibleCollision);
                        }
                    }
                }
                else // On Collision End
                {
                    if (rb.currentlyCollidingWith.Contains(possibleCollision.Id))
                    {
                        if (gameObject.ContainsComponentOfParentType<Script>())
                        {
                            gameObject.GetComponent<Script>().OnCollisionEnd(possibleCollision);
                        }
                    }
                }
            }

            rb.currentlyCollidingWith = collisionsThisFrame;

        }


        private bool HasCollision(GameObject one, GameObject two)
        {
            if (one == two)
            {
                return false;
            }
            if (one.ContainsComponent<CircleCollider>())
            {
                if (two.ContainsComponent<CircleCollider>())
                {
                    return CircleOnCircle(one, two);
                }
                else
                {
                    return CircleOnSquare(one, two);
                }
            }
            else
            {
                if (two.ContainsComponent<CircleCollider>())
                {
                    return CircleOnSquare(two, one);
                }
                else
                {
                    return SquareOnSquare(one, two);
                }
            }
        }


        private bool CircleOnCircle(GameObject circle1, GameObject circle2)
        {
            // Squared distance is less than the summed squared radius
            // TODO: IS THIS RIGHT? I DON'T THINK SO
            return (Vector2.DistanceSquared(circle1.GetComponent<Transform>().position + circle1.GetComponent<Collider>().offset, circle2.GetComponent<Transform>().position + circle2.GetComponent<Collider>().offset) < MathF.Pow(circle1.GetComponent<CircleCollider>().radius + circle2.GetComponent<CircleCollider>().radius, 2));
        }

        // Used http://jeffreythompson.org/collision-detection/circle-rect.php
        private bool CircleOnSquare(GameObject circle, GameObject square)
        {


            Transform squareTransform = square.GetComponent<Transform>();
            Transform circleTransform = circle.GetComponent<Transform>();
            RectangleCollider squareCollider = square.GetComponent<RectangleCollider>();

            Vector2 testLocation = circleTransform.position;

            if (circleTransform.position.X < squareTransform.position.X - squareCollider.size.X / 2)
            {
                testLocation.X = squareTransform.position.X - squareCollider.size.X / 2;
            }
            else if (circleTransform.position.X > squareTransform.position.X + squareCollider.size.X / 2)
            {
                testLocation.X = squareTransform.position.X + squareCollider.size.X / 2;
            }

            if (circleTransform.position.Y < squareTransform.position.Y - squareCollider.size.Y / 2)
            {
                testLocation.Y = squareTransform.position.Y - squareCollider.size.Y / 2;
            }
            else if (circleTransform.position.Y > squareTransform.position.Y + squareCollider.size.Y / 2)
            {
                testLocation.Y = squareTransform.position.Y + squareCollider.size.Y / 2;
            }

            float squaredDistance = Vector2.DistanceSquared(circleTransform.position, testLocation);

            return (squaredDistance <= MathF.Pow(circle.GetComponent<CircleCollider>().radius, 2));


        }

        private bool SquareOnSquare(GameObject square1, GameObject square2)
        {


            RectangleCollider square1Collider = square1.GetComponent<RectangleCollider>();
            RectangleCollider square2Collider = square2.GetComponent<RectangleCollider>();

            Transform square1Transform = new Transform(square1.GetComponent<Transform>().position + square1Collider.offset, square1.GetComponent<Transform>().rotation, square1.GetComponent<Transform>().scale);
            Transform square2Transform = new Transform(square2.GetComponent<Transform>().position + square2Collider.offset, square2.GetComponent<Transform>().rotation, square2.GetComponent<Transform>().scale);

            return !(
                    square1Transform.position.X - square1Collider.size.X / 2f > square2Transform.position.X + square2Collider.size.X / 2f || // sq1 left is greater than sq2 right
                    square1Transform.position.X + square1Collider.size.X / 2f < square2Transform.position.X - square2Collider.size.X / 2f || // sq1 right is less than sq2 left
                    square1Transform.position.Y - square1Collider.size.Y / 2f > square2Transform.position.Y + square2Collider.size.Y / 2f || // sq1 top is below sq2 bottom
                    square1Transform.position.Y + square1Collider.size.Y / 2f < square2Transform.position.Y - square2Collider.size.Y / 2f // sq1 bottom is above sq1 top
                    );
        }


    }
}