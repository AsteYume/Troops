using UnityEngine;

public class TilePlacer : MonoBehaviour
{
    private MapDataProvider _provider;
    private GameObject[,] _mapTilePrefabs;
    
    // ================================================================================

    private MapTileEnum[,] MapTileArray => _provider.MapTileArray;
    private int MapSize => _provider.MapSize;
    
    // ================================================================================

    private void Awake()
    {
        _provider = GetComponent<MapDataProvider>();
    }
    
    // ================================================================================

    public void SetMap()
    {
        PlaceMapTiles(MapTileArray);
    }
    
    // ================================================================================

    private void PlaceMapTiles(MapTileEnum[,] mapTileData)
    {
        for (int z = 0; z < MapSize; z++)
        {
            for (int x = 0; x < MapSize; x++)
            {
                MapTileEnum mapTile = mapTileData[x, z];
                Vector3 vec = new Vector3(x, 0, z);
                GameObject tile = Instantiate(SetTiles(mapTile), gameObject.transform); 
                _provider.AddTile(tile, x, z, mapTile);
                tile.transform.position = vec;
            }
        }
    }

    private GameObject SetTiles(MapTileEnum tileEnum)
    {
        return _provider.GetTilePrefab(tileEnum);
    }
}
