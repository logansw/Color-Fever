using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link
{
    public Vector2Int Position;
    public TileData TileData;

    public Link(Vector2Int position, TileData data) {
        Position = position;
        TileData = data;
    }
}
