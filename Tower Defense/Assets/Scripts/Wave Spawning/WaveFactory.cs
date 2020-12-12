using System.Collections.Generic;
using UnityEngine;

namespace Wave_Spawning {
    public class WaveFactory : MonoBehaviour {
        enum EWaveDifficulty {
            Easy,
            Medium,
            Hard
        }

        [Header("Wave Timing Configs")]
        [Range(1, 100)] [SerializeField] private int baseNumOfEnemiesPerSubWave;

        [SerializeField] private float minSpawnRate;
        [SerializeField] private float maxSpawnRate;
        [SerializeField] private float minTimeBetweenSubWaves;
        [SerializeField] private float maxTimeBetweenSubWaves;
        
        
        [Header("Enemies Configs")]
        [SerializeField] private GameObject[] easyEnemies;
        [SerializeField] private GameObject[] mediumEnemies;
        [SerializeField] private GameObject[] hardEnemies;
        [SerializeField] private GameObject[] bossPrefabs;

        private EWaveDifficulty _eWaveDifficulty;

        public Wave GetWave(int waveIndex) {
            //Normal Waves
            if (waveIndex <= 10) {
                _eWaveDifficulty = EWaveDifficulty.Easy;
            }
            if (waveIndex > 10 && waveIndex < 30) {
                _eWaveDifficulty = EWaveDifficulty.Medium;
            }
            if (waveIndex >= 30) {
                _eWaveDifficulty = EWaveDifficulty.Hard;
            }

            List<SubWave> subWaves = GenerateWave(waveIndex); //returns List<SubWaves>

            //Add 1 boss at the end of each 5th round
            if (waveIndex % 10 == 0) {
                subWaves.Add(new SubWave(bossPrefabs[Random.Range(0, bossPrefabs.Length)], 1));
            }
            
            return new Wave(subWaves, Random.Range(minSpawnRate,maxSpawnRate), Random.Range(minTimeBetweenSubWaves,maxTimeBetweenSubWaves));
        }

        private List<SubWave> GenerateWave(int waveIndex) {
            List<SubWave> subWaves = new List<SubWave>();

            switch (_eWaveDifficulty) {
                case EWaveDifficulty.Easy:
                    for (int i = 0; i < 2; i++) {
                        subWaves.Add(new SubWave(easyEnemies[Random.Range(0, easyEnemies.Length)],
                            baseNumOfEnemiesPerSubWave + waveIndex));
                    }

                    break;
                case EWaveDifficulty.Medium:
                    for (int i = 0; i < 2; i++) {
                        subWaves.Add(new SubWave(mediumEnemies[Random.Range(0, mediumEnemies.Length)],
                            baseNumOfEnemiesPerSubWave));
                    }

                    break;
                case EWaveDifficulty.Hard:
                    for (int i = 0; i < 2; i++) {
                        subWaves.Add(new SubWave(hardEnemies[Random.Range(0, hardEnemies.Length)],
                            baseNumOfEnemiesPerSubWave));
                    }

                    break;
            }

            return subWaves;
        }
    }
}