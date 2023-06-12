using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector] public static ScoreManager s_instance;

    [Header("ExternalReferences")]
    [SerializeField] private ScoreCalculator[] _scoreCalculators;
    [SerializeField] private TMP_Text _totalScoreText;
    private int _totalScore;

    private void Awake() {
        s_instance = this;
    }

    private void OnEnable() {
        TileManager.e_OnTilePlaced += UpdateScore;
    }

    private void OnDisable() {
        TileManager.e_OnTilePlaced -= UpdateScore;
    }

    public void Initialize() {
        _totalScore = 0;
        _totalScoreText.text = _totalScore.ToString();
    }

    public void UpdateScore() {
        int oldScore = _totalScore;
        foreach (ScoreCalculator calculator in _scoreCalculators) {
            _totalScore = calculator.GetScore();
        }
        _totalScoreText.text = _totalScore.ToString();
        if (ConfigurationManager.s_instance.DebugMode) {
            Debug.Log(_totalScore - oldScore);
        }
    }
}
