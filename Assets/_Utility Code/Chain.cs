using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain
{
    public int Length;
    public Vector2Int Origin;
    public Link FirstLink;
    public Link LastLink;

    public Chain(int length, Vector2Int origin) {
        Length = length;
        Origin = origin;
    }

    public override string ToString() {
        string output = "Origin: " + Origin + ", Length: " + Length;
        if (FirstLink != null) {
            output += ", FirstLink: " + FirstLink.Position;
        }
        if (LastLink != null) {
            output += ", LastLink: " + LastLink.Position;
        }
        return output;
    }
}