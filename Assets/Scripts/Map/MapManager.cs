using System;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private TilePlacer _tilePlacer;
    
    private MapSizeEnum MapSizeEnum = MapSizeEnum.Small;
    
    private int MapSize => (int)MapSizeEnum;
    
    private void Awake()
    {
        _tilePlacer = GetComponent<TilePlacer>();
    }

    private void Start()
    {
        PlaceMap(); 
    }
    
    private void PlaceMap()
    {
        _tilePlacer.SetMap();
    }


}
