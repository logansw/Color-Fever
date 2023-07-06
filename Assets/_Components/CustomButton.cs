using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomButton : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioSource _buttonAudioSource;
    [SerializeField] private AudioSource _undoAudioSource;
    [SerializeField] private AudioSource _diceAudioSource;

    [Header("Asset References")]
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _pressedSprite;
    [SerializeField] private Sprite _disabledSprite;

    public UnityEvent OnClick = new UnityEvent();
    public bool Interactable;
    public Bounds Bounds {
        get {
            return _boxCollider2D.bounds;
        }
    }

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
                    PlayButtonSound();
                }
            }
        } else {
            _spriteRenderer.sprite = _disabledSprite;
        }
    }

    public void SetSprites(Sprite active, Sprite pressed, Sprite disabled) {
        _activeSprite = active;
        _pressedSprite = pressed;
        _disabledSprite = disabled;
    }

    public void HideSelf() {
        _spriteRenderer.enabled = false;
        _boxCollider2D.enabled = false;
    }

    public void PlayButtonSound() {
        _buttonAudioSource.Play();
    }

    public void PlayUndoSound() {
        _undoAudioSource.Play();
    }

    public void PlayDiceSound() {
        _diceAudioSource.Play();
    }
}
