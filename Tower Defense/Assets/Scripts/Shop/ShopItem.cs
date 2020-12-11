using System;
using Managers;
using TMPro;
using Tower_Related;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Shop {
    public class ShopItem : MonoBehaviour {
        
        [SerializeField] private GameObject towerPrefab;

        //Scripts refs
        private Shop _shop;
        private Tower _tower;
        
        //UI
        private TextMeshProUGUI _costTv;
        private Image _image;

        //states
        private bool _canBuy = false;
        private bool _isHover = false;

        void Awake() {
            _image = GetComponent<Image>();
            _costTv = GetComponentInChildren<TextMeshProUGUI>();
            _tower = towerPrefab.GetComponent<Tower>();
        }

        // Start is called before the first frame update
        void Start() {
            _shop = Shop.GetInstance();
            _costTv.text = _tower.GetCost().ToString();
        }

        //**Called from Event Trigger in Unity**//
        public void OnTowerClicked() {
            if (_canBuy) {
                AudioManager.GetInstance().PlayTowerSelectedSfx();
                _shop.SelectTowerToBuild(towerPrefab);
                transform.localScale = new Vector2(1f, 1f);
            }
        }

        public void OnTowerHover() {
            if (_canBuy) {
                transform.localScale = new Vector2(1.2f, 1.2f);
            }
            AudioManager.GetInstance().PlayTowerHoverSfx();
            _shop.SetTowerDisplayName(towerPrefab.name);
            _isHover = true;
        }

        public void OnTowerExit() {
            transform.localScale = new Vector2(1f, 1f);
            _isHover = false;
            _shop.SetTowerDisplayName("");
        }

        public int GetShopItemCost() => _tower.GetCost();

        public void SetCanBuy(bool canBuy) => _canBuy = canBuy;

        public void SetHighlightColor() {
            if (!_isHover) {
                _image.color = Color.white;
            }
        }

        public void SetUnhighlightColor() {
            if (!_isHover) {
                _image.color = Color.black;
            }
        }
    }
}