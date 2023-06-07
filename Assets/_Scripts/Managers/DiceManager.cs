using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager s_instance;

    [Header("Asset References")]
    [SerializeField] private Sprite[] _diceSprites;

    [Header("External References")]
    [SerializeField] private SpriteRenderer[] _diceRenderers;

    public int[] DiceValues;

    private void Awake() {
        s_instance = this;
    }

    public void Initialize() {
        DiceValues = new int[2];
        Roll();
    }

    public void Roll() {
        for (int i = 0; i < DiceValues.Length; i++) {
            DiceValues[i] = Random.Range(1, 7);
            _diceRenderers[i].sprite = _diceSprites[DiceValues[i] - 1];
        }
    }
}
