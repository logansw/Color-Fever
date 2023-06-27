using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMark : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void PositionSelfRow(Link start, Link end) {
        _spriteRenderer.size = new Vector2(Vector2.Distance(start.Position, end.Position), 0.2f);
        transform.position += new Vector3((start.Position.x + end.Position.x) / 2f, (start.Position.y + end.Position.y) / 2f, 0f);
    }

    public void PositionSelfColumn(Link start, Link end) {
        _spriteRenderer.size = new Vector2(Vector2.Distance(start.Position, end.Position), 0.2f);
        transform.position += new Vector3((start.Position.x + end.Position.x) / 2f, (start.Position.y + end.Position.y) / 2f, 0f);
        transform.right = Vector2.up;
    }

    public void PositionSelfDiagonal(Link start, Link end) {
        _spriteRenderer.size = new Vector2(Vector2.Distance(start.Position, end.Position), 0.2f);
        transform.position += new Vector3((start.Position.x + end.Position.x) / 2f, (start.Position.y + end.Position.y) / 2f, 0f);
        if (start.Position.y < end.Position.y) {
            transform.right = new Vector2(1f, 1f);
        } else {
            transform.right = new Vector2(1f, -1f);
        }
    }

    public void PositionSelfStar(Link link) {

    }

    public void PositionSelfCorner(Link link) {

    }

    public void SetText(int score) {

    }
}
