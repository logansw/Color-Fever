using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance;
    public int RoundsRemaining;

    private void Start() {
        RoundsRemaining = 34;
    }

    public void AdvanceTurn() {
        RoundsRemaining--;
    }
}
