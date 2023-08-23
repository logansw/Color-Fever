using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndCard : MonoBehaviour
{
    [SerializeField] private GameObject[] _endCardObjects;
    [SerializeField] private GameObject[] _recordObjects;
    private int[] _scores;
    private int _index;
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private TMP_Text _leaderboardTypeText;
    [SerializeField] private TMP_Text _newScoreText;

    private void OnEnable() {
        GameManager.e_OnGameEnd += Show;
        GameManager.e_OnGameEnd += RememberScores;
    }

    private void OnDisable() {
        GameManager.e_OnGameEnd -= Show;
        GameManager.e_OnGameEnd -= RememberScores;
    }

    public void Show() {
        foreach (GameObject obj in _endCardObjects) {
            obj.SetActive(true);
        }
    }

    public void Hide() {
        foreach (GameObject obj in _endCardObjects) {
            obj.SetActive(false);
        }
        foreach (GameObject obj in _recordObjects) {
            obj.SetActive(false);
        }
    }

    private void RememberScores() {
        Debug.Log("RememberScores");
        _scores = new int[3];
        _index = 0;
        if (SceneManager.GetActiveScene().name == "Single") {
            _scores[0] = ScoreManager.s_instance.GetSingleScores()[0];
            _scores[1] = -1;
            _scores[2] = -1;
        } else if (SceneManager.GetActiveScene().name == "Double") {
            _scores[0] = ScoreManager.s_instance.GetSingleScores()[0];
            _scores[1] = ScoreManager.s_instance.GetSingleScores()[1];
            _scores[2] = ScoreManager.s_instance.GetDoubleScore();
        } else if (SceneManager.GetActiveScene().name == "Versus") {
            _scores[0] = ScoreManager.s_instance.GetSingleScores()[0];
            _scores[1] = ScoreManager.s_instance.GetSingleScores()[1];
            _scores[2] = -1;
        }
        Debug.Log("0: " + _scores[0]);
        Debug.Log("1: " + _scores[1]);
        Debug.Log("2: " + _scores[2]);
    }

    public void CheckHighscore() {
        if (_index == 0) {
            Debug.Log("Index = 0");
            if (_scores[0] != -1 && HighscoreManager.s_instance.OnSingleLeaderboard(_scores[0])) {
                Debug.Log("Halt!");
                ShowRecord(_scores[0], true);
                _index++;
            } else {
                Debug.Log("Skip");
                _index++;
                CheckHighscore();
            }
        } else if (_index == 1) {
            Debug.Log("Index = 1");
            if (_scores[1] != -1 && HighscoreManager.s_instance.OnSingleLeaderboard(_scores[1])) {
                Debug.Log("Halt!");
                ShowRecord(_scores[1], true);
                _index++;
            } else {
                Debug.Log("Skip");
                _index++;
                CheckHighscore();
            }
        } else if (_index == 2) {
            Debug.Log("Index = 2");
            if (_scores[2] != -1 && HighscoreManager.s_instance.OnDoubleLeaderboard(_scores[2])) {
                Debug.Log("Halt!");
                ShowRecord(_scores[2], false);
                _index++;
            } else {
                Debug.Log("Skip");
                _index++;
                CheckHighscore();
            }
        } else {
            Debug.Log("Index > 3");
            Hide();
        }
    }

    public void ShowRecord(int score, bool single) {
        Debug.Log("Showing Record: " + score + " " + single);
        Hide();
        foreach (GameObject obj in _recordObjects) {
            obj.SetActive(true);
        }

        _newScoreText.text = score.ToString();

        if (single) {
            _leaderboardTypeText.text = "On the Singles Leaderboard!";
        } else {
            _leaderboardTypeText.text = "On the Doubles Leaderboard!";
        }

        if (SceneManager.GetActiveScene().name == "Single" || SceneManager.GetActiveScene().name == "Double") {
            _nameInputField.text = "";
        } else if (SceneManager.GetActiveScene().name == "Versus") {
            _nameInputField.text = "";
        }
    }
}