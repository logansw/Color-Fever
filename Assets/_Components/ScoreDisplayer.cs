using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private ScoreMark _scoreMarkPrefab;

    private List<ScoreMark> _scoreMarks;

    private void Start() {
        _scoreMarks = new List<ScoreMark>();
    }

    public void DisplayScoreRow(Link start, Link end, int score) {
        ScoreMark mark = Instantiate(_scoreMarkPrefab, Vector3.zero, Quaternion.identity);
        mark.PositionSelfRow(start, end);
        mark.SetText(score);
        _scoreMarks.Add(mark);
    }

    public void DisplayScoreColumn(Link start, Link end, int score) {
        ScoreMark mark = Instantiate(_scoreMarkPrefab, Vector3.zero, Quaternion.identity);
        mark.PositionSelfColumn(start, end);
        mark.SetText(score);
        _scoreMarks.Add(mark);
    }

    public void DisplayScoreDiagonal(Link start, Link end, int score) {
        ScoreMark mark = Instantiate(_scoreMarkPrefab, Vector3.zero, Quaternion.identity);
        mark.PositionSelfDiagonal(start, end);
        mark.SetText(score);
        _scoreMarks.Add(mark);
    }

    public void DisplayScoreStar(Link link, int score) {
        ScoreMark mark = Instantiate(_scoreMarkPrefab, Vector3.zero, Quaternion.identity);
        mark.PositionSelfStar(link);
        mark.SetText(score);
        _scoreMarks.Add(mark);
    }

    public void DisplayScoreCorner(Link link, int score) {
        ScoreMark mark = Instantiate(_scoreMarkPrefab, Vector3.zero, Quaternion.identity);
        mark.PositionSelfCorner(link);
        mark.SetText(score);
        _scoreMarks.Add(mark);
    }

    public void ClearMarks() {
        foreach (ScoreMark mark in _scoreMarks) {
            Destroy(mark.gameObject);
        }
    }
}
