using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Touchable _touchable;

    public TileData TileData;
    private Board _parentBoard;
    public int X { get; private set; }
    public int Y { get; private set; }

    public void Initialize(Board board, int x, int y)
    {
        _parentBoard = board;
        this.X = x;
        this.Y = y;
        if (ConfigurationManager.s_instance.DebugMode) {
            _touchable.e_OnTouched += DebugUpdateBoard;
        } else {
            _touchable.e_OnTouched += UpdateBoard;
        }
        TileData = TileData.s;
    }

    private void OnDisable() {
        if (ConfigurationManager.s_instance.DebugMode) {
            _touchable.e_OnTouched -= DebugUpdateBoard;
        } else {
            _touchable.e_OnTouched -= UpdateBoard;
        }
    }

    private void UpdateBoard() {
        switch(SpecialManager.s_instance.CurrentSelectionMode) {
            case SpecialManager.SelectionMode.Normal:
                UpdateBoardNormal();
                break;
            case SpecialManager.SelectionMode.MoveA:
                UpdateBoardMoveA();
                break;
            case SpecialManager.SelectionMode.MoveB:
                UpdateBoardMoveB();
                break;
            case SpecialManager.SelectionMode.SwapA:
                UpdateBoardSwapA();
                break;
            case SpecialManager.SelectionMode.SwapB:
                UpdateBoardSwapB();
                break;
            case SpecialManager.SelectionMode.Remove:
                UpdateBoardRemove();
                break;
            default:
                UpdateBoardNormal();
                Debug.Log("Fallthrough");
                break;
        }
    }

    private void UpdateBoardNormal() {
        TileSlot selectedTileSlot = TileManager.s_instance.SelectedTileSlot;
        if (selectedTileSlot == null) {
            return;
        }

        bool result = BoardManager.s_instance.SetTile(_parentBoard, TileManager.s_instance.SelectedTileSlot.TileData, X, Y);
        if (result) {
            TileManager.s_instance.DisableSelectedTile();
        } else {
            // Do nothing
        }
    }

    private void UpdateBoardMoveA() {
        if (!TileData.IsNormal() || (_parentBoard.LowestInColumn(X) != Y + 1 && Y != _parentBoard.Height)) {
            return;
        }
        SpecialManager.s_instance.SelectedTile = this;
        SpecialManager.s_instance.CurrentSelectionMode = SpecialManager.SelectionMode.MoveB;
        SpecialManager.s_instance.InvokeMoveModeBegun(_parentBoard.Index);
    }

    private void UpdateBoardMoveB() {
        if (SpecialManager.s_instance.SelectedTile == this) {
            SpecialManager.s_instance.SelectedTile = null;
            SpecialManager.s_instance.CurrentSelectionMode = SpecialManager.SelectionMode.MoveA;
        }
        if (TileData.Equals(TileData.h)) {
            Tile original = SpecialManager.s_instance.SelectedTile;
            _parentBoard.SetTile(X, Y, original.TileData);
            _parentBoard.SetTile(original.X, original.Y, TileData.s);
            BoardManager.s_instance.ClearHighlightTiles(_parentBoard.Index);
        }
    }

    private void UpdateBoardSwapA() {
        if (!TileData.IsNormal()) {
            return;
        }
        SpecialManager.s_instance.SelectedTile = this;
        SpecialManager.s_instance.CurrentSelectionMode = SpecialManager.SelectionMode.SwapB;
    }

    private void UpdateBoardSwapB() {
        if (!TileData.IsNormal()) {
            return;
        }
        // Deselect
        if (SpecialManager.s_instance.SelectedTile == this) {
            SpecialManager.s_instance.SelectedTile = null;
            SpecialManager.s_instance.CurrentSelectionMode = SpecialManager.SelectionMode.SwapA;
        } else {
            Tile other = SpecialManager.s_instance.SelectedTile;
            TileData thisTileData = this.TileData;
            _parentBoard.SetTile(this.X, this.Y, other.TileData);
            _parentBoard.SetTile(other.X, other.Y, thisTileData);
        }
    }

    private void UpdateBoardRemove() {
        if (TileData.IsNormal()) {
            _parentBoard.SetTile(X, Y, TileData.s);
        }
    }

    private void DebugUpdateBoard() {
        bool result = BoardManager.s_instance.SetTile(_parentBoard, TileManager.s_instance.DebugTile, X, Y);
        if (result) {
            TileManager.s_instance.DebugForceTilePlacement();
        }
    }

    public void SetColor(Color32 color) {
        _spriteRenderer.color = color;
    }

    public void SetColor(TileData tileType) {
        _spriteRenderer.color = TileManager.s_instance.TileDataToColor(tileType);
    }

    public void SetSprite(Sprite sprite) {
        _spriteRenderer.sprite = sprite;
    }

    public void SetSprite(TileData tileType) {
        _spriteRenderer.sprite = TileManager.s_instance.TileDataToSprite(tileType);
    }
}
