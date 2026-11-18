using System;
using UnityEngine;
using System.Collections.Generic;

public class MapDataProvider : MonoBehaviour
{
    [SerializeField] private List<MapTileSO> _tileSo;
    [SerializeField] private TextAsset _mapData1;
    
    private Tile[,] _tileArray;
    private MapSizeEnum _mapSizeEnum;
    
    private static MapTileEnum[,] _mapTileArray;
    private static Dictionary<MapTileEnum, MapTileSO> _tileDictionary;
    
    public static MapTileEnum[,] MapTileArray => _mapTileArray;
    public static Dictionary<MapTileEnum, MapTileSO> TileDictionary => _tileDictionary;
    public int MapSize => (int)StageData.Instance.MapSize;

    private void Awake()
    {
        Init();
    }
    
    // =======================================================================================

    private void Init()
    {
        InitMapData();
        foreach (var tile in _tileSo)
        {
            _tileDictionary.Add(tile.TileType, tile);
        }
    }
    
    // =======================================================================================
    
    public static bool IsStayAvailable(int x, int z)
    {
        return _tileDictionary[_mapTileArray[x, z]].CanStay;
    }

    public void StageStart(int number)
    {
        switch (number)
        {
            case 1: 
                ReadData(_mapData1);
                break;
            default :
                break;
        }
    }

    public GameObject GetTilePrefab(MapTileEnum tileEnum)
    {
        return _tileDictionary[tileEnum].TilePrefab;
    }
    
    // =======================================================================================

    public void AddTile(GameObject obj, int x, int z, MapTileEnum tileEnum)
    {
        _tileArray[x,z] = SetTileInstance(obj, tileEnum);
    }

    public void AddUnit(GameObject obj, int x, int z, Unit unitData)
    {
        _tileArray[x,z].SetUnitData(obj, unitData);
    }

    public bool TryGetTile(int x, int z, out Tile tile)
    {
        int horizontal = Mathf.Clamp(x, 0, MapSize - 1);
        int vertical = Mathf.Clamp(z, 0, MapSize - 1);
        tile = _tileArray[horizontal,vertical];
        return (x==horizontal&& z==vertical);
    }
    
    private void ReadData(TextAsset mapData)
    {
        string[] lines; 
        lines = mapData.text.Split('\n');
        SetMapSize(lines.Length);

        for (int z = 0; z < MapSize; z++)
        {
            string[] cells = lines[z].Split(',');

            for (int x = 0; x < MapSize; x++)
            {
                string cell = cells[x];

                if (Enum.TryParse(cell, out MapTileEnum mapTileEnum)) _mapTileArray[x, z] = mapTileEnum;
            }
        }
    }
    // 탐색
    // =======================================================================================
    
    
    
    // =======================================================================================

    public MapTileSO GetTileByEnum(MapTileEnum tileEnum)
    {
        return _tileDictionary[tileEnum];
    }

    private void SetMapSize(int size)
    {
        StageData.Instance.SetMapSize(size);
        _mapTileArray = new MapTileEnum[MapSize, MapSize];
        _tileArray = new Tile[MapSize, MapSize];
    }

    private Tile SetTileInstance(GameObject obj, MapTileEnum tileEnum)
    {
        return new Tile(obj,tileEnum,this);
    }
    

    private void InitMapData()
    {
        _tileDictionary = new Dictionary<MapTileEnum, MapTileSO>();
    }
}
