using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewer : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Tile _tilePrefab;

    [Header("External References")]
    [SerializeField] private Board[] _boards;

    [Header("Asset References")]
    [SerializeField] private Sprite _starEmptySprite;

    [HideInInspector] public static BoardViewer s_instance;

    private void Awake() {
        s_instance = this;
    }

    void OnEnable() {
        Board.e_OnBoardChange += UpdateBoard;
    }

    void OnDisable() {
        Board.e_OnBoardChange -= UpdateBoard;
    }

    public void Initialize() {
        foreach (Board board in _boards) {
            board.Initialize();
            Tile[,] tileObjects = GenerateBoard(board);
            board.TileObjects = tileObjects;
        }
    }

    public Tile[,] GenerateBoard(Board board) {
        Tile[,] tileObjects = new Tile[board.Width+1, board.Height+1];
        for (int i = 1; i <= board.Width; i++) {
            for (int j = 1; j <= board.Height; j++) {
                int x = board.UpsideDown ? (board.Width + 1 - i) : (i);
                Tile tile = Instantiate(_tilePrefab, board.transform.position + new Vector3(i, j, 0), Quaternion.identity);
                tile.Initialize(board, x, j);
                tile.SetColor(board.GetTile(x, j));
                tile.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                tile.transform.SetParent(board.transform);
                tileObjects[x, j] = tile;
                if (j == 4 && (x == 1 || x == 4 || x == 7 || x == 10)) {
                    tile.SetSprite(_starEmptySprite);
                }
            }
        }

        if (board.UpsideDown) {
            board.transform.up = Vector2.down;
            board.transform.position += new Vector3(0, board.Height + 1, 0);
        }
        return tileObjects;
    }

    public void UpdateBoard(Board board) {
        for (int i = 1; i <= board.Width; i++) {
            for (int j = 1; j <= board.Height; j++) {
                Tile tile = _boards[board.Index].TileObjects[i, j];
                tile.SetSprite(board.GetTile(i, j));
                if (j == 4 && (i == 1 || i == 4 || i == 7 || i == 10) &&
                    _boards[board.Index].BoardData[i, j].Color == TileData.TileColor.s) {
                    tile.SetSprite(_starEmptySprite);
                }
                tile.SetColor(board.GetTile(i, j));
                if (board.GetTile(i, j).IsHighlighted) {
                    tile.ShadowSR.enabled = true;
                    tile.OutlineSR.enabled = true;
                } else {
                    tile.ShadowSR.enabled = false;
                    tile.OutlineSR.enabled = false;
                }
            }
        }
    }
}
