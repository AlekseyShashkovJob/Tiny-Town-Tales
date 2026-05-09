using GameCore.Coin;
using GameCore.Constants;
using GameCore.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI.Menu
{
    public class PetSkinManager : MonoBehaviour
    {
        [Header("Pet Visual")]
        [SerializeField] private Image _petImage;

        [Header("All Skins")]
        [SerializeField] private SkinData[] _allSkins;

        private CoinPool _coinPool;

        private void Awake()
        {
            _coinPool = GetComponent<CoinPool>();
        }

        private void Start()
        {
            LoadSavedSkin();
        }

        public void LoadSavedSkin()
        {
            string selectedId = PlayerPrefs.GetString(
                GameConstants.SKIN_SELECTED_KEY,
                GameConstants.DEFAULT_SKIN_ID);

            SkinData skin = FindSkinById(selectedId);

            if (skin != null)
                ApplySkin(skin);
        }

        public void ApplySkin(SkinData skinData)
        {
            if (skinData == null)
                return;

            // Применяем спрайт города
            if (skinData.PetSprite != null)
                _petImage.sprite = skinData.PetSprite;

            // Применяем цвет слитка
            if (_coinPool != null)
                _coinPool.SetCoinColor(skinData.IngotColor);
        }

        private SkinData FindSkinById(string id)
        {
            foreach (SkinData skin in _allSkins)
            {
                if (skin.SkinId == id)
                    return skin;
            }

            return null;
        }
    }
}