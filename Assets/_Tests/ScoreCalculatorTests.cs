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
        TestDiagonals();
        sb.AppendLine("");
        sb.AppendLine(TestsPassed + "/" + TotalTests + " tests passed\n");
        Debug.Log(sb.ToString());
    }

    private void TestColumns() {
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 column basic", ScoreManager.ScoreGridColumns[0, 0]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.G, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("With star", ScoreManager.ScoreGridColumns[0, 0]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 column 2", ScoreManager.ScoreGridColumns[0, 1]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 column 5", ScoreManager.ScoreGridColumns[0, 4]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 column 6", ScoreManager.ScoreGridColumns[0, 5]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 column 7", ScoreManager.ScoreGridColumns[0, 6]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 column 8", ScoreManager.ScoreGridColumns[0, 7]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s},
        });
        TestsPassed = RunTest("3 column 9", ScoreManager.ScoreGridColumns[0, 8]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g},
        });
        TestsPassed = RunTest("3 column 10", ScoreManager.ScoreGridColumns[0, 9]) ? TestsPassed + 1 : TestsPassed;

        // Stars
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.P, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.O, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.G, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 column basic STAR", ScoreManager.ScoreGridStarColumns[0, 0]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.P, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.G, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("Missing star", 0) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.P, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.O, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.G, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 column 2 STAR", ScoreManager.ScoreGridStarColumns[0, 1]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.P, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.O, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.G, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 column 5  STAR", ScoreManager.ScoreGridStarColumns[0, 4]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.P, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.O, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.G, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 column 6  STAR", ScoreManager.ScoreGridStarColumns[0, 5]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.P, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.O, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.G, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 column 7  STAR", ScoreManager.ScoreGridStarColumns[0, 6]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.P, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.O, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.G, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 column 8  STAR", ScoreManager.ScoreGridStarColumns[0, 7]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.P, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.O, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.G, TileData.s},
        });
        TestsPassed = RunTest("3 column 9  STAR", ScoreManager.ScoreGridStarColumns[0, 8]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.G},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.O},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.P},
        });
        TestsPassed = RunTest("3 column 10  STAR", ScoreManager.ScoreGridStarColumns[0, 9]) ? TestsPassed + 1 : TestsPassed;
    }

    private void TestRows() {
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.g, TileData.g, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 row basic", 400) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.g, TileData.g, TileData.g, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("4 row basic", 625) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.g, TileData.g, TileData.g, TileData.g, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("5 row basic", 950) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.g, TileData.G, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("With star", 400) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.g, TileData.g},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 row top right", 1200) ? TestsPassed + 1 : TestsPassed;

        // Stars
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.P, TileData.O, TileData.G, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 row basic STAR", 3000) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.P, TileData.O, TileData.Y, TileData.G, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("4 row basic STAR", 3200) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.P, TileData.O, TileData.Y, TileData.G, TileData.B, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("5 row basic STAR", ScoreManager.ScoreGridStarRows[2, 0, 5] + ScoreManager.ScoreGridRainbowRows[0, 0]) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.G, TileData.g, TileData.P, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("Without star", 0) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.G, TileData.O, TileData.P},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("3 row top right STAR", 3500) ? TestsPassed + 1 : TestsPassed;
    }

    private void TestRainbows() {
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.b, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.y, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.o, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.p, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("rainbow diagonal up (1,1)", 600) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.b, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.Y, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.o, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.p, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("rainbow diagonal up (1,1) with star", 600) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.b, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.y, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.o, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.p, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("rainbow diagonal up (2,1)", 600) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.b, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.y, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.o, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.p, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("rainbow diagonal up (5,1)", 1000) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.b},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.y, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.o, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.p, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("rainbow diagonal up (6,1)", 1250) ? TestsPassed + 1 : TestsPassed;

        SetBoard(new TileData[,] {
            {TileData.p, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.o, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.y, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.b, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("rainbow diagonal down (1,5)", 600) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.p, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.o, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.y, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.b, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("rainbow diagonal down (2,5)", 600) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.p, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.o, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.y, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.b},
        });
        TestsPassed = RunTest("rainbow diagonal down (6,5)", 1250) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.p, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.o, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.y, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.b, TileData.s},
        });
        TestsPassed = RunTest("rainbow diagonal down (5, 5)", 1000) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.p, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.O, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.Y, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.b, TileData.s},
        });
        TestsPassed = RunTest("rainbow diagonal with star", 1000) ? TestsPassed + 1 : TestsPassed;
    }

    private void TestDiagonals() {
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("diagonal up (1,1)", 400) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("diagonal up (8,1)", 750) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("diagonal up (1,3)", 750) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.g, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("diagonal up (2,2)", 400) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.g, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("diagonal up (8,2)", 950) ? TestsPassed + 1 : TestsPassed;
        SetBoard(new TileData[,] {
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.O, TileData.s, TileData.O, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.s, TileData.P, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
            {TileData.G, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s, TileData.s},
        });
        TestsPassed = RunTest("diagonal up star (1,1)", 1600) ? TestsPassed + 1 : TestsPassed;
    }

    private void SetBoard(TileData[,] setData) {
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
        _board.BoardData = new TileData[_board.Width+1, _board.Height+1];
        for (int i = 0; i < _board.Width+1; i++) {
            for (int j = 0; j < _board.Height+1; j++) {
                _board.BoardData[i, j] = TileData.n;
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
