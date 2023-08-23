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
    private int _playersReady;

    [SerializeField] private Transform _textArea;
    [SerializeField] private Transform _placeholder;
    [SerializeField] private Transform _text;

    void Start() {
        _playersReady = 0;
    }

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

    public void PlayerReady() {
        _playersReady++;
        if (_playersReady == 2) {
            CheckHighscore();
        }
    }

    private void RememberScores() {
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
    }

    public void CheckHighscore() {
        if (_index == 0) {
            Debug.Log("Index = 0");
            if (_scores[0] != -1 && HighscoreManager.s_instance.OnSingleLeaderboard(_scores[0])) {
                ShowRecord(_scores[0], true);
                HighscoreManager.s_instance.AddNewEntry();
                HighscoreManager.s_instance.CurrentEntry.Score = _scores[0];
                HighscoreManager.s_instance.CurrentEntry.Single = true;
                _index++;
            } else {
                _index++;
                CheckHighscore();
            }
        } else if (_index == 1) {
            Debug.Log("Index = 1");
            if (SceneManager.GetActiveScene().name == "Versus") {
                // Rotate everything.
                foreach (GameObject obj in _recordObjects) {
                    Rotate180(obj.transform);
                }
                // Rotate0(_textArea);
                // Rotate180(_placeholder);
                // Rotate180(_text);
            }
            if (_scores[1] != -1 && HighscoreManager.s_instance.OnSingleLeaderboard(_scores[1])) {
                ShowRecord(_scores[1], true);
                HighscoreManager.s_instance.AddNewEntry();
                HighscoreManager.s_instance.CurrentEntry.Score = _scores[1];
                HighscoreManager.s_instance.CurrentEntry.Single = true;
                _index++;
            } else {
                _index++;
                CheckHighscore();
            }
        } else if (_index == 2) {
            Debug.Log("Index = 2");
            if (_scores[2] != -1 && HighscoreManager.s_instance.OnDoubleLeaderboard(_scores[2])) {
                ShowRecord(_scores[2], false);
                HighscoreManager.s_instance.AddNewEntry();
                HighscoreManager.s_instance.CurrentEntry.Score = _scores[2];
                HighscoreManager.s_instance.CurrentEntry.Single = false;
                _index++;
            } else {
                _index++;
                CheckHighscore();
            }
        } else {
            Debug.Log("Index > 3");
            Hide();
            HighscoreManager.s_instance.RecordHighScores();
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

        if (SceneManager.GetActiveScene().name == "Versus") {
            _nameInputField.text = "";
        }
    }

    private void Rotate180(Transform t) {
        Vector3 point = new Vector3(0, 0, 0);
        Vector3 axis = new Vector3(0,0,1);
        t.RotateAround(point, axis, 180);
        foreach (Transform child in t) {
            Rotate180(child);
        }
    }

    private void Rotate0(Transform t) {
        Vector3 point = new Vector3(0, 0, 0);
        Vector3 axis = new Vector3(0,0,1);
        t.RotateAround(point, axis, 180);
    }
}