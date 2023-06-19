using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static Dictionary<TileData, Color32> s_colorMap = new Dictionary<TileData, Color32>()
    {
        {TileData.s, new Color32(255, 255, 255, 255)},
        // {TileData.h, new Color32(255, 224, 146, 255)},
        {TileData.p, new Color32(255, 0, 255, 255)},
        {TileData.o, new Color32(255, 128, 0, 255)},
        {TileData.y, new Color32(255, 255, 0, 255)},
        {TileData.g, new Color32(0, 255, 0, 255)},
        {TileData.b, new Color32(0, 0, 255, 255)}
    };
}