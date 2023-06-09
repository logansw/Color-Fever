using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePool : MonoBehaviour
{
    public Dictionary<TileType, int> _tilePool;
    private int _totalTiles;
    public TileSlot[] TileSlots;
    public int Index;

    private void Start() {
        _tilePool = new Dictionary<TileType, int>() {
            {TileType.Pink, 18},
            {TileType.PinkStar, 1},
            {TileType.Orange, 18},
            {TileType.OrangeStar, 1},
            {TileType.Yellow, 18},
            {TileType.YellowStar, 1},
            {TileType.Green, 18},
            {TileType.GreenStar, 1},
            {TileType.Blue, 18},
            {TileType.BlueStar, 1},
            {TileType.Special, 2},
        };

        _totalTiles = 97;
    }

    public void Initialize(int index) {
        Index = index;
        foreach (TileSlot tileSlot in TileSlots) {
            tileSlot.Initialize(this);
        }
        TileSlots[0].Disable();
    }

    public void SetRandomTile() {
        TileType tile = DrawRandomTile();
        TileSlots[0].SetTile(tile);
    }

    public TileType DrawRandomTile()
    {
        TileType tile;
        int randomNumber = Random.Range(0, _totalTiles);
        int pink = _tilePool[TileType.Pink];
        int orange = _tilePool[TileType.Orange];
        int yellow = _tilePool[TileType.Yellow];
        int green = _tilePool[TileType.Green];
        int blue = _tilePool[TileType.Blue];
        switch (randomNumber)
        {
            case int n when (n < pink):
                tile = TileType.Pink;
                break;
            case int n when (n < pink + _tilePool[TileType.PinkStar]):
                tile = TileType.PinkStar;
                break;
            case int n when (n < pink + _tilePool[TileType.PinkStar] + orange):
                tile = TileType.Orange;
                break;
            case int n when (n < pink + _tilePool[TileType.PinkStar] + orange + _tilePool[TileType.OrangeStar]):
                tile = TileType.OrangeStar;
                break;
            case int n when (n < pink + _tilePool[TileType.PinkStar] + orange + _tilePool[TileType.OrangeStar] + yellow):
                tile = TileType.Yellow;
                break;
            case int n when (n < pink + _tilePool[TileType.PinkStar] + orange + _tilePool[TileType.OrangeStar] + yellow + _tilePool[TileType.YellowStar]):
                tile = TileType.YellowStar;
                break;
            case int n when (n < pink + _tilePool[TileType.PinkStar] + pink + _tilePool[TileType.OrangeStar] + yellow + _tilePool[TileType.YellowStar] + green):
                tile = TileType.Green;
                break;
            case int n when (n < pink + _tilePool[TileType.PinkStar] + pink + _tilePool[TileType.OrangeStar] + yellow + _tilePool[TileType.YellowStar] + green + _tilePool[TileType.GreenStar]):
                tile = TileType.GreenStar;
                break;
            case int n when (n < pink + _tilePool[TileType.PinkStar] + pink + _tilePool[TileType.OrangeStar] + yellow + _tilePool[TileType.YellowStar] + green + _tilePool[TileType.GreenStar] + blue):
                tile = TileType.Blue;
                break;
            case int n when (n < pink + _tilePool[TileType.PinkStar] + orange + _tilePool[TileType.OrangeStar] + yellow + _tilePool[TileType.YellowStar] + green + _tilePool[TileType.GreenStar] + blue + _tilePool[TileType.BlueStar]):
                tile = TileType.BlueStar;
                break;
            default:
                tile = TileType.Special;
                break;
        }

        if (TileManager.s_instance.TileIsValid(this, tile)) {
            return tile;
        } else {
            return DrawRandomTile();
        }
    }

    public void ReturnTile(TileType tile) {
        _tilePool[tile] += 1;
    }

    public void SetStartingTiles(List<TileType> startTiles) {
        for (int i = 1; i < TileSlots.Length; i++) {
            TileSlots[i].SetTile(startTiles[i-1]);
        }
    }
}
