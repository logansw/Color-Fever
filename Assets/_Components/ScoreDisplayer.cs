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

    public void DisplayScoreRow(Link start, Link end, int score, Board board, int index) {
        Transform boardOrigin = board.transform;
        ScoreMark mark = Instantiate(_scoreMarkPrefab, boardOrigin.position, Quaternion.identity);
        mark.PositionSelfRow(start, end);
        mark.SetText(score, new Vector3(0.25f, 0f, 0f));
        _scoreMarks[index].Add(mark);
        if (board.UpsideDown) {
            RotateMarkAroundBoard(mark, board);
        }
    }

    public void DisplayScoreColumn(Link start, Link end, int score, Board board, int index) {
        Transform boardOrigin = board.transform;
        ScoreMark mark = Instantiate(_scoreMarkPrefab, boardOrigin.position, Quaternion.identity);
        mark.PositionSelfColumn(start, end);
        mark.SetText(score, new Vector3(0.25f, 0f, 0f));
        _scoreMarks[index].Add(mark);
        if (board.UpsideDown) {
            RotateMarkAroundBoard(mark, board);
        }
    }

    public void DisplayScoreDiagonal(Link start, Link end, int score, Board board, int index) {
        Transform boardOrigin = board.transform;
        ScoreMark mark = Instantiate(_scoreMarkPrefab, boardOrigin.position, Quaternion.identity);
        mark.PositionSelfDiagonal(start, end);
        mark.SetText(score, new Vector3(0.5f, 0f, 0f));
        _scoreMarks[index].Add(mark);
        if (board.UpsideDown) {
            RotateMarkAroundBoard(mark, board);
        }
    }

    public void DisplayScoreStar(Link link, int score, Board board, int index) {
        Transform boardOrigin = board.transform;
        ScoreMark mark = Instantiate(_scoreMarkPrefab, boardOrigin.position, Quaternion.identity);
        mark.PositionSelfStar(link);
        mark.SetText(score, new Vector3(0.5f, 0f, 0f));
        _scoreMarks[index].Add(mark);
        if (board.UpsideDown) {
            RotateMarkAroundBoard(mark, board);
        }
    }

    public void DisplayScoreCorner(Link link, int score, Board board, int index) {
        Transform boardOrigin = board.transform;
        ScoreMark mark = Instantiate(_scoreMarkPrefab, boardOrigin.position, Quaternion.identity);
        mark.PositionSelfCorner(link);
        mark.SetText(score);
        _scoreMarks[index].Add(mark);
        if (board.UpsideDown) {
            RotateMarkAroundBoard(mark, board);
        }
    }

    public void ClearMarks(int index) {
        foreach (ScoreMark mark in _scoreMarks[index]) {
            Destroy(mark.gameObject);
        }
        _scoreMarks[index].Clear();
    }

    private void RotateMarkAroundBoard(ScoreMark mark, Board board) {
        Vector3 point = board.transform.position;
        Vector3 axis = new Vector3(0,0,1);
        mark.transform.RotateAround(point, axis, 180);
        int offset = board.Width + 1;
        mark.transform.position += new Vector3(offset, 0, 0);
    }

    private void InvertDiagonalMark(ScoreMark mark) {
        float zRotation = mark.transform.rotation.eulerAngles.z;
        Vector3 point = mark.transform.position;
        Vector3 axis = new Vector3(0,0,1);
        mark.transform.RotateAround(point, axis, -zRotation * 2);
    }
}
