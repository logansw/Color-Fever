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
        TilePool.e_OnSpecialDrawn += ClearHighlightTiles;
        SpecialManager.e_OnMoveModeBegun += HighlightLowest;
    }

    private void OnDisable() {
        DiceManager.e_OnDiceRoll -= SetHighlightTiles;
        TileManager.e_OnSlotEmptied -= ClearHighlightTiles;
        TilePool.e_OnSpecialDrawn -= ClearHighlightTiles;
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

            board.SetTile(a, heightA, TileData.h);
            board.SetTile(b, heightB, TileData.h);
            board.SetTile(a + b, heightAB, TileData.h);
        }
    }

    public bool NoHighlightTiles() {
        foreach (Board board in _boards) {
            for (int i = 0; i < board.Width+1; i++) {
                for (int j = 0; j < board.Height+1; j++) {
                    if (board.GetTile(i, j).Equals(TileData.h)) {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void ClearHighlightTiles(int index) {
        Board board =_boards[index];
        for (int i = 0; i < board.Width+1; i++) {
            for (int j = 0; j < board.Height+1; j++) {
                if (board.GetTile(i, j).Equals(TileData.h)) {
                    board.SetTile(i, j, TileData.s);
                }
            }
        }
    }

    public void ClearHighlightTiles() {
        foreach (Board board in _boards) {
            for (int i = 0; i < board.Width+1; i++) {
                for (int j = 0; j < board.Height+1; j++) {
                    if (board.GetTile(i, j).Equals(TileData.h)) {
                        board.SetTile(i, j, TileData.s);
                    }
                }
            }
        }
    }

    private void HighlightLowest(int index) {
        Board board = _boards[index];
        for (int i = 1; i < board.Width+1; i++) {
            int lowest = board.LowestInColumn(i);
            if (lowest == -1 || i < 1 || i >= board.Width+1 || lowest < 1 || i == SpecialManager.s_instance.SelectedTile.X) {
                continue;
            }
            board.SetTile(i, lowest, TileData.h);
        }
    }

    public bool SetTile(Board board, TileData TileData, int x, int y) {
        if (!ConfigurationManager.s_instance.DebugMode) {
            if (!board.GetTile(x, y).Equals(TileData.h)) {
                return false;
            } else {
                board.SetTile(x, y, TileData);
                return true;
            }
        } else {
            board.SetTile(x, y, TileData);
            return true;
        }
    }
}
