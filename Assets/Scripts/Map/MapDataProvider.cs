using System;
using UnityEngine;
using System.Collections.Generic;

public class MapDataProvider : MonoBehaviour
{
    [SerializeField] private List<MapTileSO> _tileSo;
    [SerializeField] private TextAsset _mapData1;

    private PriorityQueue<Node> _priorityQueue;
    private Dictionary<(int, int), Node> _nodeDictionary;
    private int _range;
    
    private Dictionary<MapTileEnum, MapTileSO> _tileDictionary;
    private MapTileEnum[,] _mapTileArray;
    private Tile[,] _tileArray;
    private MapSizeEnum _mapSizeEnum = MapSizeEnum.Small;
    
    public MapTileEnum[,] MapTileArray => _mapTileArray;
    public int MapSize => (int)_mapSizeEnum;

    private void Awake()
    {
        Init();
        ReadData(_mapData1);
    }
    
    // =======================================================================================

    private void Init()
    {
        InitMapData();
        InitSearchData();
        foreach (var tile in _tileSo)
        {
            _tileDictionary.Add(tile.TileType, tile);
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

    public bool TryGetTile(int x, int z, out Tile tile)
    {
        int horizontal = Mathf.Clamp(x, 0, MapSize - 1);
        int vertical = Mathf.Clamp(z, 0, MapSize - 1);
        Debug.Log($"{horizontal},{vertical}");
        Debug.Log(_tileArray);
        tile = _tileArray[horizontal,vertical];
        Debug.Log(_tileArray[horizontal,vertical]);
        Debug.Log(tile);
        Debug.Log($"대상 타일 {tile.MoveCost}");
        return (x==horizontal&& z==vertical);
    }
    
    private void ReadData(TextAsset mapData)
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
    // 탐색
    // =======================================================================================

    public Dictionary<(int, int), Node> GetNodeDictionary (int x, int z, int range)
    {
        _range = range;
        _nodeDictionary.Clear(); // 딕셔너리 비우기
        Node start = new Node(x, z, 0, null); // 시작 노드 지정
        
        _priorityQueue.Enqueue(start); // 시작 노드 큐에 추가
        while (_priorityQueue.Count > 0) // 큐가 빌때까지 반복
        {
            Node currentNode = _priorityQueue.Dequeue(); // 맨 앞 노드 하나 검사
            if (IsOldCheap(currentNode)) continue; // 신규&최소비용 검사
            CheckAdjacency(currentNode);
        }
        _nodeDictionary.Remove((x, z));
        return _nodeDictionary;
    }
    
    private bool IsOldCheap(Node node)
    {

        if (!_nodeDictionary.ContainsKey((node.x, node.z)))
        {
            _nodeDictionary.Add((node.x, node.z), node);
            return false;
        }
        Node old = _nodeDictionary[(node.x, node.z)];
        if (old == node) return false;
        return node.CompareTo(old) >= 0;
    }

    private void CheckAdjacency(Node node)
    {
        TryAddNode(node.x,node.z+1, node); // 북
        TryAddNode(node.x,node.z-1, node); // 남
        TryAddNode(node.x+1,node.z, node); // 동
        TryAddNode(node.x-1,node.z, node); // 서
    }

    private void TryAddNode(int x, int z, Node node)
    {
        if (!IsNodeExists(x, z)) return;
        int cost = GetTileCost(x, z) + node.Cost;
        if (cost > _range) return;
        Node newNode = new Node(x, z, cost, node);
        if (IsOldCheap(newNode)) return;
        _priorityQueue.Enqueue(newNode);
    }

    private bool IsNodeExists(int x, int z)
    {
        return (x >= 0 && x < MapSize && z >= 0 && z < MapSize);
    }

    private int GetTileCost(int x, int z)
    {
        return _tileDictionary
            [_mapTileArray[x, z]]
            .MoveCost;
    }
    
    
    // =======================================================================================

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
    }

    private void InitSearchData()
    {
        _nodeDictionary = new Dictionary<(int, int), Node>();
        _priorityQueue = new PriorityQueue<Node>();
    }
}
