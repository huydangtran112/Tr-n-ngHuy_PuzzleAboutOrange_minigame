using UnityEngine;

public class PieceMover : MonoBehaviour
{
    public Vector2Int gridPos;

    private void Start()
    {
        UpdatePosition();
    }

    public void TryMove(Vector2Int direction)
    {
        Vector2Int newPos = gridPos + direction;
        if (GameManager.Instance.IsInsideGrid(newPos) && !GameManager.Instance.IsCellOccupied(newPos))
        {
            gridPos = newPos;
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        transform.position = GameManager.Instance.GetWorldPos(gridPos);
    }
}
