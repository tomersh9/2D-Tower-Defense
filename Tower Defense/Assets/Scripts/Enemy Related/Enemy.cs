using System;
using System.Collections;
using Managers;
using Tower_Related;
using UI;
using UnityEngine;

namespace Enemy_Related {
    public class Enemy : MonoBehaviour {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private int points = 50;
        [SerializeField] private GameObject bloodEffectPrefab;

        private int _health;

        private EnemyManager _enemyManager;
        private HealthBar _healthBar;
        private SpriteRenderer _spriteRenderer;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _enemyManager = EnemyManager.GetInstance();
        }
        
        private void Start() {
            SetUpHealthBar();
            _enemyManager.AddEnemy(this); //enemy spawned
        }

        private void SetUpHealthBar() {
            _health = maxHealth;
            _healthBar = GetComponentInChildren<HealthBar>();
            _healthBar.SetMaxHealthValue(_health);
        }
        
        //getters setters
        public int GetPoints() => points;
        public float GetMoveSpeed() => moveSpeed;

        public void TakeDamage(int dmg) {
            _health -= dmg;
            _healthBar.SetHealthBarValue(_health);
            if (_health <= 0) {
                Die();
            }
            else {
                AudioManager.GetInstance().PlayEnemyHitSfx();
                StartCoroutine(FlashDamage());
            }
        }

        public void Freeze() {
            StartCoroutine(SlowDown());
        }

        private IEnumerator SlowDown() {
            float originalSpeed = moveSpeed;
            moveSpeed = 0.5f;
            yield return new WaitForSeconds(1f);
            moveSpeed = originalSpeed;
        }

        private IEnumerator FlashDamage() {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSecondsRealtime(0.1f);
            _spriteRenderer.color = Color.white;
        }

        public void Die() {
            GameObject deathEffectGO = Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
            AudioManager.GetInstance().PlayEnemyDeathSfx();
            _enemyManager.RemoveEnemy(this);
            Destroy(deathEffectGO, 0.5f);
            Destroy(gameObject);
        }
    }
}