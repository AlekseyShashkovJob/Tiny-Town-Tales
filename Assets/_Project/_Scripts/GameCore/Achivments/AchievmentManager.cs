using GameCore.Constants;
using UnityEngine;

namespace GameCore.Achievements
{
    public static class AchievementManager
    {
        public static bool IsUnlocked(string achievementId)
        {
            return PlayerPrefs.GetInt(
                GameConstants.ACHIEVEMENT_PREFIX + achievementId, 0) == 1;
        }

        public static void Unlock(string achievementId)
        {
            if (IsUnlocked(achievementId))
                return;

            PlayerPrefs.SetInt(
                GameConstants.ACHIEVEMENT_PREFIX + achievementId, 1);
            PlayerPrefs.Save();
        }

        public static void CheckAllAchievements()
        {
            CheckLevelAchievement();
            CheckGoldAchievement();
            CheckSkinsAchievement();
        }

        public static void CheckLevelAchievement()
        {
            int lastUnlockedLevel = PlayerPrefs.GetInt(
                GameConstants.LAST_UNLOCKED_LEVEL_KEY, 0);

            // lastUnlockedLevel is 0-based index, so index 4 means level 5 is unlocked
            if (lastUnlockedLevel >= 4)
            {
                Unlock(GameConstants.ACHIEVEMENT_LEVEL_5);
            }
        }

        public static void CheckGoldAchievement()
        {
            int totalCoins = PlayerPrefs.GetInt(
                GameConstants.TOTAL_COINS_KEY, 0);

            if (totalCoins >= 1_000_000_000)
            {
                Unlock(GameConstants.ACHIEVEMENT_BILLION_GOLD);
            }
        }

        public static void CheckSkinsAchievement()
        {
            int purchasedCount = 0;

            // Count all purchased skins by checking known skin IDs
            // Default skin is always owned, so we count it too
            string[] knownSkinIds = GetAllSkinIds();

            foreach (string skinId in knownSkinIds)
            {
                if (PlayerPrefs.GetInt(
                    GameConstants.SKIN_PURCHASED_PREFIX + skinId, 0) == 1)
                {
                    purchasedCount++;
                }
            }

            if (purchasedCount >= 4)
            {
                Unlock(GameConstants.ACHIEVEMENT_4_SKINS);
            }
        }

        private static string[] GetAllSkinIds()
        {
            // Return all skin IDs that exist in the game
            // This should match your SkinData ScriptableObjects
            return new string[]
            {
                "city_default",
                "city_2",
                "city_3",
                "city_4",
                "city_5",
                "city_6",
            };
        }
    }
}
