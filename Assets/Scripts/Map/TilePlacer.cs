using System;
using UnityEngine;

public class TilePlacer : MonoBehaviour
{
    [SerializeField] private MapTileSO plainSO;
    [SerializeField] private MapTileSO lightSO;
    [SerializeField] private MapTileSO heavySO;
    [SerializeField] private MapTileSO blockSO;
    [SerializeField] private MapTileSO roadsSO;
    [SerializeField] private MapTileSO waterSO;
    
    
    // ================================================================================
    
    private GameObject[,] _mapTilePrefabs;
    private MapTileEnum[,] _mapTileData;
    private MapSizeEnum _mapSizeEnum;
    
    // ================================================================================

    private GameObject PlainPrefab => plainSO.TilePrefab;
    private GameObject LightPrefab => lightSO.TilePrefab;
    private GameObject HeavyPrefab => heavySO.TilePrefab;
    private GameObject BlockPrefab => blockSO.TilePrefab;
    private GameObject RoadPrefab => roadsSO.TilePrefab;
    private GameObject WaterPrefab => waterSO.TilePrefab;

    private int MapSize => (int)_mapSizeEnum;
    private bool HasMapPrefabs => _mapTilePrefabs != null;
    private bool HasMapTileData => _mapTileData != null;
    
    // ================================================================================

    public void Init(MapTileEnum[,] mapTileData, MapSizeEnum mapSizeEnum)
    {
        _mapTileData = mapTileData;
        _mapSizeEnum = mapSizeEnum;
        _mapTilePrefabs = new GameObject[MapSize, MapSize];
        PlaceMapTiles();
    }
    
    // ================================================================================

    private void PlaceMapTiles()
    {
        for (int z = 0; z < MapSize; z++)
        {
            for (int x = 0; x < MapSize; x++)
            {
                MapTileEnum mapTile = _mapTileData[x, z];
                Vector3 vec = new Vector3(x, 0, z);
                GameObject tile = Instantiate(SetTiles(mapTile));
                _mapTilePrefabs[x,z] = tile;
                tile.transform.position = vec;
            }
        }
    }

    private GameObject SetTiles(MapTileEnum tileEnum)
    {
        switch (tileEnum)
        {
            case (MapTileEnum.Plain):
                return PlainPrefab;
            case (MapTileEnum.Light):
                return LightPrefab;
            case (MapTileEnum.Heavy):
                return HeavyPrefab;
            case (MapTileEnum.Block):
                return BlockPrefab;
            case (MapTileEnum.Roads):
                return RoadPrefab;
            case (MapTileEnum.Water):
                return WaterPrefab;
                
            default:
                throw new ArgumentException("TilePlacer : 프리팹 누락");
        }
    }

    private void ClearArrays()
    {
        if (HasMapTileData) Array.Clear(_mapTileData, 0, _mapTileData.Length);
        if (HasMapPrefabs) Array.Clear(_mapTilePrefabs, 0, _mapTilePrefabs.Length);
    }
}
