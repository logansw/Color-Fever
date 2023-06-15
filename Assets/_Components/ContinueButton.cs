using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public static ContinueButton s_instance;
    [SerializeField] private Button _button;


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
        _button.interactable = false;
        CurrentContinueState = ContinueState.WaitingForPlacement;
    }

    private void OnEnable() {
        // DiceManager.e_OnDiceRoll += CheckAdvanceState;
        // TileManager.e_OnTilePlaced += CheckAdvanceState;
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
                    _button.interactable = true;
                }
                break;
            case ContinueState.WaitingForContinue:
                if (ContinueButtonPressed) {
                    CurrentContinueState = ContinueState.WaitingForRoll;
                    ContinueButtonPressed = false;
                    _button.interactable = false;
                }
                break;
        }
    }

    public void SetContinueButtonPressed() {
        ContinueButtonPressed = true;
    }

    // private void Update() {
    //     if (_ready) {
    //         switch (CurrentContinueState) {
    //             case ContinueState.WaitingForRoll:
    //                 CurrentContinueState = ContinueState.WaitingForPlacement;
    //                 break;
    //             case ContinueState.WaitingForPlacement:
    //                 SetWaitForContinue();
    //                 break;
    //             case ContinueState.WaitingForContinue:
    //                 CurrentContinueState = ContinueState.WaitingForRoll;
    //                 _button.interactable = false;
    //                 break;
    //         }

    //         _ready = false;
    //     }

    //     if (CurrentContinueState == ContinueState.WaitingForPlacement) {
    //         CheckAdvanceState();
    //     }
    // }

    // public void CheckAdvanceState() {
    //     switch (CurrentContinueState) {
    //         case ContinueState.WaitingForRoll:
    //             _ready = true;
    //             break;
    //         case ContinueState.WaitingForPlacement:
    //             _ready = BoardManager.s_instance.NoHighlightTiles() && SpecialManager.s_instance.ReadyToContinue;
    //             break;
    //         case ContinueState.WaitingForContinue:
    //             _ready = true;
    //             break;
    //     }
    // }

    // public void SetWaitForContinue() {
    //     CurrentContinueState = ContinueState.WaitingForContinue;
    //     _button.interactable = true;
    // }

    public void OnSpecialDrawn(int index) {
        DiceManager.s_instance.DisableRoll();
        CurrentContinueState = ContinueState.WaitingForPlacement;
        SpecialManager.s_instance.ReadyToContinue = false;
        _button.interactable = false;
    }
}