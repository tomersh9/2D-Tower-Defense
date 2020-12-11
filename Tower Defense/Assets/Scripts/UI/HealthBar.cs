using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class HealthBar : MonoBehaviour {
        private Slider _healthSlider;
        
        private void Awake() {
            _healthSlider = GetComponent<Slider>();
        }

        public void SetMaxHealthValue(int maxHealth) {
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = maxHealth;
        }

        public void SetHealthBarValue(int health) {
            _healthSlider.value = health;
        }
    }
}