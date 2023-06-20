using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileData {
    public TileColor Color;
    public bool IsStarred;
    public bool IsHighlighted { get; private set; }
    public TileData(TileColor color, bool isStarred, bool isHighlighted) {
        Color = color;
        IsStarred = isStarred;
        IsHighlighted = isHighlighted;
    }

    public enum TileColor {
        s, // space
        n, // null
        S, // special
        p, // pink
        o, // orange
        y, // yellow
        g, // green
        b, // blue
    }

    /// <summary>
    /// Returns true if the tile is a normal or star tile (not space, null, or special).
    /// </summary>
    public bool IsNormal() {
        if (Color == TileColor.s || Color == TileColor.n || Color == TileColor.S) {
            return false;
        } else {
            return true;
        }
    }

    public void SetHighlight(bool isHighlighted) {
        IsHighlighted = isHighlighted;
    }

    public static TileData p = new TileData(TileColor.p, false, false);
    public static TileData o = new TileData(TileColor.o, false, false);
    public static TileData y = new TileData(TileColor.y, false, false);
    public static TileData g = new TileData(TileColor.g, false, false);
    public static TileData b = new TileData(TileColor.b, false, false);
    public static TileData P = new TileData(TileColor.p, true, false);
    public static TileData O = new TileData(TileColor.o, true, false);
    public static TileData Y = new TileData(TileColor.y, true, false);
    public static TileData G = new TileData(TileColor.g, true, false);
    public static TileData B = new TileData(TileColor.b, true, false);
    public static TileData s = new TileData(TileColor.s, false, false);
    // public static TileData h = new TileData(TileColor.s, false, true);
    public static TileData S = new TileData(TileColor.S, false, false);
    public static TileData n = new TileData(TileColor.n, false, false);
}
