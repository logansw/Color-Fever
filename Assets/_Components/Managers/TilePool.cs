using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Text;

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
    [SerializeField] private TimelineInstance _timelineInstance;
    public bool IsSpecial;

    public void Initialize(int index) {
        Index = index;
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
        SpecialManager.e_OnNormalModeSet += HideCornerTiles;
    }

    private void OnDisable() {
        SpecialManager.e_OnCornerModeSet -= ShowCornerTiles;
        TileManager.e_OnSlotEmptied -= HideCornerTiles;
        SpecialManager.e_OnNormalModeSet -= HideCornerTiles;
    }

    private void Update() {
        if (TileSlots[0].TileData.Equals(TileData.S)) {
            IsSpecial = true;
        } else {
            IsSpecial = false;
        }

        if (TileManager.s_instance.IsSpecial &&
            !(TileSlots[0].TileData.Equals(TileData.S) || TileSlots[0].TileData.Equals(TileData.n))) {
            ForceSpecialTile(Index);
        }
    }

    public void SetRandomTile() {
        TileData tile = DrawTile();
        TileSlots[0].SetTile(tile);
        if (tile.Color == TileData.TileColor.S) {
            e_OnSpecialDrawn?.Invoke(Index);
        } else {
            e_OnNormalDrawn?.Invoke(Index);
        }
        ShowTileSlots();
        _timelineInstance.QueueLock();
    }

    public void ForceSpecialTile(int index) {
        TileSlot tileSlot = TileSlots[0];
        ReturnTile(tileSlot.TileData);
        tileSlot.SetTile(TileData.S);
        _tilePool[tileSlot.TileData]--;
        e_OnSpecialDrawn?.Invoke(index);
    }

    public TileData DrawTile() {
        if (!TileManager.s_instance.ForcedDraw.Equals(TileData.n)) {
            TileData tileToDraw = TileManager.s_instance.ForcedDraw;
            TileManager.s_instance.ForcedDraw = TileData.n;
            _tilePool[tileToDraw]--;
            return tileToDraw;
        } else {
            return DrawRandomTile();
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
            _tilePool[tile]--;
            _totalTiles--;
            if (_tilePool[tile] < 0) {
                Debug.Log(tile.Color);
            }
            Assert.AreNotEqual(-1, _tilePool[tile]);
            return tile;
        } else {
            return DrawTile();
        }
    }

    public void ReturnTile(TileData tile) {
        if (_tilePool.ContainsKey(tile)) {
            _tilePool[tile] += 1;
        }
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
        SpecialManager.s_instance.SpecialMenus[index].ReadyToContinue = true;
    }

    public void HideCornerTiles(int index) {
        if (index != Index) {
            return;
        }
        foreach (TileSlot tileSlot in CornerTileSlots) {
            tileSlot.Disable();
        }
    }

    public Dictionary<TileData, int> CopyTilePool() {
        Dictionary<TileData, int> copy = new Dictionary<TileData, int>();
        foreach (TileData tileData in _tilePool.Keys) {
            copy.Add(tileData, _tilePool[tileData]);
        }
        return copy;
    }

    public Dictionary<TileData, int> CopyTilePool(Dictionary<TileData, int> reference) {
        Dictionary<TileData, int> copy = new Dictionary<TileData, int>();
        foreach (TileData tileData in reference.Keys) {
            copy.Add(tileData, reference[tileData]);
        }
        return copy;
    }

    public TileData[] CopyTileSlots(TileSlot[] reference) {
        TileData[] copy = new TileData[reference.Length];
        for (int i = 0; i < reference.Length; i++) {
            copy[i] = reference[i].TileData;
        }
        return copy;
    }

    public void ShowTileSlots() {
        foreach (TileSlot tileSlot in TileSlots) {
            if (tileSlot.TileData.Equals(TileData.n)) {
                tileSlot.Hide();
            } else {
                tileSlot.Show();
            }
        }
    }

    public void MatchToTimeline() {
        _tilePool = CopyTilePool(_timelineInstance.TilePoolTimeline.GetCurrentFrame());
        TileData[] tileData = _timelineInstance.TileSlotsDataTimeline.GetCurrentFrame();

        for (int i = 0; i < tileData.Length; i++) {
            TileSlots[i].SetTile(tileData[i]);
        }
        ShowTileSlots();
    }

    private void DebugPrintTilePool() {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Tile Pool: ");
        foreach (TileData tileData in _tilePool.Keys) {
            string tileName;
            switch (tileData.Color) {
                case TileData.TileColor.p:
                    tileName = "Pink";
                    break;
                case TileData.TileColor.o:
                    tileName = "Orange";
                    break;
                case TileData.TileColor.y:
                    tileName = "Yellow";
                    break;
                case TileData.TileColor.g:
                    tileName = "Green";
                    break;
                case TileData.TileColor.b:
                    tileName = "Blue";
                    break;
                case TileData.TileColor.S:
                    tileName = "Special";
                    break;
                default:
                    tileName = "Error";
                    break;
            }
            if (tileData.IsStarred) {
                tileName += " Star";
            }
            sb.Append(tileName);
            sb.Append(": ");
            sb.AppendLine("" + _tilePool[tileData]);
        }
        sb.AppendLine("Total Tiles Remaining: " + _totalTiles);
        Debug.Log(sb.ToString());
    }
}
