using GameCore.Achievements;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI.Menu
{
    public class AchievementItem : MonoBehaviour
    {
        [SerializeField] private string _achievementId;
        [SerializeField] private Image _achievementImage;
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        public void Refresh()
        {
            bool unlocked = AchievementManager.IsUnlocked(_achievementId);
            _achievementImage.sprite = unlocked ? _onSprite : _offSprite;
        }
    }
}