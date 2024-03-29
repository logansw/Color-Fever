using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Touchable _touchable;

    [Header("External References")]
    public SpriteRenderer OutlineSR;
    public SpriteRenderer ShadowSR;

    public TileData TileData;
    private Board _parentBoard;
    public int X { get; private set; }
    public int Y { get; private set; }
    [HideInInspector] public int Index;

    public void Initialize(Board board, int x, int y)
    {
        _parentBoard = board;
        this.X = x;
        this.Y = y;
        Index = _parentBoard.Index;
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
        switch(SpecialManager.s_instance.CurrentSelectionMode[Index]) {
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
            case SpecialManager.SelectionMode.Corner:
                UpdateBoardCorner();
                break;
            default:
                UpdateBoardNormal();
                Debug.Log("Fallthrough");
                break;
        }
    }

    private void UpdateBoardCorner() {
        TileSlot selectedTileSlot = TileManager.s_instance.SelectedTileSlot[Index];
        if (selectedTileSlot == null) {
            return;
        }

        bool result = BoardManager.s_instance.SetTile(_parentBoard, selectedTileSlot.TileData, X, Y);
        if (result) {
            TileManager.s_instance.DisableSelectedTile(Index);
            SpecialManager.s_instance.SetNormalMode(Index);
            SpecialManager.s_instance.SpecialActionComplete(Index);
            TileManager.s_instance.HideCenterSlot(Index);
            AudioManager.s_instance.PlayButtonSound();
        } else {
            // Do nothing
        }
    }

    private void UpdateBoardNormal() {
        TileSlot selectedTileSlot = TileManager.s_instance.SelectedTileSlot[Index];
        if (selectedTileSlot == null || selectedTileSlot.TileData.Equals(TileData.s) || selectedTileSlot.TileData.Equals(TileData.n)) {
            return;
        }

        bool result = BoardManager.s_instance.SetTile(_parentBoard, selectedTileSlot.TileData, X, Y);
        if (result) {
            TileManager.s_instance.DisableSelectedTile(Index);
            AudioManager.s_instance.PlayButtonSound();
        } else {
            // Do nothing
        }
    }

    private void UpdateBoardMoveA() {
        if (SpecialManager.s_instance.SelectedIndex != Index || !TileData.IsNormal()) {
            return;
        }
        SpecialManager.s_instance.SelectedTile = this;
        SpecialManager.s_instance.CurrentSelectionMode[Index] = SpecialManager.SelectionMode.MoveB;
        SpecialManager.s_instance.InvokeMoveModeBegun(Index);
        AudioManager.s_instance.PlayButtonSound();
    }

    private void UpdateBoardMoveB() {
        if (SpecialManager.s_instance.SelectedTile == this) {
            SpecialManager.s_instance.SelectedTile = null;
            SpecialManager.s_instance.CurrentSelectionMode[Index] = SpecialManager.SelectionMode.MoveA;
            BoardManager.s_instance.ClearHighlightTiles(Index);
            AudioManager.s_instance.PlayButtonSound();
        }
        if (TileData.IsHighlighted && SpecialManager.s_instance.SelectedIndex == Index) {
            Tile original = SpecialManager.s_instance.SelectedTile;
            _parentBoard.SetTile(X, Y, original.TileData);
            _parentBoard.SetTile(original.X, original.Y, TileData.s);
            BoardManager.s_instance.ClearHighlightTiles(Index);
            SpecialManager.s_instance.SetNormalMode(Index);
            SpecialManager.s_instance.SpecialActionComplete(Index);
            TileManager.s_instance.HideCenterSlot(Index);
            AudioManager.s_instance.PlayButtonSound();
        }
    }

    private void UpdateBoardSwapA() {
        if (SpecialManager.s_instance.SelectedIndex != Index || !TileData.IsNormal()) {
            return;
        }
        SpecialManager.s_instance.SelectedTile = this;
        SpecialManager.s_instance.CurrentSelectionMode[Index] = SpecialManager.SelectionMode.SwapB;
        _parentBoard.HighlightTile(X, Y, false);
        AudioManager.s_instance.PlayButtonSound();
    }

    private void UpdateBoardSwapB() {
        if (SpecialManager.s_instance.SelectedIndex != Index || !TileData.IsNormal()) {
            return;
        }
        // Deselect
        if (SpecialManager.s_instance.SelectedTile == this) {
            SpecialManager.s_instance.SelectedTile = null;
            SpecialManager.s_instance.CurrentSelectionMode[Index] = SpecialManager.SelectionMode.SwapA;
            AudioManager.s_instance.PlayButtonSound();
        } else {
            Tile other = SpecialManager.s_instance.SelectedTile;
            TileData thisTileData = this.TileData;
            _parentBoard.SetTile(this.X, this.Y, other.TileData);
            _parentBoard.SetTile(other.X, other.Y, thisTileData);
            SpecialManager.s_instance.SetNormalMode(Index);
            SpecialManager.s_instance.SpecialActionComplete(Index);
            TileManager.s_instance.HideCenterSlot(Index);
            AudioManager.s_instance.PlayButtonSound();
        }
    }

    private void UpdateBoardRemove() {
        if (SpecialManager.s_instance.SelectedIndex != Index) {
            return;
        }

        if (TileData.IsNormal()) {
            _parentBoard.SetTile(X, Y, TileData.s);
            SpecialManager.s_instance.SetNormalMode(Index);
            SpecialManager.s_instance.SpecialActionComplete(Index);
            TileManager.s_instance.HideCenterSlot(Index);
            AudioManager.s_instance.PlayButtonSound();
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

    public void SetColor(TileData TileData) {
        _spriteRenderer.color = TileManager.s_instance.TileDataToColor(TileData);
    }

    public void SetSprite(Sprite sprite) {
        _spriteRenderer.sprite = sprite;
    }

    public void SetSprite(TileData TileData) {
        _spriteRenderer.sprite = TileManager.s_instance.TileDataToSprite(TileData);
    }
}
