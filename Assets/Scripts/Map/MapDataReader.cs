using System;
using UnityEngine;

public class MapDataReader : MonoBehaviour
{
    
    [SerializeField] private TextAsset mapData;
    private MapTileEnum[,] _mapTileData;
    private MapSizeEnum _mapSizeEnum;
    private int _mapSize;

    private void Awake()
    {
        ReadData();
    }

    private void OnDestroy()
    {
        foreach (MapTileEnum mapTileEnum in _mapTileData)
        {
            Debug.Log(mapTileEnum);
        }
    }
    
    
    
    

    private void ReadData()
    {
        string[] lines = mapData.text.Split('\n');
        
        SetMapSize(_mapSizeEnum);
        _mapTileData = new MapTileEnum[_mapSize, _mapSize];
        
        for (int y = 0; y < _mapSize; y++)
        {
            string[] cells = lines[y].Split(',');

            for (int x = 0; x < _mapSize; x++)
            {
                string cell  = cells[x];

                if (Enum.TryParse(cell, out MapTileEnum mapTileEnum)) _mapTileData[x, y] = mapTileEnum;
            }
        }
    }

    private void SetMapSize(MapSizeEnum mapSize)
    {
        switch (mapSize)
        {
            case MapSizeEnum.Small:
                _mapSize = MapConstData.SMALL_SIZE;
                break;
            case MapSizeEnum.Medium:
                _mapSize = MapConstData.MEDIUM_SIZE;
                break;
        }
    }
}
