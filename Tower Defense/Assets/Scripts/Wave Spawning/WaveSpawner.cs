using System;
using System.Collections;
using System.Collections.Generic;
using Enemy_Related;
using Managers;
using UnityEngine;

namespace Wave_Spawning {
    
    public enum SpawnState {Spawning,Waiting,Finish}
    
    public class WaveSpawner : MonoBehaviour {

        [SerializeField] private int numOfWaves;
        [SerializeField] private int startingWave; //for debug purposes
        [SerializeField] private GameObject spawnEffectPrefab;

        //EVENTS
        public delegate void AllWavesCompleteDelegate();
        public event AllWavesCompleteDelegate OnAllWavesComplete;
        public delegate void WaveCompleteDelegate();
        public event WaveCompleteDelegate OnWaveComplete;

        private WaveFactory _waveFactory;
        private EnemyManager _enemyManager;
        private GameManager _gameManager;
        private SpawnState _state;
        private int _currWaveIndex;

        private void Awake() {
            _state = SpawnState.Waiting;
            _enemyManager = EnemyManager.GetInstance(); //creates list of enemies
            _waveFactory = GetComponent<WaveFactory>();
        }

        private void Start() {
            _currWaveIndex = startingWave;
            print(_currWaveIndex);
            _gameManager = GameManager.GetInstance();
            _gameManager.SetCurrentWaveIndex(_currWaveIndex); //for UI count
        }

        private void Update() {
            //TODO some other class needs to do this!
            if (_currWaveIndex == numOfWaves && _enemyManager.GetEnemyCount() == 0 && _state == SpawnState.Waiting) {
                _state = SpawnState.Finish;
                OnAllWavesComplete?.Invoke();
            }
        }
        
        public int GetMaxWaveCount() => numOfWaves;

        public SpawnState GetCurrentState() => _state;

        public int GetCurrentWaveIndex() => _currWaveIndex;

        //check if there are waves first
        public void SpawnCurrentWave() {
            if (_state == SpawnState.Waiting && _enemyManager.GetEnemyCount() == 0) {
                if (_currWaveIndex < numOfWaves) {
                    _state = SpawnState.Spawning;
                    _gameManager.SetCurrentWaveIndex(_currWaveIndex); //for UI count
                    Wave currWave = _waveFactory.GetWave(_currWaveIndex);
                    StartCoroutine(SpawnAllSubWaves(currWave)); //spawn all sub waves
                }
            }
        }

        //spawn all enemies in the current wave and setup next wave when done
        private IEnumerator SpawnAllSubWaves(Wave currWave) {
            //Extract all sub waves of current wave
            List<SubWave> subWaves = currWave.GetSubWaves();

            for (int i = 0; i < subWaves.Count; i++) {
                SubWave currSubWave = subWaves[i];

                for (int j = 0; j < currSubWave.GetNumOfEnemies(); j++) {
                    SpawnEnemy(currSubWave);
                    yield return new WaitForSeconds(currWave.GetSpawnRate());
                }

                yield return new WaitForSeconds(currWave.GetTimeBetweenSubWaves());
            }
            //Wave Ended
            _state = SpawnState.Waiting;
            _currWaveIndex++;
            OnWaveComplete?.Invoke(); //trigger event at Game Manager
        }
        
        private void SpawnEnemy(SubWave currSubWave) {
            StartCoroutine(SpawnAnimationEffect(currSubWave));
        }

        private IEnumerator SpawnAnimationEffect(SubWave currSubWave) {
            Vector2 position = transform.position;
            Instantiate(spawnEffectPrefab, position, Quaternion.identity); //has DestroyOnExit Script when animation ends
            yield return new WaitForSeconds(0.3f);
            Instantiate(currSubWave.GetEnemyPrefab(), position, Quaternion.identity);
        }
    }
}