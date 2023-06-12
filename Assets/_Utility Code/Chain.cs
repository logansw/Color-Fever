using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain
{
    public int Length;
    public Vector2Int Origin;

    public Chain(int length, Vector2Int origin) {
        Length = length;
        Origin = origin;
    }
}