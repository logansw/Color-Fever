using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager s_instance;
    [SerializeField] private TMP_Text _highscoreText;

    private void Awake() {
        s_instance = this;
    }

    private void OnEnable() {
        GameManager.e_OnGameEnd += DisplaySingleScores;
    }

    private void OnDisable() {
        GameManager.e_OnGameEnd -= DisplaySingleScores;
    }

    public void DisplaySingleScores() {
        HighscoreData singleScores = JSONTool.ReadData<HighscoreData>("SingleScores.json");
        if (SceneManager.GetActiveScene().name == "Single") {
            singleScores.Highscores.Add(ScoreManager.s_instance.GetSingleScores()[0]);
        } else if (SceneManager.GetActiveScene().name == "Double") {
            singleScores.Highscores.Add(ScoreManager.s_instance.GetSingleScores()[0]);
            singleScores.Highscores.Add(ScoreManager.s_instance.GetSingleScores()[1]);
        }
        singleScores.Highscores.Sort((x, y) => y.CompareTo(x));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < singleScores.Highscores.Count; i++) {
            sb.Append($"{i + 1}. {singleScores.Highscores[i]}\n");
        }
        _highscoreText.text = sb.ToString();
    }

    public void DisplayDoubleScores() {
        HighscoreData doubleScores = JSONTool.ReadData<HighscoreData>("DoubleScores.json");
        if (SceneManager.GetActiveScene().name == "Double") {
            doubleScores.Highscores.Add(ScoreManager.s_instance.GetDoubleScore());
        }
        doubleScores.Highscores.Sort((x, y) => y.CompareTo(x));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < doubleScores.Highscores.Count; i++) {
            sb.Append($"{i + 1}. {doubleScores.Highscores[i]}\n");
        }
        _highscoreText.text = sb.ToString();
    }
}
