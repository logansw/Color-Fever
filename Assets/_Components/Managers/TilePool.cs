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
    public int SpecialTilesRemaining;
    public int StarTilesRemaining;
    private List<TileData> _drawOrderList;
    private int _currentIndex;

    public void Initialize(int index, Dictionary<TileData, int> tilePoolData) {
        _currentIndex = 34 - GameManager.s_instance.RoundsRemaining;
        Index = index;
        _tilePool = CopyTilePool(tilePoolData);

        foreach (TileData tileData in _tilePool.Keys) {
            _totalTiles += _tilePool[tileData];
        }

        foreach (TileSlot tileSlot in TileSlots) {
            tileSlot.Initialize(this);
            tileSlot.Disable();
        }
        foreach (TileSlot tileSlot in CornerTileSlots) {
            tileSlot.Initialize(this);
            tileSlot.Disable();
        }

        InitializeDrawOrderList();
    }

    private void OnEnable() {
        SpecialManager.e_OnCornerModeSet += ShowCornerTiles;
        TileManager.e_OnSlotEmptied += HideCornerTiles;
        SpecialManager.e_OnNormalModeSet += HideCornerTiles;
        GameManager.e_OnGameStart += ShowStartingTiles;
    }

    private void OnDisable() {
        SpecialManager.e_OnCornerModeSet -= ShowCornerTiles;
        TileManager.e_OnSlotEmptied -= HideCornerTiles;
        SpecialManager.e_OnNormalModeSet -= HideCornerTiles;
        GameManager.e_OnGameStart -= ShowStartingTiles;
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

    private void InitializeDrawOrderList() {
        _drawOrderList = new List<TileData>();
        foreach (TileData tileData in _tilePool.Keys) {
            for (int i = 0; i < _tilePool[tileData]; i++) {
                _drawOrderList.Add(tileData);
            }
        }
        ShuffleDrawOrderList();
        while (!DrawOrderValid()) {
            ShuffleDrawOrderList();
        }
    }

    private void ShuffleDrawOrderList() {
        int n = _drawOrderList.Count;
        while (n > 1) {
            n--;
            int k = Random.Range(0, n + 1);
            TileData value = _drawOrderList[k];
            _drawOrderList[k] = _drawOrderList[n];
            _drawOrderList[n] = value;
        }
    }

    private bool DrawOrderValid() {
        // Ensure starting tiles have at most 2 of a kind
        Dictionary<TileData, int> tileCounts = new Dictionary<TileData, int>();
        for (int i = 0; i < 4; i++) {
            if (tileCounts.ContainsKey(_drawOrderList[i])) {
                tileCounts[_drawOrderList[i]]++;
            } else {
                tileCounts.Add(_drawOrderList[i], 1);
            }
        }
        if (tileCounts.ContainsValue(3) || tileCounts.ContainsValue(4)) {
            return false;
        }

        for (int i = 0; i < _drawOrderList.Count; i++) {
            // Special before 18 rounds remaining
            if (i < (34 - 18 + 4) && _drawOrderList[i].Equals(TileData.S))
            {
                return false;
            }
            // Special after 4 rounds remaining
            if (i > (34 - 4 + 4) && _drawOrderList[i].Equals(TileData.S))
            {
                return false;
            }
            // Consecutive Specials
            if (i > 0 && _drawOrderList[i].Equals(TileData.S) && _drawOrderList[i - 1].Equals(TileData.S))
            {
                return false;
            }
            // Star before 18 rounds remaining
            if (i < (34 - 18 + 4) && _drawOrderList[i].IsStarred)
            {
                return false;
            }
            // Star after 5 rounds remaining
            if (i > (34 - 5 + 4) && _drawOrderList[i].IsStarred)
            {
                return false;
            }
        }
        return true;
    }

    public TileData SetNextTile() {
        TileData tile = _drawOrderList[_currentIndex];
        TileSlots[0].SetTile(tile);
        if (tile.Color == TileData.TileColor.S) {
            e_OnSpecialDrawn?.Invoke(Index);
        } else {
            e_OnNormalDrawn?.Invoke(Index);
        }
        _timelineInstance.QueueLock();
        _currentIndex++;
        return tile;
    }

    public void ForceSpecialTile(int index) {
        TileSlot tileSlot = TileSlots[0];
        ReturnTile(tileSlot.TileData);
        tileSlot.SetTile(TileData.S);
        _tilePool[tileSlot.TileData]--;
        e_OnSpecialDrawn?.Invoke(index);
    }

    public TileData DrawNextTile() {
        TileData tile = _drawOrderList[_currentIndex];
        _currentIndex++;
        return tile;
    }

    public void ReturnTile(TileData tile) {
        if (_tilePool.ContainsKey(tile)) {
            _tilePool[tile] += 1;
        }
        for (int i = _currentIndex; i < _drawOrderList.Count; i++) {
            if (_drawOrderList[i].Equals(TileData.S)) {
                _drawOrderList[i] = tile;
                break;
            }
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
        CornerTileSlots[0].SetTile(TileData.r);
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

    public void ShowStartingTiles() {
        for (int i = 1; i < TileSlots.Length; i++) {
            TileSlots[i].Show();
        }
    }

    public void HideStartingTiles() {
        for (int i = 1; i < TileSlots.Length; i++) {
            TileSlots[i].Hide();
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
                case TileData.TileColor.r:
                    tileName = "Red";
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

    public void DebugPrintDrawOrderList() {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Draw Order: ");
        for (int i = 0; i < _drawOrderList.Count; i++) {
            string tileName;
            switch (_drawOrderList[i].Color) {
                case TileData.TileColor.r:
                    tileName = "Red";
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
            if (_drawOrderList[i].IsStarred) {
                tileName += " Star";
            }
            sb.AppendLine(38 - i + ": " + tileName);
        }
        Debug.Log(sb.ToString());
    }

    public List<TileData> GetDrawOrderList() {
        return _drawOrderList;
    }
    public void SetDrawOrderList(List<TileData> drawOrderList) {
        _drawOrderList = drawOrderList;
    }
}
