using System;
using System.Collections;
using Enemy_Related;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wave_Spawning;

namespace Managers {
    public class GameManager : MonoBehaviour {
        #region Singleton

        private static GameManager _instance;

        public static GameManager GetInstance() {
            return _instance;
        }

        private void Awake() {
            if (FindObjectsOfType<GameManager>().Length > 1) {
                Destroy(gameObject);
            }
            else {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }

            //finding objects
            _enemyManager = EnemyManager.GetInstance();
            _waveSpawner = FindObjectOfType<WaveSpawner>();
            _dialogManager = FindObjectOfType<DialogManager>();
            _displayUI = GetComponent<DisplayUI>();
        }

        #endregion //singleton instace

        [Header("Player Stats")] [SerializeField]
        private int maxHealth = 15;

        [SerializeField] private int money;

        //stats
        private int _health;
        private int _money;

        //components
        private DisplayUI _displayUI;

        //managers
        private EnemyManager _enemyManager;
        private WaveSpawner _waveSpawner;
        private DialogManager _dialogManager;

        private float _gameSpeed;

        //Events Subscriptions
        private void OnEnable() {
            _enemyManager.OnEnemyKilledEvent += AddToMoney;
            _enemyManager.OnAllEnemiesDeadEvent += WaveFinished;
            _waveSpawner.OnWaveComplete += WaveFinished;
            _waveSpawner.OnAllWavesComplete += WinLevel;
        }

        private void OnDisable() {
            _enemyManager.OnEnemyKilledEvent -= AddToMoney;
            _enemyManager.OnAllEnemiesDeadEvent -= WaveFinished;
            _waveSpawner.OnWaveComplete -= WaveFinished;
            _waveSpawner.OnAllWavesComplete -= WinLevel;
        }

        // Start is called before the first frame update
        private void Start() {
            _gameSpeed = 1f;
            _money = money;
            _health = maxHealth;
            SetupUI();
        }

        private void SetupUI() {
            _displayUI.UpdateHealthUI(_health);
            _displayUI.UpdateMoneyUI(_money);
            _displayUI.UpdateMaxWaveCountUI(_waveSpawner.GetMaxWaveCount(), _waveSpawner.GetCurrentWaveIndex());
            _displayUI.UpdateCurrentWaveCountUI(1); //start with first wave always
        }

        private void Update() {
            ManageKeyboardInput();
        }

        private void ManageKeyboardInput() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                OnPlayButtonPress();
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                OnPausePress();
            }
        }

        //called from button or key code
        public void OnPausePress() => _dialogManager.TogglePauseLabel(_gameSpeed);

        //called also from UI button press
        public void OnPlayButtonPress() => _waveSpawner.SpawnCurrentWave();

        public void TakeDamage(int dmg) {
            _health -= dmg;
            if (_health <= 0) {
                _health = 0;
                AudioManager.GetInstance().PlayPlayerDeathSfx();
                LooseLevel();
            }

            _displayUI.UpdateHealthUI(_health);
            AudioManager.GetInstance().PlayPlayerHitSfx();
        }

        public void AddToMoney(int amount) {
            _money += amount;
            _displayUI.UpdateMoneyUI(_money);
        }

        public bool SpendMoney(int amount) {
            if (amount > _money) {
                return false;
            }

            _money -= amount;
            _displayUI.UpdateMoneyUI(_money);
            return true;
        }

        public int GetCurrentMoney() => _money;

        public void SetCurrentWaveIndex(int currWaveIndex) {
            _displayUI.UpdateCurrentWaveCountUI(currWaveIndex);
        }

        //when button press
        public void NextLevel() {
            CleanLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(gameObject);
        }

        public void ExitToMainMenu() {
            CleanLevel();
            FindObjectOfType<LevelLoader>().LoadMainMenuScene();
            Destroy(gameObject);
        }

        //called from button click
        public void RestartLevel() {
            FindObjectOfType<LevelLoader>().LoadCurrentScene();
            CleanLevel();
            Destroy(gameObject);
        }

        public void SetGameSpeed(float speed) {
            _gameSpeed = speed;
            Time.timeScale = speed;
        }

        private void WaveFinished() {
            if (_waveSpawner.GetCurrentState() == SpawnState.Waiting && _enemyManager.GetEnemyCount() == 0) {
                ActivateWaveFinishDialog();
            }
        }

        private void ActivateWaveFinishDialog() {
            AddToMoney(150); //each wave finish grants player with money
            _dialogManager.ActivateWaveFinishLabel();
        }

        private void WinLevel() {
            _gameSpeed = 0;
            SetGameSpeed(_gameSpeed);
            _dialogManager.ActivateWinLabel();
        }

        private void LooseLevel() {
            _gameSpeed = 0;
            SetGameSpeed(_gameSpeed);
            _dialogManager.ActivateLooseLabel();
        }

        private void CleanLevel() {
            _health = maxHealth;
            _money = money;
            _enemyManager.ClearEnemiesList();
            BuildManager.GetInstance().ClearMemory();
            _gameSpeed = 1f;
            SetGameSpeed(_gameSpeed);
        }
    }
}