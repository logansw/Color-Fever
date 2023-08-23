using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager s_instance;
    public const int MAX_ENTRIES = 30;
    private HighscoreData _singleScores;
    private HighscoreData _doubleScores;
    private bool _writeRequested;
    [SerializeField] private TMP_InputField[] _nameInputFields;
    public EndCard EndCard;
    [SerializeField] private EntryUI[] _entryUIs;
    [SerializeField] private GameObject _entriesPanel;
    private int _currentPage;
    private bool _singleOpen;
    private bool _doubleOpen;
    List<HighScoreEntry> _queuedEntries;
    public HighScoreEntry CurrentEntry;

    private void Awake() {
        s_instance = this;
        _singleScores = JSONTool.ReadData<HighscoreData>("SingleScores.json");
        Debug.Log(_singleScores);
        _doubleScores = JSONTool.ReadData<HighscoreData>("DoubleScores.json");
        Debug.Log(_doubleScores);
        if (_nameInputFields.Length > 0 && _nameInputFields[0] != null) {
            for (int i = 0; i < _nameInputFields.Length; i++) {
                _nameInputFields[i].text = _singleScores.PreviousName;
            }
        }
        _currentPage = 0;
        _queuedEntries = new List<HighScoreEntry>();
    }

    private void Update() {
        if (_writeRequested) {
            JSONTool.WriteData(_singleScores, "SingleScores.json");
            JSONTool.WriteData(_doubleScores, "DoubleScores.json");
            _writeRequested = false;
        }
    }

    private void DisplayScores(int page) {
        if (SceneManager.GetActiveScene().name == "Single") {
            DisplaySingleScores(page);
        } else if (SceneManager.GetActiveScene().name == "Double") {
            DisplaySingleScores(page);
            DisplayDoubleScores(page);
        }
    }

    public void DisplaySingleScores(int page) {
        _currentPage = page;
        _singleOpen = true;
        _entriesPanel.gameObject.SetActive(true);
        _singleScores.Highscores.Sort((x, y) => y.CompareTo(x));
        int startIndex = page * 10 + 1;
        for (int i = 0; i < 10; i++) {
            if (i + startIndex - 1 >= _singleScores.Highscores.Count) {
                _entryUIs[i].gameObject.SetActive(false);
            } else {
                _entryUIs[i].gameObject.SetActive(true);
                _entryUIs[i].SetEntry(_singleScores.Highscores[i + startIndex - 1], i + startIndex);
                if (startIndex + i == 1) {
                    _entryUIs[i].SetColor(new Color(255f/255f, 229f/255f, 0f/255f, 1f));
                } else if (startIndex + i == 2) {
                    _entryUIs[i].SetColor(new Color(201/255f, 208/255f, 217f/255f, 1f));
                } else if (startIndex + i == 3) {
                    _entryUIs[i].SetColor(new Color(243/255f, 193/255f, 96f/255f, 1f));
                } else {
                    _entryUIs[i].SetColor(Color.white);
                }
            }
        }
    }

    public void DisplayDoubleScores(int page) {
        _currentPage = page;
        _doubleOpen = true;
        _entriesPanel.gameObject.SetActive(true);
        _doubleScores.Highscores.Sort((x, y) => y.CompareTo(x));
        int startIndex = page * 10 + 1;
        for (int i = 0; i < 10; i++) {
            if (i + startIndex - 1 >= _doubleScores.Highscores.Count) {
                _entryUIs[i].gameObject.SetActive(false);
            } else {
                _entryUIs[i].gameObject.SetActive(true);
                _entryUIs[i].SetEntry(_doubleScores.Highscores[i + startIndex - 1], i + startIndex);
            }
        }
    }

    public void ChangePage(int change) {
        // Check if change is valid
        if (_currentPage + change < 0) {
            return;
        }
        if (_singleOpen && (_currentPage + change) * 10 > _singleScores.Highscores.Count) {
            return;
        }
        if (_doubleOpen && (_currentPage + change) * 10 > _doubleScores.Highscores.Count) {
            return;
        }

        _currentPage += change;
        if (_singleOpen) {
            DisplaySingleScores(_currentPage);
        } else if (_doubleOpen) {
            DisplayDoubleScores(_currentPage);
        }
    }

    public void CloseHighscores() {
        _singleOpen = false;
        _doubleOpen = false;
        _entriesPanel.gameObject.SetActive(false);
    }

    public void AddNewEntry() {
        HighScoreEntry newEntry = new HighScoreEntry("", 0, true);
        _queuedEntries.Add(newEntry);
        CurrentEntry = newEntry;
    }

    public void QueueName() {
        CurrentEntry.Name = _nameInputFields[0].text;
        _singleScores.PreviousName = _nameInputFields[0].text;
        _doubleScores.PreviousName = _nameInputFields[0].text;
    }

    public void RecordHighScores() {
        Debug.Log(_queuedEntries.Count);
        for (int i = 0; i < _queuedEntries.Count; i++) {
            if (_queuedEntries[i].Single) {
                _singleScores.Highscores.Add(new HighscoreData.Entry(_queuedEntries[i].Name, _queuedEntries[i].Score, System.DateTime.Now));
            } else {
                _doubleScores.Highscores.Add(new HighscoreData.Entry(_queuedEntries[i].Name, _queuedEntries[i].Score, System.DateTime.Now));
            }
            _singleScores.Highscores = SortAndTruncateHighscores(_singleScores.Highscores);
            _doubleScores.Highscores = SortAndTruncateHighscores(_doubleScores.Highscores);
        }
        _writeRequested = true;
    }

    private List<HighscoreData.Entry> SortAndTruncateHighscores(List<HighscoreData.Entry> highscores) {
        List<HighscoreData.Entry> highscoresTruncated = new List<HighscoreData.Entry>();
        highscores.Sort((x, y) => y.CompareTo(x));
        for (int i = 0; i < MAX_ENTRIES; i++) {
            if (i >= highscores.Count) {
                break;
            }
            highscoresTruncated.Add(highscores[i]);
        }
        return highscoresTruncated;
    }

    public bool OnSingleLeaderboard(int newScore) {
        return OnTheLeaderboard(_singleScores.Highscores, newScore);
    }

    public bool OnDoubleLeaderboard(int newScore) {
        return OnTheLeaderboard(_doubleScores.Highscores, newScore);
    }

    private bool OnTheLeaderboard(List<HighscoreData.Entry> highscores, int newScore) {
        if (highscores.Count < MAX_ENTRIES) {
            return true;
        }
        foreach (HighscoreData.Entry entry in highscores) {
            if (newScore > entry.Score) {
                return true;
            }
        }
        return false;
    }

}
public class HighScoreEntry {
    public string Name;
    public int Score;
    public bool Single;

    public HighScoreEntry(string name, int score, bool single) {
        Name = name;
        Score = score;
        Single = single;
    }
}