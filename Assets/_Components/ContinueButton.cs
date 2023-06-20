using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public static ContinueButton s_instance;
    [SerializeField] private CustomButton _button;

    public enum ContinueState {
        WaitingForRoll,
        WaitingForPlacement,
        WaitingForContinue,
    }
    public ContinueState CurrentContinueState;
    public bool ContinueButtonPressed;
    private bool _ready;

    private void Start() {
        s_instance = this;
        _button.Interactable = false;
        CurrentContinueState = ContinueState.WaitingForPlacement;
    }

    private void OnEnable() {
        TilePool.e_OnSpecialDrawn += OnSpecialDrawn;
    }

    private void OnDisable() {
        TilePool.e_OnSpecialDrawn -= OnSpecialDrawn;
    }

    private void Update() {
        switch(CurrentContinueState) {
            case ContinueState.WaitingForRoll:
                if (DiceManager.s_instance.Rolled) {
                    CurrentContinueState = ContinueState.WaitingForPlacement;
                }
                break;
            case ContinueState.WaitingForPlacement:
                if (BoardManager.s_instance.NoHighlightTiles() && SpecialManager.s_instance.ReadyToContinue) {
                    CurrentContinueState = ContinueState.WaitingForContinue;
                    _button.Interactable = true;
                }
                break;
            case ContinueState.WaitingForContinue:
                if (ContinueButtonPressed) {
                    CurrentContinueState = ContinueState.WaitingForRoll;
                    ContinueButtonPressed = false;
                    _button.Interactable = false;
                }
                break;
        }
    }

    public void SetContinueButtonPressed() {
        ContinueButtonPressed = true;
    }

    public void OnSpecialDrawn(int index) {
        DiceManager.s_instance.DisableRoll();
        CurrentContinueState = ContinueState.WaitingForPlacement;
        SpecialManager.s_instance.SpecialMenus[index].ReadyToContinue = false;
        _button.Interactable = false;
    }

    public void OnUndo() {
        CurrentContinueState = ContinueState.WaitingForPlacement;
        _button.Interactable = false;
        // SpecialManager.s_instance.ReadyToContinue is set within TileManager.cs. This is sketchy.
    }
}