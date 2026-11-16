using UnityEngine;

public class Tile
{
    public Tile(
        GameObject tileObject,
        MapTileEnum tileType
        )
    {
        _tileObject = tileObject;
        _tileType = tileType;
        _moveCost = _provider.GetTileByEnum(_tileType).MoveCost;
        _sightCost = _provider.GetTileByEnum(_tileType).SightCost;
        _armor = _provider.GetTileByEnum(_tileType).Armor;
        _canPass = _provider.GetTileByEnum(_tileType).CanPass;
        _canPierce = _provider.GetTileByEnum(_tileType).CanPierce;
        _canStay = _provider.GetTileByEnum(_tileType).CanStay;
        _canDestroy = _provider.GetTileByEnum(_tileType).CanDestroy;
        _destroyResult = _provider.GetTileByEnum(_tileType).DestroyResult;
    }
    
    private MapDataProvider _provider;
    private GameObject _tileObject;
    private MapTileEnum _tileType;
    private int _moveCost;
    private int _sightCost;
    private int _armor;
    private bool _canPass;
    private bool _canPierce;
    private bool _canStay;
    private bool _canDestroy;
    private MapTileEnum _destroyResult;
    private UnitData _unitData;
    
    public MapTileEnum TileType => _tileType;
    public GameObject TileObject => _tileObject;
    public int MoveCost => _moveCost;
    public int SightCost => _sightCost;
    public int Armor => _armor;
    public bool CanPass => _canPass;
    public bool CanPierce => _canPierce;
    public bool CanStay => _canStay;
    public bool CanDestroy => _canDestroy;
    public MapTileEnum DestroyResult => _destroyResult;
    public bool HasUnit => _unitData != null;
    public UnitData UnitData => _unitData;

    public void SetUnitData(UnitData unitData)
    {
        _unitData = unitData;
    }
    public void RemoveUnitData()
    {
        _unitData = null;
    }
}
