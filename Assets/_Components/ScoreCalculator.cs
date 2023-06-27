using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Board board;

    [Header("External References")]
    [SerializeField] private ScoreDisplayer _scoreDisplayer;

    public int GetScore() {
        _scoreDisplayer.ClearMarks();

        int score = 0;
        score += ScoreColumns();
        score += ScoreRows();
        score += ScoreDiagonals();
        score += ScoreRainbows();
        score += ScoreStars();
        score += ScoreCorners();

        return score;
    }

    public int ForceCalculateScore(Board board) {
        this.board = board;
        return GetScore();
    }

    private int ScoreColumns() {
        int score = 0;
        for (int x = 1; x <= board.Width; x++) {
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
                int addedScore = ScoreManager.ScoreGridColumns[chain.Length - 3, chain.Origin.x - 1];
                score += addedScore;
                _scoreDisplayer.DisplayScoreColumn(chain.FirstLink, chain.LastLink, addedScore, board.transform);
            }
        }
        chains = CreateStarChains(links);
        foreach (Chain chain in chains) {
            if (chain.Length-3 >= 0 && chain.Origin.x-1 >= 0) {
                int addedScore = ScoreManager.ScoreGridStarColumns[chain.Length - 3, chain.Origin.x - 1];
                score += addedScore;
                _scoreDisplayer.DisplayScoreColumn(chain.FirstLink, chain.LastLink, addedScore, board.transform);
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
                int addedScore = ScoreManager.ScoreGridRows[chain.Length - 3, chain.Origin.y - 1, chain.Origin.x - 1];
                score += addedScore;
                _scoreDisplayer.DisplayScoreRow(chain.FirstLink, chain.LastLink, addedScore, board.transform);
            }
        }
        chains = CreateStarChains(links);
        foreach (Chain chain in chains) {
            if (chain.Length-3 >= 0 && chain.Origin.x-1 >= 0 && chain.Origin.y-1 >= 0) {
                int addedScore = ScoreManager.ScoreGridStarRows[chain.Length - 3, chain.Origin.y - 1, chain.Origin.x - 1];
                score += addedScore;
                _scoreDisplayer.DisplayScoreRow(chain.FirstLink, chain.LastLink, addedScore, board.transform);
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
                int addedScore = ScoreManager.ScoreGridDiagonal[chain.Length-3, chain.Origin.y-chain.Length, chain.Origin.x-chain.Length];
                score += addedScore;
                _scoreDisplayer.DisplayScoreDiagonal(chain.FirstLink, chain.LastLink, addedScore, board.transform);
            }
        }
        chains = CreateStarChains(links);
        foreach (Chain chain in chains) {
            if (chain.Length-3 >= 0 && chain.Origin.x-chain.Length >= 0 && chain.Origin.y-chain.Length >= 0) {
                int addedScore = ScoreManager.ScoreGridStarDiagonalUp[chain.Length-3, chain.Origin.y-chain.Length, chain.Origin.x-chain.Length];
                score += addedScore;
                _scoreDisplayer.DisplayScoreDiagonal(chain.FirstLink, chain.LastLink, addedScore, board.transform);
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
                int addedScore = ScoreManager.ScoreGridDiagonal[chain.Length-3, chain.Origin.y-1, chain.Origin.x-chain.Length+1];
                score += addedScore;
                Debug.Log(chain.FirstLink == null);
                Debug.Log(chain.LastLink == null);
                _scoreDisplayer.DisplayScoreDiagonal(chain.FirstLink, chain.LastLink, addedScore, board.transform);
            }
        }
        chains = CreateStarChains(links);
        foreach (Chain chain in chains) {
            if (chain.Length-3 >= 0 && chain.Origin.x-1 >= 0 && chain.Origin.y-1 >= 0) {
                int addedScore = ScoreManager.ScoreGridStarDiagonalDown[chain.Length-3, chain.Origin.y-1, chain.Origin.x-chain.Length+1];
                score += addedScore;
                _scoreDisplayer.DisplayScoreDiagonal(chain.FirstLink, chain.LastLink, addedScore, board.transform);
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
            HashSet<TileData> tilesInColumn = new HashSet<TileData>();
            for (int y = 1; y < board.Height + 1; y++) {
                tilesInColumn.Add(board.BoardData[x, y]);
            }
            if (IsRainbow(tilesInColumn)) {
                score += ScoreManager.ScoreGridRainbowColumns[x-1];
            }
        }
        return score;
    }

    private int ScoreRainbowRows() {
        int score = 0;
        for (int y = 1; y < board.Height + 1; y++) {
            // Check for double rainbow
            HashSet<TileData> tilesInRowLeft = new HashSet<TileData>();
            for (int x = 1; x <= 5; x++) {
                tilesInRowLeft.Add(board.BoardData[x, y]);
            }
            HashSet<TileData> tilesInRowRight = new HashSet<TileData>();
            for (int x = 6; x < board.Width + 1; x++) {
                tilesInRowRight.Add(board.BoardData[x, y]);
            }
            if (IsRainbow(tilesInRowLeft) && IsRainbow(tilesInRowRight)) {
                score += ScoreManager.ScoreGridDoubleRainbow[y-1];
                continue;
            }

            // Check for single rainbow
            for (int x = 1; x < board.Width + 1 - 4; x++) {
                HashSet<TileData> tilesInRow = new HashSet<TileData>();
                for (int i = 0; i < 5; i++) {
                    tilesInRow.Add(board.BoardData[x + i, y]);
                }
                if (IsRainbow(tilesInRow)) {
                    score += ScoreManager.ScoreGridRainbowRows[y - 1, x - 1];
                    break;
                }
            }
        }
        return score;
    }

    private int ScoreRainbowDiagonals() {
        int score = 0;
        // Diagonal Up
        for (int x = 0; x < board.Width + 1 - 5; x++) {
            HashSet<TileData> tilesInDiagonal = new HashSet<TileData>();
            for (int i = 0; i < board.Height; i++) {
                tilesInDiagonal.Add(board.BoardData[x+i+1, i+1]);
            }
            if (IsRainbow(tilesInDiagonal)) {
                score += ScoreManager.ScoreGridRainbowDiagonal[x];
            }
        }

        // Diagonal Down
        for (int x = 0; x < board.Width + 1 - 5; x++) {
            HashSet<TileData> tilesInDiagonal = new HashSet<TileData>();
            for (int i = 0; i < board.Height; i++) {
                tilesInDiagonal.Add(board.BoardData[x+i+1, board.Height-i]);
            }
            if (IsRainbow(tilesInDiagonal)) {
                score += ScoreManager.ScoreGridRainbowDiagonal[x];
            }
        }

        return score;
    }

    private bool IsRainbow(HashSet<TileData> tiles) {
        bool r = false, o = false, y = false, g = false, b = false;
        foreach (TileData tileData in tiles) {
            if (tileData.Color == TileData.TileColor.r) {
                r = true;
            } else if (tileData.Color == TileData.TileColor.o) {
                o = true;
            } else if (tileData.Color == TileData.TileColor.y) {
                y = true;
            } else if (tileData.Color == TileData.TileColor.g) {
                g = true;
            } else if (tileData.Color == TileData.TileColor.b) {
                b = true;
            }
        }

        return (r && o && y && g && b);
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

    private bool IsStar(TileData TileData) {
        return TileData.IsStarred;
    }

    private int ScoreCorners() {
        int score = 0;
        Dictionary<TileData.TileColor, int> cornerTypes = new Dictionary<TileData.TileColor, int>();
        List<TileData> cornerTiles = new List<TileData>();
        TileData bottomLeft = board.BoardData[1, 1];
        TileData bottomRight = board.BoardData[10, 1];
        TileData topLeft = board.BoardData[1, 5];
        TileData topRight = board.BoardData[10, 5];
        cornerTiles.Add(bottomLeft);
        cornerTiles.Add(bottomRight);
        cornerTiles.Add(topLeft);
        cornerTiles.Add(topRight);
        foreach(TileData corner in cornerTiles) {
            if (corner.IsNormal()) {
                if (cornerTypes.ContainsKey(corner.Color)) {
                    cornerTypes[corner.Color] += 1;
                } else {
                    cornerTypes.Add(corner.Color, 1);
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
        if (ValidCornerPair(topLeft, bottomRight)) {
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

    private bool ValidCornerPair(TileData a, TileData b) {
        return (a.IsNormal() && b.IsNormal()) && (a.Color.Equals(b.Color));
    }

    private List<Chain> CreateChains(List<Link> links) {
        List<Chain> chains = new List<Chain>();
        int currentConsecutive = 1;
        Vector2Int origin = links[0].Position;
        Link firstLink = links[0];
        for (int i = 0; i < links.Count-1; i++) {
            Link lastLink = links[i];
            if (TileManager.TilesChainable(links[i].TileData, links[i+1].TileData)) {
                currentConsecutive++;
                origin = links[i+1].Position;
            } else {
                origin = links[i].Position;
                Chain chain = new Chain(currentConsecutive, origin);
                chain.FirstLink = firstLink;
                chain.LastLink = lastLink;
                chains.Add(chain);
                currentConsecutive = 1;
                firstLink = links[i+1];
            }
        }
        Chain lastChain = new Chain(currentConsecutive, links[links.Count-1].Position);
        lastChain.FirstLink = firstLink;
        lastChain.LastLink = links[links.Count-1];
        chains.Add(lastChain);

        foreach (Chain chain in chains) {
            // Debug.Log(chain.ToString());
        }

        return chains;
    }

    private List<Chain> CreateStarChains(List<Link> links) {
        List<Chain> chains = new List<Chain>();
        int currentConsecutive = 1;
        Vector2Int origin = links[0].Position;
        for (int i = 0; i < links.Count-1; i++) {
            if (links[i].TileData.IsStarred && links[i+1].TileData.IsStarred) {
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