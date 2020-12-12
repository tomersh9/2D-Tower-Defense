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

        [SerializeField] private GameObject sellCanvas;
        [SerializeField] private bool rememberSelection; //for debugging purposes

        private Node _nodeSelected;
        private GameObject _towerToBuild;
        private float _offset = 0.5f; //to center sprite in square
        private bool _isSelected = false;

        private MouseItem _mouseItem;
        private GameManager _gameManager;

        private void Start() {
            sellCanvas.SetActive(false);
            _mouseItem = GetComponent<MouseItem>();
            _gameManager = GameManager.GetInstance();
        }
        
        public void SetNodeSelected(Node node) {
            _nodeSelected = node;
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
            HideTowerCanvas();
        }

        public void ToggleTowerDialogOn() {
            _isSelected = !_isSelected;
            if (_isSelected) {
                Vector2 nodePosition = _nodeSelected.transform.position;
                nodePosition.y += 1.5f; //offset
                sellCanvas.transform.position = nodePosition;
                sellCanvas.SetActive(true);
            }
            else {
                sellCanvas.SetActive(false);
            }
        }

        //called from Node Script - return if build success or not
        public bool BuildTowerOn() {
            if (_towerToBuild != null) {
                int towerCost = _towerToBuild.GetComponent<Tower>().GetCost();
                if (_gameManager.SpendMoney(towerCost)) {
                    SpawnTower();
                    AudioManager.GetInstance().PlayTowerDownSfx();
                    return true;
                }
            }

            return false;
        }

        //called from tower ui button clicked
        public void SellTowerOn() {
            Tower currTower = _nodeSelected.GetComponentInChildren<Tower>();
            if (currTower != null) {
                _gameManager.AddToMoney((int) (currTower.GetCost() / 1.5f));
                currTower.RemoveTower(); //destroy gameObject
                _towerToBuild = null;
                AudioManager.GetInstance().PlayTowerSoldSfx();
                _nodeSelected.ClearNode();
            }
            HideTowerCanvas();
        }
        
        //called form GM
        public void ClearMemory() => Destroy(gameObject);

        private void HideTowerCanvas() {
            sellCanvas.SetActive(false);
            _isSelected = false;
        }

        private void SpawnTower() {
            _towerToBuild.GetComponent<Tower>().HoverMode(false); //tower can attack when placed
            _mouseItem.ReleaseTowerFromMouse(); //destroy preview
            Vector2 wantedPos = _nodeSelected.transform.position;
            wantedPos.y += _offset; //adjust to sit right on square
            GameObject towerGO = Instantiate(_towerToBuild, wantedPos, Quaternion.identity);
            towerGO.transform.parent = _nodeSelected.transform; //making the tower child of Node so we can delete it later on
            if (!rememberSelection) {
                _towerToBuild = null;
            }
        }
    }
}