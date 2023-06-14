using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public abstract class TestBase : MonoBehaviour
{
    public int TotalTests;
    public int TestsPassed;
    public StringBuilder sb = new StringBuilder();

    public abstract void RunTests();
}
