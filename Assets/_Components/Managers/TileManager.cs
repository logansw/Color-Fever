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
    public int[] TilesRemaining;
    public bool IsSpecial;
    public TileData ForcedDraw;

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
        TilesRemaining = new int[_tilePools.Length];
        ForcedDraw = TileData.n;
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
        TileSlot.e_OnSpecialSelected += SelectTile;
        SpecialManager.e_OnCornerModeSet += ConfigureForCornerMode;
        TilePool.e_OnSpecialDrawn += SetIsSpecial;
        TilePool.e_OnNormalDrawn += SetIsNormal;
    }

    private void OnDisable() {
        TileSlot.e_OnTileSelected -= SelectTile;
        TileSlot.e_OnSpecialSelected -= SelectTile;
        SpecialManager.e_OnCornerModeSet -= ConfigureForCornerMode;
        TilePool.e_OnSpecialDrawn -= SetIsSpecial;
        TilePool.e_OnNormalDrawn -= SetIsNormal;
    }

    private void SetIsSpecial(int index) {
        IsSpecial = true;
    }

    private void SetIsNormal(int index) {
        IsSpecial = false;
    }

    private void SelectTile(TileSlot tileSlot) {
        SelectedTileSlot = tileSlot;
    }

    public void DisableSelectedTile() {
        if (SelectedTileSlot == null) {
            return;
        }
        TilesRemaining[SelectedTileSlot.ParentTilePool.Index]--;
        if (TilesRemaining[SelectedTileSlot.ParentTilePool.Index] == 0) {
            e_OnSlotEmptied?.Invoke(SelectedTileSlot.ParentTilePool.Index);
        }
        e_OnTilePlaced?.Invoke();
        SelectedTileSlot.Disable();
        SelectedTileSlot = null;
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
        TilesRemaining[tilePool.Index] += 4;
        tilePool.SetStartingTiles(startTiles);
    }

    public void DrawNewTiles() {
        foreach (TilePool tilePool in _tilePools) {
            tilePool.SetRandomTile();
            TilesRemaining[tilePool.Index] = 1;
        }
    }

    // TODO: Complete these rules
    public bool TileIsValid(TilePool tilePool, TileData tile) {
        GameManager gameManager = GameManager.s_instance;

        if (ConfigurationManager.s_instance.DisableValidTileChecker && gameManager.RoundsRemaining != 34) {
            return true;
        }

        if (!tile.Equals(TileData.S)) {
            if (tilePool._tilePool[TileData.S] == 2 && gameManager.RoundsRemaining == 6) {
                ForcedDraw = TileData.S;
                return false;
            }
            if (tilePool._tilePool[TileData.S] == 1 && gameManager.RoundsRemaining == 4) {
                ForcedDraw = TileData.S;
                return false;
            }
        }

        if (tile.IsStarred) {
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

    public Sprite TileDataToSprite(TileData TileData) {
        if (TileData.Equals(TileData.p) || TileData.Equals(TileData.o) || TileData.Equals(TileData.y) || TileData.Equals(TileData.g) || TileData.Equals(TileData.b)) {
            return TileSpriteSquare;
        } else if (TileData.IsStarred) {
            return TileSpriteStar;
        } else if (TileData.Equals(TileData.S)) {
            return TileSpriteSpecial;
        } else if (TileData.Equals(TileData.s) || TileData.Equals(TileData.h)) {
            return TileSpriteSquare;
        } else {
            return null;
        }
    }

    public Color32 TileDataToColor(TileData TileData) {
        if (TileData.Color == TileData.TileColor.p) {
            return ColorManager.s_colorMap[TileData.p];
        } else if (TileData.Color == TileData.TileColor.o) {
            return ColorManager.s_colorMap[TileData.o];
        } else if (TileData.Color == TileData.TileColor.y) {
            return ColorManager.s_colorMap[TileData.y];
        } else if (TileData.Color == TileData.TileColor.g) {
            return ColorManager.s_colorMap[TileData.g];
        } else if (TileData.Color == TileData.TileColor.b) {
            return ColorManager.s_colorMap[TileData.b];
        } else if (TileData.Equals(TileData.h)) {
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

    public static bool TilesChainable(TileData a, TileData b) {
        if (!a.IsNormal() || !b.IsNormal()) {
            return false;
        }
        if (a.Color == TileData.TileColor.p) {
            return b.Color == TileData.TileColor.p;
        } else if (a.Color == TileData.TileColor.o) {
            return b.Color == TileData.TileColor.o;
        } else if (a.Color == TileData.TileColor.y) {
            return b.Color == TileData.TileColor.y;
        } else if (a.Color == TileData.TileColor.g) {
            return b.Color == TileData.TileColor.g;
        } else if (a.Color == TileData.TileColor.b) {
            return b.Color == TileData.TileColor.b;
        }
        return false;
    }

    private void ConfigureForCornerMode(int index) {
        TilesRemaining[index] = 1;
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
