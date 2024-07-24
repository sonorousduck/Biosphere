using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequoiaEngine
{
    public class AnimationFrame
    {
        public int FrameIndex { get; private set; }
        public string Name { get; private set; }
        public float Duration { get; private set; }


        public AnimationFrame(int frameIndex, float duration, string name)
        {
            this.FrameIndex = frameIndex;
            this.Duration = duration;
            this.Name = name;
        }
    }


    public class SpriteAnimation
    {
        public List<AnimationFrame> Frames { get; private set; }

        /// <summary>
        /// Controls whether this animation should only play a single time
        /// </summary>
        public bool PlayOnce = true;

        /// <summary>
        /// Specifies if the animation has completed
        /// </summary>
        public bool HasCompletedPlay = false;

        // Maps from frame index to a function
        // Gameobject is passed in to be able to access itself
        public Dictionary<int, Action<GameObject>> Callbacks;

        /// <summary>
        /// Creates a queue of the callbacks that should run during the update phase
        /// </summary>
        public Queue<Action<GameObject>> CallbacksToRun { get; private set; }

        /// <summary>
        /// Specifies the render depth during drawing
        /// </summary>
        public float RenderDepth;


        public SpriteAnimation(Dictionary<int, Action<GameObject>> callbacks = null, bool playOnce = false, float renderDepth = 0f)
        {
            this.Callbacks = callbacks ?? new();
            this.PlayOnce = playOnce;
            this.RenderDepth = renderDepth;
        }


        public void AddFrame(int frameIndex, float duration, string name = "")
        {
            Frames.Add(new AnimationFrame(frameIndex, duration, name));
        }
    }
}
