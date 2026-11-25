using System;
using UnityEngine;
using System.Collections.Generic;

public class UnitDataProvider : MonoBehaviour
{
    [SerializeField] private List<UnitDataSo> _allySOList;
    [SerializeField] private List<UnitDataSo> _enemySOList;
    
    [SerializeField] private List<TextAsset> _allyData;
    [SerializeField] private List<TextAsset> _enemyData;
    [SerializeField] private List<TextAsset> _aiData;

    private Dictionary<UnitTypeEnum, UnitDataSo> _allyDictionary = new();
    private Dictionary<UnitTypeEnum, UnitDataSo> _enemyDictionary = new();
    private UnitPlacer _unitPlacer;

    private UnitTeam _ally = UnitTeam.Blue;
    private UnitTeam _enemy = UnitTeam.Red;


    private void Awake()
    {
        Init();
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
    
    
    public void ReadData(int stage)
    {
        int index = stage - 1;
        ReadAllyUnit(index);
        ReadEnemyAI(index);
        ReadEnemyUnit(index);
        _unitPlacer.PlaceUnits(this);
    }
    
    // ============================================================

    private void ReadAllyUnit(int index)
    {
        TextAsset ally = _allyData[index];
        string[] lines = ally.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 1;  i < lines.Length; i++)
        {
            ParseAllyLine(lines[i]);
        }
    }

    private void ReadEnemyUnit(int index)
    {
        TextAsset enemy = _enemyData[index];
        string[] lines = enemy.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 1;  i < lines.Length; i++)
        {
            ParseEnemyLine(lines[i]);
        }
    }
    
    private void ReadEnemyAI(int index)
    {
        TextAsset ai = _aiData[index];
        string[] lines = ai.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
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

        if (Enum.TryParse(cells[0], out UnitTypeEnum unitType)  
            && int.TryParse(cells[1], out int startX)  
            && int.TryParse(cells[2], out int startZ)  
            && Enum.TryParse(cells[3], out DirectionEnum direction))
        {
            AllyUnit unit = new AllyUnit(GetUnitData(unitType,_ally), direction, startX, startZ,AIType.Chase);
            StageData.Instance.AddAlly(unit);
        }
    }
    
    private void ParseEnemyLine(string line)
    {
        string[] cells = line.Split(',');

        TrimCells(cells);   

        if (Enum.TryParse(cells[0], out UnitTypeEnum unitType)  
            && int.TryParse(cells[1], out int startX)  
            && int.TryParse(cells[2], out int startZ)  
            && Enum.TryParse(cells[3], out DirectionEnum direction)
            && Enum.TryParse(cells[4], out EnemyAIGroup aiGroup))
        {
            EnemyUnit unit = new EnemyUnit(GetUnitData(unitType,_enemy), direction, startX, startZ, AIType.Hold, aiGroup);
            StageData.Instance.AddEnemy(unit);
        }
    }
    
    private void ParseAILine(string line)  
    {  
        string[] cells = line.Split(',');
        TrimCells(cells);

        if (Enum.TryParse(cells[0], out EnemyAIGroup aiGroup)
            && Enum.TryParse(cells[1], out AIType aiType)  
            && int.TryParse(cells[2], out int targetX)  
            && int.TryParse(cells[3], out int targetZ)) 
        {
            StageData.Instance.AddAI(new EnemyAI(aiGroup, aiType, targetX, targetZ));
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
