using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuidingText : MonoBehaviour {
    [SerializeField] private TMP_Text _text;
    public List<int> Indexes;

    private void OnEnable() {
        GameManager.e_OnGameStart += OnGameStart;
    }

    private void OnDisable() {
        GameManager.e_OnGameStart -= OnGameStart;
    }

    public void SetText(int index, string text) {
        foreach (int i in Indexes) {
            if (i == index) {
                _text.text = text;
            }
        }
    }

    private void OnGameStart() {
        _text.text = "Place starting tiles along the bottom row";
    }

    public void ClearText() {
        _text.text = "";
    }
}