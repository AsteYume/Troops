using System.Collections.Generic;

public static class StageData
{
    private static int _stageNumber = 0;
    
    private static UnitTypeEnum _allyMvp;
    private static UnitTypeEnum _enemyMvp;
    private static List<Unit> _allyUnits = new();
    private static List<Unit> _enemyUnits = new();
    
    // ============================================================


    public static bool IsEnemyWin => _allyUnits.Count == 0 
                                           || (_allyMvp != UnitTypeEnum.None 
                                           && !_allyUnits.Exists(u => u.UnitType == _allyMvp));
    
    public static bool IsAllyWin => _enemyUnits.Count == 0 
                                    || ( _enemyMvp != UnitTypeEnum.None 
                                    && !_enemyUnits.Exists(u => u.UnitType == _enemyMvp));



    
    // ============================================================

    public static void SetStageData()
    {
        
    }
    
    public static void ClearStageData()
    {
        _stageNumber = 0;
        _allyMvp = UnitTypeEnum.None;
        _enemyMvp = UnitTypeEnum.None;
        _allyUnits.Clear();
        _enemyUnits.Clear();
    }

    public static void RemoveAlly(Unit allyUnit)
    {
        _allyUnits.Remove(allyUnit);
    }

    public static void AddAlly(Unit allyUnit)
    {
        _allyUnits.Add(allyUnit);
    }

    public static void RemoveEnemy(Unit enemyUnit)
    {
        _enemyUnits.Remove(enemyUnit);
    }

    public static void AddEnemy(Unit enemyUnit)
    {
        _enemyUnits.Add(enemyUnit);
    }
    
    
    public static void SetStage(int stageNumber)
    {
        _stageNumber = stageNumber;
    }
}
