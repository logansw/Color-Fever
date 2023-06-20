using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance;
    public int RoundsRemaining;
    [SerializeField] private TMP_Text _roundsRemainingText;
    public delegate void OnGameEnd();
    public static event OnGameEnd e_OnGameEnd;

    private void Awake() {
        s_instance = this;
    }

    public void Initialize() {
        RoundsRemaining = 34;
        _roundsRemainingText.text = $"{RoundsRemaining} rounds remaining";
    }

    public void AdvanceTurn() {
        RoundsRemaining--;
        _roundsRemainingText.text = $"{RoundsRemaining} rounds remaining";
        if (RoundsRemaining < 0) {
            Debug.Log("Game over!");
            e_OnGameEnd?.Invoke();
        }
    }
}