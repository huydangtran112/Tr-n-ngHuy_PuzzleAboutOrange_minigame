using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public int gridWidth = 5;
    public int gridHeight = 5;
    public float tileSize = 1.5f;

    private PuzzlePieceController[,] grid;
    public bool[,] blockedCells;

    public GameObject blockerPrefab;

    [Header("Blocked Positions (x, y)")]
    public List<Vector2Int> blockedPositions = new List<Vector2Int>();

    private void Awake()
    {
        grid = new PuzzlePieceController[gridWidth, gridHeight];
        blockedCells = new bool[gridWidth, gridHeight];

        // Đánh dấu các ô bị chặn
        foreach (var pos in blockedPositions)
        {
            if (IsValidPosition(pos.x, pos.y))
            {
                blockedCells[pos.x, pos.y] = true;
            }
            else
            {
                Debug.LogWarning($"Blocked position out of bounds: {pos}");
            }
        }

        float offsetX = -(gridWidth - 1) * tileSize / 2f;
        float offsetY = -(gridHeight - 1) * tileSize / 2f;

        // Tạo blocker vật lý
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (blockedCells[x, y] && blockerPrefab != null)
                {
                    Vector3 pos = new Vector3(x * tileSize + offsetX, y * tileSize + offsetY, 0);
                    Instantiate(blockerPrefab, pos, Quaternion.identity, transform);
                }
            }
        }
    }

    public void RegisterPiece(PuzzlePieceController piece)
    {
        if (IsValidPosition(piece.gridX, piece.gridY) && !blockedCells[piece.gridX, piece.gridY])
        {
            grid[piece.gridX, piece.gridY] = piece;
        }
        else
        {
            Debug.LogError($"Vị trí không hợp lệ hoặc bị chặn: ({piece.gridX}, {piece.gridY})");
        }
    }

    public bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < gridWidth && y >= 0 && y < gridHeight;
    }

    public bool IsCellEmpty(int x, int y)
    {
        if (!IsValidPosition(x, y)) return false;
        if (blockedCells[x, y]) return false;
        return grid[x, y] == null;
    }

    public void MovePiece(PuzzlePieceController piece, int fromX, int fromY, int toX, int toY)
    {
        grid[fromX, fromY] = null;
        grid[toX, toY] = piece;
    }

    public void CheckWin()
    {
        for (int x = 0; x < gridWidth - 1; x++)
        {
            for (int y = 0; y < gridHeight - 1; y++)
            {
                var a = grid[x, y];         // trên trái
                var b = grid[x + 1, y];     // trên phải
                var c = grid[x, y + 1];     // dưới trái
                var d = grid[x + 1, y + 1]; // dưới phải

                if (a != null && b != null && c != null && d != null)
                {
                    Debug.Log($"Checking at ({x},{y}): {a.pieceID}, {b.pieceID}, {c.pieceID}, {d.pieceID}");

                    if (a.pieceID == 3 && b.pieceID == 4 && c.pieceID == 1 && d.pieceID == 2)
                    {
                        Debug.Log("Chiến thắng đúng vị trí và thứ tự!");
                        SceneManager.LoadScene("chienthang");
                        return;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float offsetX = -(gridWidth - 1) * tileSize / 2f;
                float offsetY = -(gridHeight - 1) * tileSize / 2f;
                Vector3 pos = new Vector3(x * tileSize + offsetX, y * tileSize + offsetY, 0);
                Gizmos.DrawWireCube(pos, Vector3.one * tileSize);
            }
        }
    }
}