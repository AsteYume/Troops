using System;
using UnityEngine;
using System.Collections.Generic;

public class MapDataProvider : MonoBehaviour
{
    [SerializeField] private List<MapTileSO> _tileSo;
    [SerializeField] private TextAsset _mapData1;

    private Dictionary<MapTileEnum, MapTileSO> _tileDictionary;
    private MapTileEnum[,] _mapTileArray;
    private Tile[,] _tileArray;
    private List<MapTileEnum> _tileList;
    private MapSizeEnum _mapSizeEnum = MapSizeEnum.Small;
    
    public MapTileEnum[,] MapTileArray => _mapTileArray;
    public int MapSize => (int)_mapSizeEnum;

    private void Awake()
    {
        InitMapData();
        ReadData(_mapData1);
    }


    public GameObject GetTilePrefab(MapTileEnum tileEnum)
    {
        return _tileDictionary[tileEnum].TilePrefab;
    }
    

    public void AddTile(GameObject obj, int x, int z, MapTileEnum tileEnum)
    {
        _tileArray[x,z] = SetTileInstance(obj, tileEnum);
    }

    private Tile SetTileInstance(GameObject obj, MapTileEnum tileEnum)
    {
        return new Tile(
            obj,
            _tileDictionary[tileEnum].TileType,
            _tileDictionary[tileEnum].MoveCost,
            _tileDictionary[tileEnum].SightCost,
            _tileDictionary[tileEnum].Armor,
            _tileDictionary[tileEnum].CanPass,
            _tileDictionary[tileEnum].CanPierce,
            _tileDictionary[tileEnum].CanStay,
            _tileDictionary[tileEnum].CanDestroy,
            _tileDictionary[tileEnum].DestroyResult);
    }
    
    private void InitMapData()
    {
        _tileDictionary = new Dictionary<MapTileEnum, MapTileSO>();
        _mapTileArray = new MapTileEnum[MapSize, MapSize];
        _tileArray = new Tile[MapSize, MapSize];
        _tileList = new List<MapTileEnum>(_tileDictionary.Keys);
        foreach (var tile in _tileSo)
        {
            _tileDictionary.Add(tile.TileType, tile);
        }
    }
    
    
    public void ReadData(TextAsset mapData)
    {
        string[] lines = mapData.text.Split('\n');

        for (int y = 0; y < MapSize; y++)
        {
            string[] cells = lines[y].Split(',');

            for (int x = 0; x < MapSize; x++)
            {
                string cell = cells[x];

                if (Enum.TryParse(cell, out MapTileEnum mapTileEnum)) _mapTileArray[x, y] = mapTileEnum;
            }
        }
    }
    
    
}
