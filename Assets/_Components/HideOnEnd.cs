using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnEnd : MonoBehaviour
{
    private void OnEnable() {
        GameManager.e_OnGameEnd += Hide;
    }

    private void OnDisable() {
        GameManager.e_OnGameEnd -= Hide;
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
