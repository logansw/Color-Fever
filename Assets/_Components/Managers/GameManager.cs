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
    public delegate void OnGameStart();
    public static event OnGameStart e_OnGameStart;
    [SerializeField] private CustomButton _startButton;
    [SerializeField] private ContinueButton _continueButton;
    public static GameState State;

    public enum GameState {
        PreStart,
        Midgame,
        Endgame
    }

    private void Awake() {
        s_instance = this;
    }

    public void Initialize() {
        _roundsRemainingText.text = $"{RoundsRemaining} rounds remaining";
        _startButton.Interactable = true;
        State = GameState.PreStart;

    }

    public void StartGame() {
        e_OnGameStart?.Invoke();
        _startButton.gameObject.SetActive(false);
        State = GameState.Midgame;
    }

    public void EndGame() {
        e_OnGameEnd?.Invoke();
        State = GameState.Endgame;
        _roundsRemainingText.text = "Game Over";
    }

    public void AdvanceTurn() {
        RoundsRemaining--;
        _roundsRemainingText.text = $"{RoundsRemaining} rounds remaining";
        if (RoundsRemaining == 0) {
            _continueButton.SetGraphicToContinue();
        }
        else if (RoundsRemaining < 0) {
            EndGame();
        }
    }
}