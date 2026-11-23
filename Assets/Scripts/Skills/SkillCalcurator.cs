using System;
using UnityEngine;
using System.Collections.Generic;

public class SkillCalcurator
{
    public List<Vector2Int> GetSkillRange(SkillSO skill, Vector2Int center, Unit unit)
    {
        switch (skill.SkillType)
        {
            case SkillsEnum.Attack :
                return GetWeaponRange((WeaponData)skill);
            case SkillsEnum.Utility :
                return GetUtilityRange((UtilityData)skill, center, unit);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private List<Vector2Int> GetWeaponRange(WeaponData weapon)
    {
        switch (weapon.AttackType)
        {
            case WeaponAttackType.SingleTarget :
                break;
            case WeaponAttackType.RadiusCone :
                break;
            case WeaponAttackType.RadiusCircle :
                break;
        }
        return new List<Vector2Int>();
    }

    private List<Vector2Int> GetUtilityRange(UtilityData utility, Vector2Int center, Unit unit)
    {
        List<Vector2Int> result = new List<Vector2Int>();
        switch (utility.UtilityType)
        {
            case UtilityType.SelectDirection :
                result.AddRange(ConeAlgorithm.Cone(center, utility.MaxRange, DirectionEnum.North, UnitTeam.None));
                result.AddRange(ConeAlgorithm.Cone(center, utility.MaxRange, DirectionEnum.South, UnitTeam.None));
                result.AddRange(ConeAlgorithm.Cone(center, utility.MaxRange, DirectionEnum.East, UnitTeam.None));
                result.AddRange(ConeAlgorithm.Cone(center, utility.MaxRange, DirectionEnum.West, UnitTeam.None));
                break;
            case UtilityType.TargetTile :
                break;
            default:
                throw new NullReferenceException();
        }
        return new List<Vector2Int>();
    }
    
    
}
