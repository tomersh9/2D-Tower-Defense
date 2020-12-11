using UnityEngine;

namespace Tower_Related {
    public class Weapon : MonoBehaviour {
        [SerializeField] private float fireRate = 2f;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;

        private float _fireRateCounter = 0;

        private void Update() {
            
            if (_fireRateCounter <= 0) {
                _fireRateCounter = fireRate;
            }
            _fireRateCounter -= Time.deltaTime;
        }

        public void Shoot(Transform target) {
            if (_fireRateCounter <= 0) {
                GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                Projectile projectile = projectileGO.GetComponent<Projectile>();
                if (projectile != null) {
                    projectile.SetTarget(target);
                }
            }
        }
    }
}