using System;
using System.Collections.Generic;
using UnityEngine;

public static class AIBehavior
{
    
    
    
    public static void AIMove(Unit unit)
    {
        Vector2Range sight; 
        List<Unit> targets;
        switch (unit.AI)
        {
            case AIType.Hold:
                sight = GetVector2Range(unit.Vector2, unit.Sight);
                targets = GetTargets(sight);
                if (unit.CanUsePrimary && unit.Primary is WeaponData weapon1)
                {
                    TryGetBestAttack(unit, weapon1, targets, out Vector3Int result1);
                    
                }
                if (unit.CanUseSecondary && unit.Secondary is WeaponData weapon2)
                    TryGetBestAttack(unit, weapon2, targets, out Vector3Int result2);
                if (unit.CanUseTertiary && unit.Tertiary is WeaponData weapon3)
                    TryGetBestAttack(unit, weapon3, targets, out Vector3Int result3);
                
                
                
                break;
            case AIType.Sentry:
                sight = GetVector2Range(unit.Vector2, unit.Sight);
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
    
    
    private static Vector2Range GetVector2Range(Vector2Int center, int range)
    {
        return new Vector2Range
        {
            MinX = Mathf.Clamp(center.x - range, 0, (int)StageData.Instance.MapSize-1),
            MaxX = Mathf.Clamp(center.x + range, 0, (int)StageData.Instance.MapSize-1),
            MinZ = Mathf.Clamp(center.y - range, 0, (int)StageData.Instance.MapSize-1),
            MaxZ = Mathf.Clamp(center.y + range, 0, (int)StageData.Instance.MapSize-1)
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
        Unit attacker, WeaponData weapon, List<Unit> targets, out Vector3Int result)
    {
        result = Vector3Int.zero;
        int expect ;

        switch (weapon.AttackType)
        {
            case WeaponAttackType.SingleTarget:
                expect = weapon.Accuracy * weapon.Damage * weapon.Shots;
                foreach (var target in targets)
                {
                    int distance = GetDistanceValue(attacker.Vector2, target.Vector2);
                    if (result.z < expect+distance) 
                        result = new Vector3Int(target.Vector2.x, target.Vector2.y, expect+distance);
                }
                break;
            case WeaponAttackType.RadiusCone:
                result.z = 0;
                expect = weapon.Accuracy * weapon.Damage * weapon.Shots;
                foreach (var target in targets)
                {
                    result.z = target.Team == attacker.Team ? -expect/2 : +expect;
                }
                break;
            case WeaponAttackType.RadiusCircle:
                expect = (weapon.Accuracy * weapon.Damage
                          + (100 - weapon.Accuracy) * weapon.Damage) * weapon.Shots;
                foreach (var target in targets)
                {
                    Vector2Range radius = GetVector2Range(target.Vector2, weapon.Radius);
                    List<Unit> units = GetTargets(radius);
                    int value = 0;
                    foreach (var unit in units)
                    {
                        value = unit.Team == attacker.Team ? -expect/2 : +expect;
                    }
                    if (result.z < value)
                        result = new Vector3Int(target.Vector2.x, target.Vector2.y, value);
                }
                break;
        }
        return result.z != 0;
    }

    private static int GetDistanceValue(Vector2Int a, Vector2Int b)
    {
        return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y)) * 100;
    }
    
}
