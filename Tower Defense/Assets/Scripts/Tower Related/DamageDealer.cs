using UnityEngine;

namespace Tower_Related {
    public class DamageDealer : MonoBehaviour {
        [SerializeField] private int damage = 5;

        public int GetDamage() => damage;
    }
}