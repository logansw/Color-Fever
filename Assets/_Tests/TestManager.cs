using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static TestManager s_instance;
    public TestBase[] Tests;

    private void Awake() {
        s_instance = this;
    }

    public void Initialize() {
        if (ConfigurationManager.s_instance.TestsEnabled) {
            foreach (var test in Tests) {
                test.RunTests();
            }
        }
    }
}
