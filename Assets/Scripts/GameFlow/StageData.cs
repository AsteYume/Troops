using System.Collections.Generic;
using UnityEngine;

public static class StageData
{
    private static int _stageNumber = 0;
    private static MapSizeEnum _mapSize = MapSizeEnum.None;
    
    private static UnitTypeEnum _allyMvp;
    private static UnitTypeEnum _enemyMvp;
    private static List<AllyUnit> _allyUnits = new();
    private static List<EnemyUnit> _enemyUnits = new();
    private static Dictionary<EnemyAIGroup, EnemyAI> _enemyAis = new();
    private static Dictionary<(int, int), Unit> _unitsPosition = new();
    
    // ============================================================
    
    public static Dictionary<(int, int), Unit> UnitsPosition => _unitsPosition;
    public static List<AllyUnit> AllyUnits => _allyUnits;
    public static List<EnemyUnit> EnemyUnits => _enemyUnits;

    public static MapSizeEnum MapSize => _mapSize;

    public static bool IsEnemyWin => _allyUnits.Count == 0 
                                           || (_allyMvp != UnitTypeEnum.None 
                                           && !_allyUnits.Exists(u => u.UnitType == _allyMvp));
    
    public static bool IsAllyWin => _enemyUnits.Count == 0 
                                    || ( _enemyMvp != UnitTypeEnum.None 
                                    && !_enemyUnits.Exists(u => u.UnitType == _enemyMvp));



    
    // ============================================================

    public static void MoveUnit(Unit unit, Vector2Int targetPosition)
    {
        _unitsPosition.Remove((unit.X, unit.Z));
        _unitsPosition.Add((targetPosition.x, targetPosition.y), unit);
    }

    public static void SetMapSize(int mapSize)
    {
        _mapSize = (MapSizeEnum)mapSize;
    }

    public static void SetMapSize(MapSizeEnum mapSize)
    {
        _mapSize = mapSize;
    }

    public static bool TryGetUnit(int x, int z, out Unit unit)
    {
        return _unitsPosition.TryGetValue((x, z), out unit);
    }
    
    
    public static void ClearStageData()
    {
        _stageNumber = 0;
        _allyMvp = UnitTypeEnum.None;
        _enemyMvp = UnitTypeEnum.None;
        _enemyAis.Clear();
        _allyUnits.Clear();
        _enemyUnits.Clear();
    }

    public static void AddAI(EnemyAI ai)
    {
        _enemyAis.Add(ai.AIGroup, ai);
    }

    public static void RemoveAlly(AllyUnit allyUnit)
    {
        _unitsPosition.Remove((allyUnit.X, allyUnit.Z));
        _allyUnits.Remove(allyUnit);
    }

    public static void AddAlly(AllyUnit allyUnit)
    {
        _allyUnits.Add(allyUnit);
        _unitsPosition.Add((allyUnit.X,allyUnit.Z), allyUnit);
    }

    public static void RemoveEnemy(EnemyUnit enemyUnit)
    {
        _unitsPosition.Remove((enemyUnit.X, enemyUnit.Z));
        _enemyUnits.Remove(enemyUnit);
    }

    public static void AddEnemy(EnemyUnit enemyUnit)
    {
        _enemyUnits.Add(enemyUnit);
        _unitsPosition.Add((enemyUnit.X,enemyUnit.Z), enemyUnit);
    }
    
    public static void SetStage(int stageNumber)
    {
        _stageNumber = stageNumber;
    }
}
