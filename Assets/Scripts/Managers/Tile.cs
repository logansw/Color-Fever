using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _boxCollider2D;

    public void SetColor(TileType tileType) {
        _spriteRenderer.color = ColorManager.s_colorMap[tileType];
    }
}
