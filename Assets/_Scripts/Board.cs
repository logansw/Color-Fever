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

    public void Initialize() {
        Width = 10;
        Height = 5;
        BoardData = new TileData[Width+1, Height+1];
        for (int i = 1; i <= Width; i++) {
            for (int j = 1; j <= Height; j++) {
                if (j == 1) {
                    BoardData[i, j] = TileData.h;
                } else {
                    BoardData[i, j] = TileData.s;
                }
            }
        }
    }

    private void OnEnable() {
        SpecialManager.e_OnCornerModeSet += HighlightCorners;
    }

    private void OnDisable() {
        SpecialManager.e_OnCornerModeSet -= HighlightCorners;
    }

    public void SetTile(int x, int y, TileData tileType) {
        if (x < 0 || x >= Width+1 || y < 0 || y >= Height+1) { return; }
        BoardData[x, y] = tileType;
        TileObjects[x, y].TileData = tileType;
        QueueUpdate();
    }

    public TileData GetTile(int x, int y) {
        return BoardData[x, y];
    }

    public void QueueUpdate() {
        e_OnBoardChange(this);
    }

    public int LowestInColumn(int x) {
        if (x >= Width+1) { return -1; }

        for (int j = 1; j < Height+1; j++) {
            if (BoardData[x, j].Equals(TileData.s)) {
                return j;
            }
        }
        return -1;
    }

    private void HighlightCorners(int index) {
        if (index != Index) {
            return;
        }
        if (BoardData[1, 1].Equals(TileData.s)) {
            BoardData[1, 1] = TileData.h;
        }
        if (BoardData[1, Height].Equals(TileData.s)) {
            BoardData[1, Height] = TileData.h;
        }
        if (BoardData[Width, 1].Equals(TileData.s)) {
            BoardData[Width, 1] = TileData.h;
        }
        if (BoardData[Width, Height].Equals(TileData.s)) {
            BoardData[Width, Height] = TileData.h;
        }
        QueueUpdate();
    }

    public void DebugPrintBoard() {
        string boardString = "";
        for (int j = Height; j > 0; j--) {
            for (int i = 1; i <= Width; i++) {
                switch (BoardData[i, j].ToString()) {
                    case "Space":
                        boardString += "0 ";
                        break;
                    case "Pink":
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
}
