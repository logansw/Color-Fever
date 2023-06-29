using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] private CustomButton[] _buttons;
    [SerializeField] private GameObject _blackdrop;
    private bool _buttonsShown;

    private void Start() {
        _buttonsShown = false;
    }

    public void Toggle() {
        if (_buttonsShown) {
            Hide();
        } else {
            Show();
        }
    }

    public void Show() {
        foreach (CustomButton button in _buttons) {
            button.gameObject.SetActive(true);
        }
        _buttonsShown = true;
        _blackdrop.SetActive(true);
    }

    public void Hide() {
        foreach (CustomButton button in _buttons) {
            button.gameObject.SetActive(false);
        }
        _buttonsShown = false;
        _blackdrop.SetActive(false);
    }
}
