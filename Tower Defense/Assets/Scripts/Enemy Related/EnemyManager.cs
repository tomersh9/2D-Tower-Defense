using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Enemy_Related {
    public class EnemyManager {
        
        //EVENTS
        public delegate void EnemyKilledDelegate(int amount);
        public event EnemyKilledDelegate OnEnemyKilledEvent;
        
        public delegate void AllEnemiesDeadDelegate();
        public event AllEnemiesDeadDelegate OnAllEnemiesDeadEvent;

        private List<Enemy> enemies;

        private static EnemyManager _instance;

        public static EnemyManager GetInstance() //singleton
        {
            if (_instance == null) {
                _instance = new EnemyManager();
            }

            return _instance;
        }

        private EnemyManager() => enemies = new List<Enemy>(); //constructor

        public void AddEnemy(Enemy enemy) => enemies.Add(enemy);

        public void RemoveEnemy(Enemy enemy) {
            enemies.Remove(enemy);
            OnEnemyKilledEvent?.Invoke(enemy.GetPoints());
            if (enemies.Count == 0) {
                OnAllEnemiesDeadEvent?.Invoke();
            }
        }

        public List<Enemy> GetEnemiesList() => enemies;

        public int GetEnemyCount() => enemies.Count;

        public void ClearEnemiesList() => enemies.Clear();
    }
}