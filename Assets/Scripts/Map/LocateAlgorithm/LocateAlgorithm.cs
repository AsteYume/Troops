using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class LocateAlgorithm
{
    public static List<Vector2Int> Bfs(int x, int z, int maxRange, int minRange)
    {
        Vector2Int vec = new Vector2Int(x, z);
        return Bfs(vec, maxRange, minRange);
    }
    
    public static Stack<Vector2Int> ReadNodePath(Node end)
    {
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Node currentNode = end;
        Vector2Int nextPos = new Vector2Int(currentNode.x, currentNode.z);
        do
        {
            stack.Push(nextPos);
            currentNode = currentNode.Parent;
            nextPos = new Vector2Int(currentNode.x, currentNode.z);
        }
        while(currentNode.Parent != null);
        return stack;
    }

    private static List<Vector2Int> Bfs(Vector2Int center, int maxRange, int minRange)
    {
        List<Vector2Int> _maxTileArray = new();
        List<Vector2Int> _minTileArray = new();
        _maxTileArray = GetTilesInRange(maxRange, center);
        _minTileArray = GetTilesInRange(minRange, center);
        return _maxTileArray.Except(_minTileArray).ToList();
    }
    
    
    private static List<Vector2Int> GetTilesInRange(int range, Vector2Int center)
    { 
        int _maxIndex = (int)StageData.Instance.MapSize - 1;
        List<Vector2Int> tiles = new List<Vector2Int>();
        if (range <= 0) return tiles;
        int minX = center.x - range >= 0 ?  center.x - range : 0;
        int maxX = center.x + range <= _maxIndex ?  center.x + range : _maxIndex;

        for (int x = minX; x <= maxX; x++)
        {
            int cost = range - Mathf.Abs(center.x - x);
            int minY = (center.y - cost >= 0) ? (center.y - cost) : 0;
            int maxY = (center.y + cost <= _maxIndex) ? (center.y + cost) : _maxIndex;

            Debug.Log($"{minX},{maxX},{minY},{maxY}");
            for (int y = minY; y <= maxY; y++)
            {
                Debug.Log($"{x},{y}");
                tiles.Add(new Vector2Int(x, y));
            }
        }
        return tiles;
    }
}
