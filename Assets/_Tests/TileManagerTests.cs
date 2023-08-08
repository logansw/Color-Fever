using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagerTests : TestBase
{
    public override void RunTests() {
        sb.AppendLine("TILE MANAGER TESTS:");
        sb.AppendLine("");
        TestStars();
        TestSpecials();
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

    public void TestSpecials() {
        TotalTests += 3;
        int earliestSpecial = 38;
        int latestSpecial = 0;

        for (int i = 0; i < 20; i++) {
            TileManager.s_instance.InitializeTilePools();
            TilePool[] tilePools = TileManager.s_instance.GetTilePools();

            foreach (TilePool tilePool in tilePools) {
                int roundsRemaining = 38;
                List<TileData> drawOrderList = tilePool.GetDrawOrderList();
                for (int j = 0; j < drawOrderList.Count; j++) {
                    if (drawOrderList[j].Color.Equals(TileData.TileColor.S)) {
                        if (roundsRemaining < earliestSpecial) {
                            earliestSpecial = roundsRemaining;
                        }
                        if (roundsRemaining > latestSpecial) {
                            latestSpecial = roundsRemaining;
                        }
                    }
                    roundsRemaining--;
                }
            }
        }

        if (earliestSpecial < 4) {
            sb.AppendLine("[FAIL]: Specials Early Test | Expected: >= 4 | Actual: " + earliestSpecial);
        } else {
            TestsPassed++;
        }

        if (latestSpecial > 18) {
            sb.AppendLine("[FAIL]: Specials Late Test | Expected: <= 18 | Actual: " + latestSpecial);
        } else {
            TestsPassed++;
        }

        if (!(earliestSpecial < 4) && !(latestSpecial > 18)) {
            sb.AppendLine("[CHECK]: Specials Range Test | Expected Range: 4-18 | Actual Range: " + earliestSpecial + "-" + latestSpecial);
        }
    }
}