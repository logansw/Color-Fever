using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public static DiceManager s_instance;

    [Header("Asset References")]
    [SerializeField] private Sprite[] _diceSprites;

    [Header("External References")]
    [SerializeField] private SpriteRenderer[] _diceRenderers;
    [SerializeField] private Button _rollButton;

    public int[] DiceValues;
    public delegate void OnDiceRoll();
    public static OnDiceRoll e_OnDiceRoll;
    public bool Rolled;

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
    }

    public void Roll() {
        for (int i = 0; i < DiceValues.Length; i++) {
            DiceValues[i] = Random.Range(1, 7);
            _diceRenderers[i].sprite = _diceSprites[DiceValues[i] - 1];
        }
        if (DiceValues[0] == DiceValues[1]) {
            EnableRoll();
        } else {
            DisableRoll();
        }
        e_OnDiceRoll?.Invoke();
    }

    public void EnableRoll() {
        _rollButton.interactable = true;
    }

    public void DisableRoll() {
        _rollButton.interactable = false;
    }

    public void DisableRoll(int index) {
        DisableRoll();
    }
}
