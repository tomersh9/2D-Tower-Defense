using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUI : EventTrigger
{
    private Vector2 originalSize;
    private Vector2 hoverSize;
    private Vector2 clickedSize;

    private void Awake() {
        originalSize = new Vector2(1, 1);
        hoverSize = new Vector2(1.3f, 1.3f);
        clickedSize = new Vector2(1.1f, 1.1f);
    }

    public override void OnPointerEnter(PointerEventData data) {
        transform.localScale = hoverSize;
    }

    public override void OnPointerExit(PointerEventData data) {
        transform.localScale = originalSize;
    }
}
