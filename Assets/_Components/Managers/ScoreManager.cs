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
    [SerializeField] private TMP_Text _totalScoreFinalText;
    private int[] _individualScores;
    private int _totalScore;

    public static int[,] ScoreGridColumns = {
        {400, 400, 400, 400, 400, 400, 450, 540, 660, 800},
        {625, 625, 625, 625, 625, 625, 675, 800, 925, 1160},
        {900, 900, 900, 900, 900, 900, 980, 1160, 1320, 1750},
    };

    public static int[,,] ScoreGridRows = {
        {   // 3
            {0, 0, 400, 400, 400, 400, 400, 400, 440, 500},
            {0, 0, 400, 400, 400, 400, 400, 400, 450, 530},
            {0, 0, 460, 460, 460, 460, 460, 460, 520, 625},
            {0, 0, 520, 520, 520, 520, 520, 520, 640, 875},
            {0, 0, 625, 625, 625, 625, 625, 625, 950, 1200},
        },
        {   // 4
            {0, 0, 625, 625, 625, 625, 625, 625, 740, 820},
            {0, 0, 625, 625, 625, 625, 625, 625, 760, 860},
            {0, 0, 675, 675, 675, 675, 675, 675, 850, 980},
            {0, 0, 850, 850, 850, 850, 850, 850, 1175, 1400},
            {0, 0, 1100, 1100, 1100, 1100, 1100, 1100, 1500, 2000},
        },
        {   // 5
            {0, 0, 950, 950, 950, 950, 1000, 1200, 1500, 2000},
            {0, 0, 950, 950, 950, 950, 1200, 1450, 1650, 2400},     // TODO: Check these values
            {0, 0, 1150, 1150, 1150, 1150, 1550, 2000, 2000, 3000}, // 2000 seems wrong
            {0, 0, 1500, 1500, 1500, 1500, 2100, 3000, 2700, 3800}, // 3000 seems wrong
            {0, 0, 2000, 2000, 2000, 2000, 3000, 5000, 3800, 5000}, // 5000 seems wrong
        },
    };

    public static int[,,] ScoreGridDiagonal = {
        {   // 3
            {400, 400, 400, 400, 400, 450, 525, 750, 0, 0},
            {400, 400, 400, 400, 400, 450, 550, 950, 0, 0},
            {750, 750, 750, 750, 775, 875, 975, 1200, 0, 0},
        },
        {   // 4
            {625, 625, 625, 625, 625, 800, 1050, 0, 0, 0},
            {750, 750, 750, 750, 750, 975, 1275, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        },
        {   // 5
            {1400, 1400, 1400, 1700, 2200, 2800, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        }
    };

    public static int[] ScoreGridRainbowColumns = {
        600, 600, 600, 600, 600, 600, 660, 775, 875, 1050
    };

    public static int[,] ScoreGridRainbowRows = {
        {600, 600, 600, 600, 600, 600, 0, 0, 0, 0},
        {600, 600, 600, 600, 600, 700, 0, 0, 0, 0},
        {700, 700, 700, 800, 800, 800, 0, 0, 0, 0},
        {850, 850, 960, 1125, 1250, 1500, 0, 0, 0, 0},
        {1000, 1000, 1200, 1400, 1800, 2500, 0, 0, 0, 0},
    };

    public static int[] ScoreGridDoubleRainbow = {
        1475, 1700, 2400, 3000, 3500
    };

    public static int[] ScoreGridRainbowDiagonal = {
        600, 600, 700, 850, 1000, 1250, 0, 0, 0, 0
    };

    public static int[,,] ScoreGridStarDiagonalUp = {
        // 3
        {
            {1600, 1600, 1600, 1600, 1600, 1800, 2200, 2500, 0, 0},
            {1600, 1600, 1600, 1600, 1600, 2100, 2300, 2500, 0, 0},
            {1600, 1800, 2000, 1800, 2000, 1900, 2400, 3000, 0, 0},
        },
        // 4
        {
            {2500, 2500, 2500, 2500, 2500, 2500, 3000, 0, 0, 0},
            {2500, 2500, 2500, 2500, 2500, 2500, 3500, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        },
        //5
        {
            {5000, 5000, 5000, 5000, 5000, 7000, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        }
    };

    public static int[,,] ScoreGridStarDiagonalDown = {
        // 3
        {
            {1600, 1600, 1600, 1600, 1600, 1600, 2000, 2500, 0, 0},
            {1600, 1600, 1600, 1600, 1600, 1600, 2300, 2400, 0, 0},
            {1800, 1800, 1800, 1800, 2000, 1900, 2200, 2800, 0, 0},
        },
        // 4
        {
            {2500, 2500, 2500, 2500, 2500, 2500, 3000, 0, 0, 0},
            {2500, 2500, 2500, 2500, 2500, 2500, 3000, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        },
        //5
        {
            {5000, 5000, 5000, 5000, 5000, 7500, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        }
    };

    public static int[,,] ScoreGridStarRows = {
        {   // 3
            {0, 0, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000},
            {0, 0, 2500, 2500, 2500, 2500, 2500, 2500, 2500, 2500},
            {0, 0, 1600, 1600, 1600, 1600, 1600, 1600, 1800, 2100},
            {0, 0, 1600, 1600, 1600, 1600, 1600, 1600, 2000, 2400},
            {0, 0, 2000, 2000, 2000, 2000, 2300, 2000, 2800, 3500},
        },
        {   // 4
            {0, 0, 0, 3200, 3200, 3200, 3200, 3200, 3200, 3200},
            {0, 0, 0, 3200, 3200, 3200, 3200, 3200, 3200, 3200},
            {0, 0, 0, 2500, 2500, 2500, 2500, 2500, 2800, 3200},
            {0, 0, 0, 2500, 2500, 2500, 2500, 2500, 2900, 3500},
            {0, 0, 0, 3000, 3000, 3000, 3000, 3000, 3500, 4800},
        },
        {   // 5
            {0, 0, 0, 0, 3400, 3400, 3400, 3400, 3400, 3400},
            {0, 0, 0, 0, 3400, 3400, 3400, 3400, 3400, 3400},
            {0, 0, 0, 0, 3400, 3400, 3400, 3400, 4300, 5500},
            {0, 0, 0, 0, 3200, 3200, 3200, 3200, 4200, 5500},
            {0, 0, 0, 0, 4000, 4000, 4000, 4000, 5300, 7000},
        },
    };

    public static int[,] ScoreGridStarColumns = {
        {1600, 1600, 1600, 1600, 1600, 1600, 1800, 2000, 2400, 3000},
        {2500, 2500, 2500, 2500, 2500, 2500, 2800, 3200, 3800, 4500},
        {3200, 3200, 3200, 3200, 3200, 3200, 3500, 4000, 5000, 7000},
    };

    private void Awake() {
        s_instance = this;
    }

    private void OnEnable() {
        Board.e_OnBoardChange += UpdateScore;
        GameManager.e_OnGameEnd += RecordScores;
    }

    private void OnDisable() {
        Board.e_OnBoardChange -= UpdateScore;
        GameManager.e_OnGameEnd -= RecordScores;
    }

    public void Initialize() {
        _totalScore = 0;
        _totalScoreText.text = _totalScore.ToString();
        _individualScores = new int[_scoreCalculators.Length];
        if (!JSONTool.FileExists("SingleScores.json")) {
            HighscoreData data = new HighscoreData();
            data = data.CreateNewFile();
            JSONTool.WriteData(data, "SingleScores.json");
        }
        if (!JSONTool.FileExists("DoubleScores.json")) {
            HighscoreData data = new HighscoreData();
            data = data.CreateNewFile();
            JSONTool.WriteData(data, "DoubleScores.json");
        }
    }

    public void UpdateScore(Board board) {
        _totalScore = 0;
        for (int i = 0; i < _scoreCalculators.Length; i++) {
            _individualScores[i] = _scoreCalculators[i].GetScore();
            _totalScore += _individualScores[i];
        }
        _totalScoreText.text = _totalScore.ToString();
    }

    public void RecordScores() {
        HighscoreData singleData = JSONTool.ReadData<HighscoreData>("SingleScores.json");
        HighscoreData doubleData = JSONTool.ReadData<HighscoreData>("DoubleScores.json");

        int count = _scoreCalculators.Length;
        int[] scores = new int[count];
        for (int i = 0; i < count; i++) {
            scores[i] = _scoreCalculators[i].GetScore();
            singleData.Highscores.Add(scores[i]);
        }

        if (count == 2) {
            doubleData.Highscores.Add(scores[0] + scores[1]);
        }

        _totalScoreFinalText.text = _totalScore.ToString();
        JSONTool.WriteData<HighscoreData>(singleData, "SingleScores.json");
        JSONTool.WriteData<HighscoreData>(doubleData, "DoubleScores.json");
    }

    public int[] GetSingleScores() {
        return _individualScores;
    }

    public int GetDoubleScore() {
        return _individualScores[0] + _individualScores[1];
    }
}
