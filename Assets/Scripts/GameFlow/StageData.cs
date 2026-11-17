using System.Collections.Generic;

public static class StageData
{
    private static int _stageNumber = 0;
    
    private static UnitTypeEnum _allyMvp;
    private static UnitTypeEnum _enemyMvp;
    private static List<AllyUnit> _allyUnits = new();
    private static List<EnemyUnit> _enemyUnits = new();
    private static Dictionary<EnemyAIGroup, EnemyAI> _enemyAis = new();
    
    // ============================================================
    
    public static List<AllyUnit> AllyUnits => _allyUnits;
    public static List<EnemyUnit> EnemyUnits => _enemyUnits;


    public static bool IsEnemyWin => _allyUnits.Count == 0 
                                           || (_allyMvp != UnitTypeEnum.None 
                                           && !_allyUnits.Exists(u => u.UnitType == _allyMvp));
    
    public static bool IsAllyWin => _enemyUnits.Count == 0 
                                    || ( _enemyMvp != UnitTypeEnum.None 
                                    && !_enemyUnits.Exists(u => u.UnitType == _enemyMvp));



    
    // ============================================================
    
    
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
        _allyUnits.Remove(allyUnit);
    }

    public static void AddAlly(AllyUnit allyUnit)
    {
        _allyUnits.Add(allyUnit);
    }

    public static void RemoveEnemy(EnemyUnit enemyUnit)
    {
        _enemyUnits.Remove(enemyUnit);
    }

    public static void AddEnemy(EnemyUnit enemyUnit)
    {
        _enemyUnits.Add(enemyUnit);
    }
    
    public static void SetStage(int stageNumber)
    {
        _stageNumber = stageNumber;
    }
}
