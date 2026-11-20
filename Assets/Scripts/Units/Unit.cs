using System;
using UnityEngine;

public abstract class Unit
{
    protected UnitTypeEnum _unitType;
    protected string _unitName;
    protected int _maxHealth;
    protected int _maxSupply;
    protected int _maxActivePoint;
    protected int _activeRecover;
    protected int _sight;
    protected int _moveRange;
    protected SkillSO _primary;
    protected SkillSO _secondary;
    protected SkillSO _tertiary;
    protected int _x;
    protected int _z;
    protected int _unitNumber;
    protected bool _hasMoved;
    protected bool _isMounted;
    
    protected GameObject _object;
    
    protected int _health;
    protected int _supply;
    protected int _activePoint;
    protected DirectionEnum _direction;
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
    public int X => _x;
    public int Z => _z;
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
            return CheckCost(1) || CheckCost(2) || CheckCost(3);
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
        _x = point.x;
        _z = point.y;
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
        _x = x;
        _z = z;
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

    public bool CheckCost(int slot)
    {
        switch (slot)
        {
            case 1:
                return IsPrimaryExist
                       && _primary.ActiveCost <= _activePoint 
                       && _primary.SupplyCost <= _supply;
            case 2:
                return IsSecondaryExist
                       && _secondary.ActiveCost <= _activePoint
                       && _secondary.SupplyCost <=  _supply;
            case 3:
                return IsTertiaryExist 
                       && _tertiary.ActiveCost <= _activePoint 
                       && _tertiary.SupplyCost <= _supply;
            default:
                return false;
        }
    }
    
    public SkillSO GetSkill(int slot)
    {
        switch (slot)
        {
            case 1:
                return _primary;
            case 2:
                return _secondary;
            case 3:
                return _tertiary;
            default:
                return null;
        }
    }

}
