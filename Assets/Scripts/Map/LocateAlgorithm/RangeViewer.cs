using UnityEngine;
using System.Collections.Generic;

public class RangeViewer : MonoBehaviour
{
    [SerializeField] GameObject _moveViewerPrefab;
    [SerializeField] GameObject _rangeViewerPrefab;
    
    private List<GameObject> _viewers = new List<GameObject>();

    private PriorityQueue<Node> _priorityQueue;
    private Dictionary<(int, int), Node> _nodeDictionary;
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
        return _nodeDictionary.TryGetValue((x,z), out node);
    }
    
    public bool TryGetUnit(Vector2Int vec, out Unit unit)
    {
        if (!StageData.Instance.TryGetUnit(vec.x, vec.y, out unit)) return false;
        return true;
    }

    public void ConeView(Vector2Int vec, int range,DirectionEnum direction)
    {
        ClearViewerObject();
        PlaceViewer(ConeAlgorithm.Cone(vec,range,direction),true);
    }

    public void BfsView(Vector2Int vec, int max, int min)
    {
        ClearViewerObject();
        PlaceViewer(LocateAlgorithm.Bfs(vec.x, vec.y, max, min),true);
    }

    public void CalculateMovement(Vector2Int vec, int range)
    {
        ClearViewerObject();
        GetNodeDictionary(vec.x, vec.y, range);
        if (TrySeeMovement(out var list)) PlaceViewer(list,false);
    }

    public bool TrySeeMovement(out List<Vector2Int> list)
    {
        list = new List<Vector2Int>();
        if (_nodeDictionary.Count == 0) return false;
        foreach ((int, int) vector in _nodeDictionary.Keys)
        {
            if (StageData.Instance.UnitsPosition.ContainsKey(vector)) continue;
            list.Add(new Vector2Int(vector.Item1, vector.Item2));
        }
        return true;
    }
    

    private void PlaceViewer(List<Vector2Int> positions, bool isSkill)
    {
        foreach (var vec in positions)
        {
            Vector3 pos = new Vector3(vec.x, 0, vec.y);
            GameObject viewer = isSkill ? Instantiate(_rangeViewerPrefab, transform)
                    : Instantiate(_moveViewerPrefab,transform);
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
    
    // ============================================================
    
    private Dictionary<(int, int), Node> GetNodeDictionary (int x, int z, int range)
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
        if (StageData.Instance.UnitsPosition.ContainsKey((x,z))) return;
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
    
    private void InitSearchData()
    {
        _nodeDictionary = new Dictionary<(int, int), Node>();
        _priorityQueue = new PriorityQueue<Node>();
    }
}
