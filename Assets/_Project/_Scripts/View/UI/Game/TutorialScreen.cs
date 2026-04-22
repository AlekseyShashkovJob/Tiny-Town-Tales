using System;
using UnityEngine;
using View.Button;

namespace View.UI.Game
{
    public class TutorialScreen : UIScreen
    {
        [SerializeField] private CustomButton _start;

        private Action _onComplete;

        public void Show(Action onComplete)
        {
            _onComplete = onComplete;
            StartScreen();
        }

        private void OnEnable()
        {
            _start.AddListener(OnStartPressed);
        }

        private void OnDisable()
        {
            _start.RemoveListener(OnStartPressed);
        }

        private void OnStartPressed()
        {
            _onComplete?.Invoke();
        }
    }
}