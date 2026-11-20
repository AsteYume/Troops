using System;
using UnityEngine;
using System.Collections.Generic;

public class RangeViewer : MonoBehaviour
{
    [SerializeField] GameObject _tileGreenPrefab;
    [SerializeField] GameObject _tileRedPrefab;
    [SerializeField] GameObject _unitGreenPrefab;
    [SerializeField] GameObject _unitRedPrefab;
    
    private List<GameObject> _viewers = new List<GameObject>();
    private StateContext _context;

    private PriorityQueue<Node> _priorityQueue;
    private Dictionary<Vector2Int, Node> _nodeDictionary;
    private int _range;

    private int MapSize => (int)StageData.Instance.MapSize;
    private Dictionary<MapTileEnum, MapTileSO> _tileDictionary => MapDataProvider.Instance.TileDictionary;
    private MapTileEnum[,] _mapTileArray => MapDataProvider.Instance.MapTileArray;
    

    private void Awake()
    {
        InitSearchData();
    }

    public bool IsReachableTile(int x, int z, out Node node)
    {
        return _nodeDictionary.TryGetValue(new Vector2Int(x,z), out node);
    }
    
    public bool TryGetUnit(Vector2Int vec, out Unit unit)
    {
        if (!StageData.Instance.TryGetUnit(vec.x, vec.y, out unit)) return false;
        return true;
    }

    public void ConeView(Vector2Int vec, int range,DirectionEnum direction, UnitTeam target, MarkerType marker)
    {
        ClearViewerObject();
        PlaceViewer(ConeAlgorithm.Cone(vec,range,direction,target),marker);
    }

    public void BfsView(Vector2Int vec, int max, int min, UnitTeam target, MarkerType marker)
    {
        ClearViewerObject();
        PlaceViewer(LocateAlgorithm.Bfs(vec.x, vec.y, max, min,target),marker);
    }

    public void ListView(List<Vector2Int> vec, MarkerType marker)
    {
        ClearViewerObject();
        PlaceViewer(vec,marker);
    }

    public bool CalculateMovement(Vector2Int vec, int range, out List<Vector2Int> list)
    {
        ClearViewerObject();
        _nodeDictionary.Clear(); // 딕셔너리 비우기
        _nodeDictionary = GetNodeDictionary(vec.x, vec.y, range);
        if (!TrySeeMovement(out list)) return false;
        PlaceViewer(list, MarkerType.TileGreen);
        return true;
    }

    private bool TrySeeMovement(out List<Vector2Int> list)
    {
        list = new List<Vector2Int>();
        if (_nodeDictionary.Count == 0) return false;
        foreach (Vector2Int vector in _nodeDictionary.Keys)
        {
            // 여기임 < 뭐가
            if (StageData.Instance.UnitsPosition.ContainsKey(vector)) continue;
            list.Add(vector);
        }
        return true;
    }
    
    

    private void PlaceViewer(List<Vector2Int> positions, MarkerType marker)
    {
        foreach (var vec in positions)
        {
            Vector3 pos = new Vector3(vec.x, 0, vec.y);
            GameObject viewer;
            switch (marker)
            {
                case MarkerType.TileGreen :
                    viewer = Instantiate(_tileGreenPrefab, transform);
                    break;
                case MarkerType.TileRed :
                    viewer = Instantiate(_tileRedPrefab, transform);
                    break;
                case MarkerType.UnitGreen :
                    viewer = Instantiate(_unitGreenPrefab, transform);
                    break;
                case MarkerType.UnitRed :
                    viewer = Instantiate(_unitRedPrefab, transform);
                    break;
                default:
                    throw new ArgumentNullException();
            }
            viewer.transform.position = pos;
            _viewers.Add(viewer);
        }
    }

    public void ClearViewerObject()
    {
        foreach (var viewer in _viewers)
        {
            Destroy(viewer);
        }
        _viewers.Clear();
    }

    public void SetStateContext(StateContext context)
    {
        _context = context;
    }
    
    // ============================================================
    
    public Dictionary<Vector2Int, Node> GetNodeDictionary (int x, int z, int range)
    {
        Dictionary<Vector2Int, Node> nodeDictionary = new Dictionary<Vector2Int, Node>();
        _range = range;
        Node start = new Node(x, z, 0, null); // 시작 노드 지정
        
        _priorityQueue.Enqueue(start); // 시작 노드 큐에 추가
        while (_priorityQueue.Count > 0) // 큐가 빌때까지 반복
        {
            Node currentNode = _priorityQueue.Dequeue(); // 맨 앞 노드 하나 검사
            if (IsOldCheap(currentNode, nodeDictionary, out nodeDictionary)) continue; // 신규&최소비용 검사
            CheckAdjacency(currentNode, nodeDictionary, out nodeDictionary);
        } 
        nodeDictionary.Remove(new Vector2Int(x,z));
        return nodeDictionary;
    }
    
    private bool IsOldCheap(Node node
        , Dictionary<Vector2Int, Node> nodeDictionary, out Dictionary<Vector2Int, Node> result)
    {
        result = nodeDictionary;
        Vector2Int vector = new Vector2Int(node.x, node.z);
        if (!nodeDictionary.TryGetValue(vector, out var old))
        {
            nodeDictionary.Add(vector, node);
            return false;
        }
        if (old == node) return false;
        return node.CompareTo(old) >= 0;
    }

    private void CheckAdjacency(Node node
        , Dictionary<Vector2Int, Node> nodeDictionary, out Dictionary<Vector2Int, Node> result)
    {
        result = nodeDictionary;
        TryAddNode(node.x,node.z+1, node, nodeDictionary, nodeDictionary); // 북
        TryAddNode(node.x,node.z-1, node, nodeDictionary, nodeDictionary); // 남
        TryAddNode(node.x+1,node.z, node, nodeDictionary, nodeDictionary); // 동
        TryAddNode(node.x-1,node.z, node, nodeDictionary, nodeDictionary); // 서
    }

    private void TryAddNode(int x, int z, Node node
        , Dictionary<Vector2Int, Node> nodeDictionary, Dictionary<Vector2Int, Node> result)
    {
        if (!IsNodeExists(x, z)) return;
        if (StageData.Instance.UnitsPosition.ContainsKey(new Vector2Int(x,z))) return;
        int cost = GetTileCost(x, z) + node.Cost;
        if (cost > _range) return;
        Node newNode = new Node(x, z, cost, node);
        if (IsOldCheap(newNode, nodeDictionary, out nodeDictionary)) return;
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
    
    private void InitSearchData()
    {
        _nodeDictionary = new Dictionary<Vector2Int, Node>();
        _priorityQueue = new PriorityQueue<Node>();
    }
}
