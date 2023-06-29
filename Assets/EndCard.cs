using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCard : MonoBehaviour
{
    [SerializeField] private GameObject[] _endCardObjects;

    private void OnEnable() {
        GameManager.e_OnGameEnd += Show;
    }

    private void OnDisable() {
        GameManager.e_OnGameEnd -= Show;
    }

    public void Show() {
        foreach (GameObject obj in _endCardObjects) {
            obj.SetActive(true);
        }
    }

    public void Hide() {
        foreach (GameObject obj in _endCardObjects) {
            obj.SetActive(false);
        }
    }
}
