using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager s_instance;
    [Header("External References")]
    [SerializeField] private Board[] _boards;

    private void Awake() {
        s_instance = this;
    }

    private void OnEnable() {
        DiceManager.e_OnDiceRoll += SetHighlightTiles;
        TileManager.e_OnSlotEmptied += ClearHighlightTiles;
    }

    private void OnDisable() {
        DiceManager.e_OnDiceRoll -= SetHighlightTiles;
        TileManager.e_OnSlotEmptied -= ClearHighlightTiles;
    }

    private void SetHighlightTiles() {
        ClearHighlightTiles();
        int[] diceValues = DiceManager.s_instance.DiceValues;
        int a = diceValues[0];
        int b = diceValues[1];
        foreach (Board board in _boards) {
            int heightA = board.LowestInColumn(a);
            int heightB = board.LowestInColumn(b);
            int heightAB = board.LowestInColumn(a + b);

            board.SetTile(a, heightA, TileType.h);
            board.SetTile(b, heightB, TileType.h);
            board.SetTile(a + b, heightAB, TileType.h);
        }
    }

    public bool NoHighlightTiles() {
        foreach (Board board in _boards) {
            for (int i = 0; i < board.Width+1; i++) {
                for (int j = 0; j < board.Height+1; j++) {
                    if (board.GetTile(i, j) == TileType.h) {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private void ClearHighlightTiles(int index) {
        Board board =_boards[index];
        for (int i = 0; i < board.Width+1; i++) {
            for (int j = 0; j < board.Height+1; j++) {
                if (board.GetTile(i, j) == TileType.h) {
                    board.SetTile(i, j, TileType.s);
                }
            }
        }
    }

    private void ClearHighlightTiles() {
        foreach (Board board in _boards) {
            for (int i = 0; i < board.Width+1; i++) {
                for (int j = 0; j < board.Height+1; j++) {
                    if (board.GetTile(i, j) == TileType.h) {
                        board.SetTile(i, j, TileType.s);
                    }
                }
            }
        }
    }

    public bool SetTile(Board board, TileType tileType, int x, int y) {
        if (!ConfigurationManager.s_instance.DebugMode) {
            if (board.GetTile(x, y) != TileType.h) {
                return false;
            } else {
                board.SetTile(x, y, tileType);
                return true;
            }
        } else {
            board.SetTile(x, y, tileType);
            return true;
        }
    }
}
