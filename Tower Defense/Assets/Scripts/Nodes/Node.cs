using System;
using Managers;
using UnityEngine;

namespace Nodes {
    public class Node : MonoBehaviour {
        private BuildManager _buildManager;
        private NodeStyle _nodeStyle;

        private bool _isPlaced = false;

        private void Start() {
            _nodeStyle = GetComponent<NodeStyle>();
            _buildManager = BuildManager.GetInstance();
        }

        private void OnMouseDown() {
            _buildManager.SetNodeSelected(this); //set this Node as selected first

            if (!_isPlaced) {
                TryBuildTower();
            }
            else {
                //pop sell dialog
                _buildManager.ToggleTowerDialogOn();
            }
        }

        private void TryBuildTower() {
            if (_buildManager.BuildTowerOn()) {
                _isPlaced = true; //cannot build anymore
                _nodeStyle.SetPlacedColor(); //occupied color
            }
        }

        public void ClearNode() {
            _isPlaced = false;
            _nodeStyle.SetFreeColor(); //free color
        }
    }
}