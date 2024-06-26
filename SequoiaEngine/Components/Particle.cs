using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Containers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.TextureAtlases;


namespace SequoiaEngine
{
    public class Particle : Component
    {
        public ParticleEffect Effect;
        public Texture2D Texture;

        public Particle(Texture2D texture, ParticleEffect effect)
        {
            this.Texture = texture;
            this.Effect = effect;
        }

    }
}
