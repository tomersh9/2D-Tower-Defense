using System;
using System.Collections.Generic;
using Managers;
using Tower_Related;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Shop {
    public class Shop : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI towerDisplayNameTv;

        private GameManager _gameManager;
        private BuildManager _buildManager;
        private ShopItem[] _items;

        private static Shop _instance;
        public static Shop GetInstance() => _instance;

        private void Awake() {
            if (FindObjectsOfType<Shop>().Length > 1) {
                Destroy(gameObject);
            }
            else {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            _items = GetComponentsInChildren<ShopItem>();
        }

        private void Start() {
            _gameManager = GameManager.GetInstance();
            _buildManager = BuildManager.GetInstance();
            towerDisplayNameTv.text = "";
        }

        private void Update() {
            UpdateItemsBuyState();
        }

        private void UpdateItemsBuyState() {
            foreach (ShopItem item in _items) {
                if (item.GetShopItemCost() <= _gameManager.GetCurrentMoney()) {
                    item.SetHighlightColor();
                    item.SetCanBuy(true);
                }
                else {
                    item.SetUnhighlightColor();
                    item.SetCanBuy(false);
                }
            }
        }

        public void SelectTowerToBuild(GameObject towerGO) { //called from a button press
            _buildManager.SetTowerToManage(towerGO);
        }

        public void SetTowerDisplayName(string towerName) {
            towerDisplayNameTv.text = towerName;
        }
    }
}