using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSlot : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Touchable _touchable;

    public delegate void OnTileSelected(TileSlot tileSlot);
    public static OnTileSelected e_OnTileSelected;
    public TileType TileType;
    public TilePool ParentTilePool { get; private set; }

    private void OnEnable() {
        _touchable.e_OnTouched += SelectTile;
    }

    private void OnDisable() {
        _touchable.e_OnTouched -= SelectTile;
    }

    public void Initialize(TilePool tilePool) {
        _spriteRenderer.sprite = null;
        ParentTilePool = tilePool;
    }

    private void SelectTile() {
        e_OnTileSelected?.Invoke(this);
    }

    public void SetTile(TileType tileType) {
        TileType = tileType;
        _spriteRenderer.sprite = TileManager.s_instance.TileTypeToSprite(tileType);
        _spriteRenderer.color = TileManager.s_instance.TileTypeToColor(tileType);
    }

    public void Disable() {
        TileType = TileType.Null;
        _spriteRenderer.sprite = null;
        _spriteRenderer.color = Color.white;
        _touchable.Disable();
    }

    public void Enable() {
        TileType = TileType.Space;
        _spriteRenderer.sprite = TileManager.s_instance.TileTypeToSprite(TileType);
        _spriteRenderer.color = TileManager.s_instance.TileTypeToColor(TileType);
        // _touchable.enabled = true;
        _touchable.Enable();
    }
}
