using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ScoreCalculatorTests : TestBase
{
    [SerializeField] private ScoreCalculator _scoreCalculator;
    [SerializeField] private Board _board;

    public override void RunTests() {
        sb.AppendLine("SCORE CALCULATOR TESTS:");
        sb.AppendLine("");
        TestColumns();
        TestRows();
        TestRainbows();
        sb.AppendLine("");
        sb.AppendLine(TestsPassed + "/" + TotalTests + " tests passed\n");
        Debug.Log(sb.ToString());
    }

    private void TestColumns() {
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("3 column basic", ScoreManager.ScoreGridColumns[0, 0]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.G, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("With star", ScoreManager.ScoreGridColumns[0, 0]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("3 column 2", ScoreManager.ScoreGridColumns[0, 1]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("3 column 5", ScoreManager.ScoreGridColumns[0, 4]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("3 column 6", ScoreManager.ScoreGridColumns[0, 5]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("3 column 7", ScoreManager.ScoreGridColumns[0, 6]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("3 column 8", ScoreManager.ScoreGridColumns[0, 7]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s},
        });
        TestsPassed = RunTest("3 column 9", ScoreManager.ScoreGridColumns[0, 8]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g},
        });
        TestsPassed = RunTest("3 column 10", ScoreManager.ScoreGridColumns[0, 9]) ? TestsPassed + 1 : TestsPassed;
    }

    private void TestRows() {
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.g, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("3 row basic", 400) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.g, TileType.g, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("4 row basic", 625) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.g, TileType.g, TileType.g, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("5 row basic", 950) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.g, TileType.G, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("With star", 400) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.g, TileType.g},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("3 row top right", 1200) ? TestsPassed + 1 : TestsPassed;
    }

    private void TestRainbows() {
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.y, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.o, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("rainbow diagonal up (1,1)", 600) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.y, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.o, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("rainbow diagonal up (2,1)", 600) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.y, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.o, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("rainbow diagonal up (5,1)", 1000) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.y, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.o, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("rainbow diagonal up (6,1)", 1250) ? TestsPassed + 1 : TestsPassed;

        SetBoard(new TileType[,] {
            {TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.o, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.y, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("rainbow diagonal down (1,5)", 600) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.o, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.y, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s, TileType.s, TileType.s, TileType.s},
        });
        TestsPassed = RunTest("rainbow diagonal down (2,5)", 600) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.o, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.y, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b},
        });
        TestsPassed = RunTest("rainbow diagonal down (6,5)", 1250) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.o, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.y, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s},
        });
        TestsPassed = RunTest("rainbow diagonal down (5, 5)", 1000) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileType[,] {
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.p, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.O, TileType.s, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.Y, TileType.s, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.g, TileType.s, TileType.s},
            {TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.s, TileType.b, TileType.s},
        });
        TestsPassed = RunTest("rainbow diagonal with star", 1000) ? TestsPassed + 1 : TestsPassed;
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
        TotalTests++;
        int score = _scoreCalculator.ForceCalculateScore(_board);
        if (score == expectedScore) {
            sb.AppendLine($"[PASS]: {testName} | Expected: {expectedScore} | Actual: {score}");
            return true;
        } else {
            sb.AppendLine($"[FAIL]: {testName} | Expected: {expectedScore} | Actual: {score}");
            return false;
        }
    }
}
