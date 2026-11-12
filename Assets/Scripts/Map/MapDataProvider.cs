using System;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

public class MapDataProvider : MonoBehaviour
{
    [SerializeField] private List<MapTileSO> _tileSo;
    [SerializeField] private TextAsset _mapData1;

    private Dictionary<Vector2Int, int> _moveLogDictionary;
    private PriorityQueue<Vector2Int> _priorityQueue;
    
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

    private List<int[,]> GetMoveRange(int x, int z, int movement)
    {
        List<int[,]> moveableList = new List<int[,]>();
        _moveLogDictionary = new Dictionary<Vector2Int, int>();
        Queue<int[,]> moveQueue = new Queue<int[,]>();
        HashSet<int[,]> moveLog =  new HashSet<int[,]>();
        MoveValue start = new MoveValue(x,z,movement,0,moveQueue, moveLog);
        
        
        return moveableList;
    }


    private void CheckAdjacency(MoveValue val)
    {
        MoveValue north = Adjacency(val,  DirectionEnum.North);
        MoveValue south = Adjacency(val,  DirectionEnum.South);
        MoveValue east = Adjacency(val,  DirectionEnum.East);
        MoveValue west = Adjacency(val,  DirectionEnum.West);

        
        
    }

    private bool TryAddQueue(MoveValue val)
    {
        Vector2Int grid = new Vector2Int(val.X, val.Z);
        int cost = val.MoveCost;
        if (_moveLogDictionary.ContainsKey(grid))
        {
            
            
            
        }

        return false;// 지워
    }



    private bool TryAddMoveQueue(MoveValue val)
    {
        if (!CheckTileExists(val)) return false;
        if (!IsMoveCostEnough(val,out int cost)) return false;


        return true; // 지워
    }
    

    private bool CheckTileExists(MoveValue val)
    {
        return (val.X >= 0 && val.X < MapSize && val.Z >= 0 && val.Z < MapSize);
    }

    private bool IsMoveCostEnough(MoveValue val, out int cost)
    {
        cost = _tileArray[val.X, val.Z].MoveCost + val.MoveCost;
        return (val.Movement >= cost);
    }
    

    private MoveValue Adjacency(MoveValue val, DirectionEnum direction)
    {
        MoveValue temp = val;
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
