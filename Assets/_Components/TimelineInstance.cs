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
    [SerializeField] private CustomButton _undoButton;
    [SerializeField] private SpecialMenu _specialMenu;

    public bool AdvanceQueued;
    public bool LockQueued;

    public int Index;

    public Timeline<TileData[,]> BoardTimeline { get; private set; }
    public Timeline<Dictionary<TileData, int>> TilePoolTimeline;
    public Timeline<int> TilesRemainingTimeline;
    public Timeline<bool> IsSpecialTimeline;
    public Timeline<TileData[]> TileSlotsDataTimeline;
    public Timeline<Dictionary<CustomButton, bool>> ButtonsRemainingTimeline;

    public void Initialize() {
        BoardTimeline = new Timeline<TileData[,]>(5, _board.CopyBoard());
        TilePoolTimeline = new Timeline<Dictionary<TileData, int>>(5, _tilePool.CopyTilePool());
        TilesRemainingTimeline = new Timeline<int>(5, _tileManager.TilesRemaining[Index]);
        IsSpecialTimeline = new Timeline<bool>(5, _tileManager.IsSpecial);
        TileSlotsDataTimeline = new Timeline<TileData[]>(5, _tilePool.CopyTileSlots(_tilePool.TileSlots));
        ButtonsRemainingTimeline = new Timeline<Dictionary<CustomButton, bool>>(5, _specialMenu.CopyButtonsRemaining(_specialMenu.ButtonsRemaining));
    }

    private void Update() {
        if (AdvanceQueued) {
            Advance();
            AdvanceQueued = false;
        }
        if (LockQueued) {
            Lock();
            LockQueued = false;
        }
    }

    public void QueueAdvance() {
        AdvanceQueued = true;
    }

    public void QueueLock() {
        LockQueued = true;
    }

    private void Advance() {
        BoardTimeline.Advance(_board.CopyBoard());
        TilePoolTimeline.Advance(_tilePool.CopyTilePool());
        TilesRemainingTimeline.Advance(_tileManager.TilesRemaining[Index]);
        IsSpecialTimeline.Advance(_tileManager.IsSpecial);
        TileSlotsDataTimeline.Advance(_tilePool.CopyTileSlots(_tilePool.TileSlots));
        ButtonsRemainingTimeline.Advance(_specialMenu.CopyButtonsRemaining(_specialMenu.ButtonsRemaining));
        ToggleUndoButtonStatus();
    }

    public void Undo(int index) {
        if (GameManager.s_instance.RoundsRemaining == 34) {
            BoardTimeline.Undo(1);
            TilePoolTimeline.Undo(1);
            TilesRemainingTimeline.Undo(1);
            IsSpecialTimeline.Undo(1);
            TileSlotsDataTimeline.Undo(1);
            ButtonsRemainingTimeline.Undo(1);
        } else {
            BoardTimeline.UndoUntilLock();
            TilePoolTimeline.UndoUntilLock();
            TilesRemainingTimeline.UndoUntilLock();
            IsSpecialTimeline.UndoUntilLock();
            TileSlotsDataTimeline.UndoUntilLock();
            ButtonsRemainingTimeline.UndoUntilLock();
        }

        _tileManager.SelectedTileSlot[index] = null;
        _board.MatchToTimeline();
        _tilePool.MatchToTimeline();
        _specialMenu.MatchToTimeline();
        _tileManager.MatchToTimeline(Index);
        ToggleUndoButtonStatus();
    }

    private void Lock() {
        BoardTimeline.Lock();
        TilePoolTimeline.Lock();
        TilesRemainingTimeline.Lock();
        IsSpecialTimeline.Lock();
        TileSlotsDataTimeline.Lock();
        ToggleUndoButtonStatus();
    }

    private void ToggleUndoButtonStatus() {
        if (BoardTimeline.CanUndo()) {
            _undoButton.Interactable = true;
        } else {
            _undoButton.Interactable = false;
        }
    }
}
