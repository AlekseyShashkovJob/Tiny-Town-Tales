using GameCore.Achievements;
using UnityEngine;

namespace GameCore.Repository
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private ViewController _view;
        [SerializeField] private Misc.SceneManagment.SceneLoader _sceneLoader;
        [SerializeField] private View.UI.UIScreen _gameScreen;
        [SerializeField] private Button.CustomButtonRepository _customButtonRepository;
        [SerializeField] private View.UI.Game.VictoryScreen _victoryScreen;
        [SerializeField] private View.UI.Game.LoseScreen _loseScreen;
        [SerializeField] private View.UI.Game.TutorialScreen _tutorialScreen;

        private Pet.Pet _tamagotchi;
        private Coin.CoinSpawner _coinSpawner;

        private int _selectedLevelIndex;
        private bool _isGameOver;
        private int _totalCoins;

        private void Start()
        {
            _isGameOver = false;

            _totalCoins = PlayerPrefs.GetInt(
                Constants.GameConstants.TOTAL_COINS_KEY, 0);

            _selectedLevelIndex = PlayerPrefs.GetInt(
                Constants.GameConstants.LAST_SELECTED_LEVEL_KEY, 0);

            _tamagotchi = new Pet.Pet(_selectedLevelIndex + 1);
            _tamagotchi.Level.OnLevelUp += HandleLevelUp;
            _tamagotchi.OnDeath += HandleDeath;

            _coinSpawner = _customButtonRepository.GetTamagotchiImage()
                .GetComponent<Coin.CoinSpawner>();

            InitButtonListeners();

            if (_selectedLevelIndex == 0)
            {
                ShowTutorial();
            }
            else
            {
                _gameScreen.StartScreen();
            }
        }

        private void OnDestroy()
        {
            if (_tamagotchi != null)
            {
                _tamagotchi.Level.OnLevelUp -= HandleLevelUp;
                _tamagotchi.OnDeath -= HandleDeath;
            }
        }

        private void Update()
        {
            if (_isGameOver) return;

            _tamagotchi.UpdateStates();

            _view.UpdateUI(_tamagotchi,
                _totalCoins + _coinSpawner.CoinCounter);
        }

        private void ShowTutorial()
        {
            _isGameOver = true;
            _tutorialScreen.Show(OnTutorialComplete);
        }

        public void OnTutorialComplete()
        {
            _tutorialScreen.CloseScreen();
            _gameScreen.StartScreen();
            _isGameOver = false;
        }

        private void HandleLevelUp()
        {
            _isGameOver = true;

            int sessionCoins = _coinSpawner.CoinCounter;
            CommitCoins(sessionCoins);

            int bestScore = SaveBestScore(sessionCoins);
            UnlockNextLevel();

            // Check achievements after level up and coin commit
            AchievementManager.CheckLevelAchievement();
            AchievementManager.CheckGoldAchievement();

            _victoryScreen.Show(sessionCoins, bestScore, HandleVictoryRestart);
        }

        private void HandleVictoryRestart()
        {
            _tamagotchi.Level.Revert();
            _coinSpawner.CoinCounter = 0;
            _isGameOver = false;
        }

        private void HandleDeath()
        {
            _isGameOver = true;

            int sessionCoins = _coinSpawner.CoinCounter;
            CommitCoins(sessionCoins);

            int bestScore = SaveBestScore(sessionCoins);

            // Check gold achievement after coin commit
            AchievementManager.CheckGoldAchievement();

            _loseScreen.Show(sessionCoins, bestScore, HandleLoseRestart);
        }

        private void HandleLoseRestart()
        {
            _tamagotchi.ResetForLose();
            _coinSpawner.CoinCounter = 0;
            _isGameOver = false;
        }

        private void CommitCoins(int sessionCoins)
        {
            _totalCoins += sessionCoins;
            PlayerPrefs.SetInt(
                Constants.GameConstants.TOTAL_COINS_KEY, _totalCoins);
            PlayerPrefs.Save();

            _coinSpawner.CoinCounter = 0;
        }

        private int SaveBestScore(int currentCoins)
        {
            string bestKey = Constants.GameConstants.BEST_SCORE_PREFIX
                             + _selectedLevelIndex;
            int bestScore = PlayerPrefs.GetInt(bestKey, 0);

            if (currentCoins > bestScore)
            {
                bestScore = currentCoins;
                PlayerPrefs.SetInt(bestKey, bestScore);
                PlayerPrefs.Save();
            }

            return bestScore;
        }

        private void UnlockNextLevel()
        {
            int currentUnlocked = PlayerPrefs.GetInt(
                Constants.GameConstants.LAST_UNLOCKED_LEVEL_KEY, 0);
            int nextLevelIndex = _selectedLevelIndex + 1;

            if (nextLevelIndex > currentUnlocked
                && nextLevelIndex < GameCore.Pet.Level.MAX_LEVEL)
            {
                PlayerPrefs.SetInt(
                    Constants.GameConstants.LAST_UNLOCKED_LEVEL_KEY, nextLevelIndex);
                PlayerPrefs.Save();
            }
        }

        private void InitButtonListeners()
        {
            _customButtonRepository.GetTamagotchiImage()
                .AddListener(() =>
                {
                    _coinSpawner.SpawnCoin(_tamagotchi.Level.Lvl);
                    Misc.Services.VibroManager.Vibrate();
                });

            _customButtonRepository.GetStateButtonRepository().GetHunger()
                .AddListener(() =>
                {
                    _tamagotchi.Feed();
                    Misc.Services.VibroManager.Vibrate();
                });

            _customButtonRepository.GetStateButtonRepository().GetDirty()
                .AddListener(() =>
                {
                    _tamagotchi.Wash();
                    Misc.Services.VibroManager.Vibrate();
                });

            _customButtonRepository.GetStateButtonRepository().GetFatigue()
                .AddListener(() =>
                {
                    _tamagotchi.Sleep();
                    Misc.Services.VibroManager.Vibrate();
                });

            _customButtonRepository.GetStateButtonRepository().GetSick()
                .AddListener(() =>
                {
                    _tamagotchi.Treat();
                    Misc.Services.VibroManager.Vibrate();
                });
        }
    }
}