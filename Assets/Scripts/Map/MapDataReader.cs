using System;
using UnityEngine;

public class MapDataReader : MonoBehaviour
{

    [SerializeField] private TextAsset mapData;

    // ================================================================================
    private MapTileEnum[,] _mapTileData;
    private MapSizeEnum MapSizeEnum = MapSizeEnum.Small;
    private TilePlacer _tilePlacer;
    
    
    // ================================================================================
    
    private int MapSize => (int)MapSizeEnum;
    

    // ================================================================================

    private void Awake()
    {
        CacheComponents();
        ReadData();
        _tilePlacer.Init(_mapTileData, MapSizeEnum);
    }

    // ================================================================================


    private void ReadData()
    {
        if (mapData == null) throw new ArgumentException("MapDataReader : 맵 데이터 누락");

        string[] lines = mapData.text.Split('\n');

        _mapTileData = new MapTileEnum[MapSize, MapSize];

        for (int y = 0; y < MapSize; y++)
        {
            string[] cells = lines[y].Split(',');

            for (int x = 0; x < MapSize; x++)
            {
                string cell = cells[x];

                if (Enum.TryParse(cell, out MapTileEnum mapTileEnum)) _mapTileData[x, y] = mapTileEnum;
            }
        }
    }

    private void CacheComponents()
    {
        _tilePlacer = GetComponent<TilePlacer>();
    }
}
