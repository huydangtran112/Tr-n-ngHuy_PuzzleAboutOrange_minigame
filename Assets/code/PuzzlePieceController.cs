using UnityEngine;

public class PuzzlePieceController : MonoBehaviour
{
    public int gridX;
    public int gridY;

    [Tooltip("ID riêng biệt cho từng mảnh cam, ví dụ 1,2,3,4")]
    public int pieceID;

    private PuzzleManager puzzleManager;

    void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
        puzzleManager.RegisterPiece(this);
        UpdatePosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) TryMove(Vector2Int.up);
        else if (Input.GetKeyDown(KeyCode.DownArrow)) TryMove(Vector2Int.down);
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) TryMove(Vector2Int.left);
        else if (Input.GetKeyDown(KeyCode.RightArrow)) TryMove(Vector2Int.right);
    }

    public void TryMove(Vector2Int direction)
    {
        int newX = gridX + direction.x;
        int newY = gridY + direction.y;

        if (puzzleManager.IsCellEmpty(newX, newY))
        {
            puzzleManager.MovePiece(this, gridX, gridY, newX, newY);
            gridX = newX;
            gridY = newY;
            UpdatePosition();

            puzzleManager.CheckWin();
        }
        else
        {
            Debug.Log("Không thể di chuyển: ô không trống hoặc bị chặn.");
        }
    }

    public void UpdatePosition()
    {
        float tileSize = puzzleManager.tileSize;
        float offsetX = -(puzzleManager.gridWidth - 1) * tileSize / 2f;
        float offsetY = -(puzzleManager.gridHeight - 1) * tileSize / 2f;

        transform.position = new Vector3(
            gridX * tileSize + offsetX,
            gridY * tileSize + offsetY,
            0);
    }
}
