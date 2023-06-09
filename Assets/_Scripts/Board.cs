using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public TileType[,] BoardData;
    public Tile[,] TileObjects;
    public delegate void OnBoardChange(Board board);
    public static event OnBoardChange e_OnBoardChange;
    public int Width;
    public int Height;
    public int Index;

    public void Initialize() {
        Width = 10;
        Height = 5;
        BoardData = new TileType[Width+1, Height+1];
        for (int i = 1; i <= Width; i++) {
            for (int j = 1; j <= Height; j++) {
                if (j == 1) {
                    BoardData[i, j] = TileType.Highlight;
                } else {
                    BoardData[i, j] = TileType.Space;
                }
            }
        }
    }

    public void SetTile(int x, int y, TileType tileType) {
        if (x < 0 || x >= Width+1 || y < 0 || y >= Height+1) { return; }
        BoardData[x, y] = tileType;
        QueueUpdate();
    }

    public TileType GetTile(int x, int y) {
        return BoardData[x, y];
    }

    public void QueueUpdate() {
        e_OnBoardChange(this);
    }

    public int LowestInColumn(int x) {
        if (x >= Width+1) { return -1; }

        for (int j = 1; j < Height+1; j++) {
            if (BoardData[x, j] == TileType.Space) {
                return j;
            }
        }
        return -1;
    }

    public void DebugPrintBoard() {
        string boardString = "";
        for (int j = 0; j < Height; j++) {
            for (int i = 0; i < Width; i++) {
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
                }
            }
            boardString += "\n";
        }
        Debug.Log(boardString);
    }
}
