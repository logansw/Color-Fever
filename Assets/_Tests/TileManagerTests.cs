using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagerTests : TestBase
{
    public override void RunTests() {
        sb.AppendLine("TILE MANAGER TESTS:");
        sb.AppendLine("");
        TestStars();
        sb.AppendLine("");
        sb.AppendLine(TestsPassed + "/" + TotalTests + " tests passed\n");
        Debug.Log(sb.ToString());
    }

    public void TestStars() {
        TotalTests++;
        for (int i = 0; i < 20; i++) {
            TileManager.s_instance.InitializeTilePools();
            TilePool[] tilePools = TileManager.s_instance.GetTilePools();
            int starsCount = 0;
            foreach (TilePool tilePool in tilePools) {
                List<TileData> drawOrderList = tilePool.GetDrawOrderList();
                foreach (TileData tileData in drawOrderList) {
                    if (tileData.IsStarred) {
                        starsCount++;
                    }
                }
            }
            if (starsCount != 5) {
                sb.AppendLine($"[FAIL]: Stars Test | Expected: 5 | Actual: {starsCount}");
                foreach (TilePool tilePool in tilePools) {
                    tilePool.DebugPrintDrawOrderList();
                }
                return;
            }
        }
        TestsPassed++;
        sb.AppendLine($"[PASS]: Stars Test | Expected: 5 | Actual: 5");
    }
}
