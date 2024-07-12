using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SequoiaEngine
{
    public class RenderingSystem : System
    {
        private GameObject camera;

        public static Vector2 centerOfScreen;

        public bool debugMode = false;
        public static int width = 2 * 640;
        public static int height = 2 * 360;

        /// <summary>
        /// Renderer system. It's update method is NOT part of the normal system update
        /// </summary>
        /// <param name="spriteBatch">Spritebatch that will be used</param>
        /// <param name="clientBoundsHeight">The height of the client's window, which can be found with GameWindow.ClientBounds.Height</param>
        /// <param name="camera">The camera game object, which really just has a transform position currently. Future will have scale</param>
        public RenderingSystem(SystemManager systemManager, float clientBoundsHeight, GameObject camera, Vector2 screenSize) : base(systemManager, typeof(Transform), typeof(RenderedComponent))
        {
            /*m_scalingRatio = clientBoundsHeight / PhysicsEngine.PHYSICS_DIMENSION_HEIGHT;*/
            this.camera = camera;
            centerOfScreen = screenSize / 2;
            systemManager.UpdateSystem -= Update; // remove the automatically added update

            //ResourceManager.Load<Texture2D>("Images/circle", "circle");
            //ResourceManager.Load<Texture2D>("Images/box", "box");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool isDrawingHud = false)
        {
            foreach (uint id in gameObjects.Keys)
            {
                RenderedComponent renderedComponent = gameObjects[id].GetComponent<RenderedComponent>();
                Vector2 renderPosition = gameObjects[id].GetComponent<Transform>().position;
                Vector2 intRenderPosition = new Vector2((int)renderPosition.X, (int)renderPosition.Y);
                if (renderedComponent.IsHUD != isDrawingHud) continue;

              

                if (gameObjects[id].ContainsComponent<AnimatedSprite>())
                {
                    AnimatedSprite animatedSprite = gameObjects[id].GetComponent<AnimatedSprite>();
                    Transform transform = gameObjects[id].GetComponent<Transform>();
                    int currentX = (int)(animatedSprite.currentFrame * animatedSprite.singleFrameSize.X);
                    spriteBatch.Draw(animatedSprite.spriteSheet,
                        intRenderPosition, 
                        new Rectangle(currentX, 0, 
                        (int)animatedSprite.singleFrameSize.X, 
                        (int)animatedSprite.singleFrameSize.Y), 
                        Color.White, transform.rotation, 
                        animatedSprite.singleFrameSize / 2, 
                        transform.scale, 
                        SpriteEffects.None, 
                        gameObjects[id].GetComponent<AnimatedSprite>().renderDepth
                        );
                }
                if (gameObjects[id].ContainsComponent<Sprite>())
                {
                    Sprite sprite = gameObjects[id].GetComponent<Sprite>();
                    spriteBatch.Draw(sprite.sprite,
                        intRenderPosition, 
                        null,
                        sprite.color, 
                        gameObjects[id].GetComponent<Transform>().rotation,
                        sprite.center, 
                        gameObjects[id].GetComponent<Transform>().scale,
                        SpriteEffects.None, 
                        gameObjects[id].GetComponent<Sprite>().renderDepth);
                }

                /*if (gameObjects[id].ContainsComponent<MultiAnimatedSprite>())
                {
                    MultiAnimatedSprite multiAnimatedSprite = gameObjects[id].GetComponent<MultiAnimatedSprite>();
                    foreach (AnimatedSprite animatedSprite in multiAnimatedSprite.animatedSprites)
                    {
                        Transform transform = gameObjects[id].GetComponent<Transform>();
                        renderDepth = 0;

                        int currentX = (int)(animatedSprite.currentFrame * animatedSprite.singleFrameSize.X);
                        spriteBatch.Draw(animatedSprite.spriteSheet, trueRenderPosition, new Rectangle(currentX, 0, (int)animatedSprite.singleFrameSize.X, (int)animatedSprite.singleFrameSize.Y), Color.White, transform.rotation, animatedSprite.singleFrameSize / 2, transform.scale, SpriteEffects.None, renderDepth);
                    }
                }*/


                if (debugMode)
                {
                    CircleCollider circleCollider = gameObjects[id].GetComponent<CircleCollider>();
                    RectangleCollider rectangleCollider = gameObjects[id].GetComponent<RectangleCollider>();

                    if (gameObjects[id].ContainsComponent<Text>()) // adjust the drawing area to fit where it really should be
                    {
                        renderPosition = gameObjects[id].GetComponent<Transform>().position - Vector2.Round(new Vector2(width, height) / 2) + centerOfScreen;
                    }

                    if (circleCollider != null)
                    {
                        Texture2D circleTexture = ResourceManager.Get<Texture2D>("circle");
                        Vector2 offset = gameObjects[id].GetComponent<CircleCollider>().offset;

                        spriteBatch.Draw(circleTexture, renderPosition + offset, null,
                        Color.Green, gameObjects[id].GetComponent<Transform>().rotation,
                        new Vector2(circleTexture.Width, circleTexture.Height) / 2f, 2f / circleTexture.Width * circleCollider.radius,
                        SpriteEffects.None, 0);
                    }
                    if (rectangleCollider != null)
                    {
                        Texture2D boxTexture = ResourceManager.Get<Texture2D>("box");
                        Vector2 offset = gameObjects[id].GetComponent<RectangleCollider>().offset;
                        spriteBatch.Draw(
                            boxTexture,
                            intRenderPosition + offset, 
                            null,
                            rectangleCollider.IsColliding ? Color.Green : Color.Red, 
                            gameObjects[id].GetComponent<Transform>().rotation,
                            new Vector2(boxTexture.Width, boxTexture.Height) / 2, 
                            new Vector2(2f / boxTexture.Width * (rectangleCollider.size.X / 2), 2f / boxTexture.Height * (rectangleCollider.size.Y / 2)),
                            SpriteEffects.None, 
                            0.5f);
                    }
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
        }
    }
}