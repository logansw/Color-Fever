using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationManager : MonoBehaviour
{
    [Header("External References")]
    private BoardViewer _boardViewer;
    private DiceManager _diceManager;

    private void Start() {
        _boardViewer = BoardViewer.s_instance;
        _boardViewer.Initialize();
        _diceManager = DiceManager.s_instance;
        _diceManager.Initialize();
    }
}
