using UnityEngine;

public class GridBlocker : MonoBehaviour
{
    public int gridWidth = 5;
    public int gridHeight = 5;
    public float tileSize = 1.5f;

    // Mảng đánh dấu ô bị chặn (true = không đi được)
    public bool[,] blockedCells;

    private void Awake()
    {
        blockedCells = new bool[gridWidth, gridHeight];

        // Ví dụ đánh dấu ô bị chặn
        blockedCells[2, 2] = true;
        blockedCells[1, 3] = true;
    }

    // Kiểm tra vị trí có hợp lệ không (trong lưới)
    public bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < gridWidth && y >= 0 && y < gridHeight;
    }

    // Kiểm tra ô có bị chặn không
    public bool IsBlocked(int x, int y)
    {
        if (!IsValidPosition(x, y))
            return true;

        return blockedCells[x, y];
    }

    // Vẽ ô bị chặn lên Scene View
    private void OnDrawGizmos()
    {
        if (blockedCells == null)
            return;

        Gizmos.color = Color.red;

        float offsetX = -(gridWidth - 1) * tileSize / 2f;
        float offsetY = -(gridHeight - 1) * tileSize / 2f;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (blockedCells[x, y])
                {
                    Vector3 pos = new Vector3(x * tileSize + offsetX, y * tileSize + offsetY, 0);
                    Gizmos.DrawCube(pos, Vector3.one * tileSize * 0.9f);
                }
            }
        }
    }
}
