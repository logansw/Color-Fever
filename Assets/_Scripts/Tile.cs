using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Touchable _touchable;

    public TileType TileType;
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
        TileType = TileType.s;
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

        bool result = BoardManager.s_instance.SetTile(_parentBoard, TileManager.s_instance.SelectedTileSlot.TileType, X, Y);
        if (result) {
            TileManager.s_instance.DisableSelectedTile();
        } else {
            // Do nothing
        }
    }

    private void UpdateBoardMoveA() {
        if (!TileManager.TileIsNormal(TileType) || (_parentBoard.LowestInColumn(X) != Y + 1 && Y != _parentBoard.Height)) {
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
        if (TileType == TileType.h) {
            Tile original = SpecialManager.s_instance.SelectedTile;
            _parentBoard.SetTile(X, Y, original.TileType);
            _parentBoard.SetTile(original.X, original.Y, TileType.s);
            BoardManager.s_instance.ClearHighlightTiles(_parentBoard.Index);
        }
    }

    private void UpdateBoardSwapA() {
        if (!TileManager.TileIsNormal(TileType)) {
            return;
        }
        SpecialManager.s_instance.SelectedTile = this;
        SpecialManager.s_instance.CurrentSelectionMode = SpecialManager.SelectionMode.SwapB;
    }

    private void UpdateBoardSwapB() {
        if (!TileManager.TileIsNormal(TileType)) {
            return;
        }
        // Deselect
        if (SpecialManager.s_instance.SelectedTile == this) {
            SpecialManager.s_instance.SelectedTile = null;
            SpecialManager.s_instance.CurrentSelectionMode = SpecialManager.SelectionMode.SwapA;
        } else {
            Tile other = SpecialManager.s_instance.SelectedTile;
            TileType thisTileType = this.TileType;
            _parentBoard.SetTile(this.X, this.Y, other.TileType);
            _parentBoard.SetTile(other.X, other.Y, thisTileType);
        }
    }

    private void UpdateBoardRemove() {
        if (TileManager.TileIsNormal(TileType)) {
            _parentBoard.SetTile(X, Y, TileType.s);
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

    public void SetColor(TileType tileType) {
        _spriteRenderer.color = TileManager.s_instance.TileTypeToColor(tileType);
    }

    public void SetSprite(Sprite sprite) {
        _spriteRenderer.sprite = sprite;
    }

    public void SetSprite(TileType tileType) {
        _spriteRenderer.sprite = TileManager.s_instance.TileTypeToSprite(tileType);
    }
}
