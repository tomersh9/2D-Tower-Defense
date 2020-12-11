using UnityEngine;

namespace Wave_Spawning {
    public class SubWave
    {
        private GameObject _enemyPrefab;
        private int _numOfEnemies;

        public SubWave(GameObject enemyPrefab, int numOfEnemies) {
            _enemyPrefab = enemyPrefab;
            _numOfEnemies = numOfEnemies;
        }

        public GameObject GetEnemyPrefab()
        {
            return _enemyPrefab;
        }

        public int GetNumOfEnemies()
        {
            return _numOfEnemies;
        }
    }
}
