using System;
using Enemy_Related;
using UnityEngine;

namespace Tower_Related {
    public class Projectile : MonoBehaviour {
        
        [SerializeField] private float speed = 2f;
        [SerializeField] private GameObject hitEffect;

        private Rigidbody2D _rb;
        private DamageDealer _damageDealer;
        private Transform _target;

        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _damageDealer = GetComponent<DamageDealer>();
        }

        private void FixedUpdate() {
            MoveProjectile();
        }
        
        public void SetTarget(Transform target) => _target = target;

        private void MoveProjectile() {
            if (_target != null) {
                transform.LookAt(_target.position);
                _rb.velocity = transform.forward * speed;
                Vector2 dir = _target.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(-25, 0, angle);
            }
            else {
                Destroy(gameObject);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision) {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(_damageDealer.GetDamage());
                if (hitEffect != null) {
                    Instantiate(hitEffect, enemy.transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
}