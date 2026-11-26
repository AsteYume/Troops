using System;
using System.Collections.Generic;
using UnityEngine;

public static class AIBehavior
{
    
    
    
    public static void AIMove(Unit unit, int mapSize)
    {
        Vector2Range sight; 
        List<Unit> targets;
        switch (unit.AI)
        {
            case AIType.Hold:
                sight = GetVector2Range(unit.Vector2, unit.Sight, mapSize);
                targets = GetTargets(sight);
                
                // 
                
                
                break;
            case AIType.Sentry:
                sight = GetVector2Range(unit.Vector2, unit.Sight, mapSize);
                break;
            case AIType.Chase:
                // Chase AI
            break;
            case AIType.Patrol:
            case AIType.Move:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static List<Unit> GetTargets(Vector2Range radius)
    {
        List<Unit> targets = new();
        foreach (Unit unit in StageData.Instance.UnitsPosition.Values)
        {
            if (IsInrange(radius, unit.Vector2)) targets.Add(unit);
        }
        return targets;
    }
    
    
    private static Vector2Range GetVector2Range(Vector2Int center, int range, int mapSize)
    {
        mapSize--;
        return new Vector2Range
        {
            MinX = Mathf.Clamp(center.x - range, 0, mapSize),
            MaxX = Mathf.Clamp(center.x + range, 0, mapSize),
            MinZ = Mathf.Clamp(center.y - range, 0, mapSize),
            MaxZ = Mathf.Clamp(center.y + range, 0, mapSize)
        };
    }

    private static bool IsInrange(Vector2Range radius, Vector2Int target)
    {
        return target.x >= radius.MinX
                && target.x <= radius.MaxX
                && target.y >= radius.MinZ
                && target.y <= radius.MaxZ;
    }
    
    

    private static bool TryGetBestAttack(
        Unit attacker, WeaponData weapon, UnitTeam targetTeam, List<Unit> targets, out Vector3Int result)
    {
        result = Vector3Int.zero;
        switch (weapon.AttackType)
        {
            case WeaponAttackType.SingleTarget:
                foreach (var target in targets)
                {
                    
                }
                break;
            case WeaponAttackType.RadiusCone:
                // 콘
                break;
            case WeaponAttackType.RadiusCircle:
                // 범위
                break;
        }

        return false;
    }
    
}
