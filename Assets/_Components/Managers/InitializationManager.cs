using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationManager : MonoBehaviour
{
    [Header("External References")]
    private GameManager _gameManager;
    private BoardViewer _boardViewer;
    private DiceManager _diceManager;
    private TileManager _tileManager;
    private ScoreManager _scoreManager;
    private ConfigurationManager _configurationManager;
    private SpecialManager _specialManager;
    private TestManager _testManager;

    private void Start() {
        _configurationManager = ConfigurationManager.s_instance;
        _configurationManager.Initialize();
        _gameManager = GameManager.s_instance;
        _gameManager.Initialize();
        _boardViewer = BoardViewer.s_instance;
        _boardViewer.Initialize();
        _diceManager = DiceManager.s_instance;
        _diceManager.Initialize();
        _tileManager = TileManager.s_instance;
        _tileManager.Initialize();
        _scoreManager = ScoreManager.s_instance;
        _scoreManager.Initialize();
        _specialManager = SpecialManager.s_instance;
        _specialManager.Initialize();
        _testManager = TestManager.s_instance;
        _testManager.Initialize();
    }
}
