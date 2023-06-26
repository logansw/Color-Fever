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
        SpecialManager.e_OnMoveModeBegun += HighlightLowest;
        SpecialManager.e_OnCornerModeSet += HighlightCorners;
        SpecialManager.e_OnMoveModeSet += HighlightTiles;
        SpecialManager.e_OnMoveModeBegun += UnhighlightMovable;
        SpecialManager.e_OnRemoveModeSet += HighlightTiles;
        SpecialManager.e_OnSwapModeSet += HighlightTiles;
        SpecialManager.e_OnNormalModeSet += ClearHighlightTiles;
    }

    private void OnDisable() {
        DiceManager.e_OnDiceRoll -= SetHighlightTiles;
        TileManager.e_OnSlotEmptied -= ClearHighlightTiles;
        SpecialManager.e_OnMoveModeBegun -= HighlightLowest;
        SpecialManager.e_OnCornerModeSet -= HighlightCorners;
        SpecialManager.e_OnMoveModeSet -= HighlightTiles;
        SpecialManager.e_OnMoveModeBegun -= UnhighlightMovable;
        SpecialManager.e_OnRemoveModeSet -= HighlightTiles;
        SpecialManager.e_OnSwapModeSet -= HighlightTiles;
        SpecialManager.e_OnNormalModeSet -= ClearHighlightTiles;
    }

    private void SetHighlightTiles() {
        ClearHighlightTiles();
        int[] diceValues = DiceManager.s_instance.DiceValues;
        int a = diceValues[0];
        int b = diceValues[1];
        foreach (Board board in _boards) {
            int heightA = board.LowestSpaceInColumn(a);
            int heightB = board.LowestSpaceInColumn(b);
            int heightAB = board.LowestSpaceInColumn(a + b);

            board.HighlightTile(a, heightA, true);
            board.HighlightTile(b, heightB, true);
            board.HighlightTile(a + b, heightAB, true);
        }
    }

    public bool NoHighlightTiles() {
        foreach (Board board in _boards) {
            for (int i = 0; i < board.Width+1; i++) {
                for (int j = 0; j < board.Height+1; j++) {
                    if (board.GetTile(i, j).IsHighlighted) {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void ClearHighlightTiles(int index) {
        Board board =_boards[index];
        for (int i = 1; i < board.Width+1; i++) {
            for (int j = 1; j < board.Height+1; j++) {
                board.HighlightTile(i, j, false);
            }
        }
    }

    public void ClearHighlightTiles() {
        foreach (Board board in _boards) {
            for (int i = 0; i < board.Width+1; i++) {
                for (int j = 0; j < board.Height+1; j++) {
                    board.HighlightTile(i, j, false);
                }
            }
        }
    }

    private void HighlightLowest(int index) {
        Board board = _boards[index];
        ClearHighlightTiles(index);
        for (int i = 1; i < board.Width+1; i++) {
            int lowest = board.LowestSpaceInColumn(i);
            if (lowest == -1 || i < 1 || i >= board.Width+1 || lowest < 1 || i == SpecialManager.s_instance.SelectedTile.X) {
                continue;
            }
            board.HighlightTile(i, lowest, true);
        }
    }

    private void HighlightCorners(int index) {
        Board b = _boards[index];
        ClearHighlightTiles(index);
        if (b.BoardData[1, 1].Equals(TileData.s)) {
            b.HighlightTile(1, 1, true);
        }
        if (b.BoardData[1, b.Height].Equals(TileData.s)) {
            b.HighlightTile(1, b.Height, true);
        }
        if (b.BoardData[b.Width, 1].Equals(TileData.s)) {
            b.HighlightTile(b.Width, 1, true);
        }
        if (b.BoardData[b.Width, b.Height].Equals(TileData.s)) {
            b.HighlightTile(b.Width, b.Height, true);
        }
        b.QueueUpdate();
    }

    private void HighlightMovable(int index) {
        Board b = _boards[index];
        ClearHighlightTiles(index);
        for (int i = 1; i < b.Width+1; i++) {
            int highestTileInColumn = b.HighestTileInColumn(i);
            if (highestTileInColumn != -1) {
                b.HighlightTile(i, highestTileInColumn, true);
            }
        }
    }

    private void UnhighlightMovable(int index) {
        Board b = _boards[index];
        for (int i = 1; i < b.Width+1; i++) {
            int lowestSpaceInColumn = b.LowestSpaceInColumn(i);
            if (lowestSpaceInColumn <= 1) {
                continue;
            } else {
                b.HighlightTile(i, lowestSpaceInColumn - 1, false);
            }
        }
    }

    private void HighlightTiles(int index) {
        Board b = _boards[index];
        ClearHighlightTiles(index);
        for (int i = 1; i < b.Width + 1; i++) {
            for (int j = 1; j < b.Height + 1; j++) {
                if (b.GetTile(i, j).Color != TileData.TileColor.s) {
                    b.HighlightTile(i, j, true);
                }
            }
        }
    }

    public bool SetTile(Board board, TileData TileData, int x, int y) {
        if (!ConfigurationManager.s_instance.DebugMode) {
            if (!board.GetTile(x, y).IsHighlighted) {
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
