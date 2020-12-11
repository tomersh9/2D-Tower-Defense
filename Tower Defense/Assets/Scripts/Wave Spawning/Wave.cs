using System.Collections.Generic;
using UnityEngine;

namespace Wave_Spawning {
    public class Wave
    {
        private List<SubWave> _subWaves;
        private float _spawnRate;
        private float _timeBetweenSubWaves;

        //Normal Wave
        public Wave(List<SubWave> subWaves, float spawnRate, float timeBetweenSubWaves) {
            _subWaves = subWaves;
            _spawnRate = spawnRate;
            _timeBetweenSubWaves = timeBetweenSubWaves;
        }

        //Boss Wave
        public Wave(SubWave bossSubWave) {
            _subWaves = new List<SubWave>();
            _subWaves.Add(bossSubWave);
            _spawnRate = 0;
            _timeBetweenSubWaves = 1;
        }

        public float GetSpawnRate()
        {
            return _spawnRate;
        }

        public List<SubWave> GetSubWaves()
        {
            return _subWaves;
        }

        public float GetTimeBetweenSubWaves()
        {
            return _timeBetweenSubWaves;
        }
    }
}


