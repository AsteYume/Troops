using System;
using UnityEngine;

public class TilePlacer : MonoBehaviour
{
    [SerializeField] private GameObject plainPrefab;
    [SerializeField] private GameObject lightPrefab;
    [SerializeField] private GameObject heavyPrefab;
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject waterPrefab;
    
    // ================================================================================
    
    private GameObject[,] _mapTilePrefabs;
    private MapTileEnum[,] _mapTileData;
    
    private MapSizeEnum _mapSizeEnum;
    
    // ================================================================================

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
        Debug.Log($"{_mapTileData.Length} , {MapSize}");
        
        for (int z = 0; z < MapSize; z++)
        {
            for (int x = 0; x < MapSize; x++)
            {
                MapTileEnum mapTile = _mapTileData[x, z];
                Vector3 vec = new Vector3(x, 0, z);
                Debug.Log($"{x} , {z} 에 {mapTile} 설치 시도");
                GameObject tile = Instantiate(SetTiles(mapTile));
                Debug.Log($"{tile.name} 등록 시도");
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
                return plainPrefab;
            case (MapTileEnum.Light):
                return lightPrefab;
            case (MapTileEnum.Heavy):
                return heavyPrefab;
            case (MapTileEnum.Block):
                return blockPrefab;
            case (MapTileEnum.Water):
                return waterPrefab;
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
