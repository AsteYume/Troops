using System.Collections.Generic;
using UnityEngine;

public static class ConeAlgorithm
{
    public static List<Vector2Int> Cone(Vector2Int center, int range, DirectionEnum direction)
    {
        List<Vector2Int> radius = CheckRadius(range);
        List<Vector2Int> result = new List<Vector2Int>();
        int mapSize = (int)StageData.Instance.MapSize;
        foreach (Vector2Int value in radius)
        {
            Vector2Int vector;
            switch (direction)
            {
                case DirectionEnum.East:
                    vector = new Vector2Int(center.x+value.x, center.y+value.y);
                    break;
                case DirectionEnum.West:
                    vector = new Vector2Int(center.x-value.x, center.y+value.y);
                    break;
                case DirectionEnum.North:
                    vector = new Vector2Int(center.x+value.y, center.y+value.x);
                    break;
                case DirectionEnum.South:
                    vector = new Vector2Int(center.x+value.y, center.y-value.x);
                    break;
                default:
                    continue;
            }
            if (vector.x < 0 || vector.x > mapSize || vector.y < 0 || vector.y > mapSize) continue;
            result.Add(vector);
        }
        return result;
    }

    private static List<Vector2Int> CheckRadius(int range)
    {
        List<Vector2Int> tempTile = new List<Vector2Int>();
        for (int a = 1; a <= range; a++)
        {
            for (int b = -(a - 1); b <= (a - 1); b++)
            {
                tempTile.Add(new Vector2Int(a,b));
            }
        }
        return tempTile;
    }
    
    
}
