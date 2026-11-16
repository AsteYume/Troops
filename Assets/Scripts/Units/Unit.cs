public class Unit
{
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
    
    private int _health;
    private int _supply;
    private int _activePoint;
    private DirectionEnum _direction;
    private UnitTeam _team;

    public Unit(UnitDataSo unitData, DirectionEnum direction, UnitTeam team)
    {
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
        _team = team;
    }
    
}
