using System;
using Microsoft.Xna.Framework;

namespace SequoiaEngine
{
    public class SpritesheetAnimationSystem : System
    {
        public SpritesheetAnimationSystem(SystemManager systemManager) : base(systemManager, typeof(AnimationController))
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (uint id in gameObjects.Keys)
            {
                if (!gameObjects[id].IsEnabled()) continue;

                AnimationController animatedSprite = gameObjects[id].GetComponent<AnimationController>();


                foreach (AnimatedSprite animation in animatedSprite.AnimationTree.Tree[animatedSprite.CurrentNode].Animations)
                {
                    animation.UpdateElapsedTime(gameTime, gameObjects[id]);
                }


                foreach (Link link in animatedSprite.AnimationTree.Tree[animatedSprite.CurrentNode].Links)
                {
                    if (link.ShouldTraverseLink.Invoke())
                    {
                        foreach (AnimatedSprite animation in animatedSprite.AnimationTree.Tree[animatedSprite.CurrentNode].Animations)
                        {
                            animation.ResetAnimation();
                        }
                    }
                    animatedSprite.CurrentNode = link.ChildNode;
                    break;
                }





                /*animatedSprite.currentTime += gameTime.ElapsedGameTime;

                while (animatedSprite.currentTime.Milliseconds > animatedSprite.frameTiming[animatedSprite.currentFrame])
                {
                    animatedSprite.currentTime = animatedSprite.currentTime.Subtract(TimeSpan.FromMilliseconds(animatedSprite.frameTiming[animatedSprite.currentFrame]));

                    *//*                    if (animatedSprite.playOnce && animatedSprite.currentFrame == animatedSprite.endFrame)
                                        {
                                            animatedSprite.completedPlay = true;
                                        }*//*

                    if (animatedSprite.currentFrame != animatedSprite.endFrame || !animatedSprite.playOnce)
                    {
                        if (animatedSprite.callbacks != null && animatedSprite.callbacks.ContainsKey(animatedSprite.currentFrame))
                        {
                            // Run the callback
                            animatedSprite.callbackToRun.Enqueue(animatedSprite.callbacks[animatedSprite.currentFrame]);
                        }

                        animatedSprite.currentFrame += 1;
                    }
                    else if (animatedSprite.currentFrame == animatedSprite.endFrame)
                    {
                        if (animatedSprite.callbacks != null && animatedSprite.callbacks.ContainsKey(animatedSprite.currentFrame))
                        {
                            // Run the callback
                            animatedSprite.callbackToRun.Enqueue(animatedSprite.callbacks[animatedSprite.currentFrame]);
                        }
                    }

                    if (animatedSprite.endFrame != 0 && !animatedSprite.playOnce)
                    {
                        if (animatedSprite.currentFrame > animatedSprite.endFrame)
                        {
                            animatedSprite.currentFrame = animatedSprite.startFrame;
                        }
                    }
                    else if (animatedSprite.endFrame == 0)
                    {
                        animatedSprite.currentFrame %= animatedSprite.frameTiming.Length;
                    }*/
                    /*                    else if (animatedSprite.playOnce)
                                        {
                                            if (animatedSprite.currentFrame == animatedSprite.endFrame)
                                            {
                                                animatedSprite.completedPlay = true;
                                            }
                                        }*/

                //}
            }
        }
    }
}