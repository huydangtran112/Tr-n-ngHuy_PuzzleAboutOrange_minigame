using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public PieceMover targetPiece; // kéo mảnh cam vào để điều khiển

    public void MoveUp() => targetPiece.TryMove(Vector2Int.up);
    public void MoveDown() => targetPiece.TryMove(Vector2Int.down);
    public void MoveLeft() => targetPiece.TryMove(Vector2Int.left);
    public void MoveRight() => targetPiece.TryMove(Vector2Int.right);
}
