using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomButton : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Asset References")]
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _pressedSprite;
    [SerializeField] private Sprite _disabledSprite;

    public UnityEvent OnClick = new UnityEvent();
    public bool Interactable;

    public void Update() {
        if (Interactable) {
            _spriteRenderer.sprite = _activeSprite;
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (_boxCollider2D.bounds.IntersectRay(ray)) {
                    _spriteRenderer.sprite = _pressedSprite;
                }

                if (touch.phase == TouchPhase.Ended && _boxCollider2D.bounds.IntersectRay(ray)) {
                    OnClick.Invoke();
                }
            }
        } else {
            _spriteRenderer.sprite = _disabledSprite;
        }
    }
}
