using System;
using UnityEngine;
using System.Collections.Generic;

public class UnitDataProvider : MonoBehaviour
{
    [SerializeField] private List<UnitDataSo> _allySOList;
    [SerializeField] private List<UnitDataSo> _enemySOList;
    
    [SerializeField] private TextAsset _allyData;
    [SerializeField] private TextAsset _enemyData;
    [SerializeField] private TextAsset _aiData;

    private Dictionary<UnitTypeEnum, UnitDataSo> _allyDictionary = new();
    private Dictionary<UnitTypeEnum, UnitDataSo> _enemyDictionary = new();
    private UnitPlacer _unitPlacer;

    private UnitTeam _ally = UnitTeam.Blue;
    private UnitTeam _enemy = UnitTeam.Red;
    private int _count = 0;


    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        ReadData();
    }

    private void Init()
    {
        _unitPlacer = GetComponent<UnitPlacer>();
        foreach (var unit in _allySOList)
        {
            _allyDictionary.Add(unit.UnitType, unit);
        }
        foreach (var unit in _enemySOList)
        {
            _enemyDictionary.Add(unit.UnitType, unit);
        }
    }
    
    
    private void ReadData()
    {
        ReadAllyUnit();
        ReadEnemyAI();
        ReadEnemyUnit();
        _unitPlacer.PlaceUnits(this);
    }
    
    // ============================================================

    private void ReadAllyUnit()
    {
        string[] lines = _allyData.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 1;  i < lines.Length; i++)
        {
            ParseAllyLine(lines[i]);
        }
    }

    private void ReadEnemyUnit()
    {
        string[] lines = _enemyData.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 1;  i < lines.Length; i++)
        {
            ParseEnemyLine(lines[i]);
        }
    }
    
    private void ReadEnemyAI()
    {
        string[] lines = _aiData.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 1;  i < lines.Length; i++)
        {
            ParseAILine(lines[i]);
        }
    }
    
    // ============================================================
    
    private void ParseAllyLine(string line)  
    {  
        string[] cells = line.Split(',');
        TrimCells(cells);
        Debug.Log(line);

        if (Enum.TryParse(cells[0], out UnitTypeEnum unitType)  
            && int.TryParse(cells[1], out int startX)  
            && int.TryParse(cells[2], out int startZ)  
            && Enum.TryParse(cells[3], out DirectionEnum direction))
        {
            _count++;
            AllyUnit unit = new AllyUnit(GetUnitData(unitType,_ally), direction, startX, startZ,_count);
            StageData.AddAlly(unit);
        }
    }
    
    private void ParseEnemyLine(string line)
    {
        Debug.Log(line);
        string[] cells = line.Split(',');

        TrimCells(cells);   

        if (Enum.TryParse(cells[0], out UnitTypeEnum unitType)  
            && int.TryParse(cells[1], out int startX)  
            && int.TryParse(cells[2], out int startZ)  
            && Enum.TryParse(cells[3], out DirectionEnum direction)
            && Enum.TryParse(cells[4], out EnemyAIGroup aiGroup))
        {
            _count++;
            EnemyUnit unit = new EnemyUnit(GetUnitData(unitType,_enemy), direction, startX, startZ,_count, aiGroup);
            StageData.AddEnemy(unit);
        }
    }
    
    private void ParseAILine(string line)  
    {  
        string[] cells = line.Split(',');
        TrimCells(cells);

        if (Enum.TryParse(cells[0], out EnemyAIGroup aiGroup)
            && Enum.TryParse(cells[1], out EnemyAIType aiType)  
            && int.TryParse(cells[2], out int targetX)  
            && int.TryParse(cells[3], out int targetZ)) 
        {
            StageData.AddAI(new EnemyAI(aiGroup, aiType, targetX, targetZ));
        }
        

    }
    
    // ============================================================

    private UnitDataSo GetUnitData(UnitTypeEnum unitType, UnitTeam team)
    {
        switch (team)
        {
            case  UnitTeam.Blue:
                return _allyDictionary[unitType];
            case  UnitTeam.Red:
                return _enemyDictionary[unitType];
            default:
                return null;
        }
    }
    
    private void TrimCells(string[] cells)  
    {  
        for (int i = 0; i < cells.Length; i++)  
        {  
            cells[i] = cells[i].Trim();  
        }  
    }

    public void ClearData()
    {
        _count = 0;
        _allyData = null;
        _enemyData = null;
        _aiData = null;
    }

    public GameObject GetPrefab(UnitTypeEnum unitType, UnitTeam team)
    {
        switch (team)
        {
            case  UnitTeam.Blue:
                return _allyDictionary[unitType].Prefab;
            case  UnitTeam.Red:
                return _enemyDictionary[unitType].Prefab;
            default:
                return null;
        }
    }
}
