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
        TileSlot.e_OnSpecialSelected += ShowMenu;
        SpecialManager.e_OnNormalModeSet += HideMenu;
    }

    private void OnDisable() {
        TileSlot.e_OnSpecialSelected -= ShowMenu;
        SpecialManager.e_OnNormalModeSet -= HideMenu;
    }

    private void ShowMenu(TileSlot tileSlot) {
        if (tileSlot != _parentTileSlot) {
            return;
        } else {
            _specialMenuObject.SetActive(true);
        }
    }

    private void HideMenu(int index) {
        if (index != Index) {
            return;
        } else {
            _specialMenuObject.SetActive(false);
        }
    }
}
