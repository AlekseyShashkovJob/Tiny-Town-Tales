using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameCore.Constants;
using GameCore.Shop;
using View.Button;

namespace View.UI.Menu
{
    public class ShopItem : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private SkinData _skinData;

        [Header("UI References")]
        [SerializeField] private CustomButton _actionButton;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TMP_Text _priceText;

        [Header("Button Sprites")]
        [SerializeField] private Sprite _buySprite;
        [SerializeField] private Sprite _boughtSprite;

        [Header("Background Sprites")]
        [SerializeField] private Sprite _defaultBackground;
        [SerializeField] private Sprite _selectedBackground;

        public event Action<ShopItem> OnPurchased;
        public event Action<ShopItem> OnSelected;

        public SkinData SkinData => _skinData;
        public bool IsPurchased { get; private set; }
        public bool IsSelected { get; private set; }


        public void Initialise()
        {
            IsPurchased = PlayerPrefs.GetInt(
                GameConstants.SKIN_PURCHASED_PREFIX + _skinData.SkinId, 0) == 1;

            string selectedId = PlayerPrefs.GetString(
                GameConstants.SKIN_SELECTED_KEY, GameConstants.DEFAULT_SKIN_ID);

            IsSelected = selectedId == _skinData.SkinId;

            RefreshVisuals();
        }

        public void Subscribe() => _actionButton.AddListener(HandleClick);
        public void Unsubscribe() => _actionButton.RemoveListener(HandleClick);

        private void HandleClick()
        {
            if (!IsPurchased)
            {
                TryPurchase();
                return;
            }

            if (!IsSelected)
            {
                Select();
            }
        }

        private void TryPurchase()
        {
            int totalCoins = PlayerPrefs.GetInt(GameConstants.TOTAL_COINS_KEY, 0);

            if (totalCoins < _skinData.Price)
                return;

            totalCoins -= _skinData.Price;
            PlayerPrefs.SetInt(GameConstants.TOTAL_COINS_KEY, totalCoins);

            IsPurchased = true;
            PlayerPrefs.SetInt(
                GameConstants.SKIN_PURCHASED_PREFIX + _skinData.SkinId, 1);

            PlayerPrefs.Save();

            RefreshVisuals();

            OnPurchased?.Invoke(this);

            Select();
        }

        public void Select()
        {
            IsSelected = true;

            PlayerPrefs.SetString(
                GameConstants.SKIN_SELECTED_KEY, _skinData.SkinId);
            PlayerPrefs.Save();

            RefreshVisuals();

            OnSelected?.Invoke(this);
        }

        public void Deselect()
        {
            IsSelected = false;
            RefreshVisuals();
        }

        private void RefreshVisuals()
        {
            _priceText.text = IsPurchased ? "0" : _skinData.Price.ToString();

            _buttonImage.sprite = IsPurchased ? _boughtSprite : _buySprite;

            _backgroundImage.sprite = IsSelected
                ? _selectedBackground
                : _defaultBackground;
        }
    }
}

