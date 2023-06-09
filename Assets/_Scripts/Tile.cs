using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Touchable _touchable;
    private Board _parentBoard;
    private int x;
    private int y;

    public void Initialize(Board board, int x, int y) {
        _parentBoard = board;
        this.x = x;
        this.y = y;
    }

    private void OnEnable() {
        _touchable.e_OnTouched += UpdateBoard;
    }

    private void OnDisable() {
        _touchable.e_OnTouched -= UpdateBoard;
    }

    private void UpdateBoard() {
        TileSlot selectedTileSlot = TileManager.s_instance.SelectedTileSlot;
        if (selectedTileSlot == null) {
            return;
        }

        bool result = BoardManager.s_instance.SetTile(_parentBoard, TileManager.s_instance.SelectedTileSlot.TileType, x, y);
        if (result) {
            TileManager.s_instance.DisableSelectedTile();
        } else {
            // Do nothing
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
