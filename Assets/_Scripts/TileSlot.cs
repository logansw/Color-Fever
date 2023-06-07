using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSlot : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Touchable _touchable;

    public TileType TileType;

    private void OnEnable() {
        _touchable.e_OnTouched += SelectTile;
    }

    private void OnDisable() {
        _touchable.e_OnTouched -= SelectTile;
    }

    private void SelectTile() {
        TileManager.s_instance.SelectedTileSlot = this;
    }

    public void SetTile(TileType tileType) {
        TileType = tileType;
        _spriteRenderer.sprite = TileManager.s_instance.TileTypeToSprite(tileType);
        _spriteRenderer.color = TileManager.s_instance.TileTypeToColor(tileType);
    }
}
