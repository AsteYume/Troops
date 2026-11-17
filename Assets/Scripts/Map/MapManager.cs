using System;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    
    private TilePlacer _tilePlacer;
    private MapDataProvider _mapDataProvider;
    private MapSizeEnum MapSizeEnum = MapSizeEnum.Small;
    
    // ============================================================
    
    private int MapSize => (int)MapSizeEnum;

    public static MapManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<MapManager>();
            }
            if (_instance == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<PhaseManager>();
            }
            return _instance;
        }
    }
    
    // ============================================================
    
    private void Awake()
    {
        SetSingleTon();
        Init();
    }
    
    // ============================================================
    
    public void StageStart(int stage)
    {
        _mapDataProvider.StageStart(stage);
        _tilePlacer.SetMap();
    }
    
    // ============================================================

    private void Init()
    {
        _tilePlacer = GetComponent<TilePlacer>();
        _mapDataProvider = GetComponent<MapDataProvider>();
    }
    
    
    private void SetSingleTon()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }


}
