using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static Dictionary<TileType, Color32> s_colorMap = new Dictionary<TileType, Color32>()
    {
        {TileType.Space, new Color32(255, 255, 255, 255)},
        {TileType.Highlight, new Color32(255, 224, 146, 255)},
        {TileType.Pink, new Color32(255, 0, 255, 255)},
        {TileType.Orange, new Color32(255, 128, 0, 255)},
        {TileType.Yellow, new Color32(255, 255, 0, 255)},
        {TileType.Green, new Color32(0, 255, 0, 255)},
        {TileType.Blue, new Color32(0, 0, 255, 255)}
    };
}