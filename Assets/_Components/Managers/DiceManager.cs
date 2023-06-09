using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public static DiceManager s_instance;

    [Header("Asset References")]
    [SerializeField] private Sprite[] _diceSprites;
    [SerializeField] private Sprite _diceZeroSprite;

    [Header("External References")]
    [SerializeField] private SpriteRenderer[] _diceRenderers;
    [SerializeField] private CustomButton _rollButton;

    public int[] DiceValues;
    public delegate void OnDiceRoll();
    public static OnDiceRoll e_OnDiceRoll;
    public bool Rolled;
    private int[,] _previousRolls;

    private void Awake() {
        s_instance = this;
    }

    private void OnEnable() {
        TileManager.e_OnSlotEmptied += DisableRoll;
    }

    private void OnDisable() {
        TileManager.e_OnSlotEmptied -= DisableRoll;
    }

    public void Initialize() {
        DiceValues = new int[2];
        _previousRolls = new int[2, 2];
        DisableRoll();
    }

    public void Roll() {
        bool goodRoll = false;
        while (!goodRoll) {
            for (int i = 0; i < DiceValues.Length; i++) {
                DiceValues[i] = Random.Range(1, 7);
                _diceRenderers[i].sprite = _diceSprites[DiceValues[i] - 1];
            }
            if ((DiceValues[0] == _previousRolls[1, 0] || DiceValues[0] == _previousRolls[1, 1]) &&
                (DiceValues[1] == _previousRolls[1, 0] || DiceValues[1] == _previousRolls[1, 1]) &&
                (_previousRolls[1, 0] == _previousRolls[0, 0] || _previousRolls[1, 0] == _previousRolls[0, 1]) &&
                (_previousRolls[1, 1] == _previousRolls[0, 0] || _previousRolls[1, 1] == _previousRolls[0, 1])) {
                continue;
            } else {
                goodRoll = true;
            }
        }

        RecordRoll();

        if (DiceValues[0] == DiceValues[1]) {
            EnableRoll();
        } else {
            DisableRoll();
        }
        e_OnDiceRoll?.Invoke();
        Rolled = true;
    }

    private void RecordRoll() {
        _previousRolls[0, 0] = _previousRolls[1, 0];
        _previousRolls[0, 1] = _previousRolls[1, 1];
        _previousRolls[1, 0] = DiceValues[0];
        _previousRolls[1, 1] = DiceValues[1];
    }

    private void Update() {
        if (TileManager.s_instance.IsSpecial) {
            DisableRoll();
        }
    }

    public void EnableRoll() {
        _rollButton.Interactable = true;
        Rolled = false;
    }

    public void DisableRoll() {
        _rollButton.Interactable = false;
        Rolled = true;
    }

    public void DisableRoll(int index) {
        DisableRoll();
    }

    public void DisableRollAlias(int index) {
        Rolled = true;
    }

    public void SetDiceByDoubles() {
        for (int i = 0; i < TileManager.s_instance.TilesRemaining.Length; i++) {
            if (TileManager.s_instance.TilesRemaining[i] != 1) {
                DisableRoll();
                return;
            }
        }
        if (DiceValues[0] == DiceValues[1] && DiceValues[0] != -1) {
            EnableRoll();
        } else {
            DisableRoll();
        }
    }

    public void ZeroDice() {
        for (int i = 0; i < _diceRenderers.Length; i++) {
            DiceValues[i] = -1;
            _diceRenderers[i].sprite = _diceZeroSprite;
        }
    }
}
