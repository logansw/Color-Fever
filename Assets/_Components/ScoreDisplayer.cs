using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private ScoreMark _scoreMarkPrefab;

    private List<ScoreMark>[] _scoreMarks;

    private void Start() {
        _scoreMarks = new List<ScoreMark>[2];
        _scoreMarks[0] = new List<ScoreMark>();
        _scoreMarks[1] = new List<ScoreMark>();
    }

    public void DisplayScoreRow(Link start, Link end, int score, Transform boardOrigin, int index) {
        ScoreMark mark = Instantiate(_scoreMarkPrefab, boardOrigin.position, Quaternion.identity);
        mark.PositionSelfRow(start, end);
        mark.SetText(score);
        _scoreMarks[index].Add(mark);
    }

    public void DisplayScoreColumn(Link start, Link end, int score, Transform boardOrigin, int index) {
        ScoreMark mark = Instantiate(_scoreMarkPrefab, boardOrigin.position, Quaternion.identity);
        mark.PositionSelfColumn(start, end);
        mark.SetText(score);
        _scoreMarks[index].Add(mark);
    }

    public void DisplayScoreDiagonal(Link start, Link end, int score, Transform boardOrigin, int index) {
        ScoreMark mark = Instantiate(_scoreMarkPrefab, boardOrigin.position, Quaternion.identity);
        mark.PositionSelfDiagonal(start, end);
        mark.SetText(score);
        _scoreMarks[index].Add(mark);
    }

    public void DisplayScoreStar(Link link, int score, Transform boardOrigin, int index) {
        ScoreMark mark = Instantiate(_scoreMarkPrefab, boardOrigin.position, Quaternion.identity);
        mark.PositionSelfStar(link);
        mark.SetText(score);
        _scoreMarks[index].Add(mark);
    }

    public void DisplayScoreCorner(Link link, int score, Transform boardOrigin, int index) {
        ScoreMark mark = Instantiate(_scoreMarkPrefab, boardOrigin.position, Quaternion.identity);
        mark.PositionSelfCorner(link);
        mark.SetText(score);
        _scoreMarks[index].Add(mark);
    }

    public void ClearMarks(int index) {
        foreach (ScoreMark mark in _scoreMarks[index]) {
            Destroy(mark.gameObject);
        }
        _scoreMarks[index].Clear();
    }
}
