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
            DebugTile = TileType.Pink;
        }
    }

    private void OnEnable() {
        TileSlot.e_OnTileSelected += SelectTile;
    }

    private void OnDisable() {
        TileSlot.e_OnTileSelected -= SelectTile;
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

        if (tile != TileType.Special) {
            if (tilePool._tilePool[TileType.Special] == 2 && gameManager.RoundsRemaining == 6) {
                return false;
            }
            if (tilePool._tilePool[TileType.Special] == 1 && gameManager.RoundsRemaining == 4) {
                return false;
            }
        }

        if (tile == TileType.PinkStar || tile == TileType.OrangeStar || tile == TileType.YellowStar || tile == TileType.GreenStar || tile == TileType.BlueStar) {
            if (gameManager.RoundsRemaining > 18 || gameManager.RoundsRemaining < 5) {
                return false;
            }
        }

        if (tile == TileType.Special) {
            if (gameManager.RoundsRemaining > 18 || gameManager.RoundsRemaining < 4) {
                return false;
            }
        }

        return true;
    }

    public Sprite TileTypeToSprite(TileType tileType) {
        switch (tileType) {
            case TileType.Pink or TileType.Orange or TileType.Yellow or TileType.Green or TileType.Blue:
                return TileSpriteSquare;
            case TileType.PinkStar or TileType.OrangeStar or TileType.YellowStar or TileType.GreenStar or TileType.BlueStar:
                return TileSpriteStar;
            case TileType.Special:
                return TileSpriteSpecial;
            case TileType.Space or TileType.Highlight:
                return TileSpriteSquare;
            default:
                return null;
        }
    }

    public Color32 TileTypeToColor(TileType tileType) {
        switch (tileType) {
            case TileType.Pink or TileType.PinkStar:
                return ColorManager.s_colorMap[TileType.Pink];
            case TileType.Orange or TileType.OrangeStar:
                return ColorManager.s_colorMap[TileType.Orange];
            case TileType.Yellow or TileType.YellowStar:
                return ColorManager.s_colorMap[TileType.Yellow];
            case TileType.Green or TileType.GreenStar:
                return ColorManager.s_colorMap[TileType.Green];
            case TileType.Blue or TileType.BlueStar:
                return ColorManager.s_colorMap[TileType.Blue];
            case TileType.Highlight:
                return ColorManager.s_colorMap[TileType.Highlight];
            default:
                return new Color32(255, 255, 255, 255);
        }
    }

    public void EnableCenterSlots() {
        foreach (TilePool tilePool in _tilePools) {
            tilePool.TileSlots[0].Enable();
        }
    }

    public static bool TileIsNormal(TileType tileType) {
        if (tileType == TileType.Space || tileType == TileType.Null || tileType == TileType.Highlight || tileType == TileType.Special) {
            return false;
        } else {
            return true;
        }
    }

    public void DebugForceTilePlacement() {
        e_OnTilePlaced?.Invoke();
        switch (DebugTile) {
            case TileType.Pink:
                DebugTile = TileType.Orange;
                break;
            case TileType.Orange:
                DebugTile = TileType.Yellow;
                break;
            case TileType.Yellow:
                DebugTile = TileType.Green;
                break;
            case TileType.Green:
                DebugTile = TileType.Blue;
                break;
            case TileType.Blue:
                DebugTile = TileType.Pink;
                break;
        }
    }
}
