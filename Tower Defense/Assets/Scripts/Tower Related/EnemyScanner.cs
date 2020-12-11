using System;
using System.Collections.Generic;
using Enemy_Related;
using UnityEngine;

namespace Tower_Related {
    public class EnemyScanner : MonoBehaviour {
        [SerializeField] private float scanRange = 5f;

        private EnemyManager _enemyManager;
        private Transform _target;

        private void Awake() {
            _enemyManager = EnemyManager.GetInstance();
        }

        private void Start() {
            //visible scan range
            float radiusScale = scanRange * 2;
            transform.localScale = new Vector2(radiusScale, radiusScale);
        }

        //set first enemy we see as target until dead / out of range
        public void ScanEnemiesInRange() {
            //check against each enemy with those vars
            Transform targetEnemy = null;

            List<Enemy> enemies = _enemyManager.GetEnemiesList();

            foreach (Enemy enemy in enemies) {
                float currDistanceFromEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                //check if still no target assigned and the current distance
                if (targetEnemy == null && currDistanceFromEnemy <= scanRange) {
                    targetEnemy = enemy.transform; //first enemy as target
                }
            }

            _target = targetEnemy; //can be null or not
        }

        public bool IsTargetFound() {
            return _target != null;
        }

        public Transform GetTarget() {
            return _target;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, scanRange);
        }
    }
}