using UnityEngine;
using TMPro;
using View.Button;

namespace View.UI.Menu
{
    public class ShopScreen : UIScreen
    {
        [SerializeField] private CustomButton _back;
        [SerializeField] private TMP_Text _coinsText;

        private void OnEnable()
        {
            _back.AddListener(BackToMenu);

            int totalCoins = PlayerPrefs.GetInt(
                GameCore.Constants.GameConstants.TOTAL_COINS_KEY, 0);
            _coinsText.text = $"{totalCoins}";
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