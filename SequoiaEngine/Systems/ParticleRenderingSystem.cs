using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Particles;
using MonoGame.Extended.TextureAtlases;


namespace SequoiaEngine
{
    public class ParticleRenderingSystem : System
    {

        public ParticleRenderingSystem(SystemManager systemManager) : base(systemManager, typeof(Particle))
        {
            systemManager.UpdateSystem -= Update; // remove the automatically added update
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach ((uint id, GameObject gameObject) in gameObjects)
            {
                spriteBatch.Draw(gameObject.GetComponent<Particle>().Effect);
            }
        }

        protected override void Update(GameTime gameTime)
        {
        }
    }
}
