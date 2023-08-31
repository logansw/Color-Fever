using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector] public static ScoreManager s_instance;

    [Header("ExternalReferences")]
    public ScoreCalculator[] ScoreCalculators;
    [SerializeField] private TMP_Text _totalScoreText;
    [SerializeField] private TMP_Text _totalScoreFinalText;
    public TMP_Text[] VersusScoreTexts;
    private int[] _individualScores;
    private int _totalScore;
    private int _previousScore;
    [SerializeField] private AudioSource _smallScoreAudioSource;
    [SerializeField] private AudioSource _bigScoreAudioSource;

    #region ScoreGrids
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
            {0, 0, 0, 625, 625, 625, 625, 625, 740, 820},
            {0, 0, 0, 625, 625, 625, 625, 625, 760, 860},
            {0, 0, 0, 675, 675, 675, 675, 675, 850, 980},
            {0, 0, 0, 850, 850, 850, 850, 850, 1175, 1400},
            {0, 0, 0, 1100, 1100, 1100, 1100, 1100, 1500, 2000},
        },
        {   // 5
            {0, 0, 0, 0, 950, 950, 1000, 1200, 1500, 2000},
            {0, 0, 0, 0, 950, 950, 1200, 1450, 1650, 2400},     // TODO: Check these values
            {0, 0, 0, 0, 1150, 1150, 1550, 2000, 2000, 3000}, // 2000 seems wrong
            {0, 0, 0, 0, 1500, 1500, 2100, 3000, 2700, 3800}, // 3000 seems wrong
            {0, 0, 0, 0, 2000, 2000, 3000, 5000, 3800, 5000}, // 5000 seems wrong
        },
        {   // 6
            {0, 0, 0, 0, 0, 1350, 1400, 1600, 1900, 2500},
            {0, 0, 0, 0, 0, 1450, 1700, 1950, 2150, 3000},
            {0, 0, 0, 0, 0, 1750, 2150, 2600, 2600, 3600},
            {0, 0, 0, 0, 0, 2300, 2900, 3800, 3500, 4600},
            {0, 0, 0, 0, 0, 3000, 4000, 6000, 4800, 6000},
        },
        {   // 7
            {0, 0, 0, 0, 0, 0, 2400, 2600, 2900, 3500},
            {0, 0, 0, 0, 0, 0, 2700, 2950, 3150, 4000},
            {0, 0, 0, 0, 0, 0, 3150, 3600, 3600, 4600},
            {0, 0, 0, 0, 0, 0, 3900, 4800, 4500, 5600},
            {0, 0, 0, 0, 0, 0, 5000, 7000, 5800, 7000},
        },
        {   // 8
            {0, 0, 0, 0, 0, 0, 0, 3600, 3900, 4500},
            {0, 0, 0, 0, 0, 0, 0, 3950, 4150, 5000},
            {0, 0, 0, 0, 0, 0, 0, 4600, 4600, 5600},
            {0, 0, 0, 0, 0, 0, 0, 5800, 5500, 6600},
            {0, 0, 0, 0, 0, 0, 0, 8000, 6800, 8000},
        },
        {   // 9
            {0, 0, 0, 0, 0, 0, 0, 0, 4900, 5500},
            {0, 0, 0, 0, 0, 0, 0, 0, 5150, 6000},
            {0, 0, 0, 0, 0, 0, 0, 0, 5600, 6600},
            {0, 0, 0, 0, 0, 0, 0, 0, 7500, 7600},
            {0, 0, 0, 0, 0, 0, 0, 0, 7800, 9000},
        },
        {   // 10
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 6500},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 7000},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 7600},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 8600},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 10000},
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

    #endregion

    private void Awake() {
        s_instance = this;
    }

    private void OnEnable() {
        Board.e_OnBoardChange += UpdateScore;
        GameManager.e_OnGameEnd += DisplayFinalScore;
    }

    private void OnDisable() {
        Board.e_OnBoardChange -= UpdateScore;
        GameManager.e_OnGameEnd -= DisplayFinalScore;
    }

    public void Initialize() {
        _totalScore = 0;
        _previousScore = 0;
        _totalScoreText.text = "Score: " + _totalScore.ToString();
        _individualScores = new int[ScoreCalculators.Length];
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
        for (int i = 0; i < ScoreCalculators.Length; i++) {
            _individualScores[i] = ScoreCalculators[i].GetScore();
            _totalScore += _individualScores[i];
        }
        _totalScoreText.text = "Score: " + _totalScore.ToString();
        if (_totalScore - _previousScore > 0) {
            if (_totalScore - _previousScore >= 1000) {
                _bigScoreAudioSource.Play();
            } else {
                _smallScoreAudioSource.Play();
            }
        }
        _previousScore = _totalScore;
    }

    public int[] GetSingleScores() {
        return _individualScores;
    }

    public int GetDoubleScore() {
        return _individualScores[0] + _individualScores[1];
    }

    private void DisplayFinalScore()
    {
        if (SceneManager.GetActiveScene().name.Equals("Single")) {
            _totalScoreFinalText.text = GetFinalSingleText();
        } else if (SceneManager.GetActiveScene().name.Equals("Double")) {
            _totalScoreFinalText.text = GetFinalDoubleText();
        } else if (SceneManager.GetActiveScene().name.Equals("Versus")) {
            VersusScoreTexts[0].text = GetFinalVersusText(0);
            VersusScoreTexts[1].text = GetFinalVersusText(1);
        }
    }

    private string GetFinalSingleText() {
        return _totalScore.ToString();
        // if (HighscoreManager.s_instance.OnSingleLeaderboard(_totalScore)) {
        //     return $"On the leaderboard! {_totalScore}";
        // } else {
        //     return _totalScore.ToString();
        // }
    }

    private string GetFinalDoubleText() {
        string totalScore = _totalScore.ToString();
        string topScore = _individualScores[1].ToString();
        string bottomScore = _individualScores[0].ToString();
        return $"Total Score: {totalScore}\n Top Board: {topScore}\n Bottom Board: {bottomScore}";
        // string totalScoreText;
        // string scoreOneText;
        // string scoreTwoText;
        // if (HighscoreManager.s_instance.OnDoubleLeaderboard(_totalScore)) {
        //     totalScoreText = $"On the leaderboard! {_totalScore}";
        // } else {
        //     totalScoreText = _totalScore.ToString();
        // }
        // if (HighscoreManager.s_instance.OnSingleLeaderboard(_individualScores[1])) {
        //     scoreOneText = $"On the leaderboard! {_individualScores[1]}";
        // } else {
        //     scoreOneText = _individualScores[1].ToString();
        // }
        // if (HighscoreManager.s_instance.OnSingleLeaderboard(_individualScores[0])) {
        //     scoreTwoText = $"On the leaderboard! {_individualScores[0]}";
        // } else {
        //     scoreTwoText = _individualScores[0].ToString();
        // }
        // return $"Total Score: {totalScoreText}\n Top Board:{scoreOneText}\n Bottom Board:{scoreTwoText}";
    }

    private string GetFinalVersusText(int index) {
        int other = (index == 0) ? 1 : 0;
        if (_individualScores[index] < _individualScores[other]) {
            return $"Defeat\n{_individualScores[index]}";
        } else {
            return $"Victory!\n{_individualScores[index]}";
        }
    }
}
