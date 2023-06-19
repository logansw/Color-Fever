using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMenu : MonoBehaviour
{
    [SerializeField] private TileSlot _parentTileSlot;
    [SerializeField] private GameObject _specialMenuObject;
    public int Index;

    private void Start() {
        _specialMenuObject.SetActive(false);
    }

    private void OnEnable() {
        TilePool.e_OnSpecialDrawn += ShowMenu;
    }

    private void OnDisable() {
        TilePool.e_OnSpecialDrawn -= ShowMenu;
    }

    public void ShowMenu(int index) {
        if (index != Index) {
            return;
        } else {
            _specialMenuObject.SetActive(true);
        }
    }

    public void HideMenu(int index) {
        if (index != Index) {
            return;
        } else {
            _specialMenuObject.SetActive(false);
        }
    }
}
