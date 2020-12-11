using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class SpeedButton : MonoBehaviour {
        [SerializeField] private TMP_Text speedTv;

        private Button _speedBtn;
        private bool _isPressed;
        private float _gameSpeed;

        private void Awake() {
            _isPressed = false;
            _gameSpeed = 1f;
            Time.timeScale = _gameSpeed;
            _speedBtn = GetComponent<Button>();
            _speedBtn.onClick.AddListener(OnSpeedBtnClick);
        }

        private void Start() {
            speedTv.text = "X" + _gameSpeed; //default speed
        }

        private void OnSpeedBtnClick() {
            _isPressed = !_isPressed;
            _gameSpeed = _isPressed ? 2f : 1f;
            GameManager.GetInstance().SetGameSpeed(_gameSpeed);
            speedTv.text = "X" + _gameSpeed;
        }
    }
}