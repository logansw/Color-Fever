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
    public delegate void OnSpecialSelected(TileSlot tileSlot);
    public static OnSpecialSelected e_OnSpecialSelected;
    public TileData TileData;
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
        if (TileData.Equals(TileData.S)) {
            e_OnSpecialSelected?.Invoke(this);
        } else {
            e_OnTileSelected?.Invoke(this);
        }
    }

    public void SetTile(TileData tileType) {
        TileData = tileType;
        _spriteRenderer.sprite = TileManager.s_instance.TileDataToSprite(tileType);
        _spriteRenderer.color = TileManager.s_instance.TileDataToColor(tileType);
    }

    public void Disable() {
        TileData = TileData.n;
        _spriteRenderer.sprite = null;
        _spriteRenderer.color = Color.white;
        _touchable.Disable();
    }

    public void Enable() {
        TileData = TileData.s;
        _spriteRenderer.sprite = TileManager.s_instance.TileDataToSprite(TileData);
        _spriteRenderer.color = TileManager.s_instance.TileDataToColor(TileData);
        // _touchable.enabled = true;
        _touchable.Enable();
    }
}
