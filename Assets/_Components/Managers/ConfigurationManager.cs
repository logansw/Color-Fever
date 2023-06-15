using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationManager : MonoBehaviour
{
    [HideInInspector] public static ConfigurationManager s_instance;
    public bool DebugMode;
    public bool TestsEnabled;

    private void Awake() {
        s_instance = this;
    }

    public void Initialize() {
        // Do nothing
    }
}
