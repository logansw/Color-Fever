using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewer : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Tile _tilePrefab;

    [Header("External References")]
    [SerializeField] private Board[] _boards;

    [HideInInspector] public static BoardViewer s_instance;

    void OnEnable() {
        Board.e_OnBoardChange += UpdateBoard;
    }

    void OnDisable() {
        Board.e_OnBoardChange -= UpdateBoard;
    }

    private void Awake() {
        s_instance = this;
    }

    public void Initialize() {
        foreach (Board board in _boards) {
            board.Initialize();
            GenerateBoard(board);
        }
    }

    public void GenerateBoard(Board board) {
        for (int i = 0; i < board.Width; i++) {
            for (int j = 0; j < board.Height; j++) {
                Tile tile = Instantiate(_tilePrefab, board.transform.position + new Vector3(i, j, 0), Quaternion.identity);
                tile.Initialize(board);
                tile.SetColor(board.GetTile(i, j));
                tile.transform.localScale = new Vector3(0.9f, 0.9f, 1.0f);
                tile.transform.SetParent(board.transform);
            }
        }
    }

    public void UpdateBoard(Board board) {

    }
}
