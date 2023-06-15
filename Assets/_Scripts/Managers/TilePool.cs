using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePool : MonoBehaviour
{
    public Dictionary<TileType, int> _tilePool;
    private int _totalTiles;
    public TileSlot[] TileSlots;
    public TileSlot[] CornerTileSlots;
    public int Index;
    public delegate void OnSpecialDrawn(int index);
    public static OnSpecialDrawn e_OnSpecialDrawn;

    private void Start() {
        _tilePool = new Dictionary<TileType, int>() {
            {TileType.p, 18},
            {TileType.P, 1},
            {TileType.o, 18},
            {TileType.O, 1},
            {TileType.y, 18},
            {TileType.Y, 1},
            {TileType.g, 18},
            {TileType.G, 1},
            {TileType.b, 18},
            {TileType.B, 1},
            {TileType.S, 2},
        };

        _totalTiles = 97;
    }

    public void Initialize(int index) {
        Index = index;
        foreach (TileSlot tileSlot in TileSlots) {
            tileSlot.Initialize(this);
        }
        TileSlots[0].Disable();
        foreach (TileSlot tileSlot in CornerTileSlots) {
            tileSlot.Initialize(this);
            tileSlot.Disable();
        }
    }

    private void OnEnable() {
        SpecialManager.e_OnCornerModeSet += ShowCornerTiles;
    }

    private void OnDisable() {
        SpecialManager.e_OnCornerModeSet -= ShowCornerTiles;
    }

    public void SetRandomTile() {
        TileType tile = DrawRandomTile();
        TileSlots[0].SetTile(tile);
        if (tile == TileType.S) {
            e_OnSpecialDrawn?.Invoke(Index);
        }
    }

    public TileType DrawRandomTile()
    {
        TileType tile;
        int randomNumber = Random.Range(0, _totalTiles);
        int pink = _tilePool[TileType.p];
        int orange = _tilePool[TileType.o];
        int yellow = _tilePool[TileType.y];
        int green = _tilePool[TileType.g];
        int blue = _tilePool[TileType.b];
        switch (randomNumber) {
            case int n when (n < pink):
                tile = TileType.p;
                break;
            case int n when (n < pink + _tilePool[TileType.P]):
                tile = TileType.P;
                break;
            case int n when (n < pink + _tilePool[TileType.P] + orange):
                tile = TileType.o;
                break;
            case int n when (n < pink + _tilePool[TileType.P] + orange + _tilePool[TileType.O]):
                tile = TileType.O;
                break;
            case int n when (n < pink + _tilePool[TileType.P] + orange + _tilePool[TileType.O] + yellow):
                tile = TileType.y;
                break;
            case int n when (n < pink + _tilePool[TileType.P] + orange + _tilePool[TileType.O] + yellow + _tilePool[TileType.Y]):
                tile = TileType.Y;
                break;
            case int n when (n < pink + _tilePool[TileType.P] + pink + _tilePool[TileType.O] + yellow + _tilePool[TileType.Y] + green):
                tile = TileType.g;
                break;
            case int n when (n < pink + _tilePool[TileType.P] + pink + _tilePool[TileType.O] + yellow + _tilePool[TileType.Y] + green + _tilePool[TileType.G]):
                tile = TileType.G;
                break;
            case int n when (n < pink + _tilePool[TileType.P] + pink + _tilePool[TileType.O] + yellow + _tilePool[TileType.Y] + green + _tilePool[TileType.G] + blue):
                tile = TileType.b;
                break;
            case int n when (n < pink + _tilePool[TileType.P] + orange + _tilePool[TileType.O] + yellow + _tilePool[TileType.Y] + green + _tilePool[TileType.G] + blue + _tilePool[TileType.B]):
                tile = TileType.B;
                break;
            default:
                tile = TileType.S;
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

    private void ShowCornerTiles(int index) {
        if (index != Index) {
            return;
        }
        foreach (TileSlot tileSlot in TileSlots) {
            tileSlot.Disable();
        }
        foreach(TileSlot tileSlot in CornerTileSlots) {
            tileSlot.Enable();
        }
        CornerTileSlots[0].SetTile(TileType.p);
        CornerTileSlots[1].SetTile(TileType.o);
        CornerTileSlots[2].SetTile(TileType.y);
        CornerTileSlots[3].SetTile(TileType.g);
        CornerTileSlots[4].SetTile(TileType.b);
    }
}
