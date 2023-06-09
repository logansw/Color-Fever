using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public static ContinueButton s_instance;
    [SerializeField] private Button _button;

    private enum ContinueState {
        WaitingForRoll,
        WaitingForPlacement,
        WaitingForContinue,
    }
    private ContinueState _continueState;
    private bool _ready;

    private void Start() {
        s_instance = this;
        _button.interactable = false;
        _continueState = ContinueState.WaitingForPlacement;
    }

    private void OnEnable() {
        DiceManager.e_OnDiceRoll += CheckAdvanceState;
        TileManager.e_OnTilePlaced += CheckAdvanceState;
    }

    private void OnDisable() {
        DiceManager.e_OnDiceRoll -= CheckAdvanceState;
        TileManager.e_OnTilePlaced -= CheckAdvanceState;
    }

    private void Update() {
        if (_ready) {
            switch (_continueState) {
                case ContinueState.WaitingForRoll:
                    _continueState = ContinueState.WaitingForPlacement;
                    break;
                case ContinueState.WaitingForPlacement:
                    _continueState = ContinueState.WaitingForContinue;
                    _button.interactable = true;
                    break;
                case ContinueState.WaitingForContinue:
                    _continueState = ContinueState.WaitingForRoll;
                    _button.interactable = false;
                    break;
            }

            _ready = false;
        }

        if (_continueState == ContinueState.WaitingForPlacement) {
            CheckAdvanceState();
        }
    }

    public void CheckAdvanceState() {
        switch (_continueState) {
            case ContinueState.WaitingForRoll:
                _ready = true;
                break;
            case ContinueState.WaitingForPlacement:
                _ready = BoardManager.s_instance.NoHighlightTiles();
                break;
            case ContinueState.WaitingForContinue:
                _ready = true;
                break;
        }
    }
}