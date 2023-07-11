using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitConfirmation : MonoBehaviour
{
    [SerializeField] private GameObject _quitConfirmationPopup;

    public void ClosePopup() {
        _quitConfirmationPopup.SetActive(false);
    }

    public void ShowPopup() {
        _quitConfirmationPopup.SetActive(true);
    }
}
