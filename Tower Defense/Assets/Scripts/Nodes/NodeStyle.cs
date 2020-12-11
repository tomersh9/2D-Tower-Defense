using Managers;
using UnityEngine;

namespace Nodes {
    public class NodeStyle : MonoBehaviour {
        [SerializeField] private Sprite hoverSprite;

        private SpriteRenderer _spriteRenderer;
        private Sprite _originalSprite;
        private bool _isPlaced = false;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalSprite = _spriteRenderer.sprite;
        }

        private void OnMouseEnter() {
            if (!_isPlaced) {
                _spriteRenderer.sprite = hoverSprite;
            }
        }

        private void OnMouseExit() {
            if (!_isPlaced) {
                _spriteRenderer.sprite = _originalSprite;
            }
        }

        public void SetPlacedColor() {
            _isPlaced = true;
            _spriteRenderer.sprite = _originalSprite;
        }

        public void SetFreeColor() {
            _isPlaced = false;
            _spriteRenderer.sprite = hoverSprite;
        }
    }
}