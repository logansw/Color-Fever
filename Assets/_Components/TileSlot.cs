using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSlot : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Touchable _touchable;
    [SerializeField] private SpriteRenderer _backgroundSR;

    [Header("External References")]
    [SerializeField] private SpriteRenderer _outlineSR;
    [SerializeField] private SpriteRenderer _shadowSR;

    public delegate void OnTileSelected(TileSlot tileSlot);
    public static OnTileSelected e_OnTileSelected;
    public TileData TileData;
    public TilePool ParentTilePool { get; private set; }
    [HideInInspector] public int Index;

    private void OnEnable() {
        _touchable.e_OnTouched += SelectTile;
    }

    private void OnDisable() {
        _touchable.e_OnTouched -= SelectTile;
    }

    public void Initialize(TilePool tilePool) {
        _spriteRenderer.sprite = null;
        ParentTilePool = tilePool;
        Index = ParentTilePool.Index;
        _backgroundSR.color = new Color32(255, 255, 255, 0);
        _outlineSR.enabled = false;
        _shadowSR.enabled = false;
    }

    private void SelectTile() {
        if (TileData.Equals(TileData.S)) {
            return;
        } else {
            e_OnTileSelected?.Invoke(this);
        }
    }

    public void SetTile(TileData tileData) {
        TileData = tileData;
        _spriteRenderer.sprite = TileManager.s_instance.TileDataToSprite(TileData);
        _spriteRenderer.color = TileManager.s_instance.TileDataToColor(TileData);
        _backgroundSR.color = new Color32(255, 255, 255, 255);
        _spriteRenderer.enabled = true;
    }

    public void Disable() {
        TileData = TileData.n;
        _spriteRenderer.sprite = null;
        _spriteRenderer.color = Color.white;
        _touchable.Disable();
        _backgroundSR.color = new Color32(255, 255, 255, 0);
    }

    public void Enable() {
        _spriteRenderer.sprite = TileManager.s_instance.TileDataToSprite(TileData);
        _spriteRenderer.color = TileManager.s_instance.TileDataToColor(TileData);
        _touchable.enabled = true;
        _touchable.Enable();
        _backgroundSR.color = new Color32(255, 255, 255, 255);
    }

    public void Show() {
        _spriteRenderer.enabled = true;
        _touchable.Enable();
        _backgroundSR.color = new Color32(255, 255, 255, 255);
    }

    public void Hide() {
        _spriteRenderer.enabled = false;
        _touchable.Disable();
        _backgroundSR.color = new Color32(255, 255, 255, 0);
    }

    public void Highlight() {
        _outlineSR.enabled = true;
        _shadowSR.enabled = true;
    }

    public void Unhighlight() {
        _outlineSR.enabled = false;
        _shadowSR.enabled = false;
    }
}
