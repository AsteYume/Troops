using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private static UnitManager _instance;
    private UnitDataProvider _unitData;
    private UnitPlacer _unitPlacer;
    
    
    public static UnitManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<UnitManager>();
            }
            if (_instance == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<PhaseManager>();
            }
            return _instance;
        }
    }
    
    
    public void StageStart(int stage)
    {
        _unitData.ReadData(stage);
    }

    private void Awake()
    {
        SetSingleTon();
        Init();
    }

    private void Init()
    {
        _unitData = GetComponent<UnitDataProvider>();
        _unitPlacer = GetComponent<UnitPlacer>();
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
