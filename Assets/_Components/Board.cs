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
    public bool UpsideDown;

    public void Initialize() {
        Width = 10;
        Height = 5;
        BoardData = new TileData[Width+1, Height+1];
        for (int i = 1; i <= Width; i++) {
            for (int j = 1; j <= Height; j++) {
                BoardData[i, j] = TileData.s;
            }
        }
        _updateQueued = true;
    }

    private void OnEnable() {
        GameManager.e_OnGameStart += OnGameStart;
    }

    private void OnDisable() {
        GameManager.e_OnGameStart -= OnGameStart;
    }

    private void Update() {
        if (_updateQueued) {
            e_OnBoardChange(this);
            _updateQueued = false;
        }
    }

    private void OnGameStart() {
        HighlightLowest();
        _timelineInstance.QueueAdvance();
        _timelineInstance.QueueLock();
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
                switch (BoardData[i, j].Color) {
                    case TileData.TileColor.s:
                        boardString += BoardData[i,j].IsHighlighted ? "H " : "0 ";
                        break;
                    case TileData.TileColor.r:
                        boardString += "r ";
                        break;
                    case TileData.TileColor.o:
                        boardString += "o ";
                        break;
                    case TileData.TileColor.y:
                        boardString += "y ";
                        break;
                    case TileData.TileColor.g:
                        boardString += "g ";
                        break;
                    case TileData.TileColor.b:
                        boardString += "b ";
                        break;
                    default:
                        boardString += "x ";
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

    private void HighlightLowest() {
        for (int i = 1; i <= Width; i++) {
            BoardData[i, 1].SetHighlight(true);
        }
        _updateQueued = true;
    }

    public bool OpenCorners() {
        if (BoardData[1, 1].Color.Equals(TileData.TileColor.s) ||
            BoardData[Width, 1].Color.Equals(TileData.TileColor.s) ||
            BoardData[1, Height].Color.Equals(TileData.TileColor.s) ||
            BoardData[Width, Height].Color.Equals(TileData.TileColor.s)) {
            return true;
        } else {
            return false;
        }
    }
}
