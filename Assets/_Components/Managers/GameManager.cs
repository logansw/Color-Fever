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
    [SerializeField] private CustomButton[] _startButtons;
    [SerializeField] private ContinueButton _continueButton;
    public static GameState State;
    int _playersReady;

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
        foreach (CustomButton startButton in _startButtons) {
            startButton.Interactable = true;
        }
        _playersReady = 0;
        State = GameState.PreStart;

    }

    public void StartGame(CustomButton startButton) {
        _playersReady++;
        startButton.gameObject.SetActive(false);
        if (_playersReady == _startButtons.Length) {
            e_OnGameStart?.Invoke();
            State = GameState.Midgame;
        }
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