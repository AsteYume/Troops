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
    
    protected GameObject _prefab;
    
    protected int _health;
    protected int _supply;
    protected int _activePoint;
    protected DirectionEnum _direction;
    protected UnitTeam _team;
    
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
    public GameObject Prefab => _prefab;
    
    public int Health => _health;
    public int Supply => _supply;
    public int ActivePoint => _activePoint;
    public DirectionEnum Direction => _direction;
    public UnitTeam Team => _team;


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
        _prefab = prefab;
    }

}
