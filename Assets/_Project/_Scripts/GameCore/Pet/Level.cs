using UnityEngine;

namespace GameCore.Pet
{
    public class Level
    {
        public const int MAX_LEVEL = 10;

        public event System.Action OnLevelUp;

        public int Lvl { get; private set; }
        public float Experience { get; private set; }
        public float MaxExperience { get; private set; }

        public bool IsMaxLevel => Lvl >= MAX_LEVEL;

        public Level(int startLevel = 1)
        {
            Lvl = Mathf.Clamp(startLevel, 1, MAX_LEVEL);
            Experience = 0.0f;
            MaxExperience = 50.0f * Lvl;
        }

        public Level(int level, float experience, float maxExperience)
        {
            Lvl = Mathf.Clamp(level, 1, MAX_LEVEL);
            Experience = experience;
            MaxExperience = maxExperience;
        }

        public void UpExperience(float happinessCoef)
        {
            if (IsMaxLevel) return;

            Experience += Time.deltaTime * happinessCoef;

            if (Experience >= MaxExperience)
            {
                Lvl++;
                Experience -= MaxExperience;
                MaxExperience *= Lvl;
                OnLevelUp?.Invoke();

                if (IsMaxLevel)
                    Experience = MaxExperience;
            }
        }

        public void Revert()
        {
            if (Lvl <= 1) return;
            MaxExperience /= Lvl;
            Lvl--;
            Experience = 0.0f;
        }

        public void ResetExperience()
        {
            Experience = 0.0f;
            MaxExperience = 50.0f * Lvl;
        }

        public float GetExperienceCoef() =>
            IsMaxLevel ? 1.0f : Experience / MaxExperience;
    }
}