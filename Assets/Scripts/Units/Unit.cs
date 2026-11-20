using System;
using UnityEngine;

public abstract class Unit
{
    private UnitTypeEnum _unitType;
    private string _unitName;
    private int _maxHealth;
    private int _maxSupply;
    private int _maxActivePoint;
    private int _activeRecover;
    private int _sight;
    private int _moveRange;
    private SkillSO _primary;
    private SkillSO _secondary;
    private SkillSO _tertiary;
    private Vector2Int _vector2;
    private int _unitNumber;
    private bool _hasMoved;
    private bool _isMounted;
    
    private GameObject _object;
    
    private int _health;
    private int _supply;
    private int _activePoint;
    private DirectionEnum _direction;
    protected UnitTeam _team;

    public event Action OnDead;
    
    public UnitTypeEnum UnitType => _unitType;
    public string UnitName => _unitName;
    public int MaxHealth => _maxHealth;
    public int MaxSupply => _maxSupply;
    public int MaxActivePoint => _maxActivePoint;
    public int ActiveRecover => _activeRecover;
    public int Sight => _sight;
    public int MoveRange => _moveRange;
    public SkillSO Primary => _primary;
    public SkillSO Secondary => _secondary;
    public SkillSO Tertiary => _tertiary;
    public Vector2Int Vector2 => _vector2;
    public GameObject Object => _object;
    public bool HasMoved => _hasMoved;
    public bool IsMounted => _isMounted;
    
    public bool IsPrimaryExist => _primary != null;
    public bool IsSecondaryExist => _secondary != null;
    public bool IsTertiaryExist => _tertiary != null;
    
    

    public bool CanAction
    {
        get
        {
            if (!_hasMoved) return true;
            return CheckCost(SkillSlotEnum.Primary) || CheckCost(SkillSlotEnum.Secondary) || CheckCost(SkillSlotEnum.Tertiary);
        }
    }
    
    public int Health => _health;
    public int Supply => _supply;
    public int ActivePoint => _activePoint;
    public DirectionEnum Direction => _direction;
    public UnitTeam Team => _team;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) OnDead?.Invoke();
    }

    public void MoveDone(Vector2Int point)
    {
        _hasMoved = true;
        _vector2 = point;
    }

    public void NextTurn()
    {
        _hasMoved = false;
        _activePoint += _activeRecover;
    }


    protected Unit(UnitDataSo unitData, DirectionEnum direction, int x, int z, int number)
    {
        _unitType = unitData.UnitType;
        _unitName = unitData.UnitName;
        _maxHealth = unitData.MaxHealth;
        _maxSupply = unitData.MaxSupply;
        _maxActivePoint = unitData.MaxActivePoint;
        _activeRecover = unitData.ActiveRecover;
        _sight = unitData.Sight;
        _moveRange = unitData.MoveRange;
        _primary = unitData.Primary;
        _secondary = unitData.Secondary;
        _tertiary = unitData.Tertiary;
        _vector2 = new Vector2Int(x, z);
        _unitNumber =  number;

        _health = _maxHealth;
        _supply = _maxSupply;
        _activePoint = _maxActivePoint;
        _direction = direction;
    }
    
    public void SetPrefab(GameObject prefab)
    {
        _object = prefab;
    }

    public bool CheckCost(SkillSlotEnum slot)
    {
        switch (slot)
        {
            case SkillSlotEnum.Primary:
                return IsPrimaryExist
                       && _primary.ActiveCost <= _activePoint 
                       && _primary.SupplyCost <= _supply;
            case SkillSlotEnum.Secondary:
                return IsSecondaryExist
                       && _secondary.ActiveCost <= _activePoint
                       && _secondary.SupplyCost <=  _supply;
            case SkillSlotEnum.Tertiary:
                return IsTertiaryExist 
                       && _tertiary.ActiveCost <= _activePoint 
                       && _tertiary.SupplyCost <= _supply;
            default:
                return false;
        }
    }
    
    public SkillSO GetSkill(SkillSlotEnum slot)
    {
        switch (slot)
        {
            case SkillSlotEnum.Primary:
                return _primary;
            case SkillSlotEnum.Secondary:
                return _secondary;
            case SkillSlotEnum.Tertiary:
                return _tertiary;
            default:
                return null;
        }
    }

    public UnitTeam GetEnemyTeam()
    {
        switch (_team)
        {
            case UnitTeam.None :
                return UnitTeam.None;
            case UnitTeam.Blue :
                return UnitTeam.Red;
            case UnitTeam.Red :
                return UnitTeam.Blue;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

}
