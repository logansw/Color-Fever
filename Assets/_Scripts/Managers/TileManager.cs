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

    public TileData DebugTile;

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
            DebugTile = TileData.p;
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
        List<TileData> startTiles = new List<TileData>();
        Dictionary<TileData, int> tileCounts = new Dictionary<TileData, int>();
        while (startTiles.Count < 4) {
            TileData tile = tilePool.DrawRandomTile();
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
    public bool TileIsValid(TilePool tilePool, TileData tile) {
        GameManager gameManager = GameManager.s_instance;

        if (!tile.Equals(TileData.S)) {
            if (tilePool._tilePool[TileData.S] == 2 && gameManager.RoundsRemaining == 6) {
                return false;
            }
            if (tilePool._tilePool[TileData.S] == 1 && gameManager.RoundsRemaining == 4) {
                return false;
            }
        }

        if (tile.Equals(TileData.P) || tile.Equals(TileData.O) || tile.Equals(TileData.Y) || tile.Equals(TileData.G) || tile.Equals(TileData.B)) {
            if (gameManager.RoundsRemaining > 18 || gameManager.RoundsRemaining < 5) {
                return false;
            }
        }

        if (tile.Equals(TileData.S)) {
            if (gameManager.RoundsRemaining > 18 || gameManager.RoundsRemaining < 4) {
                return false;
            }
        }

        return true;
    }

    public Sprite TileDataToSprite(TileData tileType) {
        if (tileType.Equals(TileData.p) || tileType.Equals(TileData.o) || tileType.Equals(TileData.y) || tileType.Equals(TileData.g) || tileType.Equals(TileData.b)) {
            return TileSpriteSquare;
        } else if (tileType.Equals(TileData.P) || tileType.Equals(TileData.O) || tileType.Equals(TileData.Y) || tileType.Equals(TileData.G) || tileType.Equals(TileData.B)) {
            return TileSpriteStar;
        } else if (tileType.Equals(TileData.S)) {
            return TileSpriteSpecial;
        } else if (tileType.Equals(TileData.s) || tileType.Equals(TileData.h)) {
            return TileSpriteSquare;
        } else {
            return null;
        }
    }

    public Color32 TileDataToColor(TileData tileType) {
        if (tileType.Equals(TileData.p) || tileType.Equals(TileData.P)) {
            return ColorManager.s_colorMap[TileData.p];
        } else if (tileType.Equals(TileData.o) || tileType.Equals(TileData.O)) {
            return ColorManager.s_colorMap[TileData.o];
        } else if (tileType.Equals(TileData.y) || tileType.Equals(TileData.Y)) {
            return ColorManager.s_colorMap[TileData.y];
        } else if (tileType.Equals(TileData.g) || tileType.Equals(TileData.G)) {
            return ColorManager.s_colorMap[TileData.g];
        } else if (tileType.Equals(TileData.b) || tileType.Equals(TileData.B)) {
            return ColorManager.s_colorMap[TileData.b];
        } else if (tileType.Equals(TileData.h)) {
            return ColorManager.s_colorMap[TileData.h];
        } else {
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
    public static bool TileIsNormal(TileData tileType) {
        if (tileType.Equals(TileData.s) || tileType.Equals(TileData.n) || tileType.Equals(TileData.h) || tileType.Equals(TileData.S)) {
            return false;
        } else {
            return true;
        }
    }

    public static bool TilesChainable(TileData a, TileData b) {
        if (a.Equals(TileData.s) || b.Equals(TileData.s) || a.Equals(TileData.n) || b.Equals(TileData.n) || a.Equals(TileData.h) || b.Equals(TileData.h) || a.Equals(TileData.S) || b.Equals(TileData.S)) {
            return false;
        }
        if (a.Equals(TileData.p) || a.Equals(TileData.P)) {
            return b.Equals(TileData.p) || b.Equals(TileData.P);
        } else if (a.Equals(TileData.o) || a.Equals(TileData.O)) {
            return b.Equals(TileData.o) || b.Equals(TileData.O);
        } else if (a.Equals(TileData.y) || a.Equals(TileData.Y)) {
            return b.Equals(TileData.y) || b.Equals(TileData.Y);
        } else if (a.Equals(TileData.g) || a.Equals(TileData.G)) {
            return b.Equals(TileData.g) || b.Equals(TileData.G);
        } else if (a.Equals(TileData.b) || a.Equals(TileData.B)) {
            return b.Equals(TileData.b) || b.Equals(TileData.B);
        }
        return false;
    }

    private void ConfigureForCornerMode(int index) {
        _tilesRemaining[index] = 1;
    }

    public void DebugForceTilePlacement() {
        e_OnTilePlaced?.Invoke();
        if (DebugTile.Equals(TileData.p)) {
            DebugTile = TileData.o;
        } else if (DebugTile.Equals(TileData.o)) {
            DebugTile = TileData.y;
        } else if (DebugTile.Equals(TileData.y)) {
            DebugTile = TileData.g;
        } else if (DebugTile.Equals(TileData.g)) {
            DebugTile = TileData.b;
        } else if (DebugTile.Equals(TileData.b)) {
            DebugTile = TileData.p;
        }
    }
}
