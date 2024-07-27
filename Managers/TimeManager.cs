using Microsoft.Xna.Framework;
using SequoiaEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosphere
{
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }


    public class TimeManager : SequoiaEngine.System
    {
        public static TimeManager Instance;

        public float ModifiedDeltaTime = 0;
        public TimeSpan TotalElapsedTime; // TODO: Eventually, this will need to be saved and loaded
        public DateTime CurrentDate = new DateTime();
        public Season CurrentSeason;
        private float gameSpeedModifier = 1.0f;
        private float baseSpeedModifier = 1000000.0f;

        private float slowestGameSpeed = 0.3f;
        private float fastestGameSpeed = 20.0f;

        public TimeManager(SystemManager systemManager) : base(systemManager) 
        {
            Instance = this;

        }

        protected override void Update(GameTime gameTime)
        {
            ModifiedDeltaTime = GameManager.Instance.ElapsedSeconds * gameSpeedModifier;
            TimeSpan deltaTimeSpan = TimeSpan.FromMilliseconds(ModifiedDeltaTime * baseSpeedModifier * 1000f); // Convert from microseconds to milliseconds
            CurrentDate += deltaTimeSpan;

            if (CurrentDate.Month >= 3 && CurrentDate.Month < 6)
            {
                CurrentSeason = Season.Spring;
            }
            else if (CurrentDate.Month >= 6 && CurrentDate.Month < 9)
            {
                CurrentSeason = Season.Summer;
            }
            else if (CurrentDate.Month >= 9 && CurrentDate.Month < 12)
            {
                CurrentSeason = Season.Fall;
            }
            else
            {
                CurrentSeason = Season.Winter;
            }

            TotalElapsedTime += deltaTimeSpan;
        }

        public float GetGameSpeed()
        {
            return gameSpeedModifier;
        }

        public void SetGameSpeed(float speed)
        {
            gameSpeedModifier = speed;

            gameSpeedModifier = Math.Clamp(gameSpeedModifier, slowestGameSpeed, fastestGameSpeed);
            Debug.WriteLine(gameSpeedModifier);

        }

        public void IncreaseGameSpeed(float increasedAmount)
        {
            gameSpeedModifier += increasedAmount;

            gameSpeedModifier = Math.Clamp(gameSpeedModifier, slowestGameSpeed, fastestGameSpeed);
            Debug.WriteLine(gameSpeedModifier);
        }

        public void DecreaseGameSpeed(float decreasedAmount)
        {
            gameSpeedModifier -= decreasedAmount;

            gameSpeedModifier = Math.Clamp(gameSpeedModifier, slowestGameSpeed, fastestGameSpeed);
        }

    }
}
