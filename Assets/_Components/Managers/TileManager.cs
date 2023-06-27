using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour
{
    public static TileManager s_instance;
    [Header("Asset References")]
    [SerializeField] private Sprite TileSpriteSquare;
    [SerializeField] private Sprite TileSpriteStar;
    [SerializeField] private Sprite TileSpriteSpecial;

    public TileSlot[] SelectedTileSlot;
    [SerializeField] private TilePool[] _tilePools;
    public int[] TilesRemaining;
    /// True if the last tile drawn was a special tile.
    public bool IsSpecial;
    public TileData ForcedDraw;
    [SerializeField] private TimelineInstance[] _timelineInstances;
    [SerializeField] private SpecialMenu[] _specialMenus;
    private Dictionary<TileData, int> _tileCounts;

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
        SelectedTileSlot = new TileSlot[_tilePools.Length];
        for (int i = 0; i < SelectedTileSlot.Length; i++) {
            SelectedTileSlot[i] = null;
        }
        ForcedDraw = TileData.n;
        TilesRemaining = new int[_tilePools.Length];
        InitializeTilePools();
        if (ConfigurationManager.s_instance.DebugMode) {
            DebugTile = TileData.r;
        }
    }

    private void OnEnable() {
        TileSlot.e_OnTileSelected += SelectTile;
        SpecialManager.e_OnCornerModeSet += ConfigureForCornerMode;
        SpecialManager.e_OnNormalModeSet += EnableCenterSlots;
    }

    private void OnDisable() {
        TileSlot.e_OnTileSelected -= SelectTile;
        SpecialManager.e_OnCornerModeSet -= ConfigureForCornerMode;
        SpecialManager.e_OnNormalModeSet -= EnableCenterSlots;
    }

    private void Update() {
        IsSpecial = false;
        // If any tilepool is special
        foreach (TilePool tilePool in _tilePools) {
            if (tilePool.IsSpecial) {
                IsSpecial = true;
            }
        }
        // Make all the tilepools special
        if (IsSpecial) {
            foreach (TilePool tilePool in _tilePools) {
                tilePool.IsSpecial = true;
            }
        }
    }

    private void InitializeTilePools() {
        if (SceneManager.GetActiveScene().name.Equals("Single")) {
            _tileCounts = new Dictionary<TileData, int>() {
                {TileData.r, 9},
                {TileData.R, 1},
                {TileData.o, 9},
                {TileData.O, 1},
                {TileData.y, 9},
                {TileData.Y, 1},
                {TileData.g, 9},
                {TileData.G, 1},
                {TileData.b, 9},
                {TileData.B, 1},
                {TileData.S, 2},
            };
            _tilePools[0].Initialize(0, _tileCounts);
            DrawStartTiles(_tilePools[0]);
            _tilePools[0].HideStartingTiles();
        } else if (SceneManager.GetActiveScene().name.Equals("Double")) {
            _tileCounts = new Dictionary<TileData, int>() {
                {TileData.r, 10},
                {TileData.R, 1},
                {TileData.o, 10},
                {TileData.O, 1},
                {TileData.y, 10},
                {TileData.Y, 1},
                {TileData.g, 10},
                {TileData.G, 1},
                {TileData.b, 10},
                {TileData.B, 1},
                {TileData.S, 0},
            };

            Dictionary<TileData, int> poolA = new Dictionary<TileData, int>() {
                {TileData.r, 4},
                {TileData.R, 0},
                {TileData.o, 4},
                {TileData.O, 0},
                {TileData.y, 4},
                {TileData.Y, 0},
                {TileData.g, 4},
                {TileData.G, 0},
                {TileData.b, 4},
                {TileData.B, 0},
                {TileData.S, 2},
            };

            Dictionary<TileData, int> poolB = new Dictionary<TileData, int>() {
                {TileData.r, 4},
                {TileData.R, 0},
                {TileData.o, 4},
                {TileData.O, 0},
                {TileData.y, 4},
                {TileData.Y, 0},
                {TileData.g, 4},
                {TileData.G, 0},
                {TileData.b, 4},
                {TileData.B, 0},
                {TileData.S, 2},
            };

            int tilesRemaining = GetTotalCounts(_tileCounts);
            while (tilesRemaining > 27)
            {
                TileData tile;
                int randomNumber = Random.Range(0, tilesRemaining);
                int red = _tileCounts[TileData.r];
                int orange = _tileCounts[TileData.o];
                int yellow = _tileCounts[TileData.y];
                int green = _tileCounts[TileData.g];
                int blue = _tileCounts[TileData.b];
                switch (randomNumber)
                {
                    case int n when (n < red):
                        tile = TileData.r;
                        break;
                    case int n when (n < red + _tileCounts[TileData.R]):
                        tile = TileData.R;
                        break;
                    case int n when (n < red + _tileCounts[TileData.R] + orange):
                        tile = TileData.o;
                        break;
                    case int n when (n < red + _tileCounts[TileData.R] + orange + _tileCounts[TileData.O]):
                        tile = TileData.O;
                        break;
                    case int n when (n < red + _tileCounts[TileData.R] + orange + _tileCounts[TileData.O] + yellow):
                        tile = TileData.y;
                        break;
                    case int n when (n < red + _tileCounts[TileData.R] + orange + _tileCounts[TileData.O] + yellow + _tileCounts[TileData.Y]):
                        tile = TileData.Y;
                        break;
                    case int n when (n < red + _tileCounts[TileData.R] + red + _tileCounts[TileData.O] + yellow + _tileCounts[TileData.Y] + green):
                        tile = TileData.g;
                        break;
                    case int n when (n < red + _tileCounts[TileData.R] + red + _tileCounts[TileData.O] + yellow + _tileCounts[TileData.Y] + green + _tileCounts[TileData.G]):
                        tile = TileData.G;
                        break;
                    case int n when (n < red + _tileCounts[TileData.R] + red + _tileCounts[TileData.O] + yellow + _tileCounts[TileData.Y] + green + _tileCounts[TileData.G] + blue):
                        tile = TileData.b;
                        break;
                    case int n when (n < red + _tileCounts[TileData.R] + orange + _tileCounts[TileData.O] + yellow + _tileCounts[TileData.Y] + green + _tileCounts[TileData.G] + blue + _tileCounts[TileData.B]):
                        tile = TileData.B;
                        break;
                    default:
                        tile = TileData.S;
                        break;
                }
                tilesRemaining--;
                _tileCounts[tile]--;
                poolA[tile]++;
            }

            foreach (TileData tileData in _tileCounts.Keys) {
                poolB[tileData] += _tileCounts[tileData];
            }

            _tilePools[0].Initialize(0, poolA);
            DrawStartTiles(_tilePools[0]);
            _tilePools[0].HideStartingTiles();
            _tilePools[1].Initialize(1, poolB);
            DrawStartTiles(_tilePools[1]);
            _tilePools[1].HideStartingTiles();
        }
    }

    private int GetTotalCounts(Dictionary<TileData, int> tileCounts) {
        int total = 0;
        foreach (TileData tileData in tileCounts.Keys) {
            total += tileCounts[tileData];
        }
        return total;
    }

    private void SelectTile(TileSlot tileSlot) {
        SelectedTileSlot[tileSlot.Index] = tileSlot;
        foreach (TilePool tilePool in _tilePools) {
            foreach (TileSlot slot in tilePool.TileSlots) {
                slot.Unhighlight();
            }
        }
        tileSlot.Highlight();
    }

    public void DisableSelectedTile(int index) {
        if (SelectedTileSlot[index] == null) {
            return;
        }
        TilesRemaining[index]--;
        if (TilesRemaining[index] == 0) {
            e_OnSlotEmptied?.Invoke(index);
        }
        e_OnTilePlaced?.Invoke();
        SelectedTileSlot[index].Disable();
        SelectedTileSlot[index].Unhighlight();
        SelectedTileSlot[index] = null;
    }

    private void DrawStartTiles(TilePool tilePool) {
        List<TileData> startTiles = new List<TileData>();
        while (startTiles.Count < 4) {
            TileData tile = tilePool.DrawNextTile();
            startTiles.Add(tile);
        }
        TilesRemaining[tilePool.Index] += 4;
        tilePool.SetStartingTiles(startTiles);
    }

    public void DrawNewTiles() {
        foreach (TilePool tilePool in _tilePools) {
            TileData tile = tilePool.SetNextTile();
            TilesRemaining[tilePool.Index] = 1;
            AutoSetSelected(tilePool.Index);
        }
    }

    public void AutoSetSelected(int index) {
        TileData tileData = _tilePools[index].TileSlots[0].TileData;
        if (!tileData.Equals(TileData.S)) {
            SelectedTileSlot[index] = _tilePools[index].TileSlots[0];
            _tilePools[index].TileSlots[0].TileData = tileData;
        }
    }

    public Sprite TileDataToSprite(TileData tileData) {
        if (tileData.IsStarred) {
            return TileSpriteStar;
        } else if (tileData.Color == TileData.TileColor.r || tileData.Color == TileData.TileColor.o ||
            tileData.Color == TileData.TileColor.y || tileData.Color == TileData.TileColor.g ||
            tileData.Color == TileData.TileColor.b || tileData.Color == TileData.TileColor.s) {
            return TileSpriteSquare;
        } else if (tileData.Color == TileData.TileColor.S) {
            return TileSpriteSpecial;
        } else {
            return null;
        }
    }

    public Color32 TileDataToColor(TileData TileData) {
        if (TileData.Color == TileData.TileColor.r) {
            return ColorManager.s_colorMap[TileData.r];
        } else if (TileData.Color == TileData.TileColor.o) {
            return ColorManager.s_colorMap[TileData.o];
        } else if (TileData.Color == TileData.TileColor.y) {
            return ColorManager.s_colorMap[TileData.y];
        } else if (TileData.Color == TileData.TileColor.g) {
            return ColorManager.s_colorMap[TileData.g];
        } else if (TileData.Color == TileData.TileColor.b) {
            return ColorManager.s_colorMap[TileData.b];
        } else {
            return new Color32(255, 255, 255, 255);
        }
    }

    public void EnableCenterSlots(int index) {
        _tilePools[index].TileSlots[0].Enable();
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
        if (a.Color == TileData.TileColor.r) {
            return b.Color == TileData.TileColor.r;
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
        if (DebugTile.Equals(TileData.r)) {
            DebugTile = TileData.o;
        } else if (DebugTile.Equals(TileData.o)) {
            DebugTile = TileData.y;
        } else if (DebugTile.Equals(TileData.y)) {
            DebugTile = TileData.g;
        } else if (DebugTile.Equals(TileData.g)) {
            DebugTile = TileData.b;
        } else if (DebugTile.Equals(TileData.b)) {
            DebugTile = TileData.r;
        }
    }

    public void MatchToTimeline(int index) {
        TilesRemaining[index] = _timelineInstances[index].TilesRemainingTimeline.GetCurrentFrame();
        IsSpecial = _timelineInstances[index].IsSpecialTimeline.GetCurrentFrame();
        if (_tilePools[index].TileSlots[0].TileData.Equals(TileData.S)) {
            _specialMenus[index].ActivateMenu(index);
            SpecialManager.s_instance.SpecialMenus[index].ReadyToContinue = false;
            SpecialManager.s_instance.ReadyToContinue = false;
        } else {
            SpecialManager.s_instance.SpecialMenus[index].ReadyToContinue = true;
        }
    }

    public void HideCenterSlot(int index) {
        _tilePools[index].TileSlots[0].Disable();
    }

    public bool TilePoolIsSpecial(int index) {
        return _tilePools[index].IsSpecial;
    }
}
