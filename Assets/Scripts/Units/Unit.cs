using UnityEngine;

public abstract class Unit : MonoBehaviour
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
    private GameObject _gameObject;
    
    private int _health;
    private int _supply;
    private int _activePoint;
    private DirectionEnum _direction;
    private UnitTeam _team;
    
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
    
    public int Health => _health;
    public int Supply => _supply;
    public int ActivePoint => _activePoint;
    public DirectionEnum Direction => _direction;
    public UnitTeam Team => _team;
    

    public Unit(UnitDataSo unitData, DirectionEnum direction, UnitTeam team)
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

        _health = _maxHealth;
        _supply = _maxSupply;
        _activePoint = _maxActivePoint;
        _direction = direction;
    }

    public abstract void SetUnitData();

}
