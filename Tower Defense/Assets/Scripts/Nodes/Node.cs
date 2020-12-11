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
            if (!_isPlaced) {
                TryBuildTower();
            }
        }

        private void TryBuildTower() {
            if (_buildManager.BuildTowerOn(this)) {
                _isPlaced = true; //cannot build anymore
                _nodeStyle.SetPlacedColor(); //occupied color
            }
        }

        private void TrySellTower() {
            if (_buildManager.SellTowerOn(this)) {
                _isPlaced = false;
                _nodeStyle.SetFreeColor(); //free color
            }
        }
    }
}