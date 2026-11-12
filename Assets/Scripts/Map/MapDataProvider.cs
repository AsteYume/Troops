using System;
using UnityEngine;
using System.Collections.Generic;

public class MapDataProvider : MonoBehaviour
{
    [SerializeField] private List<MapTileSO> _tileSo;
    [SerializeField] private TextAsset _mapData1;
    
    private Queue<PriorityValue> _searchQueue;
    private Dictionary<int[,], int> _searchDictionary;
    private List<int[,]> _searchTilesInRange;
    
    private Dictionary<MapTileEnum, MapTileSO> _tileDictionary;
    private MapTileEnum[,] _mapTileArray;
    private Tile[,] _tileArray;
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
    
    // =============================
    
    
    
    

    public List<int[,]> GetMoveRange(int x, int z, int movement)
    {
        _searchQueue = new Queue<PriorityValue>();
        _searchDictionary = new Dictionary<int[,], int>();
        Queue<int[,]> moveQueue = new();
        _searchTilesInRange = new();
        PriorityValue val = new PriorityValue(x, z, 0, 0, moveQueue);
        SearchTile(val, movement);
        return _searchTilesInRange;
    }

    private void SearchTile(PriorityValue val, int movement)
    {
        PriorityValue north = Adjacency(val, DirectionEnum.North);
        PriorityValue south = Adjacency(val, DirectionEnum.South);
        PriorityValue east = Adjacency(val, DirectionEnum.East);
        PriorityValue west = Adjacency(val, DirectionEnum.West);
        
        if (IsTileValid(north)&&TryMoveTile(north, movement, out PriorityValue northResult))
        {
            
        }
        if (IsTileValid(south)&&TryMoveTile(south, movement, out PriorityValue southResult))
        {
            
        }
        if (IsTileValid(east)&&TryMoveTile(east, movement, out PriorityValue eastResult))
        {
            
        }
        if (IsTileValid(west)&&TryMoveTile(west, movement, out PriorityValue westResult))
        {
            
        }
        
        
        
    }
    
    /// <summary>
    /// 해당 타일이 존재하는지 확인
    /// </summary>
    /// <param name="val">좌표</param>
    /// <returns>존재 여부</returns>
    private bool IsTileValid(PriorityValue val)
    {
        return (val.X >= 0 && val.X < MapSize && val.Z >= 0 && val.Z < MapSize);
    }
    
    /// <summary>
    /// 해당 타일로 이동 가능한지 확인
    /// </summary>
    /// <param name="val">좌표,누적 이동비용</param>
    /// <param name="movement">이동력</param>
    /// <param name="result">좌표, 누적 이동비용</param>
    /// <returns>이동 가능 여부</returns>
    private bool TryMoveTile(PriorityValue val, int movement, out PriorityValue result)
    {
        result = val;
        result.MoveCost = _tileArray[val.X, val.Z].MoveCost + val.MoveCost;
        return movement >= result.MoveCost;
    }

    /// <summary>
    /// 해당 방향으로 인접한 좌표
    /// </summary>
    /// <param name="val">기준 좌표</param>
    /// <param name="direction">방향</param>
    /// <returns>목표 좌표</returns>
    private PriorityValue Adjacency(PriorityValue val, DirectionEnum direction)
    {
        PriorityValue temp = val;
        switch (direction)
        {
            case DirectionEnum.East:
                temp.X++;
                break;
            case DirectionEnum.West:
                temp.X--;
                break;
            case DirectionEnum.North:
                temp.Z++;
                break;
            case DirectionEnum.South:
                temp.Z--;
                break;
        }
        return temp;
    }
    
    
    
}
