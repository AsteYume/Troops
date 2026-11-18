using UnityEngine;
using System.Collections.Generic;

public class SkillBehavior
{
    [SerializeField] private RangeViewer _rangeViewer;







    private List<Vector2Int> CheckWeaponTarget(WeaponData data, Unit unit, List<Vector2Int> range)
    {
        switch (data.AttackType)
        {
            case WeaponAttackType.SingleTarget:
                
            case WeaponAttackType.RadiusCircle:
                return LocateAlgorithm.Bfs(unit.X, unit.Z, data.MinRange, data.MaxRange);
            case WeaponAttackType.RadiusCone:
                break;
            default:
                break;
        }
        return null;
    }
    
    private List<Vector2Int> CheckWeaponRange(WeaponData data, Unit unit) 
    {
        switch (data.AttackType)
        {
            case WeaponAttackType.SingleTarget:
            case WeaponAttackType.RadiusCircle:
                return LocateAlgorithm.Bfs(unit.X, unit.Z, data.MinRange, data.MaxRange);
            case WeaponAttackType.RadiusCone:
                break;
            default:
                break;
        }
        return null;
    }
}
