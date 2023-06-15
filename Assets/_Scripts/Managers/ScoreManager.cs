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

    public static int[,,] ScoreGridDiagonalUp = {
        {   // 3
            {400, 400, 400, 400, 400, 400, 525, 750, 0, 0},
            {400, 400, 400, 400, 400, 400, 600, 800, 0, 0},
            {775, 775, 775, 775, 775, 875, 1000, 1250, 0, 0},
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

    public static int[,,] ScoreGridDiagonalDown = {
        {   // 3
            {0, 0, 400, 400, 400, 400, 400, 400, 525, 750},
            {0, 0, 400, 400, 400, 400, 400, 400, 600, 800},
            {0, 0, 775, 775, 775, 775, 775, 875, 1000, 1250},
        },
        {   // 4
            {0, 0, 0, 625, 625, 625, 625, 625, 800, 1050},
            {0, 0, 0, 750, 750, 750, 750, 750, 975, 1275},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        },
        {   // 5
            {0, 0, 0, 0, 1400, 1400, 1400, 1700, 2200, 2800},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        }
    };

    public static int[] ScoreGridRainbowColumns = {
        600, 600, 600, 600, 600, 600, 650, 775, 900, 1150
    };

    public static int[,] ScoreGridRainbowRows = {
        {600, 600, 600, 600, 600, 600, 0, 0, 0, 0},
        {600, 600, 600, 600, 600, 700, 0, 0, 0, 0},
        {700, 700, 700, 800, 800, 800, 0, 0, 0, 0},
        {850, 850, 960, 1125, 1250, 1500, 0, 0, 0, 0},
        {1000, 1000, 1200, 1400, 1800, 2500, 0, 0, 0, 0},
    };

    public static int[] ScoreGridDoubleRainbow = {
        5000, 6000, 7000, 8000, 9000
    };

    public static int[] ScoreGridRainbowDiagonal = {
        600, 600, 700, 850, 1000, 1250, 0, 0, 0, 0
    };



    private void Awake() {
        s_instance = this;
    }

    private void OnEnable() {
        Board.e_OnBoardChange += UpdateScore;
    }

    private void OnDisable() {
        Board.e_OnBoardChange -= UpdateScore;
    }

    public void Initialize() {
        _totalScore = 0;
        _totalScoreText.text = _totalScore.ToString();
    }

    public void UpdateScore(Board board) {
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
