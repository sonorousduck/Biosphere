using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SequoiaEngine;


namespace SequoiaEngine
{
    public class AnimatedSprite
    {
        // The texture from which to take the frame data

        public Dictionary<string, SpriteAnimation> Animations { get; private set; } = new();
        public string CurrentAnimation = "none";
        public TextureAtlas TextureAtlas { get; private set; }

        public int CurrentFrame { get => Animations[CurrentAnimation].CurrentFrame; }

        public AnimatedSprite(TextureAtlas atlas)
        {
            this.TextureAtlas = atlas;
        }

        public void ResetAnimation()
        {
            Animations[CurrentAnimation].ResetAnimation();
        }

        public void UpdateElapsedTime(GameTime gameTime, GameObject go)
        {
            Animations[CurrentAnimation].UpdateElapsedTime(gameTime, go);
        }

        /// <summary>
        /// Create an animation that you can then add frames to inside the spritesheet
        /// </summary>
        /// <param name="animationName">Name that you can get and use for switching to (Like .play("run")</param>
        /// <returns></returns>
        public SpriteAnimation CreateAnimation(string animationName)
        {
            Animations.Add(animationName, new SpriteAnimation());

            return Animations[animationName];
        }

        public void Play(string animationName)
        {
            if (!Animations.ContainsKey(animationName))
            {
                Debug.WriteLine($"ERROR! {animationName} is not a valid animation!");
            }
            else
            {
                if (CurrentAnimation != "none")
                {
                    Animations[CurrentAnimation].ResetAnimation();
                }
                CurrentAnimation = animationName;
            }
        }

        

       /* public Texture2D spriteSheet { get; set; }
        public int currentFrame { get; set; }
        public int[] frameTiming { get; set; }
        public TimeSpan currentTime { get; set; }
        public Vector2 singleFrameSize { get; set; }
        public int layerDepth { get; set; }
        public int startFrame { get; set; }
        public int endFrame { get; set; }
        public bool playOnce { get; set; }
        public bool completedPlay { get; set; }
        public Dictionary<int, string> callbacks { get; set; }
        public Queue<string> callbackToRun { get; set; }
        public float renderDepth { get; set; }*/

/*        public AnimatedSprite(Texture2D spriteSheet, int[] frameTiming, Vector2 singleFrameSize, int layerDepth = 0, bool isHUD = false, int startFrame = 0, int endFrame = 0, bool playOnce = false, Dictionary<int, string> callbacks = null, float renderDepth = 0f)
        {
            this.spriteSheet = spriteSheet;
            this.currentFrame = startFrame;
            this.frameTiming = frameTiming;
            this.currentTime = new TimeSpan();
            this.singleFrameSize = singleFrameSize;
            this.layerDepth = layerDepth;
            this.startFrame = startFrame;
            this.endFrame = endFrame;
            this.playOnce = playOnce;
            this.completedPlay = false;
            this.callbacks = callbacks;
            this.callbackToRun = new Queue<string>();
            this.renderDepth = renderDepth;
        }*/
    }
}