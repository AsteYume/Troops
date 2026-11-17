using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class MapBfsFinder
{
    private static List<Vector2Int> _maxTileArray = new();
    private static List<Vector2Int> _minTileArray = new();
    private static Vector2Int _center;
    private static int _maxIndex => (int)StageData.MapSize - 1;

    public static List<Vector2Int> Bfs(int x, int z, int maxRange, int minRange)
    {
        Vector2Int vec = new Vector2Int(x, z);
        return Bfs(vec, maxRange, minRange);
    }
    
    public static List<Vector2Int> Bfs(Vector2Int center, int maxRange, int minRange)
    {
        ClearArray();
        _center = center;
        _maxTileArray = GetTilesInRange(maxRange);
        _minTileArray = GetTilesInRange(minRange);
        return _maxTileArray.Except(_minTileArray).ToList();
    }
    
    
    private static List<Vector2Int> GetTilesInRange(int range)
    {
        List<Vector2Int> tiles = new List<Vector2Int>();
        int minX = _center.x - range >= 0 ?  _center.x - range : 0;
        int maxX = _center.x + range <= _maxIndex ?  _center.x + range : _maxIndex;

        for (int x = minX; x <= maxX; x++)
        {
            int cost = range - Mathf.Abs(_center.x - x);
            int minY = _center.y - cost >= 0 ? _center.y - cost : 0;
            int maxY = _center.y + cost <= _maxIndex ? _center.y - cost : _maxIndex;

            for (int y = minY; y <= maxY; y++)
            {
                tiles.Add(new Vector2Int(x, y));
            }
        }
        return tiles;
    }

    private static void ClearArray()
    {
        _maxTileArray.Clear();
        _minTileArray.Clear();
    }
}
