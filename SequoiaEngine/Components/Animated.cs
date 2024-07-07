using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequoiaEngine
{
    public class Animated : Component
    {

        public Action OnStart;
        public Action OnEnd;
        public Action<float> OnUpdate;

        public bool IsStarted;
        public bool IsFinished;

        public float AnimationTime;
        public float MaxAnimationTime;



        public Animated(float animationTime, Action onStart, Action<float> onUpdate, Action onEnd = null) 
        {
            this.AnimationTime = animationTime;
            this.MaxAnimationTime = animationTime;
            this.OnStart = onStart;
            this.OnStart += OnBegin;


            if (onEnd == null)
            {
                this.OnEnd = OnFinish;
            }
            else
            {
                this.OnEnd = onEnd;
                this.OnEnd += OnFinish;
            }

            this.OnUpdate = onUpdate;
            this.OnUpdate += Update;
            this.IsStarted = false;
            this.IsFinished = false;
        }

        public void OnBegin()
        {
            this.IsStarted = true;
        }

        public void OnFinish()
        {
            this.IsFinished = true;
        }

        public void Restart()
        {
            this.IsFinished = false;
            this.IsStarted = false;

            this.OnStart?.Invoke();
        }


        public void Update(float deltaTime)
        {
            this.AnimationTime -= deltaTime;

            if (this.AnimationTime < 0)
            {
                this.OnEnd?.Invoke();
            }
        }

    }
}
