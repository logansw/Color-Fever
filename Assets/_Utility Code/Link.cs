using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link
{
    public Vector2Int Position;
    public TileType TileType;

    public Link(Vector2Int position, TileType type) {
        Position = position;
        TileType = type;
    }
}
