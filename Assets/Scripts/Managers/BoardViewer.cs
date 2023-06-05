using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewer : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Tile _tilePrefab;

    void OnEnable() {
        BoardManager.OnBoardChange += UpdateBoard;
    }

    void OnDisable() {
        BoardManager.OnBoardChange -= UpdateBoard;
    }

    public void Initialize() {

    }

    public void UpdateBoard() {

    }
}
