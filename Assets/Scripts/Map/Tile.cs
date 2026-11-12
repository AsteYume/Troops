using UnityEngine;

public class Tile
{
    public Tile(
        GameObject tileObject,
        MapTileEnum tileType,
        int moveCost,
        int sightCost,
        int armor,
        bool canPass,
        bool canPierce,
        bool canStay,
        bool canDestroy,
        MapTileEnum destroyResult)
    {
        _tileObject = tileObject;
        _tileType = tileType;
        _moveCost = moveCost;
        _sightCost = sightCost;
        _armor = armor;
        _canPass = canPass;
        _canPierce = canPierce;
        _canStay = canStay;
        _canDestroy = canDestroy;
        _destroyResult = destroyResult;
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
}
