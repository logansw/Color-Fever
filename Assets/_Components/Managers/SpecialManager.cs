using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialManager : MonoBehaviour
{
    public static SpecialManager s_instance;
    public SelectionMode CurrentSelectionMode;
    public bool ReadyToContinue;
    public enum SelectionMode {
        Normal,
        Corner,
        MoveA, MoveB,
        SwapA, SwapB,
        Remove,
    }
    public Tile SelectedTile;

    public delegate void OnCornerModeSet(int index);
    public static OnCornerModeSet e_OnCornerModeSet;
    public delegate void OnMoveModeSet(int index);
    public static OnMoveModeSet e_OnMoveModeSet;
    public delegate void OnSwapModeSet(int index);
    public static OnSwapModeSet e_OnSwapModeSet;
    public delegate void OnRemoveModeSet(int index);
    public static OnRemoveModeSet e_OnRemoveModeSet;
    public delegate void OnMoveModeBegun(int index);
    public static OnMoveModeBegun e_OnMoveModeBegun;
    public delegate void OnNormalModeSet(int index);
    public static OnNormalModeSet e_OnNormalModeSet;

    private void Awake() {
        s_instance = this;
    }

    public void Initialize() {
        CurrentSelectionMode = SelectionMode.Normal;
        ReadyToContinue = true;
    }

    public void SetCornerMode(int index) {
        CurrentSelectionMode = SelectionMode.Corner;
        e_OnCornerModeSet?.Invoke(index);
    }

    public void SetMoveMode(int index) {
        CurrentSelectionMode = SelectionMode.MoveA;
        e_OnMoveModeSet?.Invoke(index);
    }

    public void SetSwapMode(int index) {
        CurrentSelectionMode = SelectionMode.SwapA;
        e_OnSwapModeSet?.Invoke(index);
    }

    public void InvokeMoveModeBegun(int index) {
        e_OnMoveModeBegun?.Invoke(index);
    }

    public void SetRemoveMode(int index) {
        CurrentSelectionMode = SelectionMode.Remove;
        e_OnRemoveModeSet?.Invoke(index);
    }

    public void SetNormalMode(int index) {
        CurrentSelectionMode = SelectionMode.Normal;
        e_OnNormalModeSet?.Invoke(index);
        ReadyToContinue = true;
        TileManager.s_instance.DisableSelectedTile();
    }

    public void Pass() {
        ReadyToContinue = true;
    }
}