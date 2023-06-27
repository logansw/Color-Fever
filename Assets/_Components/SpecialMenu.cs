using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMenu : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] private CustomButton _cornerButton;
    [SerializeField] private CustomButton _moveButton;
    [SerializeField] private CustomButton _swapButton;
    [SerializeField] private CustomButton _removeButton;
    [SerializeField] private CustomButton _passButton;
    [SerializeField] private TimelineInstance _timelineInstance;
    [SerializeField] private GuidingText[] _guidingTexts;

    public Dictionary<CustomButton, bool> ButtonsRemaining;
    public int Index;
    public bool ReadyToContinue;
    [SerializeField] private bool _horizontal;

    public void Initialize() {
        ButtonsRemaining = new Dictionary<CustomButton, bool>();
        ButtonsRemaining.Add(_cornerButton, true);
        ButtonsRemaining.Add(_swapButton, true);
        ButtonsRemaining.Add(_moveButton, true);
        ButtonsRemaining.Add(_removeButton, true);
        ButtonsRemaining.Add(_passButton, true);
        ReadyToContinue = true;
        RenderButtons();
        DeactivateMenu(Index);
    }

    private void OnEnable() {
        TilePool.e_OnSpecialDrawn += ActivateMenu;
    }

    private void OnDisable() {
        TilePool.e_OnSpecialDrawn -= ActivateMenu;
    }

    public void ActivateMenu(int index) {
        if (index != Index) { return; }

        foreach (GuidingText guidingText in _guidingTexts) {
            guidingText.SetText(index, "Choose an Action");
        }

        foreach (CustomButton button in ButtonsRemaining.Keys) {
            if (ButtonsRemaining[button]) {
                button.Interactable = true;
            }
        }
    }

    public void DeactivateMenu(int index) {
        if (index != Index) { return; }

        foreach (CustomButton button in ButtonsRemaining.Keys) {
            if (ButtonsRemaining[button]) {
                button.Interactable = false;
            }
        }
    }

    public void RemoveButton(CustomButton button) {
        ButtonsRemaining[button] = false;
        button.gameObject.SetActive(false);
        RenderButtons();
    }

    public void RestoreButton(CustomButton button) {
        ButtonsRemaining[button] = true;
        button.gameObject.SetActive(true);
        RenderButtons();
    }

    private void RenderButtons() {
        int count = 0;
        foreach (CustomButton button in ButtonsRemaining.Keys) {
            if (ButtonsRemaining[button]) {
                count++;
            }
        }
        int i = 0;
        if (_horizontal) {
            foreach (CustomButton button in ButtonsRemaining.Keys) {
                if (ButtonsRemaining[button]) {
                    Vector3 leftMost = new Vector3(-((float)count - 1f) / 2f * button.Bounds.size.x, 0, 0) * 1.05f;
                    button.transform.localPosition = leftMost + new Vector3(i * 1.05f * button.Bounds.size.x, 0, 0);
                    i++;
                }
            }
        } else {
            foreach (CustomButton button in ButtonsRemaining.Keys) {
                if (ButtonsRemaining[button]) {
                    Vector3 topMost = new Vector3(0, ((float)count - 1f) / 2f * button.Bounds.size.y, 0) * 1.05f;
                    button.transform.localPosition = topMost + new Vector3(0, -i * 1.05f * button.Bounds.size.y, 0);
                    i++;
                }
            }
        }
    }

    public Dictionary<CustomButton, bool> CopyButtonsRemaining(Dictionary<CustomButton, bool> reference) {
        Dictionary<CustomButton, bool> copy = new Dictionary<CustomButton, bool>();
        foreach (CustomButton button in reference.Keys) {
            copy.Add(button, reference[button]);
        }
        return copy;
    }

    public void MatchToTimeline() {
        ButtonsRemaining = CopyButtonsRemaining(_timelineInstance.ButtonsRemainingTimeline.GetCurrentFrame());
        if (ButtonsRemaining[_cornerButton]) {
            RestoreButton(_cornerButton);
        }
        if (ButtonsRemaining[_moveButton]) {
            RestoreButton(_moveButton);
        }
        if (ButtonsRemaining[_swapButton]) {
            RestoreButton(_swapButton);
        }
        if (ButtonsRemaining[_removeButton]) {
            RestoreButton(_removeButton);
        }
        RenderButtons();
        if (TileManager.s_instance.TilePoolIsSpecial(Index)) {
            ActivateMenu(Index);
        }
    }

    public void SetReadyToContinue(bool ready) {
        if (TileManager.s_instance.IsSpecial) {
            ReadyToContinue = ready;
            if (!ready) {
                SpecialManager.s_instance.ReadyToContinue = false;
            }
        }
    }
}
