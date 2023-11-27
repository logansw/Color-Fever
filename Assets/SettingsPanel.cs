using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    public GameObject[] children;

    public void ShowPanel() {
        foreach (GameObject child in children) {
            child.SetActive(true);
        }
    }

    public void HidePanel() {
        foreach (GameObject child in children) {
            child.SetActive(false);
        }
    }
}
