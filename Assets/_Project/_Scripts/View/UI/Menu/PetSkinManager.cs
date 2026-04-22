using UnityEngine;
using UnityEngine.UI;
using GameCore.Constants;
using GameCore.Shop;

namespace View.UI.Menu
{
    public class PetSkinManager : MonoBehaviour
    {
        [Header("Pet Visual")]
        [SerializeField] private Image _petImage;

        [Header("All Skins (same order is not required)")]
        [SerializeField] private SkinData[] _allSkins;

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
            if (skinData == null || skinData.PetSprite == null)
                return;

            _petImage.sprite = skinData.PetSprite;
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

        private void Start()
        {
            LoadSavedSkin();
        }
    }
}

