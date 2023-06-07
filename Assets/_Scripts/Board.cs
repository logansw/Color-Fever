using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private TileType[,] _boardData;
    public delegate void OnBoardChange(Board board);
    public static event OnBoardChange e_OnBoardChange;
    public int Width;
    public int Height;
    public int Index;

    public void Initialize() {
        Width = 10;
        Height = 5;
        _boardData = new TileType[Width, Height];
        for (int i = 0; i < Width; i++) {
            for (int j = 0; j < Height; j++) {
                _boardData[i, j] = TileType.Space;
            }
        }
    }

    public void SetTile(int x, int y, TileType tileType) {
        _boardData[x, y] = tileType;
    }

    public TileType GetTile(int x, int y) {
        return _boardData[x, y];
    }

    public void QueueUpdate() {
        e_OnBoardChange(this);
    }

    public void DebugPrintBoard() {
        string boardString = "";
        for (int j = 0; j < Height; j++) {
            for (int i = 0; i < Width; i++) {
                switch (_boardData[i, j].ToString()) {
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
