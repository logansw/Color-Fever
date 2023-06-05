using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private TileType[,] _boardData;
    public delegate void e_OnBoardChange();
    public static event e_OnBoardChange OnBoardChange;

    public void Initialize() {
        _boardData = new TileType[10, 5];
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 5; j++) {
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

    public void DebugPrintBoard() {
        string boardString = "";
        for (int j = 4; j >= 0; j--) {
            for (int i = 0; i < 10; i++) {
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