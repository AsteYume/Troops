using System.Collections.Generic;
using UnityEngine;

public class StageData
{
    public static StageData _instance;
    public static StageData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StageData();
            }
            return _instance;
        }
    }
    
    private int _counts = 0;
    private int _stageNumber = 0;
    private MapSizeEnum _mapSize = MapSizeEnum.None;
    
    private UnitTypeEnum _allyMvp;
    private UnitTypeEnum _enemyMvp;
    private List<AllyUnit> _allyUnits = new();
    private List<EnemyUnit> _enemyUnits = new();
    private Dictionary<EnemyAIGroup, EnemyAI> _enemyAis = new();
    private Dictionary<Vector2Int, Unit> _unitsPosition = new();
    
    // ============================================================
    
    public Dictionary<Vector2Int, Unit> UnitsPosition => _unitsPosition;
    public List<AllyUnit> AllyUnits => _allyUnits;
    public List<EnemyUnit> EnemyUnits => _enemyUnits;
    
    public int MapSeed => _stageNumber.GetHashCode();
    public int Counts => _counts;

    public MapSizeEnum MapSize => _mapSize;

    public bool IsEnemyWin => _allyUnits.Count == 0 
                                           || (_allyMvp != UnitTypeEnum.None 
                                           && !_allyUnits.Exists(u => u.UnitType == _allyMvp));
    
    public bool IsAllyWin => _enemyUnits.Count == 0 
                                    || ( _enemyMvp != UnitTypeEnum.None 
                                    && !_enemyUnits.Exists(u => u.UnitType == _enemyMvp));



    
    // ============================================================

    public void MoveUnit(Unit unit, Vector2Int targetPosition)
    {
        _unitsPosition.Remove(unit.Vector2);
        _unitsPosition.Add(targetPosition, unit);
    }

    public void SetMapSize(int mapSize)
    {
        _mapSize = (MapSizeEnum)mapSize;
    }

    public void SetMapSize(MapSizeEnum mapSize)
    {
        _mapSize = mapSize;
    }

    public bool TryGetUnit(int x, int z, out Unit unit)
    {
        return _unitsPosition.TryGetValue(new Vector2Int(x,z), out unit);
    }
    
    
    public void ClearStageData()
    {
        _stageNumber = 0;
        _allyMvp = UnitTypeEnum.None;
        _enemyMvp = UnitTypeEnum.None;
        _enemyAis.Clear();
        _allyUnits.Clear();
        _enemyUnits.Clear();
    }

    public void AddAI(EnemyAI ai)
    {
        _enemyAis.Add(ai.AIGroup, ai);
    }

    public void RemoveAlly(AllyUnit allyUnit)
    {
        _unitsPosition.Remove(allyUnit.Vector2);
        _allyUnits.Remove(allyUnit);
    }

    public void AddAlly(AllyUnit allyUnit)
    {
        _allyUnits.Add(allyUnit);
        _unitsPosition.Add(allyUnit.Vector2, allyUnit);
    }

    public void RemoveEnemy(EnemyUnit enemyUnit)
    {
        _unitsPosition.Remove(enemyUnit.Vector2);
        _enemyUnits.Remove(enemyUnit);
    }

    public void AddEnemy(EnemyUnit enemyUnit)
    {
        _enemyUnits.Add(enemyUnit);
        _unitsPosition.Add(enemyUnit.Vector2, enemyUnit);
    }
    
    public void SetStage(int stageNumber)
    {
        _stageNumber = stageNumber;
    }

    public void AddCount()
    {
        _counts++;
    }
}
