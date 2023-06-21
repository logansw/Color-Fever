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
    [SerializeField] private GameObject _endCard;

    private void Awake() {
        s_instance = this;
    }

    public void Initialize() {
        RoundsRemaining = 18;
        _roundsRemainingText.text = $"{RoundsRemaining} rounds remaining";
    }

    public void AdvanceTurn() {
        RoundsRemaining--;
        _roundsRemainingText.text = $"{RoundsRemaining} rounds remaining";
        if (RoundsRemaining < 0) {
            _endCard.gameObject.SetActive(true);
            e_OnGameEnd?.Invoke();
        }
    }
}