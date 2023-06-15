using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public static TileManager s_instance;
    [Header("Asset References")]
    [SerializeField] private Sprite TileSpriteSquare;
    [SerializeField] private Sprite TileSpriteStar;
    [SerializeField] private Sprite TileSpriteSpecial;

    public TileSlot SelectedTileSlot;
    [SerializeField] private TilePool[] _tilePools;
    private int[] _tilesRemaining;

    // Events
    public delegate void OnSlotEmptied(int index);
    public static OnSlotEmptied e_OnSlotEmptied;
    public delegate void OnTilePlaced();
    public static OnTilePlaced e_OnTilePlaced;

    public TileType DebugTile;

    private void Awake() {
        s_instance = this;
    }

    public void Initialize() {
        SelectedTileSlot = null;
        _tilesRemaining = new int[_tilePools.Length];
        for (int i = 0; i < _tilePools.Length; i++) {
            _tilePools[i].Initialize(i);
            DrawStartTiles(_tilePools[i]);
        }
        if (ConfigurationManager.s_instance.DebugMode) {
            DebugTile = TileType.p;
        }
    }

    private void OnEnable() {
        TileSlot.e_OnTileSelected += SelectTile;
        SpecialManager.e_OnCornerModeSet += ConfigureForCornerMode;
    }

    private void OnDisable() {
        TileSlot.e_OnTileSelected -= SelectTile;
        SpecialManager.e_OnCornerModeSet -= ConfigureForCornerMode;
    }

    private void SelectTile(TileSlot tileSlot) {
        SelectedTileSlot = tileSlot;
    }

    public void DisableSelectedTile() {
        _tilesRemaining[SelectedTileSlot.ParentTilePool.Index]--;
        if (_tilesRemaining[SelectedTileSlot.ParentTilePool.Index] == 0) {
            e_OnSlotEmptied?.Invoke(SelectedTileSlot.ParentTilePool.Index);
        }
        e_OnTilePlaced?.Invoke();
        SelectedTileSlot.Disable();
        SelectedTileSlot = null;
        foreach (int remaining in _tilesRemaining) {
            if (remaining > 0) {
                return;
            }
        }
    }

    private void DrawStartTiles(TilePool tilePool) {
        List<TileType> startTiles = new List<TileType>();
        Dictionary<TileType, int> tileCounts = new Dictionary<TileType, int>();
        while (startTiles.Count < 4) {
            TileType tile = tilePool.DrawRandomTile();
            if (TileIsValid(tilePool, tile)) {
                startTiles.Add(tile);
                if (tileCounts.ContainsKey(tile)) {
                    tileCounts[tile] += 1;
                } else {
                    tileCounts.Add(tile, 1);
                }

                if (tileCounts[tile] >= 3) {
                    tilePool.ReturnTile(tile);
                    startTiles.Remove(tile);
                    tileCounts[tile] -= 1;
                }
            }
        }
        _tilesRemaining[tilePool.Index] += 4;
        tilePool.SetStartingTiles(startTiles);
    }

    public void DrawNewTiles() {
        foreach (TilePool tilePool in _tilePools) {
            tilePool.SetRandomTile();
            _tilesRemaining[tilePool.Index] = 1;
        }
    }

    // TODO: Complete these rules
    public bool TileIsValid(TilePool tilePool, TileType tile) {
        GameManager gameManager = GameManager.s_instance;

        if (tile != TileType.S) {
            if (tilePool._tilePool[TileType.S] == 2 && gameManager.RoundsRemaining == 6) {
                return false;
            }
            if (tilePool._tilePool[TileType.S] == 1 && gameManager.RoundsRemaining == 4) {
                return false;
            }
        }

        if (tile == TileType.P || tile == TileType.O || tile == TileType.Y || tile == TileType.G || tile == TileType.B) {
            if (gameManager.RoundsRemaining > 18 || gameManager.RoundsRemaining < 5) {
                return false;
            }
        }

        if (tile == TileType.S) {
            if (gameManager.RoundsRemaining > 18 || gameManager.RoundsRemaining < 4) {
                return false;
            }
        }

        return true;
    }

    public Sprite TileTypeToSprite(TileType tileType) {
        switch (tileType) {
            case TileType.p or TileType.o or TileType.y or TileType.g or TileType.b:
                return TileSpriteSquare;
            case TileType.P or TileType.O or TileType.Y or TileType.G or TileType.B:
                return TileSpriteStar;
            case TileType.S:
                return TileSpriteSpecial;
            case TileType.s or TileType.h:
                return TileSpriteSquare;
            default:
                return null;
        }
    }

    public Color32 TileTypeToColor(TileType tileType) {
        switch (tileType) {
            case TileType.p or TileType.P:
                return ColorManager.s_colorMap[TileType.p];
            case TileType.o or TileType.O:
                return ColorManager.s_colorMap[TileType.o];
            case TileType.y or TileType.Y:
                return ColorManager.s_colorMap[TileType.y];
            case TileType.g or TileType.G:
                return ColorManager.s_colorMap[TileType.g];
            case TileType.b or TileType.B:
                return ColorManager.s_colorMap[TileType.b];
            case TileType.h:
                return ColorManager.s_colorMap[TileType.h];
            default:
                return new Color32(255, 255, 255, 255);
        }
    }

    public void EnableCenterSlots() {
        foreach (TilePool tilePool in _tilePools) {
            tilePool.TileSlots[0].Enable();
        }
    }

    /// <summary>
    /// Returns true if the tile can be placed on the board.
    /// This excludes space, null, highlights, and special tiles.
    /// </summary>
    /// <param name="tileType">The tile being measured</param>
    /// <returns>True if normal, false if not.</returns>
    public static bool TileIsNormal(TileType tileType) {
        if (tileType == TileType.s || tileType == TileType.n || tileType == TileType.h || tileType == TileType.S) {
            return false;
        } else {
            return true;
        }
    }

    public static bool TilesChainable(TileType a, TileType b) {
        if (a == TileType.s || b == TileType.s || a == TileType.n || b == TileType.n || a == TileType.h || b == TileType.h || a == TileType.S || b == TileType.S) {
            return false;
        }
        switch (a) {
            case TileType.p or TileType.P:
                return b == TileType.p || b == TileType.P;
            case TileType.o or TileType.O:
                return b == TileType.o || b == TileType.O;
            case TileType.y or TileType.Y:
                return b == TileType.y || b == TileType.Y;
            case TileType.g or TileType.G:
                return b == TileType.g || b == TileType.G;
            case TileType.b or TileType.B:
                return b == TileType.b || b == TileType.B;
        }
        return false;
    }

    private void ConfigureForCornerMode(int index) {
        _tilesRemaining[index] = 1;
    }

    public void DebugForceTilePlacement() {
        e_OnTilePlaced?.Invoke();
        switch (DebugTile) {
            case TileType.p:
                DebugTile = TileType.o;
                break;
            case TileType.o:
                DebugTile = TileType.y;
                break;
            case TileType.y:
                DebugTile = TileType.g;
                break;
            case TileType.g:
                DebugTile = TileType.b;
                break;
            case TileType.b:
                DebugTile = TileType.p;
                break;
        }
    }
}
