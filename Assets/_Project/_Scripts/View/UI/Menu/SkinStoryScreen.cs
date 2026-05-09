using GameCore.Shop;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.Button;
using View.UI;

namespace View.UI.Menu
{
    public class SkinStoryScreen : UIScreen
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _storyText;
        [SerializeField] private Image _skinPreviewImage;
        [SerializeField] private CustomButton _okButton;

        [Header("Settings")]
        [SerializeField] private float _typeSpeed = 0.03f;

        private Coroutine _typingCoroutine;
        private Action _onComplete;

        private void Awake()
        {
            if (_okButton != null)
                _okButton.AddListener(OnOkPressed);
        }

        private void OnDestroy()
        {
            if (_okButton != null)
                _okButton.RemoveListener(OnOkPressed);
        }

        public void ShowSkinStory(SkinData skinData, Action onComplete = null)
        {
            _onComplete = onComplete;

            gameObject.SetActive(true);

            // Настраиваем UI
            _titleText.text = skinData.SkinName;
            _storyText.text = "";

            if (_skinPreviewImage != null && skinData.PetSprite != null)
            {
                _skinPreviewImage.sprite = skinData.PetSprite;
                _skinPreviewImage.gameObject.SetActive(true);
            }

            // Прячем кнопку OK до окончания набора текста
            _okButton.Interactable = false;
            _okButton.gameObject.SetActive(false);

            // Запускаем эффект печати
            if (_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);

            _typingCoroutine = StartCoroutine(TypeRoutine(skinData.StoryText));
        }

        private IEnumerator TypeRoutine(string text)
        {
            _storyText.text = "";

            foreach (char c in text)
            {
                _storyText.text += c;
                yield return new WaitForSeconds(_typeSpeed);
            }

            // Текст полностью выведен — показываем кнопку
            _okButton.gameObject.SetActive(true);
            _okButton.Interactable = true;

            _typingCoroutine = null;
        }

        private void OnOkPressed()
        {
            _okButton.Interactable = false;

            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
                _typingCoroutine = null;
            }

            gameObject.SetActive(false);

            _onComplete?.Invoke();
            _onComplete = null;
        }
    }
}