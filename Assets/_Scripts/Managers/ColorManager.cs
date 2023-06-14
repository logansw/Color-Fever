using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static Dictionary<TileType, Color32> s_colorMap = new Dictionary<TileType, Color32>()
    {
        {TileType.s, new Color32(255, 255, 255, 255)},
        {TileType.h, new Color32(255, 224, 146, 255)},
        {TileType.p, new Color32(255, 0, 255, 255)},
        {TileType.o, new Color32(255, 128, 0, 255)},
        {TileType.y, new Color32(255, 255, 0, 255)},
        {TileType.g, new Color32(0, 255, 0, 255)},
        {TileType.b, new Color32(0, 0, 255, 255)}
    };
}