using UnityEngine;
using TMPro;
using View.Button;
using GameCore.Constants;

namespace View.UI.Menu
{
    public class ShopScreen : UIScreen
    {
        [SerializeField] private CustomButton _back;
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private ShopItem[] _shopItems;
        [SerializeField] private PetSkinManager _petSkinManager;

        private void OnEnable()
        {
            _back.AddListener(BackToMenu);

            EnsureDefaultSkinOwned();

            foreach (ShopItem item in _shopItems)
            {
                item.Initialise();
                item.Subscribe();
                item.OnPurchased += HandlePurchased;
                item.OnSelected += HandleSelected;
            }

            RefreshCoinsDisplay();
        }

        private void OnDisable()
        {
            _back.RemoveListener(BackToMenu);

            foreach (ShopItem item in _shopItems)
            {
                item.Unsubscribe();
                item.OnPurchased -= HandlePurchased;
                item.OnSelected -= HandleSelected;
            }
        }

        private void EnsureDefaultSkinOwned()
        {
            string purchasedKey = GameConstants.SKIN_PURCHASED_PREFIX
                                + GameConstants.DEFAULT_SKIN_ID;

            if (PlayerPrefs.GetInt(purchasedKey, 0) == 1)
                return;

            PlayerPrefs.SetInt(purchasedKey, 1);

            if (!PlayerPrefs.HasKey(GameConstants.SKIN_SELECTED_KEY))
            {
                PlayerPrefs.SetString(
                    GameConstants.SKIN_SELECTED_KEY,
                    GameConstants.DEFAULT_SKIN_ID);
            }

            PlayerPrefs.Save();
        }

        private void HandlePurchased(ShopItem purchasedItem)
        {
            RefreshCoinsDisplay();
        }

        private void HandleSelected(ShopItem selectedItem)
        {
            // Deselect every other item
            foreach (ShopItem item in _shopItems)
            {
                if (item != selectedItem)
                    item.Deselect();
            }

            // Apply skin to pet
            if (_petSkinManager != null)
                _petSkinManager.ApplySkin(selectedItem.SkinData);
        }

        private void RefreshCoinsDisplay()
        {
            int totalCoins = PlayerPrefs.GetInt(
                GameConstants.TOTAL_COINS_KEY, 0);
            _coinsText.text = $"{totalCoins}";
        }

        private void BackToMenu()
        {
            CloseScreen();
        }
    }
}