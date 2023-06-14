using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ScoreCalculatorTests : MonoBehaviour
{
    [SerializeField] private ScoreCalculator _scoreCalculator;
    [SerializeField] private Board _board;

    void Start() {
        TestColumns();
        TestRows();
        TestRainbows();
    }

    private void TestColumns() {
        int testsPassed = 0;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("3 column basic", ScoreManager.ScoreGridColumns[0, 0]) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.G, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("With star", ScoreManager.ScoreGridColumns[0, 0]) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("3 column 2", ScoreManager.ScoreGridColumns[0, 1]) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("3 column 5", ScoreManager.ScoreGridColumns[0, 4]) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("3 column 6", ScoreManager.ScoreGridColumns[0, 5]) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("3 column 7", ScoreManager.ScoreGridColumns[0, 6]) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s},
        });
        testsPassed = RunTest("3 column 8", ScoreManager.ScoreGridColumns[0, 7]) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s},
        });
        testsPassed = RunTest("3 column 9", ScoreManager.ScoreGridColumns[0, 8]) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g},
        });
        testsPassed = RunTest("3 column 10", ScoreManager.ScoreGridColumns[0, 9]) ? testsPassed + 1 : testsPassed;

        Debug.Log($"Column tests passed: {testsPassed}");
    }

    private void TestRows() {
        int testsPassed = 0;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.g, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("3 row basic", 400) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.g, TileType.g, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("4 row basic", 625) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.g, TileType.g, TileType.g, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("5 row basic", 950) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.G, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("With star", 400) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.g, TileType.g},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("3 row top right", 1200) ? testsPassed + 1 : testsPassed;

        Debug.Log($"Row tests passed: {testsPassed}");
    }

    private void TestRainbows() {
        int testsPassed = 0;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.y, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.o, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("rainbow diagonal up (1,1)", 600) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.y, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.o, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("rainbow diagonal up (2,1)", 600) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.y, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.o, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("rainbow diagonal up (5,1)", 1000) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.y, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.o, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("rainbow diagonal up (6,1)", 1250) ? testsPassed + 1 : testsPassed;

        SetBoard(new TileType[,] {
            {TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.o, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.y, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("rainbow diagonal down (1,5)", 600) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.o, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.y, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        testsPassed = RunTest("rainbow diagonal down (2,5)", 600) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.o, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.y, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b},
        });
        testsPassed = RunTest("rainbow diagonal down (6,5)", 1250) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.o, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.y, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s},
        });
        testsPassed = RunTest("rainbow diagonal down (5, 5)", 1000) ? testsPassed + 1 : testsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.O, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.Y, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s},
        });
        testsPassed = RunTest("rainbow diagonal with star", 1000) ? testsPassed + 1 : testsPassed;

        Debug.Log($"Rainbow tests passed: {testsPassed}");
    }

    private void SetBoard(TileType[,] setData) {
        InitializeBoard();
        Assert.AreEqual(setData.GetLength(1), _board.BoardData.GetLength(0) - 1);
        Assert.AreEqual(setData.GetLength(0), _board.BoardData.GetLength(1) - 1);
        for (int i = 0; i < setData.GetLength(0); i++) {
            for (int j = 0; j < setData.GetLength(1); j++) {
                _board.BoardData[j+1, i+1] = setData[setData.GetLength(0) - i - 1, j];
            }
        }
    }

    private void InitializeBoard() {
        _board.Height = 5;
        _board.Width = 10;
        _board.BoardData = new TileType[_board.Width+1, _board.Height+1];
        for (int i = 0; i < _board.Width+1; i++) {
            for (int j = 0; j < _board.Height+1; j++) {
                _board.BoardData[i, j] = TileType.n;
            }
        }
    }

    private bool RunTest(string testName, int expectedScore) {
        int score = _scoreCalculator.ForceCalculateScore(_board);
        if (score == expectedScore) {
            Debug.Log($"[PASS]: {testName} | Expected: {expectedScore} | Actual: {score}");
            return true;
        } else {
            Debug.Log($"[FAIL]: {testName} | Expected: {expectedScore} | Actual: {score}");
            return false;
        }
    }
}
