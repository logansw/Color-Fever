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
    private HighscoreData _singleScores;
    private HighscoreData _doubleScores;
    private bool _writeRequested;
    [SerializeField] private TMP_InputField _nameInputField;

    private void Awake() {
        s_instance = this;
        _singleScores = JSONTool.ReadData<HighscoreData>("SingleScores.json");
        _doubleScores = JSONTool.ReadData<HighscoreData>("DoubleScores.json");
        if (_nameInputField != null) {
            _nameInputField.text = _singleScores.PreviousName;
        }
    }

    private void Update() {
        if (_writeRequested) {
            JSONTool.WriteData(_singleScores, "SingleScores.json");
            JSONTool.WriteData(_doubleScores, "DoubleScores.json");
            _writeRequested = false;
        }
    }

    private void DisplayScores() {
        if (SceneManager.GetActiveScene().name == "Single") {
            DisplaySingleScores();
        } else if (SceneManager.GetActiveScene().name == "Double") {
            DisplaySingleScores();
            DisplayDoubleScores();
        }
    }

    public void DisplaySingleScores() {
        _singleScores.Highscores.Sort((x, y) => y.CompareTo(x));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < _singleScores.Highscores.Count; i++) {
            sb.Append($"{i + 1}. {_singleScores.Highscores[i].ToString()}\n");
        }
        _highscoreText.text = sb.ToString();
    }

    public void DisplayDoubleScores() {
        _doubleScores.Highscores.Sort((x, y) => y.CompareTo(x));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < _doubleScores.Highscores.Count; i++) {
            sb.Append($"{i + 1}. {_doubleScores.Highscores[i].ToString()}\n");
        }
        _highscoreText.text = sb.ToString();
    }

    public void RecordHighScores() {
        int count = ScoreManager.s_instance.ScoreCalculators.Length;
        string name = _nameInputField.text;
        int[] scores = new int[count];
        for (int i = 0; i < count; i++) {
            scores[i] = ScoreManager.s_instance.ScoreCalculators[i].GetScore();
            _singleScores.Highscores.Add(new HighscoreData.Entry(name, scores[i]));
        }

        if (SceneManager.GetActiveScene().name.Equals("Double")) {
            _doubleScores.Highscores.Add(new HighscoreData.Entry(name, scores[0] + scores[1]));
        }

        _singleScores.PreviousName = name;
        _doubleScores.PreviousName = name;

        _writeRequested = true;
    }
}
