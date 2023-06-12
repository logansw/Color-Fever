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
            {0, 0, 400, 400, 400, 400, 400, 400, 440, 500},
            {0, 0, 400, 400, 400, 400, 400, 400, 450, 530},
            {0, 0, 460, 460, 460, 460, 460, 460, 520, 625},
            {0, 0, 520, 520, 520, 520, 520, 520, 640, 875},
            {0, 0, 625, 625, 625, 625, 625, 625, 950, 1200},
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
                Debug.Log(chain.Origin.x + ", " + chain.Origin.y);
            }
        }
        return score;
    }

    private int ScoreRainbows() {
        int score = 0;

        return score;
    }

    private int ScoreStars() {
        int score = 0;

        return score;
    }

    private int ScoreCorners() {
        int score = 0;

        return score;
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
