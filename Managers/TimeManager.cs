using Microsoft.Xna.Framework;
using SequoiaEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosphere
{
    public class TimeManager : SequoiaEngine.System
    {
        public static TimeManager Instance;

        public float ModifiedDeltaTime = 0;
        private float gameSpeedModifier = 1.0f;

        private float slowestGameSpeed = 0.3f;
        private float fastestGameSpeed = 20.0f;

        public TimeManager(SystemManager systemManager) : base(systemManager) { }

        protected override void Update(GameTime gameTime)
        {
            ModifiedDeltaTime = GameManager.Instance.ElapsedMicroseconds * gameSpeedModifier;
        }

        public float GetGameSpeed()
        {
            return gameSpeedModifier;
        }

        public void SetGameSpeed(float speed)
        {
            gameSpeedModifier = speed;

            gameSpeedModifier = Math.Clamp(gameSpeedModifier, slowestGameSpeed, fastestGameSpeed);
        }

        public void IncreaseGameSpeed(float increasedAmount)
        {
            gameSpeedModifier += increasedAmount;

            gameSpeedModifier = Math.Clamp(gameSpeedModifier, slowestGameSpeed, fastestGameSpeed);
        }

        public void DecreaseGameSpeed(float decreasedAmount)
        {
            gameSpeedModifier -= decreasedAmount;

            gameSpeedModifier = Math.Clamp(gameSpeedModifier, slowestGameSpeed, fastestGameSpeed);
        }

    }
}
