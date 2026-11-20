using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class WeaponBehavior
{
    [SerializeField] private RangeViewer _rangeViewer;
    
    private Dictionary<Vector2Int, Unit> Positions => StageData.Instance.UnitsPosition;
    
    public void ProjectileAttack(WeaponData data, Vector2Int target)
    {
        Tile mainTile = MapDataProvider.Instance.TileArray[target.x, target.y];
        Unit mainTarget = Positions[target];
        int damage = ProjectileDamage(data.Pierce, data.Accuracy, data.Shots, data.Damage, mainTile);
        mainTarget.TakeDamage(damage);
        Debug.Log($"단일 공격 : {mainTarget}에게 {damage}의 데미지");
    }
    
    
    public bool IsUnitsInRange(WeaponData data
        , List<Vector2Int> range
        , out List<Unit> inRange)
    {
        inRange = new List<Unit>();
        foreach (Vector2Int vec in range)
        {
            if (Positions.TryGetValue(vec, out Unit unit)) inRange.Add(unit);
        }
        return inRange.Count > 0;
    }

    public int ProjectileDamage(int pierce, int accuracy, int shots, int damage, Tile tile)
    {
        int hits = 0;
        PierceResult pierceResult = CalculatePierce(pierce, tile);
        switch (pierceResult)
        {
            case PierceResult.Over:
            case PierceResult.Pierce:
                break;
            case PierceResult.Half:
                accuracy /= 2;
                break;
            case PierceResult.Full:
                accuracy /= 5;
                break;
        }
        for (int i = 0; i < shots; i++)
            if (HitCheck(accuracy))
                hits++;
        return hits * damage;
    }

    private int ExplosiveDamage(int accuracy, int damage)
    {
        return HitCheck(accuracy) ? damage : damage / 2;
    }

    public PierceResult CalculatePierce(int pierce, Tile tile)
    { 
        return (PierceResult)Mathf.Clamp(tile.Armor - pierce, -1, 2);
    }

    public bool CanAttack(WeaponData data, Vector2Int target, Unit attacker, List<Vector2Int> attackRange)
    {
        if (!attackRange.Contains(target)) return false;
        if (data.IsNeedMount && !attacker.IsMounted) return false;
        switch (data.AttackType)
        {
            case WeaponAttackType.SingleTarget:
                return Positions.TryGetValue(target, out Unit unit) && unit.Team != attacker.Team;
            case WeaponAttackType.RadiusCircle:
            case WeaponAttackType.RadiusCone:
            default:
                break;
        }
        return false;
    }

    private bool HitCheck(int accuracy)
    {
        if (accuracy >= 100) return true;
        StageData.Instance.AddCount();
        int seed = StageData.Instance.MapSeed + StageData.Instance.Counts;
        Random.InitState(seed);
        int result = Random.Range(0, 100);
        return result <= accuracy;
    }
    
    
    private List<Vector2Int> CheckWeaponRange(WeaponData data, Unit unit)
    {
        Vector2Int vector = unit.Vector2;
        switch (data.AttackType)
        {
            case WeaponAttackType.SingleTarget:
                return LocateAlgorithm.Bfs(vector, data.MinRange, data.MaxRange,unit.GetEnemyTeam());
            case WeaponAttackType.RadiusCircle:
                return LocateAlgorithm.Bfs(vector, data.MinRange, data.MaxRange,UnitTeam.None);
            case WeaponAttackType.RadiusCone:
                return ConeAlgorithm.Cone(vector, data.MaxRange, unit.Direction, UnitTeam.None);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
