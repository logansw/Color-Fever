using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Board board;

    private int[,] _scoreGridColumns = {
        {400, 400, 400, 400, 400, 400, 450, 540, 660, 800},
        {625, 625, 625, 625, 625, 625, 675, 800, 925, 1160},
        {900, 900, 900, 900, 900, 900, 980, 1160, 1320, 1750},
    };

    private int[,,] _scoreGridRows = {
        {   // 3
            {0, 0, 400, 400, 400, 400, 400, 400, 440, 500},
            {0, 0, 400, 400, 400, 400, 400, 400, 450, 530},
            {0, 0, 460, 460, 460, 460, 460, 460, 520, 625},
            {0, 0, 520, 520, 520, 520, 520, 520, 640, 875},
            {0, 0, 625, 625, 625, 625, 625, 625, 950, 1200},
        },
        {   // 4
            {0, 0, 625, 625, 625, 625, 625, 625, 740, 820},
            {0, 0, 625, 625, 625, 625, 625, 625, 760, 860},
            {0, 0, 675, 675, 675, 675, 675, 675, 850, 980},
            {0, 0, 850, 850, 850, 850, 850, 850, 1175, 1400},
            {0, 0, 1100, 1100, 1100, 1100, 1100, 1100, 1500, 2000},
        },
        {   // 5
            {0, 0, 950, 950, 950, 950, 1000, 1200, 1500, 2000},
            {0, 0, 950, 950, 950, 950, 1200, 1450, 1650, 2400},     // TODO: Check these values
            {0, 0, 1150, 1150, 1150, 1150, 1550, 2000, 2000, 3000}, // 2000 seems wrong
            {0, 0, 1500, 1500, 1500, 1500, 2100, 3000, 2700, 3800}, // 3000 seems wrong
            {0, 0, 2000, 2000, 2000, 2000, 3000, 5000, 3800, 5000}, // 5000 seems wrong
        },
    };

    private int[,,] _scoreGridDiagonalUp = {
        {   // 3
            {400, 400, 400, 400, 400, 400, 525, 750, 0, 0},
            {400, 400, 400, 400, 400, 400, 600, 800, 0, 0},
            {775, 775, 775, 775, 775, 875, 1000, 1250, 0, 0},
        },
        {   // 4
            {625, 625, 625, 625, 625, 800, 1050, 0, 0, 0},
            {750, 750, 750, 750, 750, 975, 1275, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        },
        {   // 5
            {1400, 1400, 1400, 1700, 2200, 2800, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        }
    };

    private int[,,] _scoreGridDiagonalDown = {
        {   // 3
            {0, 0, 400, 400, 400, 400, 400, 400, 525, 750},
            {0, 0, 400, 400, 400, 400, 400, 400, 600, 800},
            {0, 0, 775, 775, 775, 775, 775, 875, 1000, 1250},
        },
        {   // 4
            {0, 0, 0, 625, 625, 625, 625, 625, 800, 1050},
            {0, 0, 0, 750, 750, 750, 750, 750, 975, 1275},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        },
        {   // 5
            {0, 0, 0, 0, 1400, 1400, 1400, 1700, 2200, 2800},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        }
    };

    private int[] _scoreGridRainbowColumns = {
        600, 600, 600, 600, 600, 600, 650, 775, 900, 1150
    };

    private int[,] _scoreGridRainbowRows = {
        {600, 600, 600, 600, 600, 600, 0, 0, 0, 0},
        {600, 600, 600, 600, 600, 700, 0, 0, 0, 0},
        {700, 700, 700, 800, 800, 800, 0, 0, 0, 0},
        {850, 850, 960, 1125, 1250, 1500, 0, 0, 0, 0},
        {1000, 1000, 1200, 1400, 1800, 2500, 0, 0, 0, 0},
    };

    private int[] _scoreGridDoubleRainbow = {
        5000, 6000, 7000, 8000, 9000
    };

    public int GetScore() {
        int score = 0;
        score += ScoreColumns();
        score += ScoreRows();
        score += ScoreDiagonals();
        score += ScoreRainbows();
        score += ScoreStars();
        score += ScoreCorners();

        return score;
    }

    private int ScoreColumns() {
        int score = 0;
        for (int x = 0; x < board.Width + 1; x++) {
            score += ScoreColumn(x);
        }
        return score;
    }

    private int ScoreColumn(int x) {
        int score = 0;
        List<Link> links = new List<Link>();
        for (int y = 1; y < board.Height + 1; y++) {
            links.Add(new Link(new Vector2Int(x, y), board.BoardData[x, y]));
        }
        List<Chain> chains = CreateChains(links);
        foreach (Chain chain in chains) {
            if (chain.Length-3 >= 0 && chain.Origin.x-1 >= 0) {
                score += _scoreGridColumns[chain.Length-3, chain.Origin.x-1];
            }
        }
        return score;
    }

    private int ScoreRows() {
        int score = 0;
        for (int y = 1; y < board.Height + 1; y++) {
            score += ScoreRow(y);
        }
        return score;
    }

    private int ScoreRow(int y) {
        int score = 0;
        List<Link> links = new List<Link>();
        for (int x = 1; x < board.Width + 1; x++) {
            links.Add(new Link(new Vector2Int(x, y), board.BoardData[x, y]));
        }
        List<Chain> chains = CreateChains(links);
        foreach (Chain chain in chains) {
            if (chain.Length-3 >= 0 && chain.Origin.x-1 >= 0 && chain.Origin.y-1 >= 0) {
                score += _scoreGridRows[chain.Length-3, chain.Origin.y-1, chain.Origin.x-1];
            }
        }
        return score;
    }

    private int ScoreDiagonals() {
        int score = 0;
        score += ScoreDiagonalUp(1, 1);
        for (int y = 2; y < board.Height + 1 - 2; y++) {
            score += ScoreDiagonalUp(1, y);
        }
        for (int x = 2; x < board.Width + 1 - 2; x++) {
            score += ScoreDiagonalUp(x, 1);
        }
        score += ScoreDiagonalDown(1, board.Height);
        for (int y = board.Height - 1; y >= 3; y--) {
            score += ScoreDiagonalDown(1, y);
        }
        for (int x = 2; x < board.Width + 1 - 2; x++) {
            score += ScoreDiagonalDown(x, board.Height);
        }
        return score;
    }

    private int ScoreDiagonalUp(int x, int y) {
        int score = 0;
        List<Link> links = new List<Link>();
        for (int i = 0; y + i < board.Height + 1; i++) {
            if (x+i < board.Width + 1 && y+i < board.Height + 1) {
                links.Add(new Link(new Vector2Int(x+i, y+i), board.BoardData[x+i, y+i]));
            }
        }
        List<Chain> chains = CreateChains(links);
        foreach (Chain chain in chains) {
            if (chain.Length-3 >= 0 && chain.Origin.x-chain.Length >= 0 && chain.Origin.y-chain.Length >= 0) {
                score += _scoreGridDiagonalUp[chain.Length-3, chain.Origin.y-chain.Length, chain.Origin.x-chain.Length];
            }
        }
        return score;
    }

    private int ScoreDiagonalDown(int x, int y) {
        int score = 0;
        List<Link> links = new List<Link>();
        for (int i = 0; y - i > 0; i++) {
            if (x+i < board.Width+1 && y-i > 0) {
                links.Add(new Link(new Vector2Int(x+i, y-i), board.BoardData[x+i, y-i]));
            }
        }
        List<Chain> chains = CreateChains(links);
        foreach (Chain chain in chains) {
            if (chain.Length-3 >= 0 && chain.Origin.x-1 >= 0 && chain.Origin.y-1 >= 0) {
                score += _scoreGridDiagonalDown[chain.Length-3, chain.Origin.y-1, chain.Origin.x-1];
            }
        }
        return score;
    }

    private int ScoreRainbows() {
        int score = 0;
        score += ScoreRainbowColumns();
        score += ScoreRainbowRows();
        score += ScoreRainbowDiagonals();
        return score;
    }

    private int ScoreRainbowColumns() {
        int score = 0;
        for (int x = 1; x < board.Width + 1; x++) {
            HashSet<TileType> tilesInColumn = new HashSet<TileType>();
            for (int y = 1; y < board.Height + 1; y++) {
                tilesInColumn.Add(board.BoardData[x, y]);
            }
            if (IsRainbow(tilesInColumn)) {
                score += _scoreGridRainbowColumns[x-1];
            }
        }
        return score;
    }

    private int ScoreRainbowRows() {
        int score = 0;
        for (int y = 1; y < board.Height + 1; y++) {
            // Check for double rainbow
            HashSet<TileType> tilesInRowLeft = new HashSet<TileType>();
            for (int x = 1; x <= 5; x++) {
                tilesInRowLeft.Add(board.BoardData[x, y]);
            }
            HashSet<TileType> tilesInRowRight = new HashSet<TileType>();
            for (int x = 6; x < board.Width + 1; x++) {
                tilesInRowRight.Add(board.BoardData[x, y]);
            }
            if (IsRainbow(tilesInRowLeft) && IsRainbow(tilesInRowRight)) {
                score += _scoreGridDoubleRainbow[y-1];
                continue;
            }

            // Check for single rainbow
            for (int x = 1; x < board.Width + 1 - 4; x++) {
                HashSet<TileType> tilesInRow = new HashSet<TileType>();
                for (int i = 0; i < 5; i++) {
                    tilesInRow.Add(board.BoardData[x + i, y]);
                }
                if (IsRainbow(tilesInRow)) {
                    score += _scoreGridRainbowRows[y - 1, x - 1];
                    break;
                }
            }
        }
        return score;
    }

    // TODO:
    private int ScoreRainbowDiagonals() {
        return 0;
    }

    private bool IsRainbow(HashSet<TileType> tiles) {
        return ((tiles.Contains(TileType.Pink) || tiles.Contains(TileType.PinkStar)) &&
            (tiles.Contains(TileType.Orange) || tiles.Contains(TileType.OrangeStar)) &&
            (tiles.Contains(TileType.Yellow) || tiles.Contains(TileType.YellowStar)) &&
            (tiles.Contains(TileType.Green) || tiles.Contains(TileType.GreenStar)) &&
            (tiles.Contains(TileType.Blue) || tiles.Contains(TileType.BlueStar)));
    }

    private int ScoreStars() {
        int score = 0;
        if (IsStar(board.BoardData[1, 4])) {
            score += 600;
        }
        if (IsStar(board.BoardData[4, 4])) {
            score += 600;
        }
        if (IsStar(board.BoardData[7, 4])) {
            score += 900;
        }
        if (IsStar(board.BoardData[10, 4])) {
            score += 1200;
        }
        return score;
    }

    private bool IsStar(TileType tileType) {
        return tileType == TileType.PinkStar || tileType == TileType.OrangeStar || tileType == TileType.YellowStar || tileType == TileType.GreenStar || tileType == TileType.BlueStar;
    }

    private int ScoreCorners() {
        int score = 0;
        Dictionary<TileType, int> cornerTypes = new Dictionary<TileType, int>();
        List<TileType> cornerTiles = new List<TileType>();
        TileType bottomLeft = board.BoardData[1, 1];
        TileType bottomRight = board.BoardData[10, 1];
        TileType topLeft = board.BoardData[1, 5];
        TileType topRight = board.BoardData[10, 5];
        cornerTiles.Add(bottomLeft);
        cornerTiles.Add(bottomRight);
        cornerTiles.Add(topLeft);
        cornerTiles.Add(topRight);
        foreach(TileType corner in cornerTiles) {
            if (TileManager.TileIsNormal(corner)) {
                if (cornerTypes.ContainsKey(corner)) {
                    cornerTypes[corner] += 1;
                } else {
                    cornerTypes.Add(corner, 1);
                }
            }
        }
        if (cornerTypes.ContainsValue(4)) {
            score += 475 * 4;
            return score;
        } else if (cornerTypes.ContainsValue(3)) {
            score += 260 * 3;
            return score;
        }

        if (ValidCornerPair(bottomLeft, bottomRight) ||
            ValidCornerPair(bottomLeft, topLeft)) {
            score += 180 * 2;
        }
        if (ValidCornerPair(bottomLeft, bottomRight)) {
            score += 210 * 2;
        }
        if (ValidCornerPair(bottomLeft, topRight)) {
            score += 220 * 2;
        }
        if (ValidCornerPair(bottomRight, topRight)) {
            score += 240 * 2;
        }

        return score;
    }

    private bool ValidCornerPair(TileType a, TileType b) {
        return (TileManager.TileIsNormal(a) && TileManager.TileIsNormal(b)) && (a == b);
    }

    private List<Chain> CreateChains(List<Link> links) {
        List<Chain> chains = new List<Chain>();
        int currentConsecutive = 1;
        Vector2Int origin = links[0].Position;
        for (int i = 0; i < links.Count-1; i++) {
            if (links[i].TileType != TileType.Space && links[i].TileType != TileType.Null &&
                links[i].TileType != TileType.Highlight && links[i].TileType == links[i+1].TileType) {
                currentConsecutive++;
                origin = links[i+1].Position;
            } else {
                chains.Add(new Chain(currentConsecutive, origin));
                currentConsecutive = 1;
                origin = links[i].Position;
            }
        }
        chains.Add(new Chain(currentConsecutive, origin));
        return chains;
    }
}