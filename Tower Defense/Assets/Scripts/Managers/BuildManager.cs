using System;
using Nodes;
using Tower_Related;
using UnityEngine;

namespace Managers {
    public class BuildManager : MonoBehaviour {
        #region Singleton

        private static BuildManager _instance;

        public static BuildManager GetInstance() {
            return _instance;
        }

        private void Awake() {
            if (FindObjectsOfType<BuildManager>().Length > 1) {
                Destroy(gameObject);
            }
            else {
                DontDestroyOnLoad(gameObject);
                _instance = this;
            }
        }

        #endregion

        [SerializeField] private bool rememberSelection; //for debugging purposes

        private GameObject _towerToBuild;
        private float offset = 0.5f; //to center sprite in square
        
        private MouseItem _mouseItem;
        private GameManager _gameManager;

        private void Start() {
            _mouseItem = GetComponent<MouseItem>();
            _gameManager = GameManager.GetInstance();
        }
        
        public void DeselectTowerToManage() {
            _mouseItem.ReleaseTowerFromMouse();
            _towerToBuild = null;
        }

        //gets called from the Shop Script (from the UI Buttons)
        public void SetTowerToManage(GameObject towerGO) {
            //can drag 1 item at a time
            _mouseItem.ReleaseTowerFromMouse();
            _towerToBuild = towerGO;
            _mouseItem.LockTowerToMouse(towerGO); //hover mode true
        }

        //called from Node Script - return if build success or not
        public bool BuildTowerOn(Node node) {
            if (_towerToBuild != null) {
                int towerCost = _towerToBuild.GetComponent<Tower>().GetCost();
                if (_gameManager.SpendMoney(towerCost)) {
                    SpawnTower(node);
                    AudioManager.GetInstance().PlayTowerDownSfx();
                    return true;
                }
            }

            return false;
        }

        public bool SellTowerOn(Node node) {
            Tower currTower = node.GetComponentInChildren<Tower>();
            if (currTower != null) {
                _gameManager.AddToMoney((int) (currTower.GetCost() / 1.5f));
                currTower.RemoveTower(); //destroy gameObject
                _towerToBuild = null;
                AudioManager.GetInstance().PlayTowerSoldSfx();
                return true;
            }

            return false;
        }

        //called form GM
        public void ClearMemory() {
            Destroy(gameObject);
        }

        private void SpawnTower(Node node) {
            _towerToBuild.GetComponent<Tower>().HoverMode(false); //tower can attack when placed
            _mouseItem.ReleaseTowerFromMouse(); //destroy preview
            Vector2 wantedPos = node.transform.position;
            wantedPos.y += offset; //adjust to sit right on square
            GameObject towerGO = Instantiate(_towerToBuild, wantedPos, Quaternion.identity);
            towerGO.transform.parent = node.transform; //making the tower child of Node so we can delete it later on
            if (!rememberSelection) {
                _towerToBuild = null;
            }
        }
    }
}