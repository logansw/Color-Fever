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

    private void Start() {
        _gameManager = GameManager.s_instance;
        _gameManager.Initialize();
        _boardViewer = BoardViewer.s_instance;
        _boardViewer.Initialize();
        _diceManager = DiceManager.s_instance;
        _diceManager.Initialize();
        _tileManager = TileManager.s_instance;
        _tileManager.Initialize();
    }
}
