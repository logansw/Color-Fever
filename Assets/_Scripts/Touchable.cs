using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private BoxCollider2D _boxCollider2D;

    public delegate void OnTouched();
    public OnTouched e_OnTouched;
    public bool IsTouchable;

    private void Start() {
        IsTouchable = true;
    }

    private void Update() {
        if (IsTouchable && Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (_boxCollider2D.bounds.IntersectRay(ray)) {
                    e_OnTouched?.Invoke();
                }
            }
        }
    }

    public void Disable() {
        _boxCollider2D.enabled = false;
        this.enabled = false;
    }

    public void Enable() {
        _boxCollider2D.enabled = true;
        this.enabled = true;
    }
}
