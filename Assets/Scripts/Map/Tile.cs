using UnityEngine;

public class Tile
{
    public Tile(
        GameObject tileObject,
        MapTileEnum tileType,
        MapDataProvider provider
        )
    {
        _tileObject = tileObject;
        _tileType = tileType;
        _moveCost = provider.GetTileByEnum(_tileType).MoveCost;
        _sightCost = provider.GetTileByEnum(_tileType).SightCost;
        _armor = provider.GetTileByEnum(_tileType).Armor;
        _canPass = provider.GetTileByEnum(_tileType).CanPass;
        _canPierce = provider.GetTileByEnum(_tileType).CanPierce;
        _canStay = provider.GetTileByEnum(_tileType).CanStay;
        _canDestroy = provider.GetTileByEnum(_tileType).CanDestroy;
        _destroyResult = provider.GetTileByEnum(_tileType).DestroyResult;
    }
    
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
    private GameObject _unitObject;
    private Unit _unitData;
    
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
    public GameObject UnitObject => _unitObject;
    public Unit UnitData => _unitData;

    public void SetUnitData(GameObject unitObject ,Unit unitData)
    {
        _unitObject = unitObject;
        _unitData = unitData;
    }
    public void RemoveUnitData()
    {
        _unitData = null;
    }
}
