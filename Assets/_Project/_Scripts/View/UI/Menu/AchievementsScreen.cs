using GameCore.Achievements;
using UnityEngine;
using View.Button;

namespace View.UI.Menu
{
    public class AchievementsScreen : UIScreen
    {
        [SerializeField] private CustomButton _back;
        [SerializeField] private AchievementItem[] _achievementItems;

        private void OnEnable()
        {
            _back.AddListener(BackToMenu);

            AchievementManager.CheckAllAchievements();

            foreach (AchievementItem item in _achievementItems)
            {
                item.Refresh();
            }
        }

        private void OnDisable()
        {
            _back.RemoveListener(BackToMenu);
        }

        private void BackToMenu()
        {
            CloseScreen();
        }
    }
}