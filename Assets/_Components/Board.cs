using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public TileData[,] BoardData;
    public Tile[,] TileObjects;
    public delegate void OnBoardChange(Board board);
    public static event OnBoardChange e_OnBoardChange;
    public int Width;
    public int Height;
    public int Index;
    public bool _updateQueued;
    [SerializeField] private TimelineInstance _timelineInstance;

    public void Initialize() {
        Width = 10;
        Height = 5;
        BoardData = new TileData[Width+1, Height+1];
        for (int i = 1; i <= Width; i++) {
            for (int j = 1; j <= Height; j++) {
                BoardData[i, j] = TileData.s;
                if (j == 1) {
                    BoardData[i, j].SetHighlight(true);
                } else {
                    BoardData[i, j].SetHighlight(false);
                }
            }
        }
        _updateQueued = true;
    }

    private void Update() {
        if (_updateQueued) {
            e_OnBoardChange(this);
            _updateQueued = false;
        }
    }

    public void SetTile(int x, int y, TileData TileData) {
        if (x < 1 || x >= Width+1 || y < 1 || y >= Height+1) { return; }
        BoardData[x, y] = TileData;
        TileObjects[x, y].TileData = TileData;
        QueueUpdate();
        _timelineInstance.QueueAdvance();
    }

    public TileData GetTile(int x, int y) {
        return BoardData[x, y];
    }

    public void QueueUpdate() {
        _updateQueued = true;
    }

    /// <summary>
    /// Returns the lowest empty space in column <paramref name="x"/>, or Board.Height+1 if the column is full.
    /// </summary>
    /// <param name="x"></param>
    /// <returns>Returns -1 the specified column x is out of bounds</returns>
    public int LowestSpaceInColumn(int x) {
        if (x < 0 || x >= Width+1) { return -1; }

        for (int j = 1; j < Height+1; j++) {
            if (BoardData[x, j].Color == TileData.TileColor.s) {
                return j;
            }
        }
        return Height+1;
    }

    public int HighestTileInColumn(int x) {
        if (x < 0 || x >= Width + 1) { return -1; }
        for (int j = Height; j > 0; j--) {
            if (BoardData[x, j].IsNormal()) {
                return j;
            }
        }

        return -1;
    }

    public void DebugPrintBoard() {
        string boardString = "";
        for (int j = Height; j > 0; j--) {
            for (int i = 1; i <= Width; i++) {
                switch (BoardData[i, j].ToString()) {
                    case "Space":
                        boardString += "0 ";
                        break;
                    case "Red":
                        boardString += "P ";
                        break;
                    case "Orange":
                        boardString += "O ";
                        break;
                    case "Yellow":
                        boardString += "Y ";
                        break;
                    case "Green":
                        boardString += "G ";
                        break;
                    case "Blue":
                        boardString += "B ";
                        break;
                    case "Highlight":
                        boardString += "H ";
                        break;
                    default:
                        boardString += "X ";
                        break;
                }
            }
            boardString += "\n";
        }
        Debug.Log(boardString);
    }

    public void HighlightTile(int x, int y, bool on) {
        if (x < 1 || x >= Width+1 || y < 1 || y >= Height+1) { return; }
        TileData newTileData = GetTile(x, y);
        newTileData.SetHighlight(on);
        SetTile(x, y, newTileData);
        QueueUpdate();
    }

    public TileData[,] CopyBoard() {
        TileData[,] copy = new TileData[BoardData.GetLength(0), BoardData.GetLength(1)];
        for (int i = 0; i < BoardData.GetLength(0); i++) {
            for (int j = 0; j < BoardData.GetLength(1); j++) {
                copy[i, j] = BoardData[i, j];
            }
        }
        return copy;
    }

    public TileData[,] CopyBoard(TileData[,] reference) {
        TileData[,] copy = new TileData[reference.GetLength(0), reference.GetLength(1)];
        for (int i = 0; i < reference.GetLength(0); i++) {
            for (int j = 0; j < reference.GetLength(1); j++) {
                copy[i, j] = reference[i, j];
            }
        }
        return copy;
    }

    public void MatchToTimeline() {
        BoardData = CopyBoard(_timelineInstance.BoardTimeline.GetCurrentFrame());
        e_OnBoardChange?.Invoke(this);
    }
}
