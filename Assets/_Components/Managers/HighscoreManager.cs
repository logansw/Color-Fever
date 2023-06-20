using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager s_instance;
    [SerializeField] private TMP_Text _highscoreText;

    private void Awake() {
        s_instance = this;
    }

    public void DisplaySingleScores() {
        HighscoreData singleScores = JSONTool.ReadData<HighscoreData>("SingleScores.json");
        singleScores.Highscores.Sort((x, y) => y.CompareTo(x));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < singleScores.Highscores.Count; i++) {
            sb.Append($"{i + 1}. {singleScores.Highscores[i]}\n");
        }
        _highscoreText.text = sb.ToString();
    }

    public void DisplayDoubleScores() {
        HighscoreData doubleScores = JSONTool.ReadData<HighscoreData>("DoubleScores.json");
        doubleScores.Highscores.Sort((x, y) => y.CompareTo(x));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < doubleScores.Highscores.Count; i++) {
            sb.Append($"{i + 1}. {doubleScores.Highscores[i]}\n");
        }
        _highscoreText.text = sb.ToString();
    }
}
