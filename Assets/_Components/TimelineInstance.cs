using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineInstance : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] private Board _board;
    [SerializeField] private TilePool _tilePool;
    [SerializeField] private TileManager _tileManager;
    [SerializeField] private Button _undoButton;

    public int Index;

    public Timeline<TileData[,]> BoardTimeline { get; private set; }
    public Timeline<Dictionary<TileData, int>> TilePoolTimeline;
    public Timeline<int> TilesRemainingTimeline;
    public Timeline<bool> IsSpecialTimeline;
    public Timeline<TileData[]> TileSlotsDataTimeline;

    public void Initialize() {
        BoardTimeline = new Timeline<TileData[,]>(5, _board.CopyBoard());
        TilePoolTimeline = new Timeline<Dictionary<TileData, int>>(5, _tilePool.CopyTilePool());
        TilesRemainingTimeline = new Timeline<int>(5, _tileManager.TilesRemaining[Index]);
        IsSpecialTimeline = new Timeline<bool>(5, _tileManager.IsSpecial);
        TileSlotsDataTimeline = new Timeline<TileData[]>(5, _tilePool.CopyTileSlots(_tilePool.TileSlots));
    }

    public void Advance() {
        BoardTimeline.Advance(_board.CopyBoard());
        TilePoolTimeline.Advance(_tilePool.CopyTilePool());
        TilesRemainingTimeline.Advance(_tileManager.TilesRemaining[Index]);
        IsSpecialTimeline.Advance(_tileManager.IsSpecial);
        TileSlotsDataTimeline.Advance(_tilePool.CopyTileSlots(_tilePool.TileSlots));
        ToggleUndoButtonStatus();
    }

    public void Undo() {
        if (GameManager.s_instance.RoundsRemaining == 34) {
            BoardTimeline.Undo(1);
            TilePoolTimeline.Undo(1);
            TilesRemainingTimeline.Undo(1);
            IsSpecialTimeline.Undo(1);
            TileSlotsDataTimeline.Undo(1);
        } else {
            BoardTimeline.UndoUntilLock();
            TilePoolTimeline.UndoUntilLock();
            TilesRemainingTimeline.UndoUntilLock();
            IsSpecialTimeline.UndoUntilLock();
            TileSlotsDataTimeline.UndoUntilLock();
        }

        _tileManager.SelectedTileSlot = null;
        _board.MatchToTimeline();
        _tilePool.MatchToTimeline();
        _tileManager.MatchToTimeline(Index);
        ToggleUndoButtonStatus();
    }

    public void Lock() {
        BoardTimeline.Lock();
        TilePoolTimeline.Lock();
        TilesRemainingTimeline.Lock();
        IsSpecialTimeline.Lock();
        TileSlotsDataTimeline.Lock();
        ToggleUndoButtonStatus();
    }

    private void ToggleUndoButtonStatus() {
        if (BoardTimeline.CanUndo()) {
            _undoButton.interactable = true;
        } else {
            _undoButton.interactable = false;
        }
    }
}
