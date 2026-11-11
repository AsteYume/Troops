using UnityEngine;

[CreateAssetMenu(fileName = "MapTile", menuName = "Scriptable Objects/MapTileSO")]
public class MapTileSO : ScriptableObject
{
    [Tooltip("타일 종류")] [SerializeField] private MapTileEnum _tileType;
    [Tooltip("프리팹")] [SerializeField] private GameObject _tilePrefab;
    [Tooltip("이동 비용")] [SerializeField] private int _moveCost;
    [Tooltip("시야 비용")] [SerializeField] private int _sightCost;
    [Tooltip("장갑")] [SerializeField] private int _armor;
    [Tooltip("통행 가능 여부")] [SerializeField] private bool _canPass;
    [Tooltip("관통 가능 여부")] [SerializeField] private bool _canPierce;
    [Tooltip("정지 가능 여부")] [SerializeField] private bool _canStay;
    [Tooltip("파괴 가능 여부")] [SerializeField] private bool _canDestroy;
    [Tooltip("파괴후 변환되는 타일")] [SerializeField] private MapTileEnum _destroyResult;
    
    public MapTileEnum TileType => _tileType;
    public GameObject TilePrefab => _tilePrefab;
    public int MoveCost => _moveCost;
    public int SightCost => _sightCost;
    public int Armor => _armor;
    public bool CanPass => _canPass;
    public bool CanPierce => _canPierce;
    public bool CanStay => _canStay;
    public bool CanDestroy => _canDestroy;
    public MapTileEnum DestroyResult => _destroyResult;
}
