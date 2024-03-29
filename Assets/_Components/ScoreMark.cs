using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMark : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TMP_Text _scoreText;
    public int HorizontalStart;
    public int Length;

    public void PositionSelfRow(Link start, Link end) {
        HorizontalStart = start.Position.x;
        Length = end.Position.x - start.Position.x;
        _spriteRenderer.size = new Vector2(Vector2.Distance(start.Position, end.Position), 0.2f);
        transform.position += new Vector3((start.Position.x + end.Position.x) / 2f, (start.Position.y + end.Position.y) / 2f, 0f);
    }

    public void PositionSelfColumn(Link start, Link end) {
        HorizontalStart = start.Position.x;
        Length = end.Position.x - start.Position.x;
        _spriteRenderer.size = new Vector2(Vector2.Distance(start.Position, end.Position), 0.2f);
        transform.position += new Vector3((start.Position.x + end.Position.x) / 2f, (start.Position.y + end.Position.y) / 2f, 0f);
        transform.right = Vector2.up;
    }

    public void PositionSelfDiagonal(Link start, Link end) {
        HorizontalStart = start.Position.x;
        Length = end.Position.x - start.Position.x;
        _spriteRenderer.size = new Vector2(Vector2.Distance(start.Position, end.Position), 0.2f);
        transform.position += new Vector3((start.Position.x + end.Position.x) / 2f, (start.Position.y + end.Position.y) / 2f, 0f);
        if (start.Position.y < end.Position.y) {
            transform.right = new Vector2(1f, 1f);
        } else {
            transform.right = new Vector2(1f, -1f);
        }
    }

    public void PositionSelfStar(Link link) {
        HorizontalStart = link.Position.x;
        _spriteRenderer.size = new Vector2(0.5f, 0.2f);
        transform.position += new Vector3(link.Position.x, link.Position.y, 0f);
        transform.position += new Vector3(-0.25f, 0.4f);
    }

    public void PositionSelfCorner(Link link) {
        HorizontalStart = link.Position.x;
        _spriteRenderer.size = new Vector2(0.5f, 0.2f);
        transform.position += new Vector3(link.Position.x, link.Position.y, 0f);
        transform.position += new Vector3(-0.25f, 0.4f);
    }

    public void SetText(int score, Vector3 offset = default) {
        _scoreText.text = score.ToString();
        _scoreText.transform.localPosition = new Vector3(-_spriteRenderer.size.x/2f + 0.25f, 0f, 0f) + offset;
    }
}
