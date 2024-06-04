using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class PathfindingGrid : MonoBehaviour
{
    public SpriteShapeController spriteShapeController; // Reference to the SpriteShapeController
    public float nodeRadius; // Radius of each node
    public LayerMask obstacleMask; // Layer mask to identify obstacles

    Node[,] grid;
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
		nodeDiameter = nodeRadius * 2;
		//Debug.Log("Start upgrade grid");
		if (spriteShapeController == null)
        {
            Debug.LogError("SpriteShapeController not assigned.");
            return;
        }

        // Get the points defining the SpriteShape
        Spline spline = spriteShapeController.spline;
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < spline.GetPointCount(); i++)
        {
			Vector3 localPosition = spline.GetPosition(i); // Get the local position of the spline point
			Vector3 worldPosition = spriteShapeController.transform.TransformPoint(localPosition); // Convert it to world position
			points.Add(worldPosition);
        }
		/*Debug.Log($"Bounds {points.Count}:");
		foreach (var node in points)
			Debug.Log($"{node}");*/

		// Determine the bounds of the grid based on the SpriteShape
		Vector2 min = points[0];
        Vector2 max = points[2];
		/*foreach (Vector2 point in points)
        {
            if (point.x < min.x) min.x = point.x;
            if (point.y < min.y) min.y = point.y;
            if (point.x > max.x) max.x = point.x;
            if (point.y > max.y) max.y = point.y;
        }*/

		Vector3 worldBottomLeft = new Vector3(min.x, min.y, 0);
		Vector3 worldTopRight = new Vector3(max.x, max.y, 0);

		gridSizeX = Mathf.CeilToInt((max.x - min.x) / nodeDiameter);
		gridSizeY = Mathf.CeilToInt((max.y - min.y) / nodeDiameter);
		grid = new Node[gridSizeX, gridSizeY];
		//Debug.Log($"gridSizeX {gridSizeX} gridSizeY {gridSizeY}");

		for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, obstacleMask));
                // Check if the point is inside the SpriteShape polygon
                if (IsPointInPolygon(points, worldPoint))
                {
                    grid[x, y] = new Node(walkable, worldPoint, x, y);
                }
                else
                {
                    grid[x, y] = new Node(false, worldPoint, x, y); // Mark as not walkable if outside the polygon
                }
            }
        }
		//Debug.Log($"Grid {grid.Length}:");
		//foreach (var node in grid)
		//	Debug.Log($"{node.worldPosition}");
		//Debug.Log("End upgrade grid");
	}

    bool IsPointInPolygon(List<Vector2> polygon, Vector2 point)
    {
        bool isInside = false;
        for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
        {
            if (((polygon[i].y > point.y) != (polygon[j].y > point.y)) &&
                (point.x < (polygon[j].x - polygon[i].x) * (point.y - polygon[i].y) / (polygon[j].y - polygon[i].y) + polygon[i].x))
            {
                isInside = !isInside;
            }
        }
        return isInside;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x - grid[0, 0].worldPosition.x) / (grid[gridSizeX - 1, 0].worldPosition.x - grid[0, 0].worldPosition.x);
        float percentY = (worldPosition.y - grid[0, 0].worldPosition.y) / (grid[0, gridSizeY - 1].worldPosition.y - grid[0, 0].worldPosition.y);
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    void OnDrawGizmos()
    {
        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
			}
        }
    }
}
