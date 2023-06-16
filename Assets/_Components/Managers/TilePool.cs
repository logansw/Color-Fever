using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TilePool : MonoBehaviour
{
    public Dictionary<TileData, int> _tilePool;
    private int _totalTiles;
    public TileSlot[] TileSlots;
    public TileSlot[] CornerTileSlots;
    public int Index;
    public delegate void OnSpecialDrawn(int index);
    public static OnSpecialDrawn e_OnSpecialDrawn;
    public delegate void OnNormalDrawn(int index);
    public static OnNormalDrawn e_OnNormalDrawn;

    private void Start() {
        _tilePool = new Dictionary<TileData, int>() {
            {TileData.p, 18},
            {TileData.P, 1},
            {TileData.o, 18},
            {TileData.O, 1},
            {TileData.y, 18},
            {TileData.Y, 1},
            {TileData.g, 18},
            {TileData.G, 1},
            {TileData.b, 18},
            {TileData.B, 1},
            {TileData.S, 2},
        };

        foreach (TileData tileData in _tilePool.Keys) {
            _totalTiles += _tilePool[tileData];
        }
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
        TileManager.e_OnSlotEmptied += HideCornerTiles;
    }

    private void OnDisable() {
        SpecialManager.e_OnCornerModeSet -= ShowCornerTiles;
        TileManager.e_OnSlotEmptied -= HideCornerTiles;
    }

    public void SetRandomTile() {
        TileData tile = DrawRandomTile();
        TileSlots[0].SetTile(tile);
        if (tile.Color == TileData.TileColor.S) {
            e_OnSpecialDrawn?.Invoke(Index);
        } else {
            e_OnNormalDrawn?.Invoke(Index);
        }
    }

    public TileData DrawRandomTile() {
        TileData tile;
        int randomNumber = Random.Range(0, _totalTiles);
        int pink = _tilePool[TileData.p];
        int orange = _tilePool[TileData.o];
        int yellow = _tilePool[TileData.y];
        int green = _tilePool[TileData.g];
        int blue = _tilePool[TileData.b];
        switch (randomNumber) {
            case int n when (n < pink):
                tile = TileData.p;
                break;
            case int n when (n < pink + _tilePool[TileData.P]):
                tile = TileData.P;
                break;
            case int n when (n < pink + _tilePool[TileData.P] + orange):
                tile = TileData.o;
                break;
            case int n when (n < pink + _tilePool[TileData.P] + orange + _tilePool[TileData.O]):
                tile = TileData.O;
                break;
            case int n when (n < pink + _tilePool[TileData.P] + orange + _tilePool[TileData.O] + yellow):
                tile = TileData.y;
                break;
            case int n when (n < pink + _tilePool[TileData.P] + orange + _tilePool[TileData.O] + yellow + _tilePool[TileData.Y]):
                tile = TileData.Y;
                break;
            case int n when (n < pink + _tilePool[TileData.P] + pink + _tilePool[TileData.O] + yellow + _tilePool[TileData.Y] + green):
                tile = TileData.g;
                break;
            case int n when (n < pink + _tilePool[TileData.P] + pink + _tilePool[TileData.O] + yellow + _tilePool[TileData.Y] + green + _tilePool[TileData.G]):
                tile = TileData.G;
                break;
            case int n when (n < pink + _tilePool[TileData.P] + pink + _tilePool[TileData.O] + yellow + _tilePool[TileData.Y] + green + _tilePool[TileData.G] + blue):
                tile = TileData.b;
                break;
            case int n when (n < pink + _tilePool[TileData.P] + orange + _tilePool[TileData.O] + yellow + _tilePool[TileData.Y] + green + _tilePool[TileData.G] + blue + _tilePool[TileData.B]):
                tile = TileData.B;
                break;
            default:
                tile = TileData.S;
                break;
        }

        if (TileManager.s_instance.TileIsValid(this, tile)) {
            // TODO: Test that this subtraction works properly
            _tilePool[tile]--;
            Assert.AreNotEqual(-1, _tilePool[tile]);
            return tile;
        } else {
            return DrawRandomTile();
        }
    }

    public void ReturnTile(TileData tile) {
        _tilePool[tile] += 1;
    }

    public void SetStartingTiles(List<TileData> startTiles) {
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
        CornerTileSlots[0].SetTile(TileData.p);
        CornerTileSlots[1].SetTile(TileData.o);
        CornerTileSlots[2].SetTile(TileData.y);
        CornerTileSlots[3].SetTile(TileData.g);
        CornerTileSlots[4].SetTile(TileData.b);
        TileManager.s_instance.TilesRemaining[Index] = 1;
        SpecialManager.s_instance.ReadyToContinue = true;
    }

    private void HideCornerTiles(int index) {
        if (index != Index) {
            return;
        }
        foreach (TileSlot tileSlot in CornerTileSlots) {
            tileSlot.Disable();
        }
    }
}
